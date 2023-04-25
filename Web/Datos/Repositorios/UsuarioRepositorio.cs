using Dapper;
using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;

namespace Datos.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private string CadenaConexion;
        public UsuarioRepositorio(string _cadenaConexion)
        {
            CadenaConexion = _cadenaConexion;
        }
        private MySqlConnection Conexion()
        {
            return new MySqlConnection(CadenaConexion);
        }


        public async Task<bool> ActualizarAsync(Usuario usuario)
        {
            bool resultado = false;
            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = @"UPDATE Usuarios SET Nombre = @Nombre,  Contrasena = @Contrasena, Correo = @Correo, Rol =@Rol, Foto = @Foto, EstadoActivo = @EstadoActivo 
                               WHERE CodigoUsuario = @CodigoUsuario;";
                resultado = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, usuario));
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public async Task<bool> EliminarAsync(string codigo)
        {
            bool resultado = false;
            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = @" DELETE FROM Usuarios WHERE CodigoUsuario = @CodigoUsuario;";
                resultado = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, new { codigo }));
            }
            catch (Exception)
            {
            }
            return resultado;
        }

        public Task<IEnumerable<Usuario>> GetListaAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> GetPorCodigoAsync(string codigo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> NuevoAsync(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
