using System.Text.Json;
using System.Collections.Generic;
using System.IO;

public interface IAccesoADatos
{
    List<string[]> LeerDatosCSV(string filename);
    string LeerDatosJSON(string filename);
}

public class AccesoADatosCSV : IAccesoADatos
{
    public List<string[]> LeerDatosCSV(string filename)
    {
        var filas = new List<string[]>();

        foreach (var linea in File.ReadAllLines(filename))
        {
            if (linea.StartsWith("id")) continue; // saltea cabecera
            var partes = linea.Split(',');
            filas.Add(partes); // devuelve la fila como array de strings
        }

        return filas;
    }

    public string LeerDatosJSON(string filename)
    {
        // en este caso, no tiene sentido procesar: devolvemos el string crudo
        return File.ReadAllText(filename);
    }
}

public class AccesoADatosJSON : IAccesoADatos
{
    public List<string[]> LeerDatosCSV(string filename)
    {
        // si quisieras leer CSV desde JSON, lo dejamos vacío o lanzamos excepción
        throw new NotImplementedException("No soportado en JSON.");
    }

    public string LeerDatosJSON(string filename)
    {
        return File.ReadAllText(filename);
    }
}
