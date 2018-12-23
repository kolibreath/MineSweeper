using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace MineSweeper.Views
{
    class DialogCreator
    {
     
       

      //  ChallengePlayerEvent(ContentDialog dialog, ContentDialogButtonClickEventArgs e)
        public static async void CreateDialog(string title, 
            string content, TypedEventHandler<ContentDialog,ContentDialogButtonClickEventArgs> primaryClickEvent = null , 
            TypedEventHandler<ContentDialog, ContentDialogButtonClickEventArgs> secondaryClickEvent = null )
        {
            ContentDialog dialog = new ContentDialog()
            {
            
                
                Title = title,
                Content = content,
                PrimaryButtonText = "确定",
                SecondaryButtonText = "取消",
            };

            if (primaryClickEvent != null)
                dialog.PrimaryButtonClick += primaryClickEvent;
            if (secondaryClickEvent != null)
                dialog.SecondaryButtonClick += secondaryClickEvent;

              try {
                    await dialog.ShowAsync();
               }catch(Exception e) { }
        }


     
       
    }

}
