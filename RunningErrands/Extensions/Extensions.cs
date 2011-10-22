using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using RunningErrands.Model;

namespace RunningErrands.Extensions
{
    public static class Extensions
    {
        public static void Save(this ListItem listItem)
        {
            int currentIndex = (Application.Current as App).Database.Query<ListItem, int>().Count();
            if (listItem.Key == -1)
            {
                listItem.Key = currentIndex;
            }
            
            (Application.Current as App).Database.Save(listItem);
            (App.Current as App).Database.Flush();

        }

        public static IEnumerable<ListItem> Load(this IEnumerable<ListItem> listItem)
        {

            var list = (Application.Current as App).Database.Query<ListItem, int>();
            return new ObservableCollection<ListItem>(list.Select(item => item.LazyValue.Value).ToList());
        }
    }
}
