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
        private MediaPlayback(string musicName)
        {
            this.musicName = musicName;
         
        }

        /// <summary>
        /// 停止播放
        /// </summary>
        /// <param name="player"></param>
        public static void StopPlay( MediaPlayer player)
        {
            player.Dispose();
        }

        private  async Task<MediaPlayer> InitPlaybackSource()
        {
            Windows.Storage.StorageFolder Folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            Folder = await Folder.GetFolderAsync("Assets");
            Windows.Storage.StorageFile sf = await Folder.GetFileAsync(musicName);



            MediaPlayer PlayMusic = new MediaPlayer
            {
                Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/" + musicName))
            };

            return PlayMusic;
        }

        public static async Task<MediaPlayer> Welcome(bool loop = false)
        {
            //开始播放背景音乐
            MediaPlayback playback = new MediaPlayback("begin.mp3");
            MediaPlayer element = await playback.InitPlaybackSource();

            element.Play();
            //完成之后循环播放

            if (loop)
            {
                element.MediaEnded += delegate
                {
                element.Position = TimeSpan.Zero;
                    element.Play();
                };

            }
            return element;
        }

        /// <summary>
        /// 使用方法：var player = await MusicPlayback.Playxxx()
        /// </summary>
        /// static 方法 直接使用类名称调用
        /// 第一个参数表示是否进行循环
        /// <param name="loop"></param>
        /// <returns></returns>

        public static async Task<MediaPlayer> MinePanelClick(bool loop = false)
        {
            //开始播放背景音乐
            MediaPlayback playback = new MediaPlayback("click-1.mp3");
            MediaPlayer element = await playback.InitPlaybackSource();

            element.Play();
            //完成之后循环播放

            if (loop)
            {
                element.MediaEnded += delegate
                {
                    element.Position = TimeSpan.Zero;
                    element.Play();
                };

            }
            return element;
        }

        public static async Task<MediaPlayer> NewStatus(bool loop = false)
        {
            //开始播放背景音乐
            MediaPlayback playback = new MediaPlayback("message-1.mp3");
            MediaPlayer element = await playback.InitPlaybackSource();

            element.Play();
            //完成之后循环播放

            if (loop)
            {
                element.MediaEnded += delegate
                {
                    element.Position = TimeSpan.Zero;
                    element.Play();
                };

            }
            return element;
        }

        public static async Task<MediaPlayer> GameFailed(bool loop = false)
        {
            string[] names = {"fail-1.mp3", "fail-2.mp3", "fail-3.mp3" };
            string file = names[new Random().Next() % 3];
            //开始播放背景音乐
            MediaPlayback playback = new MediaPlayback(file);
            MediaPlayer element = await playback.InitPlaybackSource();

            element.Play();
            //完成之后循环播放

            if (loop)
            {
                element.MediaEnded += delegate
                {
                    element.Position = TimeSpan.Zero;
                    element.Play();
                };

            }
            return element;
        }

        public static async Task<MediaPlayer> GameVictory(bool loop = false)
        {
            string[] names = { "victory-1.mp3", "victory-2.mp3"};
            string file = names[new Random().Next() % 2];
            //开始播放背景音乐
            MediaPlayback playback = new MediaPlayback(file);
            MediaPlayer element = await playback.InitPlaybackSource();

            element.Play();
            //完成之后循环播放

            if (loop)
            {
                element.MediaEnded += delegate
                {
                    element.Position = TimeSpan.Zero;
                    element.Play();
                };

            }
            return element;
        }


    }
}
