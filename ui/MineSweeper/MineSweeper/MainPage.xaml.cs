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

        private MediaPlayer player;

        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;

            service = new IService();
        }

        //todo test will this works???
        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            player = await MediaPlayback.Welcome(false);
         
            TopPlayers = await service.GetTopPlayers() ;
            PlayerListView.ItemsSource = TopPlayers;

            UserLogin userlogin = new UserLogin();
         //   if(await service.GetStatus(userlogin))

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

        private async void Normal_Stage_Click(object sender, RoutedEventArgs e)
        {
            await MediaPlayback.MinePanelClick();
            var parameters = new MinerPageParams();
            parameters.Row = 8;
            parameters.Column = 8;
            parameters.Bombs = 10;
            parameters.OpenButtonNum = 20;
            parameters.OpenButtonDepth = 3;
            Frame.Navigate(typeof(Miner), parameters);
        }

        private async void Medium_Stage_Click(object sender, RoutedEventArgs e)
        {
            await MediaPlayback.MinePanelClick();
            var parameters = new MinerPageParams();
            parameters.Row = 12;
            parameters.Column = 12;
            parameters.Bombs = 20;
            parameters.OpenButtonNum = 40;
            parameters.OpenButtonDepth = 4;
            Frame.Navigate(typeof(Miner), parameters);
        }

        private async void Hard_Stage_Click(object sender, RoutedEventArgs e)
        {
            await MediaPlayback.MinePanelClick();
            var parameters = new MinerPageParams();
            parameters.Row = 16;
            parameters.Column = 16;
            parameters.Bombs = 40;
            parameters.OpenButtonNum = 80;
            parameters.OpenButtonDepth = 5;
            Frame.Navigate(typeof(Miner), parameters);
        }
    }
}
