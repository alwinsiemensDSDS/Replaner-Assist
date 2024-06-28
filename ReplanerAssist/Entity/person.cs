namespace ReplanerAssist.Entity
{
    public class Person
    {
        public Person()
        {
            PID = Guid.NewGuid().ToString();
        }
        public string PID { get; set;}
        public string? Vorname { get; set; }
        public string? Nachname { get; set; }
        public string? Notiz { get; set;}
    }
}