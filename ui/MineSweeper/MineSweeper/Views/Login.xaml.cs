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
    /// 
    
        //这个页面是初始页面
    public sealed partial class Login : Page
    {
        string UserPassword;
        string UserName;
        IService Service = new IService();

        private UserLogin UserLogin;
        private bool _isExistingAccount;

        public Login()
        {
            this.InitializeComponent();
            Loaded += InitAccounts;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                _isExistingAccount = true;
                UserLogin = e.Parameter as UserLogin;
                //事实上应该命名为UserEmail
               // UsernameTextBox.Text = UserLogin.Email;
                SignInPassport();
            }
            else
            {
                DialogCreator.CreateDialog("文件读写错误！", "用户名信息传递为空！");
            }
            
        }

        private async void  InitAccounts(object sender, RoutedEventArgs e)
        {
           
            await UserAccountHelper.LoadAccountListAsync();
            Frame.Navigate(typeof(UserSelection));
        }

        private void PassportSignInButton_Click(object sender, RoutedEventArgs e)
        {
            SignInPassport();
        }


        private async void  SignInPassport()
        {
            //默认成功登录
            //todo 之后

            UserName = UsernameTextBox.Text;
            UserPassword = UserpasswordTextBox.Text;

            var user = new UserLogin(UserName, UserPassword);

            //暂时默认为成功

            bool result = await Service.Login(user);
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
