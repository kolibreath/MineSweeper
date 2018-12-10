using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MineSweeper.Views
{
    class DialogCreator
    {
       
        public static async void CreateDialog(string title, string content)
        {


            ContentDialog dialog = new ContentDialog()
            {
                Title = title,
                Content = content,
                PrimaryButtonText = "确定",
                SecondaryButtonText = "取消",
            };

              try {
                    await dialog.ShowAsync();
               }catch(Exception e) { }
        }


     
       
    }

}
