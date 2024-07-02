using Microsoft.Maui.Storage;
using ReplanerAssist.database;
using ReplanerAssist.Entity;
using System.Linq;

namespace ReplanerAssist.Database
{
    public class ReplanerDB : IReplanerDB
    {
        private static string mainDir = FileSystem.Current.AppDataDirectory;
        private static string fileName = "replaner.db";
        private static string filePath = System.IO.Path.Combine(mainDir, fileName);

        public List<Termin> Termine { get; set; }
        public List<Person> Personen { get; set; }
        public List<WiederkehrendeAufgabe> Aufgaben { get; set; }

        public WiederkehrendeAufgabe? selectedAufgabe { get; set; }

        public ReplanerDB()
        {
            Termine = new List<Termin>();
            Personen = new List<Person>();
            Aufgaben = new List<WiederkehrendeAufgabe>();

        }
        public async Task<DbReturnType> LoadDatabase()
        {
            string json = "";

            if (!File.Exists(filePath))
            {
                var file = File.Create(filePath);
                file.Close();
            }
            else {
                using Stream fileStream = System.IO.File.OpenRead(filePath);
                using StreamReader reader = new StreamReader(fileStream);

                json = await reader.ReadToEndAsync();
            }
         

            if (json == "")
            {
                return DbReturnType.Empty;
            }

            //Deserialization Process
            ReplanerDB loaded_db;
            try
            {
                loaded_db = System.Text.Json.JsonSerializer.Deserialize<ReplanerDB>(json);
            }
            catch (Exception)
            {
                Console.WriteLine("Database couldn't be deserialized");
                return DbReturnType.Error;
            }

            Termine = loaded_db!.Termine;
            Personen = loaded_db!.Personen;
            Aufgaben = loaded_db!.Aufgaben;


            if (Aufgaben.Count != 0)
            {
                // select Aufgabe
                selectedAufgabe = loaded_db.selectedAufgabe;
                if (loaded_db.selectedAufgabe == null)
                {
                    selectedAufgabe = Aufgaben.FirstOrDefault();
                }
                // Add Person-Objects to selectedAufgabe
                foreach (var person_id in selectedAufgabe.PersonenIDs)
                {
                    Person person = Personen.FirstOrDefault(person => person.PID == person_id);
                    if (person != null)
                    {
                        selectedAufgabe.PersonenListe.Add(person);
                    }
                    else
                    {
                        Console.WriteLine("Person with id " + person_id + " not found in db");
                    }
                }

                // Add Person-Objects to Aufgaben
                foreach (var aufgabe in Aufgaben)
                {
                    if (aufgabe.PersonenIDs.Count > 0)
                    {
                        foreach (var person_id in aufgabe.PersonenIDs)
                        {
                            Person person = Personen.FirstOrDefault(person => person.PID == person_id);
                            if (person != null)
                            {
                                aufgabe.PersonenListe.Add(person);
                            }
                            else
                            {
                                Console.WriteLine("Person with id " + person_id + " not found in db");
                            }
                        }
                    }
                }
            }
            else
            {
                selectedAufgabe = null;
            }


            if (Termine.Count != 0)
            {
                foreach (var termin in Termine)
                {
                    if (termin.PersonenIDs.Count != 0)
                    {
                        //Add Person-Objects to Termin
                        foreach (var person_id in termin.PersonenIDs)
                        {
                            Person person = Personen.FirstOrDefault(person => person.PID == person_id)!;
                            if (person != null)
                            {
                                termin.PersonenListe.Add(person);
                            }
                            else
                            {
                                Console.WriteLine("Person with id " + person_id + " not found in db");
                            }
                        }
                    }
                    
                    

                    //Add Wiederkehrende-Aufgabe-Object to Termin
                    WiederkehrendeAufgabe aufgabe = Aufgaben.FirstOrDefault(aufgabe => aufgabe.WID == termin.WID)!;
                    if (aufgabe != null)
                    {
                        termin.Aufgabe = aufgabe;
                    }
                    else
                    {
                        Console.WriteLine("Aufgabe with id " + termin.WID + " not found in db");
                    }
                }
            }

            return DbReturnType.SuccessFull;


        }

        public List<Termin> GetTermineForAufgabe(WiederkehrendeAufgabe aufgabe)
        {
            return Termine.FindAll(termin => termin.Aufgabe?.WID == aufgabe.WID);
        }

        public void MockData()
        {
            //Termine.Add(new Termin() { Datum = DateTime.Now, PersonenIDs = [1, 3], TID = 1, WID = 1 });
            //Termine.Add(new Termin() { Datum = DateTime.Now, PersonenIDs = [1, 2], TID = 2, WID = 2 });
            //Termine.Add(new Termin() { Datum = DateTime.Now, PersonenIDs = [1, 4], TID = 3, WID = 2 });



            //Personen.Add(new Person() { PID = 1, Vorname = "Alwin", Nachname = "Siemens", Notiz = "Alwin ist cool" });
            //Personen.Add(new Person() { PID = 2, Vorname = "Johannes", Nachname = "Just", Notiz = "" });
            //Personen.Add(new Person() { PID = 3, Vorname = "Pascal", Nachname = "namenlos", Notiz = "" });
            //Personen.Add(new Person() { PID = 4, Vorname = "Rafail", Nachname = "Kalendaris", Notiz = "Kalender" });

            //Aufgaben.Add(new WiederkehrendeAufgabe() { WID = 1, Titel = "Aufr‰umen", Beschreibung = "JOJOJOJOJJ" });
            //Aufgaben.Add(new WiederkehrendeAufgabe() { WID = 2, Titel = "Blumen gieﬂen", Beschreibung = "" });
            //Aufgaben.Add(new WiederkehrendeAufgabe() { WID = 3, Titel = "predigen", Beschreibung = "" });
            //Aufgaben.Add(new WiederkehrendeAufgabe() { WID = 4, Titel = "was anderes machen", Beschreibung = "" });
        }

        public async Task SaveDatabase()
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(this);
                //save to database

                using FileStream outputStream = System.IO.File.Create(filePath);
                using StreamWriter streamWriter = new StreamWriter(outputStream);

                await streamWriter.WriteAsync(json);
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
                return;
            }
           
        }
    }
}