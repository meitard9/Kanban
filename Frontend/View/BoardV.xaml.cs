using Frontend.Model;
using Frontend.ViewModel;
using System.Windows;

namespace Frontend.View
{
    /// <summary>
    /// Interaction logic for BoardV.xaml
    /// </summary>
    public partial class BoardV : Window
    {
        private BoardVM boardVM;
        public BoardV(BoardM board)
        {
            InitializeComponent();
            this.boardVM = new BoardVM(board);
            DataContext = boardVM;

        }
    }
}
