using IntroSE.Kanban.Backend.ServiceLayer.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Model
{
    public class BoardM : Notifiable
    {
        public string CreatorEmail { get; set; }
        public string Name { get; set; }
        public ObservableCollection<ColumnM> columnMs { get; set; }

        public BoardM(string email,string name)
        {
            CreatorEmail = email;
            Name = name;
        }
        public BoardM(BoardS board)
        {
            CreatorEmail = board.userEmail;
            Name = board.name;
        }
    }
}
