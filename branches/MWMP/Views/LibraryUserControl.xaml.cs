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
	/// Interaction logic for LibraryUserControl.xaml
	/// </summary>
	public partial class LibraryUserControl : UserControl
	{
		public LibraryUserControl()
		{
			this.InitializeComponent();
            DataContext = ModuleManager.getInstanceOf<ILibraryViewModel>("LibraryViewModel");
		}
	}
}