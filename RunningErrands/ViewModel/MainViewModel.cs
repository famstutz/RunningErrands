using GalaSoft.MvvmLight;
using RunningErrands.Model;
using System.Collections.ObjectModel;

namespace RunningErrands.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public string ApplicationTitle
        {
            get
            {
                return "Running Errands".ToUpper();
            }
        }

        public string PageName
        {
            get
            {
                return "My page:";
            }
        }

        public string Welcome
        {
            get
            {
                return "Welcome to MVVM Light";
            }
        }

        /// <summary>
        /// The <see cref="Shops" /> property's name.
        /// </summary>
        public const string ShopsPropertyName = "Shops";

        private ObservableCollection<Shop> _shops = new ObservableCollection<Shop>();

        public ObservableCollection<Shop> Shops
        {
            get
            {
                return _shops;
            }

            set
            {
                if (_shops == value)
                {
                    return;
                }

                var oldValue = _shops;
                _shops = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ShopsPropertyName);

            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                Shops.Add(new Shop() { Name = "Manor", Tasks = new ObservableCollection<Task>() {
                    new Task() { Description = "bla", IsDone = false},
                    new Task() { Description = "bla", IsDone = true},
                    new Task() { Description = "bla", IsDone = false}
                }});
                Shops.Add(new Shop() { Name = "Coop" });
                Shops.Add(new Shop() { Name = "Migros" });

                // Code runs "for real"
            }
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}