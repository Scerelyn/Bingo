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
        Random rng = new Random();

        public MainWindow()
        {
            InitializeComponent();
            BuildBoard();
        }

        public void BuildBoard()
        {
            BingoBoardGrid.Children.Clear();
            List<string> cellText = PickWithoutDupes();
            int cellTextIndex = 0;
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
                                ? new BingoCellInfo() { CellText = cellText[cellTextIndex] }
                                : new BingoCellInfo();
                        cellTextIndex++;
                    }
                    textBlock.MouseDown += (o, args) => { BingoCellInfo bci = ((BingoCellInfo)textBlock.DataContext); bci.IsChecked = !bci.IsChecked; };
                    textBlock.TextWrapping = TextWrapping.WrapWithOverflow;
                    textBlock.Padding = new Thickness(10);

                    border.Child = textBlock;
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

        /// <summary>
        /// Gets 24 cell text entries randomly selected without duplicates
        /// </summary>
        /// <returns>A List of string randomly choosen without duplicates</returns>
        public List<string> PickWithoutDupes()
        {
            List<string> choosen = new List<string>();
            List<int> choosenInt = new List<int>();
            if (cellContents.Count == 0)
            {
                return choosen;
            }
            List<int> nums = Enumerable.Range(1,cellContents.Count-1).ToList();
            for (int i = 0; i < 24; i++)
            {
                int num = nums[rng.Next(0,nums.Count)];
                nums.Remove(num);
                choosen.Add(cellContents[num]);
            }
            return choosen;
        }

        public void DoFileHelp(object sender, RoutedEventArgs args)
        {
            MessageBox.Show("The text file to use should be a plain text (.txt) file. Entries to put into cells should be separated by new lines. The first line of the file will always occupy the center cell.","Title");
        }

        public void DoReRandomize(object sender, RoutedEventArgs args)
        {
            BuildBoard();
        }
    }
}
