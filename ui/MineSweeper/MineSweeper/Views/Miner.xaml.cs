using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MineSweeper.Utils;
using MineSweeper.Presenter;
using Windows.Foundation;
using MineSweeper.Views;
using System.Diagnostics;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading;


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
        int FirstClickFlag = 1;
        int Cnum = 8;
        int Rnum = 8;
        int Bombnum = 10;
        int OpenButtonNum = 10;
        int OpenButtonDepth = 5;
        int InitHardScore = 1000;
        TimerCounter counter1;
        IService Service = new IService();

        bool [,] IsRightTapped;

        ColumnDefinition[] cdefs;
        RowDefinition[] rdefs ;
        Button[,] buttons;
        
        MineGenerator MG;

        private int index = 0;

        private Timer AvatarTimer;

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

            IsRightTapped = new bool[Rnum, Cnum];

            for (int i = 0; i < Cnum; i++)
            {
                for (int j = 0; j < Rnum; j++)
                {
                    IsRightTapped[i, j] = false;
                }
            }

            OpenButtonNum = parameters.OpenButtonNum;
            OpenButtonDepth = parameters.OpenButtonDepth;
            InitHardScore = parameters.HardScore;

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
            counter1 = new TimerCounter(Timetext);
            counter1.StartCountDown();

            ColumnDefinition[] cdefs = new ColumnDefinition[Cnum];
            RowDefinition[] rdefs = new RowDefinition[Rnum];
            Button[,] buttons = new Button[Cnum, Rnum];
        }

        private async void Miner_RightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {
            var b = (Button)sender;
            var row = Grid.GetRow(b);
            var column = Grid.GetColumn(b);
            await MediaPlayback.MinePanelClick();

            if (!IsRightTapped[row, column])
            {
                //变成橙色
                b.Background = new SolidColorBrush(Color.FromArgb(255, 228, 163, 48));
                IsRightTapped[row, column] = true;
            }
            else
            {
                //todo 恢复正常颜色 
                IsRightTapped[row, column] = false;
                b.Background = new SolidColorBrush(Color.FromArgb(255, 228, 100, 48));
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            /*四种对话框点击事件*/
            // 成功，上传成绩
            Debug.WriteLine(MG.GetOpenCount());
            
            TypedEventHandler<ContentDialog, ContentDialogButtonClickEventArgs> SuccessTrue = async delegate
            {
                counter1.StopCountDown();
                await Service.PostScore(UserAccountHelper.USER_ID, InitHardScore - counter1.Count);
                Frame.Navigate(typeof(MainPage), null);
            };
            // 成功，不上传成绩
            TypedEventHandler<ContentDialog, ContentDialogButtonClickEventArgs> SuccessFalse = delegate
            {
                counter1.StopCountDown();
;               Frame.Navigate(typeof(MainPage), null);
            };
            // 失败，上传成绩（设置为负数）
            TypedEventHandler<ContentDialog, ContentDialogButtonClickEventArgs> FailedTrue = async delegate
            {
                counter1.StopCountDown();
                await Service.PostScore(UserAccountHelper.USER_ID, InitHardScore - counter1.Count - InitHardScore);
                Frame.Navigate(typeof(MainPage), null);
            };
            // 失败，不上传成绩
            TypedEventHandler<ContentDialog, ContentDialogButtonClickEventArgs> FailedFalse = delegate
            {
                counter1.StopCountDown();
                Frame.Navigate(typeof(MainPage), null);
            };

            // click声音
            await MediaPlayback.MinePanelClick();

            Button b = (Button)sender;
            var column = Grid.GetColumn(b);
            var row = Grid.GetRow(b);

            var eater = new EaterEgg(Cnum, Rnum, row, column);
            if(eater.TriggerEaterEgg(MG.Field))
            {
                for(int i = 0;i< Cnum; i++)
                {
                    for (int j = 0; j < Rnum; j++)
                    {
                        if (MG.Field[i, j] == 1)
                        {
                            buttons[i, j].Background = new SolidColorBrush(Color.FromArgb(255, 255, 00, 00));
                            buttons[i, j].Content = MG.Panel[i, j];
                        }
                    }
                }
                return;
            }

            //  首次点击
            if (FirstClickFlag == 1 && MG.Field[row, column] != -1)
            {
                AnimateAvatar();
                MG.InitArea(OpenButtonDepth, 0, row, column, OpenButtonNum);
                
                for(int i = 0; i < Rnum; i++)
                {
                    for(int j = 0; j < Cnum; j++)
                    {
                        // 9 代表open
                        if(MG.Record[i, j] == 9)
                        {
                            buttons[i, j].Background = new SolidColorBrush(Color.FromArgb(255, 77, 153, 79));
                            buttons[i, j].Content = MG.Panel[i, j];
                        }
                    }
                }
                buttons[row, column].Background = new SolidColorBrush(Color.FromArgb(255, 77, 153, 79));
                buttons[row, column].Content = MG.Panel[row, column];
                FirstClickFlag = 0;
            }
            // 第二次之后的点击
            else
            {
                if (MG.Field[row, column] == -1)
                {
                    // failed 失败 <<<<<<<<<<<<<<<<<<<
                    b.Background = new SolidColorBrush(Color.FromArgb(255, 236, 103, 98));
                    await MediaPlayback.GameFailed();
                    DialogCreator.CreateDialog("失败", "失败，是否上传成绩?", FailedTrue, FailedFalse);

                    ImageBrush imageBrush = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/" + "failed.jpg", UriKind.Absolute))
                    };
                    Player_status.Fill = imageBrush;
                    //Frame.Navigate(typeof(MainPage), null);
                }
                else
                {
                    AnimateAvatar();
                    b.Background = new SolidColorBrush(Color.FromArgb(255, 77, 153, 79));
                    b.Content = MG.Panel[row, column];
                    MG.SetOpen(row, column);
                }
                if (MG.GetOpenCount() == (Cnum * Rnum) - Bombnum)
                {
                    //success 成功  <<<<<<<<<<<<<<<<<<<<<<<<
                    //Frame.Navigate(typeof(MainPage), null);
                                     
                    Debug.WriteLine((Cnum * Rnum) - Bombnum);
                    await MediaPlayback.GameVictory();
                    DialogCreator.CreateDialog("成功", "成功，是否上传成绩？", SuccessTrue, SuccessFalse);
                }
            }
        }

        private void GridInit()
        {
            //初始长度不要太长
            grid.Width = 600;
            grid.Height = 600;
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
                    buttons[i, j] = new Button
                    {
                        Content = bstr,
                        Margin = new Thickness(2, 2, 2, 2),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        Background = new SolidColorBrush(Windows.UI.Colors.Gray),
                        BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black),
                        BorderThickness = new Thickness(1, 1, 1, 1)
                    };

                    Grid.SetRow(buttons[i, j], i);
                    Grid.SetColumn(buttons[i, j], j);
                    grid.Children.Add(buttons[i, j]);
                    buttons[i, j].Click += Button_Click;
                    buttons[i, j].RightTapped += Miner_RightTapped;
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
            Grid.SetColumn(Timetext, 0);
            Grid.SetRow(Timetext, 0);
        }

        private void Timetext_SelectionChanged(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public async void Invoke(Action action, Windows.UI.Core.CoreDispatcherPriority Priority = Windows.UI.Core.CoreDispatcherPriority.Normal)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Priority, () => { action(); });
        }

      
        private void AnimateAvatar()
        {
           AvatarTimer = new System.Threading.Timer(TimerAction, null, TimeSpan.FromSeconds(0), TimeSpan.FromMilliseconds(100));
            
        }


        private void TimerAction(object obj)
        {
            this.Invoke(() => {
                
                string[] pics = { "phase1.jpg", "phase2.jpg", "phase3.jpg","phase1.jpg" };

                index++;
                ImageBrush imageBrush = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/" + pics[index], UriKind.Absolute))
                };
                Player_status.Fill = imageBrush;

                if(index == 3)
                {
                    index = 0;
                    imageBrush = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/" + pics[index], UriKind.Absolute))
                    };
                    Player_status.Fill = imageBrush;
                    AvatarTimer.Dispose();
                 
                }
                    
            }
             );
        }

    }
}

