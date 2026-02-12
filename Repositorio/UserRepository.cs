using Repaso.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repaso.Repositorio
{
    public class UserRepository : IUserRepository
    {

        private readonly string _path;

        public UserRepository(string? path = null)
        {
            _path = string.IsNullOrWhiteSpace(path)
                ? Path.Combine(AppContext.BaseDirectory, "Data", "users.json")
                : path;
        }

        public void AgregarUsuario(List<User> users)
        {
            var dir = Path.GetDirectoryName(_path);
            if (!string.IsNullOrWhiteSpace(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var tmp = _path + ".tmp";

             using (var fs = File.Create(tmp))
            {
                JsonSerializer.Serialize(fs, users);
            }

            File.Copy(tmp, _path, overwrite: true);
            File.Delete(tmp);

        }

        public bool Login(string username, string password)
        {
            var usuarios = ObtUsuarios();
            var user = usuarios.FirstOrDefault(a => a.UserN == username && a.Password == password);

            if (user == null) { 
            return false;
            }
            else
            {
                return true;
            }
        }

        public List<User> ObtUsuarios()
        {
            if (!File.Exists(_path))
            {
                return [];
            }

            using var fs = File.OpenRead(_path);
            var items =  JsonSerializer.Deserialize<List<User>>(fs);
            return items ?? [];

        }
    }
}
