using System;
using examenOptativoP.Modelos;
using Repository.Data.Clientes;
using Repository.Data.Facturas;
using Service.logica;




class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Host=localhost;port=5432;Database=examenoptap;Username=postgres;Password=1234;";
        ClienteService clienteService = new ClienteService(connectionString);
        FacturaService facturaService = new FacturaService(connectionString);
        SucursalService sucursalService = new SucursalService(connectionString);

        Console.WriteLine("Ingrese: \n Clientes \n a - para insertar \n b - para listar \n c - para modificar \n d - eliminar por ID \n e - buscar por ID \n Facturacion \n A - agregar factura \n B - listar facturas \n C - modificar facturas \n D - eliminar factura por ID \n E - buscar facturas por el ID \n consultas sucursales \n 1 - agregar sucursal \n 2 - listar sucursales \n 3 - modificar sucursal \n 4 - eliminar sucursal por ID \n 5 - buscar sucursales por ID");
        string opcion = Console.ReadLine();

        if (opcion == "a")
        {
            clienteService.add(new ClienteModel
            {
                id_banco = 3,
                nombre = "Liliana",
                apellido = "Baez",
                documento = "4793090",
                direccion = "Milano",
                mail = "lili@gmail.com",
                celular = "0982427337",
                estado = "Activo"
            });
        }
        if (opcion == "b")
        {
            clienteService.GetAll().ToList().ForEach(cliente =>
            Console.WriteLine(
                $"ID: {cliente.id} \n " +
                $"ID Banco: {cliente.id_banco} \n " +
                $"Nombre: {cliente.nombre} \n " +
                $"Apellido: {cliente.apellido} \n " +
                $"Documento: {cliente.documento} \n " +
                $"Correo {cliente.mail} \n " +
                $"Celular: {cliente.celular} \n " +
                $"Estado: {cliente.estado} \n "
                )
            );
        }
        if (opcion == "c")
        {
            clienteService.update(new ClienteModel
            {
                id = 3, 
                id_banco = 2,
                nombre = "Tatiana",
                apellido = "Molinas Baez",
                documento = "4793090",
                direccion = "Milano",
                mail = "tatimolba@gmail.com",
                celular = "0982427337",
                estado = "Activo"
            });
        }
        if ( opcion == "d") {
            Console.WriteLine("ingresar el id que quiere eliminar");
            string input = Console.ReadLine();
            int id_elegido = int.Parse(input);
            clienteService.delete(id_elegido);
        }
        if(opcion == "e")
        {
            Console.WriteLine("Ingrese el id del cliente que quieres buscar:");
            string input = Console.ReadLine();
            int idSelect = int.Parse(input);
            ClienteModel cliente = clienteService.get(idSelect);
            if (cliente != null)
            {
                Console.WriteLine(
                    $"Nombre: {cliente.nombre} \n" +
                    $"ID Banco: {cliente.id_banco} \n" +
                    $"Apellido: {cliente.apellido} \n" +
                    $"Documento: {cliente.documento} \n" +
                    $"Correo: {cliente.mail} \n" +
                    $"Direccion: {cliente.direccion} \n" +
                    $"Celular: {cliente.celular} \n" +
                    $"Estado: {cliente.estado} \n"
                );
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
        }
        if(opcion == "A")
        {
            facturaService.add(new FacturaModel
            {
                id_cliente = 3,
                id_sucursal = 1,
                nro_factura = "479-090-973122",
                fecha_hora = new DateTime(2024, 05, 10),
                total = 250000,
                total_iva5 = 1250,
                total_iva10 = 2500,
                total_iva = 3750,
                total_letras = "Doscientos cincuenta mil",
                sucursal = "Asuncion"
            });
        }
        if (opcion == "B")
        {
            facturaService.GetAll().ToList().ForEach(factura =>
            Console.WriteLine(
                $"ID: {factura.id} \n " +
                $"ID Sucursal: {factura.id_sucursal} \n " +
                $"ID Cliente: {factura.id_cliente} \n " +
                $"Numero de factura: {factura.nro_factura} \n " +
                $"Fecha y Hora: {factura.fecha_hora} \n " +
                $"Total: {factura.total} \n " +
                $"Total del IVA 5% {factura.total_iva5} \n " +
                $"Total del IVA 10%: {factura.total_iva10} \n " +
                $"Total en letras: {factura.total_letras} \n " +
                $"Sucursal: {factura.sucursal} \n "
                )
            );
        }
        if (opcion == "C")
        {
            facturaService.update(new FacturaModel
            {
                id = 1,
                id_cliente = 3,
                id_sucursal=1,
                nro_factura = "479-090-973133",
                fecha_hora = new DateTime(2024, 05, 21),
                total = 250000,
                total_iva5 = 1250,
                total_iva10 = 2500,
                total_iva = 3750,
                total_letras = "Doscientos cincuenta mil",
                sucursal = "Lambare"
            });
        }
        if (opcion == "D")
        {
            Console.WriteLine("ingresar el id que desea eliminar");
            string input = Console.ReadLine();
            int id_elegid = int.Parse(input);
            facturaService.delete(id_elegid);
        }
        if (opcion == "E")
        {
            Console.WriteLine("Ingrese el id de la factura que quieras buscar:");
            string input = Console.ReadLine();
            int idSelecto = int.Parse(input);
            FacturaModel factura = facturaService.get(idSelecto);
            if (factura != null)
            {
                Console.WriteLine(
                    $"ID Cliente: {factura.id_cliente} \n" +
                    $"ID Sucursal: {factura.id_sucursal} \n" +
                    $"Numero de factura: {factura.nro_factura} \n" +
                    $"Fecha y hora: {factura.fecha_hora} \n" +
                    $"Total: {factura.total} \n" +
                    $"Total del IVA del 5%: {factura.total_iva5} \n" +
                    $"Total del IVA del 10%: {factura.total_iva10} \n" +
                    $"Total del IVA: {factura.total_iva} \n" +
                    $"Total en letras: {factura.total_letras} \n" +
                    $"Sucursal: {factura.sucursal} \n"
                );
            }
            else
            {
                Console.WriteLine("Factura no encontrada.");
            }
           
        }
        if (opcion == "1")
        {
            sucursalService.add(new SucursalModel
            {
                descripcion = "casa blanca",
                direccion = "España 1534",
                telefono = "021356332",
                whatsapp = "0982427337",
                email = "sucursal1@gmail.com",
                estado = "Activo"
            });
        }
        if (opcion == "2")
        {
            sucursalService.GetAll().ToList().ForEach(sucursal =>
            Console.WriteLine(
                $"ID: {sucursal.id} \n " +
                $"Descripcion: {sucursal.descripcion} \n " +
                $"Direccion: {sucursal.direccion} \n " +
                $"Telefono: {sucursal.telefono} \n " +
                $"Whatsapp: {sucursal.whatsapp} \n " +
                $"Correo {sucursal.email} \n " +
                $"Estado: {sucursal.estado} \n "
                )
            );
        }
        if (opcion == "3")
        {
            sucursalService.update(new SucursalModel
            {
                id = 1,
                descripcion = "casa blanca",
                direccion = "España 1334",
                telefono = "021356222",
                whatsapp = "0982427337",
                email = "sucursal1@gmail.com",
                estado = "Activo"
            });
        }
        if (opcion == "4")
        {
            Console.WriteLine("ingresar el id de la sucursal que quiere eliminar");
            string input = Console.ReadLine();
            int id_elegidos = int.Parse(input);
            clienteService.delete(id_elegidos);
        }
        if (opcion == "5")
        {
            Console.WriteLine("Ingrese el id de la sucursal que quieres buscar:");
            string input = Console.ReadLine();
            int idSele = int.Parse(input);
            SucursalModel sucursal = sucursalService.get(idSele);
            if (sucursal != null)
            {
                Console.WriteLine(
                $"Descripcion: {sucursal.descripcion} \n " +
                $"Direccion: {sucursal.direccion} \n " +
                $"Telefono: {sucursal.telefono} \n " +
                $"Whatsapp: {sucursal.whatsapp} \n " +
                $"Correo {sucursal.email} \n " +
                $"Estado: {sucursal.estado} \n "
                );
            }
            else
            {
                Console.WriteLine("Sucursal no encontrada.");
            }
        }
    }
}
