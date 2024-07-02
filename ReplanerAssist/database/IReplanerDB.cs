using ReplanerAssist.database;
using ReplanerAssist.Entity;

namespace ReplanerAssist.Database
{
    public interface IReplanerDB
    {
        List<WiederkehrendeAufgabe> Aufgaben { get; set; }
        List<Person> Personen { get; set; }
        WiederkehrendeAufgabe? selectedAufgabe { get; set; }
        List<Termin> Termine { get; set; }

        List<Termin> GetTermineForAufgabe(WiederkehrendeAufgabe aufgabe);
        Task<DbReturnType> LoadDatabase();
        void MockData();
        Task SaveDatabase();
    }
}