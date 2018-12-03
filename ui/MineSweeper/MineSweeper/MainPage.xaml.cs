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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MineSweeper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IService service;
        private List<Player> TopPlayers;
        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;

            service = new IService();
        }

        //todo test will this works???
        private   void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //   UserListView.ItemsSource = await service.GetTopPlayers();
            List<Player> players = new List<Player>();
            for(int i = 0;i<5;i++)
            {
                players.Add(new Player
                {
                    NickName = "hha" + i,
                    Score = 100,
                    Email = "fuck"
                });
            }
            TopPlayers = players;
            PlayerListView.ItemsSource = players;
            Debug.WriteLine("loaded?");
        }

    
        //
        private void Normal_Stage_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {

        }

        private void Medium_Stage_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {

        }

        private void Hard_Stage_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {

        }

        private void PlayerListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            dynamic  clickItem = e.ClickedItem;
            Debug.WriteLine("if the value is null?");
            ListViewItem item = PlayerListView.ContainerFromItem(clickItem) as ListViewItem;
            if(item != null)
            {
                Player value = ((ListView)sender).SelectedValue as Player;
                item.Background = new SolidColorBrush(Colors.Transparent);
                Debug.WriteLine("want to trigger info dialog");
                if(value == null)
                    Debug.WriteLine("player is null!");
                else
                    ShowPlayerInfoDialog(value);
               
            }
        }

        //显示一个提示框
        private async void ShowPlayerInfoDialog(Player player)
        {
            Debug.WriteLine("dialog triggered!!");
            var content = "你确定要挑战这名玩家吗？\n" + "player:" + player.Email
                + "\n nickname:" + player.NickName + "\n score" + player.Score ;
            ContentDialog dialog = new ContentDialog()
            {
                Title = "No wifi connection",
                Content = content,
                PrimaryButtonText = "确定",
                SecondaryButtonText = "取消",
            };

            await dialog.ShowAsync();
        }
    }
}
