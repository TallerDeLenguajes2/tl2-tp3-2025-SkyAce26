namespace MiCadeteria;
public class Cliente
{
    private string nombre;
    private string direccion;
    private string telefono;
    private string datosReferenciaDireccion;

    public Cliente(string nombre, string direccion, string tel, string datos)
    {
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = tel;
        this.datosReferenciaDireccion = datos;
    }

    public string GetNombre() => nombre;
    public string GetDireccion() => direccion;
    public string GetTelefono() => telefono;
    public string GetDatosReferenciaDireccion() => datosReferenciaDireccion;
}
