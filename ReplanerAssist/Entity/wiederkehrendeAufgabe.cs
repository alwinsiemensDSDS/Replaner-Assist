using System.Data;
using System.Text.Json.Serialization;

namespace ReplanerAssist.Entity
{
    public class WiederkehrendeAufgabe
    {
        public string WID { get; set; }
        public string? Titel { get; set; }
        public string? Beschreibung { get; set; }
        public List<string> PersonenIDs { get; set; }

        [JsonIgnore]
        public List<Person> PersonenListe { get; set; }

        public WiederkehrendeAufgabe()
        {
            PersonenListe = new List<Person>();
            PersonenIDs = new List<string>();
            WID = Guid.NewGuid().ToString();
        }
    }
}