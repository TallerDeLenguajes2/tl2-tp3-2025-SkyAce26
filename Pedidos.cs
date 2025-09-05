namespace MiCadeteria;
public class Pedidos
{
    private int nro;
    private string obs;
    private Cliente cliente;
    private string estado;

    public Pedidos(int nro, string obs, Cliente c, string estado)
    {
        this.nro = nro;
        this.obs = obs;
        this.cliente = c;
        this.estado = estado;
    }

    public int GetNro() => nro;
    public string GetObs() => obs;
    public Cliente GetCliente() => cliente;
    public string GetEstado() => estado;
    public void SetEstado(string nuevoEstado)
    {
        this.estado = nuevoEstado;
    }

    public string VerDireccionCliente()
    {
        return cliente.GetDireccion();
    }
    public cliente VerDatosCliente()
    {
        return cliente;
    }


}