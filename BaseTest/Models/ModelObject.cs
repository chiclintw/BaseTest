﻿using System;
using System.ComponentModel;

namespace BaseTest
{
	public class ModelObject : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		protected void RaisePropertyChanged(string name) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}
	}
}

