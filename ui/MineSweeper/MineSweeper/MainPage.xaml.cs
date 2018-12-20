using MineSweeper.Model;
using MineSweeper.Presenter;
using MineSweeper.Views;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using System;
using System.Diagnostics;
using MineSweeper.Utils;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Playback;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MineSweeper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        //系统同时给两个玩家的Mine 设置
        MineGenerator MyMine;
        //tobe set!
        private Player Challenged;

        //todo to be set
        private int Row;
        //todo to be set
        private int Column;

        //todo to be set
        private int Bombs;

        private IService service;
        private List<Player> TopPlayers;

        private MediaPlayback playback;
        private MediaPlayer element;

        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;

            service = new IService();
        }

        //todo test will this works???
        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //开始播放背景音乐
            playback = new MediaPlayback("begin.mp3");
            element = await playback.InitPlaybackSource();

            element.Play();
            //完成之后循环播放
          
            element.MediaEnded += delegate
            {
                Debug.WriteLine("fuck end");
                //MediaPlayback playback2 = new MediaPlayback("begin.mp3");
                //element = await playback2.InitPlaybackSource();
                element.Position = TimeSpan.Zero;
                element.Play();
            };

         
            TopPlayers = await service.GetTopPlayers() ;
            PlayerListView.ItemsSource = TopPlayers;

            UserLogin userlogin = new UserLogin();
         //   if(await service.GetStatus(userlogin))
            Debug.WriteLine("loaded?");

        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
          
        }


     



        //挑战玩家的点击事件！
        private void PlayerListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Player  clickItem = e.ClickedItem as Player;
            ListViewItem item = PlayerListView.ContainerFromItem(clickItem) as ListViewItem;
            if(item != null)
            {
                item.Background = new SolidColorBrush(Colors.Transparent);

                if (clickItem == null)
                    Debug.WriteLine("player is null!");
                else
                {
                    Challenged = clickItem;
                    ShowPlayerInfoDialog();
                }
            }
        }

        //显示一个提示框
        private async void ShowPlayerInfoDialog()
        {
            Debug.WriteLine("dialog triggered!!");
            var content = "你确定要挑战这名玩家吗？\n" + "player:" + Challenged.Email
                + "\n nickname:" + Challenged.UserName + "\n score" + Challenged.Score ;
            ContentDialog dialog = new ContentDialog()
            {
                Title = "发出挑战通知",
                Content = content,
                PrimaryButtonText = "确定",
                SecondaryButtonText = "取消",
            };
            dialog.PrimaryButtonClick += ChallengePlayerEvent;
            await dialog.ShowAsync();
        }



        
        private async void ChallengePlayerEvent(ContentDialog dialog,ContentDialogButtonClickEventArgs e)
        {
            MineGenerator mine = await service.Challenge(Challenged,Row,Column,Bombs);
          if ( mine != null){
                //挑战成功
                MyMine = mine;

                ContentDialog gotoChanllenge = new ContentDialog()
                {
                    Title = "挑战成功",
                    Content = "确定现在开始游戏吗",
                    PrimaryButtonText = "确定",
                    SecondaryButtonText = "取消",
                };

                gotoChanllenge.PrimaryButtonClick += delegate
                {
                    Debug.WriteLine("fuck youself!");
                };

                try
                {
                    await dialog.ShowAsync();
                }
                catch (Exception exception) { }


                //同时向两个玩家发送
            }
            else
            {
                DialogCreator.CreateDialog("挑战失败！","网络错误，挑战失败");
            }
        }

        private void Normal_Stage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Miner), null);
        }

        private void Medium_Stage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Hard_Stage_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
