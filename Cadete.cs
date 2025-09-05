namespace MiCadeteria;
public class Cadete
{
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;
    private List<Pedidos> listadoPedidos = new List<Pedidos>();

    public int GetId() => id;
    public string GetNombre() => nombre;
    public string GetDireccion() => direccion;
    public string GetTelefono() => telefono;
    public List<Pedidos> GetListadoPedidos() => listadoPedidos;

    public Cadete(int id, string nombre, string direccion, string tel, List<Pedidos> pedidos)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = tel;
        this.listadoPedidos = pedidos;
    }
    public float JornalACobrar()
    {
        return listadoPedidos.Count * 500;
    }

    public void agregarPedido(Pedidos p)
    {
        listadoPedidos.Add(p);
    }

    public void cambiarEstadoPedido(int nroPedido, string nuevoEstado)
    {
        foreach (Pedidos p in listadoPedidos)
        {
            if (p.GetNro() == nroPedido)
            {
                p.SetEstado(nuevoEstado);
                break;
            }
        }
    }

    public void elimiarPedidoPorNro(int nro)
    {
        Pedidos pedidoBorrar = listadoPedidos.Find(p => p.GetNro() == nro);
        if (pedidoBorrar != null)
        {
            listadoPedidos.Remove(pedidoBorrar);
        }
    }



}