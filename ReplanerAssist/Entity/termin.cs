using System.Data;
using System.Text.Json.Serialization;

namespace ReplanerAssist.Entity
{
    public class Termin
    {
        public int TID { get; set; }
        public int WID { get; set; }
        public List<int> PersonenIDs { get; set; }
        public DateTime Datum { get; set; }

        [JsonIgnore]
        public List<Person> PersonenListe { get; set; }

        [JsonIgnore]
        public WiederkehrendeAufgabe Aufgabe { get; set; }

        public Termin()
        {
            PersonenListe = new List<Person>();
        }
    }
}