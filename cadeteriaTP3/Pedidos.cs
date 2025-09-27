namespace MiCadeteria;

public class Pedidos
{
    private int nro;
    private string obs;
    private Cliente cliente;
    private string estado;
    private Cadete cadete;

    public Pedidos(int nro, string obs, Cliente c, string estado, Cadete cade = null)
    {
        this.nro = nro;
        this.obs = obs;
        this.cliente = c;
        this.estado = estado;
        this.cadete = cade;
    }

    public int GetNro() => nro;
    public string GetObs() => obs;
    public Cliente GetCliente() => cliente;
    public string GetEstado() => estado;
    public bool SetEstado(string nuevoEstado)
    {

        string[] estadosValidos = { "Entregado", "EnProceso" };
        if (!estadosValidos.Contains(nuevoEstado))
        {
            return false;
        }
            
        this.estado = nuevoEstado;
        return true;
    }
    public Cadete GetCadete() => cadete;

    public bool asignarCadete(Cadete c)
    {
        this.cadete = c;
        return cadete != null;
    }

    public string VerDireccionCliente()
    {
        return $"Dirección: {cliente.GetDireccion()}";
    }

    public string VerDatosCliente()
    {
        return $"Nombre: {cliente.GetNombre()}, Direccion: {cliente.GetDireccion()}, Teléfono: {cliente.GetTelefono()}, Datos: {cliente.GetDatosReferenciaDireccion()}.";
    }


}