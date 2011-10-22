using GalaSoft.MvvmLight;
using RunningErrands.Extensions;
using RunningErrands.Model;

namespace RunningErrands.ViewModels
{
    public class ListItemViewModel : ViewModelBase
    {
        public ListItemViewModel()
        {
            _listItem = new ListItem();
        }

        private readonly ListItem _listItem;

        public ListItemViewModel(ListItem model)
        {
            _listItem = model;
        }

        public bool IsFinished
        {
            get { return _listItem.IsFinished; }
            set
            {
                _listItem.IsFinished = value;
                // check state changed, save ListItem model to isolated storage
                _listItem.Save();
                RaisePropertyChanged("IsFinished");
            }
        }


        public string Text
        {
            get { return _listItem.Text; }
            set
            {
                _listItem.Text = value;
                // text changed, save ListItem model to isolated storage
                _listItem.Save();
                RaisePropertyChanged("Text");
            }
        }
    }
}