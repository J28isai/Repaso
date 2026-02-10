using Repaso.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repaso.Repositorio
{
    public interface IUserRepository
    {
        List<User> ObtUsuarios();

        bool Login(string username, string password);

        void AgregarUsuario(List<User> users);

    }
}
