using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Playback;
using MineSweeper.Utils;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

//MineSweeper.Views

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板


namespace MineSweeper.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Miner : Page
    {
        const string bstr = "";
        const string cstr = "";
        int Cnum = 16;
        int Rnum = 16;
        ColumnDefinition[] cdefs;
        RowDefinition[] rdefs ;
        Button[,] buttons;
        int[,] bombs;

        public Miner()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var parameters = (MinerPageParams)e.Parameter;
            Cnum = parameters.Column;
            Rnum = parameters.Row;
            cdefs = new ColumnDefinition[Cnum];
            rdefs = new RowDefinition[Rnum];
            buttons = new Button[Cnum, Rnum];
            bombs = new int[Cnum, Rnum];
        }

        
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            GridInit();
            BombInit();
            ButtonInit();

            // TimeCounter counter1 :记录使用了多长时间
            TimerCounter counter1 = new TimerCounter(timetext);
            counter1.StartCountDown();

            ColumnDefinition[] cdefs = new ColumnDefinition[Cnum];
            RowDefinition[] rdefs = new RowDefinition[Rnum];
            Button[,] buttons = new Button[Cnum, Rnum];
            bombs = new int[Cnum, Rnum];
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
          
            Button b = (Button)sender;
            var column = Grid.GetColumn(b);
            var row = Grid.GetRow(b);

            if (bombs[column, row] == 1)
            {
                b.Background = new SolidColorBrush(Color.FromArgb(255, 236, 103, 98));
                await MediaPlayback.GameFailed();
                Frame.Navigate(typeof(MainPage), null);
            }
            else
            {
                // need a bfs
                // first is row second is column
                for (int i = column - 1; i <= column + 1; i += 1)
                {
                    for (int j = row - 1; j <= row + 1; j += 1)
                    {
                        if (i < Cnum && j < Rnum && i >= 0 && j >= 0)
                        {
                            if (bombs[j, i] != 1)
                            {
                                Button btemp = buttons[j, i];
                                btemp.Background = new SolidColorBrush(Color.FromArgb(255, 77, 153, 79));
                            }
                        }
                    }
                }
            }
        }

        private void GridInit()
        {
            grid.Width = 900;
            grid.Height = 900;
            grid.Margin = new Thickness(5, 5, 5, 5);
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.VerticalAlignment = VerticalAlignment.Center;
            grid.Background = new SolidColorBrush(Windows.UI.Colors.Gray);
            //double clen = Width / Cnum;
            //double rlen = Height / Rnum;
        }

        private void BombInit()
        {
            for (int i = 0; i < Cnum; i++)
            {
                for (int j = 0; j < Rnum; j++)
                {
                    if (i == 3 && j == 3)
                    {
                        bombs[i, j] = 1;
                    }
                    else
                    {
                        bombs[i, j] = 0;
                    }
                }
            }
        }

        private void ButtonInit()
        {
            for (int i = 0; i < Cnum; i += 1)
            {
                for (int j = 0; j < Rnum; j += 1)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Content = bstr;
                    buttons[i, j].Margin = new Thickness(2, 2, 2, 2);
                    buttons[i, j].HorizontalAlignment = HorizontalAlignment.Stretch;
                    buttons[i, j].VerticalAlignment = VerticalAlignment.Stretch;
                    buttons[i, j].Background = new SolidColorBrush(Windows.UI.Colors.Gray);
                    buttons[i, j].BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black);
                    buttons[i, j].BorderThickness = new Thickness(1, 1, 1, 1);

                    Grid.SetRow(buttons[i, j], i);
                    Grid.SetColumn(buttons[i, j], j);
                    grid.Children.Add(buttons[i, j]);
                    buttons[i, j].Click += Button_Click;
                    //buttons[i, j] = button;
                    //buttons[i, j].Width = buttons[i, j].Height;
                }
            }
            for (int i = 0; i < Cnum; i += 1)
            {
                cdefs[i] = new ColumnDefinition();
                grid.ColumnDefinitions.Add(cdefs[i]);
            }

            for (int i = 0; i < Rnum; i += 1)
            {
                rdefs[i] = new RowDefinition();
                grid.RowDefinitions.Add(rdefs[i]);
            }
            Grid.SetColumn(timetext, 0);
            Grid.SetRow(timetext, 0);
        }

    }
}

