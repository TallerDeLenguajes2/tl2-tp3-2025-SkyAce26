namespace MiCadeteria;

public class Cadeteria
{
    private string nombre;
    private string telefono;
    private List<Cadete> listadoCadetes = new List<Cadete>();
    private List<Pedidos> listadoPedidos = new List<Pedidos>();

    public string GetNombre() => nombre;
    public string GetTelefono() => telefono;
    public List<Cadete> GetListadoCadetes() => listadoCadetes;
    public List<Pedidos> GetListadoPedidos() => listadoPedidos;

    public Cadeteria(string nombre, string tel, List<Cadete> cadetes)
    {
        this.nombre = nombre;
        this.telefono = tel;
        this.listadoCadetes = cadetes;
    }

    public bool asignarPedidoCadete(int idCadete, Pedidos pedido)
    {
        Cadete cadetes = listadoCadetes.Find(c => c.GetId() == idCadete);

        if (cadetes != null && pedido != null)
        {
            return pedido.asignarCadete(cadetes);

        }
        else
        {
            return false;
        }
    }

    public float JornalACobrar(int idCadete)
    {
        int entregados = GetListadoPedidos().Count(p => p.GetCadete() != null && p.GetEstado() == "Entregado" && p.GetCadete().GetId() == idCadete);
        return entregados * 500;
    }

    public bool agregarPedido(Pedidos p)
    {
        if (p == null) return false;

        if (listadoPedidos.Any(x => x.GetNro() == p.GetNro()))
            return false;

        listadoPedidos.Add(p);
        return true;
    }

    public bool cambiarEstadoPedido(int nroPedido, string nuevoEstado)
    {
        Pedidos pedido = listadoPedidos.Find(p => p.GetNro() == nroPedido);
        if (pedido != null)
        {
            return pedido.SetEstado(nuevoEstado);
        }
        return false;
    }

    public bool elimiarPedidoPorNro(int nro)
    {
        Pedidos pedidoBorrar = listadoPedidos.Find(p => p.GetNro() == nro);

        return pedidoBorrar != null && listadoPedidos.Remove(pedidoBorrar);
    }

    public bool reasignarPedidos(int idCadete, int idCadeteNuevo, int nroPedido)
    {
        Cadete cadeteActual = listadoCadetes.Find(c => c.GetId() == idCadete);
        Cadete cadeteNuevo = listadoCadetes.Find(c => c.GetId() == idCadeteNuevo);

        if (cadeteActual == null || cadeteNuevo == null)
        {
            return false;
        }
        Pedidos pedido = listadoPedidos.Find(p => p.GetNro() == nroPedido && p.GetEstado() != "Entregado");

        if (pedido == null)
        {
            return false;
        }

        pedido.asignarCadete(cadeteNuevo);
        return true;
    }

    public string generarInforme()
    {
        if (listadoCadetes == null || listadoCadetes.Count == 0)
        {
            return "No hay cadetes.";
        }

        int totalEnvios = 0;
        float totalMonto = 0;
        string informe = "";

        foreach (Cadete c in listadoCadetes)
        {
            // Contar los pedidos entregados de este cadete
            List<Pedidos> pedidosCadete = listadoPedidos
                .Where(p => p.GetCadete() != null && p.GetCadete().GetId() == c.GetId())
                .ToList();

            int enviosCant = pedidosCadete.Count(p => p.GetEstado() == "Entregado");
            float monto = JornalACobrar(c.GetId());

            if (enviosCant > 0)
            {
                informe += $"Cadete: {c.GetNombre()} - Id: {c.GetId()}\n" +
                           $"Cantidad de envíos: {enviosCant}\n" +
                           $"Monto a cobrar: {monto}\n" +
                           "-----------------\n";
            }
            else
            {
                informe += $"Cadete: {c.GetNombre()} - Id: {c.GetId()}\n" +
                           "No se entregaron pedidos.\n" +
                           "-----------------\n";
            }

            totalEnvios += enviosCant;
            totalMonto += monto;
        }

        float promedioEnvios = listadoCadetes.Count > 0 ? (float)totalEnvios / listadoCadetes.Count : 0;
        informe += mostrarGanancias(totalEnvios, totalMonto, promedioEnvios);

        return informe;
    }

    public string mostrarGanancias(int envios, float monto, float promedio)
    {
        return "------Totales:-----" + $"Envíos totales: {envios}\n" + $"Monto total ganado: {monto}\n" + $"Promedio de envios por cadete: {promedio}\n";
    }
}
