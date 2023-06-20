using Frontend.Model;
using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Frontend.ViewModel
{
    class LoginVM:Notifiable
    {
        private readonly ModelController controllerM;
        private string _error;
        private String _textColor;
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Error { get=>_error;
            set {
                _error = value;
                RaisePropertyChanged("Error");
                } }
        public String TextColor
        {
            get => _textColor;
            set
            {
                _textColor = value;
                RaisePropertyChanged("TextColor");
            }
        }
        public LoginVM(ModelController controllerM)
        {
            this.controllerM = controllerM;
        }
        internal MUser Login()
        {
             return controllerM.Login(UserName, Password);
        }
        internal void Register()
        {

             controllerM.Register(UserName, Password);
        }

    }
}
