using MiCadeteria;
IAccesoADatos accesoDatos = new AccesoADatosCSV();

string archivoCadetes = "cadetes.csv";
List<string[]> datosCadetes = accesoDatos.LeerDatosCSV(archivoCadetes);


List<Cadete> cadetes = new List<Cadete>();
foreach (var fila in datosCadetes)
{
    int id = int.Parse(fila[0]);
    string nombre = fila[1];
    string direccion = fila[2];
    string telefono = fila[3];

    cadetes.Add(new Cadete(id, nombre, direccion, telefono));
}

Cadeteria cadeteria = new Cadeteria("Mi Cadetería", "3760975817", cadetes);

Console.WriteLine("=== Cadetes cargados desde CSV ===");
foreach (var c in cadeteria.GetListadoCadetes())
{
    Console.WriteLine($"Id: {c.GetId()} - Nombre: {c.GetNombre()} - Dirección: {c.GetDireccion()} - Tel: {c.GetTelefono()}");
}

List<Pedidos> pedidos = new List<Pedidos>();

int opcion = -1;
while (opcion != 0)
{
    Console.WriteLine("\n===== SISTEMA DE CADETERÍA =====");
    Console.WriteLine("1) Dar de alta pedido.");
    Console.WriteLine("2) Asignar pedido a cadete.");
    Console.WriteLine("3) Cambiar pedido de estado.");
    Console.WriteLine("4) Reasignar pedido a otro cadete.");
    Console.WriteLine("5) Generar informe final.");
    Console.WriteLine("6) Mostrar lista de pedidos.");
    Console.WriteLine("0) Salir del menú.");
    Console.Write("Seleccione una opción: ");

    if (!int.TryParse(Console.ReadLine(), out opcion))
    {
        Console.WriteLine("Opción inválida.");
        continue;
    }

    switch (opcion)
    {
        case 1:
            Console.WriteLine("Ingrese el nombre:");
            string nombre = Console.ReadLine();
            Console.WriteLine("Ingrese la direccion:");
            string direccion = Console.ReadLine();
            Console.WriteLine("Ingrese numero de telefono:");
            string numTel = Console.ReadLine();
            Console.WriteLine("Ingrese datos de referencia de direccion:");
            string datosReferencia = Console.ReadLine();

            Cliente cliente = new Cliente(nombre, direccion, numTel, datosReferencia);

            Console.WriteLine("Ingrese el numero del pedido:");
            int nro = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese observaciones:");
            string obs = Console.ReadLine();

            Pedidos nuevoPedido = new Pedidos(nro, obs, cliente, "Pendiente");
            if (cadeteria.agregarPedido(nuevoPedido))
            {
                pedidos.Add(nuevoPedido);
                Console.WriteLine("Pedido dado de alta.");
            }
            else
            {
                Console.WriteLine("No se pudo dar de alta (ya existe ese número de pedido).");
            }
            break;

        case 2:
            Console.WriteLine("Ingrese id del cadete:");
            int idCadete = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese nro del pedido:");
            int nroPedido = int.Parse(Console.ReadLine());

            Pedidos pedido = pedidos.Find(p => p.GetNro() == nroPedido);

            if (pedido != null)
            {
                bool asignado = cadeteria.asignarPedidoCadete(idCadete, pedido);
                if (asignado)
                    Console.WriteLine("Pedido asignado a cadete.");
                else
                    Console.WriteLine("No se pudo asignar.");
            }
            else
            {
                Console.WriteLine("No existe ese pedido.");
            }
            break;

        case 3:
            Console.WriteLine("Ingrese el nro del pedido:");
            int nroCambiar = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese el nuevo estado (Pendiente/EnProceso/Entregado):");
            string estado = Console.ReadLine();

            if (cadeteria.cambiarEstadoPedido(nroCambiar, estado))
                Console.WriteLine("Estado cambiado.");
            else
                Console.WriteLine("No se encontró el pedido.");
            break;

        case 4:
            Console.WriteLine("Ingrese id del cadete actual:");
            int idCadeteAct = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese id del nuevo cadete:");
            int idCadeteNuevo = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese nro del pedido:");
            int nroReasignar = int.Parse(Console.ReadLine());

            if (cadeteria.reasignarPedidos(idCadeteAct, idCadeteNuevo, nroReasignar))
                Console.WriteLine("Pedido reasignado.");
            else
                Console.WriteLine("No se pudo reasignar.");
            break;

        case 5:
            string informe = cadeteria.generarInforme();
            Console.WriteLine("\n===== INFORME FINAL =====");
            Console.WriteLine(informe);
            break;

        case 6:
            Console.WriteLine("\n===== LISTA DE PEDIDOS =====");
            foreach (var p in cadeteria.GetListadoPedidos())
            {
                Console.WriteLine($"Pedido Nro: {p.GetNro()} - Estado: {p.GetEstado()} - Cadete: {(p.GetCadete() != null ? p.GetCadete().GetNombre() : "Sin asignar")}");
            }
            break;

        case 0:
            Console.WriteLine("Saliendo del sistema...");
            break;

        default:
            Console.WriteLine("Opción inválida.");
            break;
    }
}

