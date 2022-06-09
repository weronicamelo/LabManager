using Microsoft.Data.Sqlite;
using LabManager.Database;
using LabManager.Repositories;
using LabManager.Models;

var databaseConfig = new DatabaseConfig();

var databaseSetup = new DatabaseSetup(databaseConfig);

// Routing
var modelName = args[0];
var modelAction = args[1];

if(modelName == "Computer")
{
    var computerRepository = new ComputerRepository(databaseConfig);

    if(modelAction == "List")
    {
        Console.WriteLine("Computer List");

        foreach(var computer in computerRepository.GetAll())
        {
            Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");
        }
    }

    if(modelAction == "New")
    {
        Console.WriteLine("Computer New");
        var id = Convert.ToInt32(args[2]);
        var ram = args[3];
        var processor = args[4]; 
        var computer = new Computer(id, ram, processor);

        computerRepository.Save(computer);
    }

    if(modelAction == "Delete")
    {
        Console.WriteLine("Computer Delete");
        var id = Convert.ToInt32(args[2]);

        if(computerRepository.ExistsById(id))
        {
            computerRepository.Delete(id);
        }
        else {
            Console.WriteLine($"Computador com id {id} não existe");
        }
    }
        
    if(modelAction == "Update")
    {
        Console.WriteLine("Computer Update");
        var id = Convert.ToInt32(args[2]);

        if(computerRepository.ExistsById(id))
        {
            var ram = args[3];
            var processor = args[4];
            var computer = new Computer(id, ram, processor);

            computerRepository.Update(computer);
        }
        else {
            Console.WriteLine($"Computador com id {id} não existe");
        }
    }

    if(modelAction == "Show")
    {
        Console.WriteLine("Computer Show");
        var id = Convert.ToInt32(args[2]);

        if(computerRepository.ExistsById(id))
        {
            var computer = computerRepository.GetById(id);
            Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");
        }
        else {
            Console.WriteLine($"O computador com id {id} não existe");
        }
    }
}

if(modelName == "Lab")
{
     Console.WriteLine("Lab List");
    var labRepository = new LabRepository(databaseConfig);

    if(modelAction == "List")
    {
        var labs = labRepository.GetAll();

        foreach(var lab in labs)
        {
            Console.WriteLine($"{lab.Id}, {lab.Number}, {lab.Name}, {lab.Block}");
        }    
    }

    if(modelAction == "New")
    {
        Console.WriteLine("Lab New");
        var id = Convert.ToInt32(args[2]);
        var number = args[3];
        var name = args[4];
        var block = args[5];
        var lab = new Lab(id, number, name, block);

        labRepository.Save(lab);
    }

    if(modelAction == "Delete")
    {
        Console.WriteLine("Delete");
        var id = Convert.ToInt32(args[2]);

        if(labRepository.ExistsById(id))
        {
            labRepository.Delete(id);
        } 
        else {
            Console.WriteLine($"Lab com id {id} não existe");
        }
    }

     if(modelAction == "Update")
    {
        Console.WriteLine("Update Lab");
        var id = Convert.ToInt32(args[2]);

        if(labRepository.ExistsById(id))
        {
            var number = args[3];
            var name = args[4]; 
            var block = args[5];
        
            var lab = new Lab(id, number, name, block);

            labRepository.Update(lab);
        } 
        else {
            Console.WriteLine($"Lab com id {id} não existe");
        }
    }

     if(modelAction == "Show")
    {
        Console.WriteLine("Show Lab");
        var id = Convert.ToInt32(args[2]);

        if(labRepository.ExistsById(id))
        {
            var lab = labRepository.GetById(id);
            Console.WriteLine($"{lab.Id}, {lab.Number}, {lab.Name}, {lab.Block}"); 
        } 
        else {
            Console.WriteLine($"Lab com id {id} não existe");
        }
    }
}