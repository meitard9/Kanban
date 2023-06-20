using Frontend.Model;
using Frontend.ViewModel;
using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Frontend.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private ModelController controllerM = ModelController.GetInstance();
        private LoginVM loginVM; 
        public Login()
        {
            controllerM.LoadAll();
            InitializeComponent();
            loginVM = new LoginVM(controllerM);
            DataContext = loginVM;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MUser user = loginVM.Login();
                if (user != null)
                {
                    UserV userView = new UserV(user);
                    userView.Show();
                    this.Close();
                }
                
            }
            catch(Exception execption) {
                loginVM.Error = execption.Message;
                loginVM.TextColor = "red";
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                loginVM.Register();
                loginVM.TextColor = "green";
                loginVM.Error = "Register seccess";
                
            }
            catch(Exception execption) {
                loginVM.Error = execption.Message;
                loginVM.TextColor = "red";
            }
        }
    }
}
