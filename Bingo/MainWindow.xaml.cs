using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        List<string> cellContents = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            BuildBoard();
        }

        public void BuildBoard()
        {
            Random rng = new Random();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Border border = new Border();
                    border.BorderThickness = new Thickness(1);
                    border.BorderBrush = Brushes.Black;

                    TextBlock textBlock = new TextBlock();
                    Binding textBind = new Binding("CellText");
                    textBlock.SetBinding(TextBlock.TextProperty, textBind);
                    Binding bgBind = new Binding("IsChecked");
                    bgBind.Converter = new BoolToBrushConverter();
                    textBlock.SetBinding(TextBlock.BackgroundProperty, bgBind);
                    if (i == 2 && j == 2)
                    {
                        textBlock.DataContext =
                            cellContents.Count > 0
                                ? new BingoCellInfo() { CellText = cellContents[0] }
                                : new BingoCellInfo();
                    }
                    else
                    {
                        textBlock.DataContext = 
                            cellContents.Count > 0 
                                ? new BingoCellInfo() { CellText = cellContents[rng.Next(1,cellContents.Count)] }
                                : new BingoCellInfo();
                    }
                    textBlock.MouseDown += (o, args) => { BingoCellInfo bci = ((BingoCellInfo)textBlock.DataContext); bci.IsChecked = !bci.IsChecked; };
                    textBlock.TextWrapping = TextWrapping.WrapWithOverflow;
                    textBlock.Padding = new Thickness(10);

                    border.Child = textBlock;
                    border.Height = 150;
                    border.Width = 150;
                    BingoBoardGrid.Children.Add(border);
                    Grid.SetColumn(border, i);
                    Grid.SetRow(border, j);
                }
            }
        }

        public void DoOpen(object sender, RoutedEventArgs args)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            bool? dialogResult = ofd.ShowDialog();
            if (dialogResult ?? false)
            {
                cellContents = File.ReadLines(ofd.FileName).ToList();
                BuildBoard();
            }
        }
    }
}
