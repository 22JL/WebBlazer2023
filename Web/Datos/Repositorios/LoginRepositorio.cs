using Dapper;
using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;

namespace Datos.Repositorios
{
    public class LoginRepositorio : ILoginRepositorio
    {
        private string CadenaConexion;
        public LoginRepositorio(string _cadenaConexion)
        {
            CadenaConexion = _cadenaConexion;
        }
        private MySqlConnection Conexion()
        {
            return new MySqlConnection(CadenaConexion);
        }
        public async Task<bool> ValidarUsuarioAsync(Login Login)
        {
            bool valido = false;
            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = "SELECT 1 FROM usuario WHERE CodigoUsuario = @CodigoUsuario And Contrasena = @Contrasena;";
                valido = await _conexion.ExecuteScalarAsync<bool>(sql, new { Login.CodigoUsuario, Login.Contraseña });
            }
            catch (Exception)
            {

                throw;
            }
            return valido;
        }


    }
}
