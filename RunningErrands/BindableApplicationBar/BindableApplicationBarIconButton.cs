using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

using Microsoft.Phone.Shell;

namespace Controls.Shell
{
	public class BindableApplicationBarIconButton : BindableApplicationBarMenuItem, IApplicationBarIconButton 
	{
		public BindableApplicationBarIconButton():base()
		{
		}

		public ApplicationBarIconButton Button
		{
			get
			{
				return (ApplicationBarIconButton)Item;
			}
		}

		protected override IApplicationBarMenuItem CreateItem()
		{
			return new ApplicationBarIconButton();
		}

		public Uri IconUri
		{
			get
			{
				return Button.IconUri;
			}
			set
			{
				Button.IconUri = value;
			}
		}

	}
}