using MineSweeper.Model;
using MineSweeper.Presenter;
using MineSweeper.Views;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MineSweeper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IService service;

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

            PlayerListView.ItemsSource = players;
        }

      
      

    }
}
