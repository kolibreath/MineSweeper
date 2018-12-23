using MineSweeper.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MineSweeper.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserSelection : Page
    {
        public UserSelection()
        {
            this.InitializeComponent();
            Loaded += UserSelection_Loaded;
        }

        //选择这个用户的时候触发
        private void UserSelectionChanged(object sender, RoutedEventArgs e)
        {
            ListView listview = sender as ListView;
            if(listview.SelectedValue != null)
            {
                UserLogin userlogin = listview.SelectedValue as UserLogin;
                //传递内容是不是有问题？
                Debug.WriteLine(userlogin.Email);
                //直接将数据传递到Login
                Frame.Navigate(typeof(Login), userlogin);
            }
        }

        private void AddUserButton_Click(object sender,RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login),null);
        }
        private void UserSelection_Loaded(object sender, RoutedEventArgs e)
        {
            //没有用户的时候自动导航到login
            if (UserAccountHelper.UserAccounts.Count == 0) 
             Frame.Navigate(typeof(Login));
            else
            {
                UserListView.ItemsSource = UserAccountHelper.UserAccounts;
                UserListView.SelectionChanged += UserSelectionChanged;
            }
        }
    }
}
