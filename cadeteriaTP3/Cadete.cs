namespace MiCadeteria;
public class Cadete
{
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;

    public int GetId() => id;
    public string GetNombre() => nombre;
    public string GetDireccion() => direccion;
    public string GetTelefono() => telefono;


    public Cadete(int id, string nombre, string direccion, string tel)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = tel;
    }

}