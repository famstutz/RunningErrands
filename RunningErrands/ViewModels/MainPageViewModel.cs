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
        private ObservableCollection<TaskViewModel> _tasks;
        public ObservableCollection<TaskViewModel> Tasks
        {
            get { return _tasks; }
            set { _tasks = value; RaisePropertyChanged("Tasks"); }
        }

        public MainPageViewModel()
        {

            AddTaskCommand = new RelayCommand<string>(OnAddTaskCommand);
            Tasks = new ObservableCollection<TaskViewModel>();

            CancelCommand = new RelayCommand(OnCancel);

            DeleteListCommand = new RelayCommand(OnDeleteList);

            OpenTaskDialogCommand = new RelayCommand(OnOpenTaskDialog);

            // if in design view, show two dummy tasks
            if (IsInDesignMode)
            {
                Tasks.Add(new TaskViewModel { IsDone = true, Text = "Take Out Garbage" });
                Tasks.Add(new TaskViewModel { IsDone = false, Text = "Bring in Newspaper" });
            }
            else
            {
                // read in real tasks from a db here
                List<Task> tempTasks = new List<Task>();

                Tasks = new ObservableCollection<TaskViewModel>(tempTasks.Load().Select(model => new TaskViewModel(model)));
            }

        }

        private void OnCancel()
        {
            PromptEntry = false;
        }

        public RelayCommand CancelCommand { get; private set; }

        private void OnDeleteList()
        {
            if (MessageBox.Show("Are you sure you want to delete the list?", "Confirm", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                (App.Current as App).Database.Truncate(typeof(Task));
                Tasks.Clear();
            }
        }

        public RelayCommand DeleteListCommand { get; private set; }

        private void OnOpenTaskDialog()
        {
            PromptEntry = true;
        }

        public RelayCommand<string> AddTaskCommand { get; private set; }
        public RelayCommand OpenTaskDialogCommand { get; private set; }

        private void OnAddTaskCommand(string text)
        {
            var model = new Task() { IsDone = false, Text = text };
            model.Save(); // persist it
            var listItemViewModel = new TaskViewModel(model);
            Tasks.Add(listItemViewModel);
            PromptEntry = false;
        }

        private bool _promptEntry;
        public bool PromptEntry
        {
            get { return _promptEntry; }
            set { _promptEntry = value; RaisePropertyChanged("PromptEntry"); }
        }
    }
}