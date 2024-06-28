namespace ReplanerAssist.Entity
{
    public class WiederkehrendeAufgabe
    {
        public WiederkehrendeAufgabe()
        {
             WID = Guid.NewGuid().ToString();
        }
        public string WID { get; set;}
        public string? Titel { get; set;}
        public string? Beschreibung { get; set;}
    }
}