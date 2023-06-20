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
    /// Interaction logic for UserV.xaml
    /// </summary>
    public partial class UserV : Window
    {
        private ModelController controllerM = ModelController.GetInstance();
        private UserVM VMuser { get; set; }
        public UserV(MUser user)
        {
            InitializeComponent();
            VMuser =new UserVM(user);
            this.DataContext = VMuser;
            
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                controllerM.Logout(VMuser.UserEmail);
                Login login = new Login();
                login.Show();
                this.Close();


            }
            catch (Exception execption)
            {
                VMuser.Error = execption.Message;
                VMuser.TextColor = "red";
            }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VMuser.RemoveBoard();
                VMuser.Error = "Board removed seccesfully";
                VMuser.TextColor = "green";
            }
            catch (Exception execption)
            {
                VMuser.Error = execption.Message;
                VMuser.TextColor = "red";
            }

        }

        private void Join_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                controllerM.JoinBoard(VMuser.UserEmail, VMuser.SelectedBoard);
                VMuser.Error = "You joined seccesfully";
                VMuser.TextColor = "green";
            }
            catch (Exception execption)
            {
                VMuser.Error = execption.Message;
                VMuser.TextColor = "red";
            }


        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VMuser.AddBoard();
                VMuser.Error = "Board Added seccesfully";
                VMuser.TextColor = "green";
            }
            catch (Exception execption)
            {
                VMuser.Error = execption.Message;
                VMuser.TextColor = "red";
            }
        }

        private void InProgress_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VMuser.ProgresTasks = controllerM.getInProgressTasks(VMuser.UserEmail);
                VMuser.Error = "show is seccess";
                VMuser.TextColor = "green";
            }
            catch (Exception execption)
            {
                VMuser.Error = execption.Message;
                VMuser.TextColor = "red";
            }
        }

        private void Into_Click(object sender, RoutedEventArgs e)
        {
            //move to new window that show all the tasks and columns in this board and can edite,remove and add new column/task
            try
            {
                BoardM board = VMuser.SelectedBoard;
                if (board != null)
                {
                    BoardV boardView = new BoardV(board);
                    boardView.Show();
                    this.Close();
                }

            }
            catch (Exception execption)
            {
                VMuser.Error = execption.Message;
                VMuser.TextColor = "red";
            }
        }
    }
}
