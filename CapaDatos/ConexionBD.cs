using Microsoft.Extensions.Configuration;

namespace CapaDatos
{
    public class ConexionBD
    {
        public static string getCadenaConexion()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            var root = builder.Build();
            string cadenaConexion = root.GetConnectionString("cn");
            return cadenaConexion;
        }
    }
}
