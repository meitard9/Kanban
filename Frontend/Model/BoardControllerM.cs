using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Model
{
    public class BoardControllerM : Notifiable
    {
        private ModelController Mcontroller { get; set; }
        private readonly MUser user;
        public ObservableCollection<BoardM> boards { get; set; }

        private BoardControllerM(ObservableCollection<BoardM> boards)
        {
            this.boards =boards;
            this.boards.CollectionChanged += HandleChangeBoard;
            Mcontroller = ModelController.GetInstance();
        }

        public BoardControllerM(MUser user)
        {
            this.user = user;
            Mcontroller = ModelController.GetInstance();
            boards = new ObservableCollection<BoardM>(Mcontroller.getAllBoardMs().ToList());
            boards.CollectionChanged += HandleChangeBoard;
           
        }
        public void RemoveBoard(BoardM b)
        {

            boards.Remove(b);

        }
        public void AddBoard(BoardM b)
        {

            boards.Add(b);

        }

        private void HandleChangeBoard(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (BoardM b in e.OldItems)
                {

                    Mcontroller.RemoveBoard(user.UserEmail, b);
                }

            }
            if (e.Action == NotifyCollectionChangedAction.Add)
            {

                foreach (BoardM b in e.NewItems)
                {
                    Mcontroller.AddBoard(user.UserEmail, b.Name);
                
                }
            }
        }
    }
}
