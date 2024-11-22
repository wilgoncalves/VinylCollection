using VinylCollection.Models;
using VinylCollection.Repositories;

namespace VinylCollection.Interfaces
{
    public interface ITitleRepository
    {
        IEnumerable<Title> GetAll();
        Title GetById(int id);
        List<Title> Get(string title);
        void Add(Title title);
        void Update(string name, int recordedYear);
        void Delete(int id);
    }
}
