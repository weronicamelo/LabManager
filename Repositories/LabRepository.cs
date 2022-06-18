using Microsoft.Data.Sqlite;
using LabManager.Models;
using LabManager.Database;
using Dapper;

namespace LabManager.Repositories;

class LabRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public LabRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }
    
    public List<Lab> GetAll()
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var labs = connection.Query<Lab>("SELECT * FROM Labs;").ToList();

        connection.Close();
        return labs;
    }

    public Lab Save(Lab lab)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Labs VALUES(@Id, @Number, @Name, @Block);", lab);
        
        connection.Close();

        return lab;
    }

    public Lab GetById(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();

        var lab = connection.QuerySingle<Lab>("SELECT * FROM Labs WHERE id = @Id;", new{Id = id});

        connection.Close();
        return lab;
    }

    public void Delete(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Labs WHERE id = @Id;", new{Id = id});
    
        connection.Close();
    }

    public Lab Update(Lab lab)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute(@"
        Update Labs 
        SET 
            number = @Number,
            name = @Name,
            block = @Block
        WHERE id = @Id;
        ", lab); 

        connection.Close();

        return lab;
    }

    public bool ExistsById(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var result = Convert.ToBoolean(connection.ExecuteScalar("SELECT COUNT(id) FROM Labs WHERE id = @Id,", new{Id = id}));

        return result;
    }

    //private Lab ReaderToLab(SqliteDataReader  reader)
    //{
       // var lab = new Lab(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),  reader.GetString(3));

        //return lab;
    //}
}