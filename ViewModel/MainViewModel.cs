using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repaso.Model;
using Repaso.Repositorio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;

namespace Repaso.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {

        private readonly IUserRepository _userRepository;
        public MainViewModel(IUserRepository userRepository)
        {
                _userRepository = userRepository;
        }

        [ObservableProperty]
        private UserLogin userloginV;

        [RelayCommand]
        public void login()
        {
            var validacion = _userRepository.Login(userloginV.Password, userloginV.UserName);
            if (validacion)
            {
                MessageBox.Show("Usuario Valido");
            }
            else 
            {
                MessageBox.Show("fallido");
            }
        }

        [RelayCommand]
        private void AgregarUsuario()
        {
            var newusuarios = new User("isai26361", "edgar", "200622", "j28isai");
            var nuevalista = new List<User>();
            nuevalista.Add(newusuarios);

            _userRepository.AgregarUsuario(nuevalista);
        }
    }
}
