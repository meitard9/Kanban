using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Frontend.ViewModel
{
    class UserVM:Notifiable
    {
        private readonly ModelController controllerM;
        public string UserEmail { get; private set; }
        public string Password { get; private set; }
        public string Title { get;private set; }
        private List<TaskM> _progresTasks;
        public List<TaskM> ProgresTasks { get=>_progresTasks; set {
                _progresTasks = value;
                RaisePropertyChanged("ProgresTasks");
            } }
        public BoardControllerM BoardCM { get; set; }
        private string _boardToAdd;
        public string BoardToAdd { get => _boardToAdd;set {
                _boardToAdd = value;
                //RaisePropertyChanged("BoardToAdd");
            } }
        private BoardM _selectedBoard;
        public BoardM SelectedBoard {
            get
            {
                return _selectedBoard;
            }
            set
            {
                _selectedBoard = value;
                EnableForward = value != null;
                RaisePropertyChanged("SelectedBoard");
            }
        }
        private bool _enableForward = false;
        public bool EnableForward
        {
            get => _enableForward;
            private set
            {
                _enableForward = value;
                RaisePropertyChanged("EnableForward");
            }
        }       
        private string _error;
        private string _textColor;
        public string Error
        {
            get => _error;
            set
            {
                _error = value;
                RaisePropertyChanged("Error");
            }
        }
        public string TextColor
        {
            get => _textColor;
            set
            {
                _textColor = value;
                RaisePropertyChanged("TextColor");
            }
        } 

        public UserVM(MUser user)
        {
            this.controllerM = ModelController.GetInstance();
            UserEmail = user.UserEmail;
            Password = user.Password;
            Title = "Forum for " + UserEmail;
            RaisePropertyChanged("Title");
            TextColor = "black";
            BoardCM = new BoardControllerM(user);
        }

        internal void AddBoard()
        {
            BoardCM.AddBoard(new BoardM(UserEmail,BoardToAdd));
        }
        internal void RemoveBoard()
        {
            BoardCM.RemoveBoard(SelectedBoard);
        }
       
    }
}
