using MineSweeper.Model;
using MineSweeper.Presenter;
using System;
using System.Collections.Generic;
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
    public sealed partial class Register : Page
    {
        IService Service = new IService();
        string UserName;
        string Userpassword;
        string Email;

        public Register()
        {
            this.InitializeComponent();
        }

        private async void PassportRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            UserName = UsernameTextBox.Text;
            Userpassword = UserpasswordTextBox.Text;
            Email = UserEmailTextBox.Text;

            UserRegister register = new UserRegister(UserName, Userpassword, Email);
            bool result = await Service.SignUp(register);
            if (result)
            {
                Frame.Navigate(typeof(Login),null);
            }
            else
            {
                DialogCreator.CreateDialog("注册失败", "请重新注册");
            }
            //todo 注册到服务器
        }

        private void RegisterButtonTextBlock_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login), null);
        }

        private void UserpasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
