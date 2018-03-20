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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bingo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Border border = new Border();
                    border.BorderThickness = new Thickness(1);
                    border.BorderBrush = Brushes.Black;
                    Label label = new Label();
                    Binding bind = new Binding("CellText");
                    label.SetBinding(Label.ContentProperty, bind);
                    label.DataContext = new BingoCellInfo() { CellText = $"C{i}R{j}" };
                    border.Child = label;
                    

                    BingoBoardGrid.Children.Add(border);
                    Grid.SetColumn(border, i);
                    Grid.SetRow(border, j);
                }
            }
        }
    }
}
