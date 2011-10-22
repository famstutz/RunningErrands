using RunningErrands.ViewModels;
using System.ComponentModel;

namespace RunningErrands.ViewModelLocator
{
    namespace Framework.Implementors.Silverlight.MVVM
    {
        public  class MainPageViewModelLocator 
        {
            private static bool? isInDesignMode;
            private MainPageViewModel runtimeViewModel;
            private MainPageViewModel designtimeViewModel;

            /// <summary>
            /// Gets a value indicating whether the control is in design mode
            /// (running in Blend or Visual Studio).
            /// </summary>
            public static bool IsInDesignMode
            {
                get
                {
                    if (!isInDesignMode.HasValue)
                    {
                        isInDesignMode = DesignerProperties.IsInDesignTool;
                    }

                    return isInDesignMode.Value;
                }
            }

            /// <summary>
            /// Holds the intance of the runtime version of the ViewModel that is instantiated only when application is really running by retrieving the instance from IOC container
            /// </summary>
            protected MainPageViewModel RuntimeViewModel
            {
                get
                {
                    // only allow a single instance of the viewmodel constructed per view
                    if (this.runtimeViewModel == null)
                    {
                        
                        this.RuntimeViewModel = new MainPageViewModel();
                    }
                    return runtimeViewModel;
                }

                set
                {
                    runtimeViewModel = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("ViewModel"));
                }
            }

            /// <summary>
            /// Gets current ViewModel instance so if we are in designer its <see cref="DesigntimeViewModel"/> and if its runtime then its <see cref="RuntimeViewModel"/>.
            /// </summary>
            public MainPageViewModel ViewModel
            {
                get
                {
                    return IsInDesignMode ? this.DesigntimeViewModel : this.RuntimeViewModel;
                }
            }

            /// <summary>
            /// Holds the intance of the designtime version of the ViewModel that is instantiated only when application is opened in IDE designer (VisualStudio, Blend etc).
            /// </summary>
            public MainPageViewModel DesigntimeViewModel
            {
                get
                {
                    // in our case, the design time view model is the same class as the 
                    // runtime view model
                    if (this.designtimeViewModel == null)
                    {
                        designtimeViewModel = new MainPageViewModel();
                    }

                    return designtimeViewModel;
                }

                set
                {
                    designtimeViewModel = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("ViewModel"));
                }
            }

            public event PropertyChangedEventHandler PropertyChanged = delegate { };
        }
    }

}
