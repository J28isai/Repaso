using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repaso.Model;
using Repaso.Repositorio;
using System.Collections.ObjectModel;

namespace Repaso.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IUserRepository _userRepository;

        [ObservableProperty]
        private string correo = string.Empty;

        [ObservableProperty]
        private string nombre = string.Empty;

        [ObservableProperty]
        private string usuario = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private string loginUsuario = string.Empty;

        [ObservableProperty]
        private string loginPassword = string.Empty;

        [ObservableProperty]
        private string mensaje = "Completa los campos para registrar o iniciar sesión.";

        public ObservableCollection<User> Usuarios { get; } = [];

        public MainViewModel() : this(new UserRepository())
        {
        }

        public MainViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            CargarUsuarios();
        }

        [RelayCommand]
        private void Registrar()
        {
            if (string.IsNullOrWhiteSpace(Correo)
                || string.IsNullOrWhiteSpace(Nombre)
                || string.IsNullOrWhiteSpace(Usuario)
                || string.IsNullOrWhiteSpace(Password))
            {
                Mensaje = "Todos los campos del registro son obligatorios.";
                return;
            }

            var usuarios = _userRepository.ObtUsuarios();

            var yaExiste = usuarios.Any(u =>
                string.Equals(u.UserN, Usuario, StringComparison.OrdinalIgnoreCase));

            if (yaExiste)
            {
                Mensaje = "Ese nombre de usuario ya existe. Elige otro.";
                return;
            }

            usuarios.Add(new User(Correo.Trim(), Usuario.Trim(), Password, Nombre.Trim()));
            _userRepository.AgregarUsuario(usuarios);
            CargarUsuarios();

            Correo = string.Empty;
            Nombre = string.Empty;
            Usuario = string.Empty;
            Password = string.Empty;
            Mensaje = "Usuario registrado correctamente.";
        }

        [RelayCommand]
        private void IniciarSesion()
        {
            if (string.IsNullOrWhiteSpace(LoginUsuario) || string.IsNullOrWhiteSpace(LoginPassword))
            {
                Mensaje = "Ingresa usuario y contraseña para iniciar sesión.";
                return;
            }

            var loginOk = _userRepository.Login(LoginUsuario.Trim(), LoginPassword);
            Mensaje = loginOk
                ? $"Bienvenido, {LoginUsuario.Trim()} 🎉"
                : "Usuario o contraseña inválidos.";
        }

        private void CargarUsuarios()
        {
            Usuarios.Clear();
            foreach (var user in _userRepository.ObtUsuarios())
            {
                Usuarios.Add(user);
            }
        }
    }
}
