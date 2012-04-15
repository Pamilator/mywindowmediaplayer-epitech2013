﻿using System.ComponentModel;
using MWMP;
using MWMP.Models;
using MWMP.Models.DAL;
using MWMP.ViewModels;
using System.Windows.Input;
using MWMP.Utils;

namespace LibraryViewModel
{
    class GlobalLibrary : IGlobalLibrary
    {
        #region Properties
        public IMusicLibrary MusicLibrary { get; private set; }
        public ILibrary<IVideoMedia> VideoLibrary { get; private set; }
        public ILibrary<IImageMedia> ImageLibrary { get; private set; }
        public ILibrary<IPlayList> PlayListLibrary { get; private set; }
        #endregion

        #region CTor
        public GlobalLibrary()
        {
            MusicLibrary = ModuleManager.GetInstanceOf<IMusicLibrary>("MusicLibrary");
            VideoLibrary = ModuleManager.GetInstanceOf<ILibrary<IVideoMedia>>("VideoLibrary");
            ImageLibrary = ModuleManager.GetInstanceOf<ILibrary<IImageMedia>>("ImageLibrary");
            PlayListLibrary = ModuleManager.GetInstanceOf<ILibrary<IPlayList>>("PlayListLibrary");

            CreatePlaylist = new RelayCommand((param) => CreatePlaylistBody(param as string));
            OpenPlayListWindow = new RelayCommand((param) =>
                {
                    CreatePlayListWindows window = new CreatePlayListWindows();
                    window.Show();
                    window.Activate();
                });

            IDAL dal = ModuleManager.GetInstanceOf<IDAL>("XMLDAL");
            if (dal != null)
            {
                foreach (IMusicMedia media in dal.MusicList)
                    MusicLibrary.MediaList.Add(media);
                foreach (IVideoMedia media in dal.VideoList)
                    VideoLibrary.MediaList.Add(media); 
                foreach (IImageMedia media in dal.ImageList)
                    ImageLibrary.MediaList.Add(media);
                foreach (IPlayList plist in dal.PlayListList)
                    PlayListLibrary.MediaList.Add(plist);
            }
        }
        #endregion

        #region DTor
        ~GlobalLibrary()
        {
            IDAL dal = ModuleManager.GetInstanceOf<IDAL>("XMLDAL");
            if (dal != null)
            {
                foreach (IMusicMedia media in MusicLibrary.MediaList)
                    dal.Save(media, "audio");
                foreach (IVideoMedia media in VideoLibrary.MediaList)
                    dal.Save(media, "video");
                foreach (IImageMedia media in ImageLibrary.MediaList)
                    dal.Save(media, "image");
                foreach (IPlayList plist in PlayListLibrary.MediaList)
                    dal.Save(plist);
                dal.Save();
            }
        }
        #endregion

        #region Method
        public void AddMedia(IMedia media)
        {
            IMusicMedia musicMedia = media as IMusicMedia;
            IVideoMedia videoMedia = media as IVideoMedia;
            IImageMedia imageMedia = media as IImageMedia;

            if (musicMedia != null) MusicLibrary.Add(musicMedia);
            else if (videoMedia != null) VideoLibrary.Add(videoMedia);
            else if (imageMedia != null) ImageLibrary.Add(imageMedia);
        }

        public ICommand CreatePlaylist { get; private set; }
        public ICommand OpenPlayListWindow { get; private set; }

        private void CreatePlaylistBody(string name)
        {
            if (name == null)
                return;
            IPlayList plist = ModuleManager.GetInstanceOf<IPlayList>("PlayList");
            if (plist == null)
                return;
            plist.Title = name;
            PlayListLibrary.Add(plist);
        }
        #endregion
    }
}
