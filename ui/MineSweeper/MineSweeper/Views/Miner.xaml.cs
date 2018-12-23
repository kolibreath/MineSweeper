using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MineSweeper.Utils;
using MineSweeper.Presenter;

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
        int Cnum = 8;
        int Rnum = 8;
        int Bombnum = 10;
        ColumnDefinition[] cdefs;
        RowDefinition[] rdefs ;
        Button[,] buttons;
        
        MineGenerator MG;

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
            Bombnum = parameters.Bombs;
            cdefs = new ColumnDefinition[Cnum];
            rdefs = new RowDefinition[Rnum];
            buttons = new Button[Cnum, Rnum];
            // -1代表炸弹，数字代表无炸弹，数字个数为上下左右的炸弹数目;
        }

        
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            GridInit();
            // MG.Field为炸弹分布
            // MG.Panel为数字分布

            MG = new MineGenerator(Rnum, Cnum, Bombnum);
            ButtonInit();

            // TimeCounter counter1 :记录使用了多长时间
            TimerCounter counter1 = new TimerCounter(timetext);
            counter1.StartCountDown();

            ColumnDefinition[] cdefs = new ColumnDefinition[Cnum];
            RowDefinition[] rdefs = new RowDefinition[Rnum];
            Button[,] buttons = new Button[Cnum, Rnum];
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
          
            Button b = (Button)sender;
            var column = Grid.GetColumn(b);
            var row = Grid.GetRow(b);

            if(MG.Field[column, row] == -1)
            {
                b.Background = new SolidColorBrush(Color.FromArgb(255, 236, 103, 98));
                await MediaPlayback.GameFailed();
                Frame.Navigate(typeof(MainPage), null);
            }
            else
            {
                b.Background = new SolidColorBrush(Color.FromArgb(255, 77, 153, 79));
                b.Content = MG.Panel[column, row];
                
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

