using Entidades;

namespace Datos.Interfaces
{
    public interface ILoginRepositorio
    {
        Task<bool> ValidarUsuarioAsync(Login Login);
    }
}
