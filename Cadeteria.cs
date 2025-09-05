namespace MiCadeteria;
    public class Cadeteria
    {
        private string nombre;
        private string telefono;
        private List<Cadete> listadoCadetes = new List<Cadete>();

        public string GetNombre() => nombre;
        public string GetTelefono() => telefono;
        public List<Cadete> GetListadoCadetes() => listadoCadetes;

        public Cadeteria(string nombre, string tel, List<Cadete> cadetes)
        {
            this.nombre = nombre;
            this.telefono = tel;
            this.listadoCadetes = cadetes;
        }

        public int asignarPedidoCadete(int idCadete, Pedidos pedido)
        {
            Cadete cadetes = listadoCadetes.Find(c => c.GetId() == idCadete);

        if (cadetes != null && pedido != null)
        {
            cadetes.agregarPedido(pedido);
            return 1;
            }
            if (cadetes == null || pedido == null)
            {
            return 0;
            }

        }

        public int reasignarPedidos(int idCadete, int idCadeteNuevo, int nroPedido)
        {
            Cadete cadeteActual = listadoCadetes.Find(c => c.GetId() == idCadete);
            Cadete cadeteNuevo = listadoCadetes.Find(c => c.GetId() == idCadeteNuevo);

            if (cadeteActual != null && cadeteNuevo != null)
            {
                Pedidos pedido = cadeteActual.GetListadoPedidos().Find(p => p.GetNro() == nroPedido);

            if (pedido != null)
            {
                cadeteActual.elimiarPedidoPorNro(nroPedido);
                cadeteNuevo.agregarPedido(pedido);
                return 1;
                }
            else
            {
                return 0;
            }
            }
        }

        public void generarInforme()
        {
            if (listadoCadetes == null)
            {
                Console.WriteLine("No hay cadetes.");
            }

            int totalEnvios = 0;
            float totalMonto = 0;

            Console.WriteLine("-----Informe de cadeteria:------");
            Console.WriteLine($"Cadeteria: {nombre} - Tel: {telefono}");

            foreach (Cadete c in listadoCadetes)
            {
                int enviosCant = c.GetListadoPedidos().Count;
                float monto = c.JornalACobrar();

                List<Pedidos> pedidos = c.GetListadoPedidos();
                bool entregado = false;
                foreach (Pedidos p in pedidos)
                {
                    string estado = p.GetEstado();
                    if (estado == "Entregado")
                    {
                        entregado = true;
                    }
                }

                if (entregado == true)
                {
                    Console.WriteLine($"Cadete: {c.GetNombre()} - Id: {c.GetId()}");
                    Console.WriteLine($"Cantidad de envios: {enviosCant}");
                    Console.WriteLine($"Monto a cobrar: {monto}");
                    Console.WriteLine("----------------------------");
                }
                else
                {
                    Console.WriteLine("Los pedidos no fueron entregados.");
                }
                

                totalEnvios += enviosCant;
                totalMonto += monto;
            }

            float promedioEnvios = totalEnvios / listadoCadetes.Count;

            Console.WriteLine("-----Totales:-----");
            Console.WriteLine($"Envíos totales: {totalEnvios}");
            Console.WriteLine($"Monto total ganado: {totalMonto}");
            Console.WriteLine($"Promedio de envios por cadete: {promedioEnvios}");
        }
    }
