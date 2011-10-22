using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using RunningErrands.Extensions;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Controls.Shell
{
	[ContentProperty("Buttons")]
	public class BindableApplicationBar : ItemsControl, IApplicationBar
	{
		private ApplicationBar _applicationBar;
        private ApplicationBarMode _mode;

		public BindableApplicationBar()
		{
			_applicationBar = new ApplicationBar();
			
			this.Loaded += new RoutedEventHandler(BindableApplicationBar_Loaded);
		}

		private void SetApplicationBar(IApplicationBar bar)
		{
			var page = this.GetVisualAncestors().Where(c => c is PhoneApplicationPage).LastOrDefault() as PhoneApplicationPage;
			if (page != null)
				page.ApplicationBar = bar;
			
			if (bar != null)
				page.BackKeyPress += new EventHandler<System.ComponentModel.CancelEventArgs>(page_BackKeyPress);
			else
				page.BackKeyPress -= page_BackKeyPress;
		}

		void page_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
		{
			SetApplicationBar(null);
		}

		void BindableApplicationBar_Loaded(object sender, RoutedEventArgs e)
		{
			SetApplicationBar(_applicationBar);
		}

		protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			base.OnItemsChanged(e);
			_applicationBar.Buttons.Clear();
			_applicationBar.MenuItems.Clear();
			
			foreach (BindableApplicationBarIconButton button in Items.Where(c => c is BindableApplicationBarIconButton))
				_applicationBar.Buttons.Add(button.Button);

			foreach (BindableApplicationBarMenuItem button in Items.Where(c => c is BindableApplicationBarMenuItem))
				_applicationBar.MenuItems.Add(button.Item);
		}

		public static readonly DependencyProperty IsVisibleProperty =
				DependencyProperty.RegisterAttached("IsVisible", typeof(bool), typeof(BindableApplicationBar), new PropertyMetadata(true, OnVisibleChanged));

		private static void OnVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue != e.OldValue)
				((BindableApplicationBar)d)._applicationBar.IsVisible = (bool)e.NewValue;
		}

		public static readonly DependencyProperty IsMenuEnabledProperty =
			 DependencyProperty.RegisterAttached("IsMenuEnabled", typeof(bool), typeof(BindableApplicationBar), new PropertyMetadata(true, OnEnabledChanged));

		private static void OnEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue != e.OldValue)
				((BindableApplicationBar)d)._applicationBar.IsMenuEnabled = (bool)e.NewValue;
		}

		public bool IsVisible
		{
			get
			{
				return (bool)GetValue(IsVisibleProperty);
			}
			set
			{
				SetValue(IsVisibleProperty, value);
			}
		}

		public double BarOpacity
		{
			get
			{
				return _applicationBar.Opacity;
			}
			set
			{
				_applicationBar.Opacity = value;
			}
		}

		public bool IsMenuEnabled
		{
			get
			{
				return (bool)GetValue(IsMenuEnabledProperty);
			}
			set
			{
				SetValue(IsMenuEnabledProperty, value);
			}
		}

		public Color BackgroundColor
		{
			get
			{
				return _applicationBar.BackgroundColor;
			}
			set
			{
				_applicationBar.BackgroundColor = value;
			}
		}

		public Color ForegroundColor
		{
			get
			{
				return _applicationBar.ForegroundColor;
			}
			set
			{
				_applicationBar.ForegroundColor = value;
			}
		}

		public IList Buttons
		{
			get
			{
				return this.Items;
			}

		}

		public IList MenuItems
		{
			get
			{
				return this.Items;
			}
		}

        public ApplicationBarMode Mode
        {
            get
            {
                return this._mode;
            }
            set
            {
                this._mode = value;
            }
        }

		public event EventHandler<ApplicationBarStateChangedEventArgs> StateChanged;


        public double DefaultSize
        {
            get { throw new NotImplementedException(); }
        }

        public double MiniSize
        {
            get { throw new NotImplementedException(); }
        }
    }
}