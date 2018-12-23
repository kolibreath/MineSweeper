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
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MineSweeper.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    
        //这个页面是初始页面
    public sealed partial class Login : Page
    {
        string UserPassword;
        string UserName;
        IService Service = new IService();

        private UserLogin UserLogin;
        private bool _isExistingAccount = false;

        public Login()
        {
            this.InitializeComponent();
            Loaded += InitAccounts;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                if (e.Parameter is UserLogin)
                {
                    _isExistingAccount = true;
                    UserLogin = (UserLogin)e.Parameter;
                    //事实上应该命名为UserEmail
                    Debug.WriteLine(UserLogin.Email);
                    // UsernameTextBox.Text = UserLogin.Email;
                    SignInPassport();
                }
            }
        }

        private async void  InitAccounts(object sender, RoutedEventArgs e)
        {

            await UserAccountHelper.LoadAccountListAsync();

            //是否从UserSelection导航过来的页面？

           
            Frame.Navigate(typeof(UserSelection));
           
        }

        private void PassportSignInButton_Click(object sender, RoutedEventArgs e)
        {
            SignInPassport();
        }


        private async void  SignInPassport()
        {
            //需要判断从哪里加载用户
            UserLogin user = null;
            if (!_isExistingAccount){
                UserName = UsernameTextBox.Text;
                UserPassword = UserpasswordTextBox.Text;
                user = new UserLogin(UserName, UserPassword);
            }else
            {
                UsernameTextBox.Text = UserLogin.Email;
                UserpasswordTextBox.Text = UserLogin.Password;
                user = UserLogin;
            } 

            //暂时默认为成功

            bool result = await Service.Login(user);
            if (result && UserAccountHelper.CheckCredential(user))
            {
                if (!_isExistingAccount)
                {
                    
                    UserAccountHelper.AddAccount(user);
                    UserAccountHelper.SaveAccountListAsync();
                }
                Debug.WriteLine("the size of the list" + UserAccountHelper.UserAccounts.Count);
                Frame.Navigate(typeof(MainPage), user);
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
