using System;
using Repository.Data.Clientes; // Asegúrate de usar los espacios de nombres adecuados

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Host=localhost;port=5432;Database=examenoptap;Username=postgres;Password=1234;";
        ClienteService clienteService = new ClienteService(connectionString);

        Console.WriteLine("Ingrese: \n a - para insertar \n b - para listar \n c - para modificar");
        string opcion = Console.ReadLine();

        if (opcion == "a")
        {
            clienteService.add(new ClienteModel
            {
                idBanco = 1,
                nom = "Jose",
                ape = "Spelt",
                docu = "5729799",
                direc = "Avda Von Grütter",
                email = "josespelt6@gmail.com",
                celu = "0999114128",
                estad = "Activo"
            });
        }
    }
}
