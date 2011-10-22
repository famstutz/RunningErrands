using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RunningErrands.Model
{
    public class Shop
    {
        public string Name { get; set; }
        public ObservableCollection<Task> Tasks { get; set; }

        public Shop()
        {
            Tasks = new ObservableCollection<Task>();
        }
    }
}
