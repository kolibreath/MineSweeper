using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace MineSweeper.Utils
{
    class MediaPlayback
    {
        /// <summary>
        /// 使用方法：
        /// 实例化MediaPlayback 传入需要播放的音频名称
        /// 使用实例初始化MediaElement 播放元素
        /// mediaElement.InitPlaybackSource();
        /// 
        /// 使用播放元素播放
        /// mediaElement.Play();
        /// 
        /// 使用实例
        /// MediaPlayback playback = new MediaPlayback("begin.mp3");
        /// MediaElement element = await playback.InitPlaybackSource();
        /// element.Play();
        /// </summary>
        string musicName;
        public MediaPlayback(string musicName)
        {
            this.musicName = musicName;
         
        }

        public  async Task<MediaPlayer> InitPlaybackSource()
        {
            Windows.Storage.StorageFolder Folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            Folder = await Folder.GetFolderAsync("Assets");
            Windows.Storage.StorageFile sf = await Folder.GetFileAsync(musicName);


             MediaPlayer PlayMusic = new MediaPlayer();
            PlayMusic.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/"+musicName));

            return PlayMusic;
        }

        
    }
}
