using Dapper;
using System.Data;
using System.Linq;
using VinylCollection.Models;

namespace VinylCollection.Repositories
{
    public class TitleRepository
    {
        private readonly IDbConnection _connection;

        public TitleRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Title> GetAll()
        {
            var query = "SELECT * FROM titles";
            return _connection.Query<Title>(query);
        }

        public Title GetById(int id)
        {
            var query = "SELECT * FROM title WHERE id = @Id";
            return _connection.QuerySingleOrDefault<Title>(query, new { Id = id })!;
        }

        public List<Title> Get(string title)
        {
            var query = "SELECT * FROM Titles";
            var titulos = _connection.Query<Title>(query).ToList();
            return titulos;
        }

        public void Add(Title title)
        {
            var query = "INSERT INTO Titles (name, recorded_year) VALUES (@Name, @RecordedYear)";
            _connection.Execute(query, title);
        }

        public void Update(string name, int recordedYear)
        {
            string updateQuery = "UPDATE Titles SET name = @NovoValor WHERE recorded_year = @Condicao";
            _connection.Execute(updateQuery, new { NovoValor = name, Condicao = recordedYear });
        }

        public void Delete(int id)
        {
            var query = "DELETE FROM Titles WHERE id = @Id";
            _connection.Execute(query, new { Id = id });
        }
    }
}
