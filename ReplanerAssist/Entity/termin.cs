using System.Data;
using System.Text.Json.Serialization;

namespace ReplanerAssist.Entity
{
    public class Termin
    {
        public string TID { get; set; }
        public string WID { get; set; }
        public List<string> PersonenIDs { get; set; }
        public DateTime Datum { get; set; }

        [JsonIgnore]
        public List<Person> PersonenListe { get; set; }

        [JsonIgnore]
        public WiederkehrendeAufgabe Aufgabe { get; set; }

        public Termin()
        {
            PersonenListe = new List<Person>();
            PersonenIDs = new List<string>();
            TID = Guid.NewGuid().ToString();
        }
    }
}