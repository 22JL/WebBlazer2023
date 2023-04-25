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
                string sql = @"UPDATE usuarios SET Nombre = @Nombre,  Contrasena = @Contrasena, Correo = @Correo, Rol =@Rol, Foto = @Foto, EstadoActivo = @EstadoActivo 
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
                string sql = @"DELETE FROM usuarios WHERE CodigoUsuario = @CodigoUsuario;";
                resultado = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, new { codigo }));
            }
            catch (Exception)
            {
            }
            return resultado;
        }

        public async Task<IEnumerable<Usuario>> GetListaAsync()
        {
            IEnumerable<Usuario> lista = new List<Usuario>();
            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = @"SELECT * FROM usuarios;";
                lista = await _conexion.QueryAsync<Usuario>(sql);
            }
            catch (Exception)
            {
            }
            return lista;
        }

        public async Task<Usuario> GetPorCodigoAsync(string codigo)
        {
            Usuario user = new Usuario();
            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = @"SELECT * FROM usuarios WHERE CodigoUsuario = @CodigoUsuario;";
                user = await _conexion.QueryFirstAsync<Usuario>(sql, new { codigo });
            }
            catch (Exception)
            {
            }
            return user;
        }

        public async Task<bool> NuevoAsync(Usuario usuario)
        {
            bool resultado = false;
            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = @"INSERT INTO usuarios (CodigoUsuario,Nombre, contrasena, Correo, Rol, Foto,FechaCreacion,EstadoActivo)
                                VALUES (@CodigoUsuario,@Nombre, @contrasena, @Correo, @Rol, @Foto, @FechaCreacion, @EstadoActivo);";
                resultado = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, usuario));
            }
            catch (Exception)
            {
            }
            return resultado;
        }
    }
}
