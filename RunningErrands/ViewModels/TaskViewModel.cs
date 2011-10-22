using GalaSoft.MvvmLight;
using RunningErrands.Extensions;
using RunningErrands.Model;

namespace RunningErrands.ViewModels
{
    public class TaskViewModel : ViewModelBase
    {
        public TaskViewModel()
        {
            _task = new Task();
        }

        private readonly Task _task;

        public TaskViewModel(Task model)
        {
            _task = model;
        }

        public bool IsDone
        {
            get { return _task.IsDone; }
            set
            {
                _task.IsDone = value;
                // check state changed, save Task model to isolated storage
                _task.Save();
                RaisePropertyChanged("IsDone");
            }
        }


        public string Text
        {
            get { return _task.Text; }
            set
            {
                _task.Text = value;
                // text changed, save Task model to isolated storage
                _task.Save();
                RaisePropertyChanged("Text");
            }
        }
    }
}