
using Microsoft.Data.SqlClient;
namespace DAL 
{ 
          

public static class Conexion
{
    public static string NombreAplicacion = "GestionColegio";
    public static string Servidor = "localhost";
    public static string Usuario = "sa";
    public static string Password = "tuPassword";
    public static string BaseDatos = "BDColegio";

    public static string ObtenerCadenaConexion(bool sqlAuthentication = true)
    {
        var constructor = new SqlConnectionStringBuilder
        {
            ApplicationName = NombreAplicacion,
            DataSource = Servidor,
            InitialCatalog = BaseDatos,
            IntegratedSecurity = !sqlAuthentication
        };

        if (sqlAuthentication)
        {
            constructor.UserID = Usuario;
            constructor.Password = Password;
        }

        return constructor.ConnectionString;
    }
}
}