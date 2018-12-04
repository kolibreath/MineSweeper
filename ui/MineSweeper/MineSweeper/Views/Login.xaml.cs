using MineSweeper.Presenter;
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
    public sealed partial class Login : Page
    {
        string UserPassword;
        string UserName;
        IService Service;

        public Login()
        {
            this.InitializeComponent();
            Loaded += InitAccounts;


          
        }

        private async void  InitAccounts(object sender, RoutedEventArgs e)
        {
            //测试生成炸弹
            MineGenerator mine = new MineGenerator(9, 9, 10);
            //随机表示炸弹
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Debug.Write(mine.Field[i, j]);
                    Debug.Write(" ");
                }
                Debug.WriteLine("");
            }

            Debug.WriteLine("");

            //随机表示炸弹
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Debug.Write(mine.Panel[i, j]);
                    Debug.Write(" ");
                }
                Debug.WriteLine("");
            }
            await  UserAccountHelper.LoadAccountListAsync();
        }

        private void PassportSignInButton_Click(object sender, RoutedEventArgs e)
        {
            //默认成功登录
            //todo 之后

            UserName = UsernameTextBox.Text;
            UserPassword = UserpasswordTextBox.Text;

            var user = new UserLogin(UserName, UserPassword);

            //暂时默认为成功
            bool result = true;
         //   bool result = await service.Login(user);
            if (result && UserAccountHelper.CheckCredential(user))
            {
                   
                UserAccountHelper.AddAccount(user);
                UserAccountHelper.SaveAccountListAsync();
                Debug.WriteLine("the size of the list" + UserAccountHelper.UserAccounts.Count);
                Frame.Navigate(typeof(MainPage), null);
            }
            else
                DialogCreator.CreateDialog("登陆失败",
                    "登录失败请重新尝试");
            
        }


        private void RegisterButtonTextBlock_OnPointerPressed(object sender, RoutedEventArgs e)
        {
            //跳转到注册界面
            Frame.Navigate(typeof(Register), null);
        }

        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void UserpasswordTextBox_TextChanged(object sender,TextChangedEventArgs e)
        {

        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
