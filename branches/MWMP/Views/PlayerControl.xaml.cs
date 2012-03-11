﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MWMP.ViewModels;

namespace MWMP
{
	/// <summary>
	/// Interaction logic for PlayerControl.xaml
	/// </summary>
	public partial class PlayerControl : UserControl
	{
		public PlayerControl()
		{
			this.InitializeComponent();
            IMusicPlayerVM player = ModuleManager.getInstanceOf<IMusicPlayerVM>("MusicPlayerViewModel");
            if (player != null)
            {
                player.MediaElement = this.MediaPlayer;
                DataContext = player;
            }
		}

        private void VolumeChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider me = sender as Slider;
            ModuleManager.getInstanceOf<IMusicPlayerVM>("MusicPlayerViewModel").Volume = e.NewValue / me.Maximum;
        }
	}
}