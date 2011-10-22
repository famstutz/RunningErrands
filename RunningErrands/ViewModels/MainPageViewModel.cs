using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RunningErrands.Extensions;
using RunningErrands.Model;

namespace RunningErrands.ViewModels
{
    public class MainPageViewModel : ViewModelBase 
    {
        // List of Tasks for the day
        private ObservableCollection<ListItemViewModel> _listItems;
        public ObservableCollection<ListItemViewModel> ListItems
        {
            get { return _listItems; }
            set { _listItems = value; RaisePropertyChanged("ListItems"); }
        }

        public MainPageViewModel()
        {

            AddToDoCommand = new RelayCommand<string>(OnAddToDoCommand);
                                 ListItems = new ObservableCollection<ListItemViewModel>();

            CancelCommand = new RelayCommand(OnCancel);

            DeleteListCommand = new RelayCommand(OnDeleteList);

            OpenListItemDialogCommand = new RelayCommand(OnOpenListItemDialog);

            // if in design view, show two dummy tasks
            if (IsInDesignMode)
            {
                ListItems.Add(new ListItemViewModel {IsFinished = true, Text = "Take Out Garbage"});
                ListItems.Add(new ListItemViewModel { IsFinished = false, Text = "Bring in Newspaper" });
            }
            else
            {
                // read in real tasks from a db here
                List<ListItem> tempItems = new List<ListItem>();
                
                ListItems = new ObservableCollection<ListItemViewModel>(tempItems.Load().Select(model => new ListItemViewModel(model)));
            }
            
        }

        private void OnCancel()
        {
            PromptEntry = false;
        }

        public  RelayCommand CancelCommand { get; private set; }

        private void OnDeleteList()
        {
            if (MessageBox.Show("Are you sure you want to delete the list?", "Confirm", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                (App.Current as App).Database.Truncate(typeof(ListItem));
                ListItems.Clear();
            }
        }

        public RelayCommand DeleteListCommand { get; private set; }

        private void OnOpenListItemDialog()
        {
            PromptEntry = true;
        }

        public RelayCommand<string> AddToDoCommand { get; private set; }
        public RelayCommand OpenListItemDialogCommand { get; private set; }

        private void OnAddToDoCommand(string text)
        {
            var model = new ListItem() {IsFinished = false, Text = text};
            model.Save(); // persist it
            var listItemViewModel = new ListItemViewModel(model);
            ListItems.Add(listItemViewModel);
            PromptEntry = false;
        }

        private bool _promptEntry;
        public bool PromptEntry
        {
            get { return _promptEntry; }
            set { _promptEntry = value; RaisePropertyChanged("PromptEntry");}
        }
    }
}