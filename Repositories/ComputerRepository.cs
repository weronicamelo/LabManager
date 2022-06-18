using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace LabManager.Repositories;

class ComputerRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public ComputerRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public List<Computer> GetAll()
    {
        
        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        var computers = connection.Query<Computer>("SELECT * FROM Computers;").ToList();

        connection.Close();

        return computers;
    }

    public Computer Save(Computer computer)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Computers VALUES (@Id, @Ram, @Processor);", computer);

        connection.Close();

        return computer;
    }

    public void Delete(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Computers WHERE id = @Id;", new{Id = id});
    
        connection.Close();
    }

    public Computer Update(Computer computer)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute(@"
            UPDATE Computers 
            SET 
                ram = @Ram,
                processor = @Processor
            WHERE id = @Id;
            ", computer);

        connection.Close();

        return computer;
    }

    public Computer GetById(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var computer = connection.QuerySingle<Computer>("SELECT * FROM Computers WHERE id = @Id;", new{Id = id});
        
        connection.Close();
        return computer;
    }

    public bool ExistsById(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var result = Convert.ToBoolean(connection.ExecuteScalar("SELECT count(id) FROM Computers WHERE id = @Id;", new {Id = id}));

        return result;
    }

    //private Computer ReaderToComputer(SqliteDataReader reader)
    //{
        //var computer = new Computer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
        //return computer;
    //}
}