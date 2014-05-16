using System;
using System.Windows;
using System.Windows.Input;

using AppStudio.Services;

namespace AppStudio.Data
{
    public class KernelSearchViewModel : ViewModelBase<BingSchema>
    {
        override protected string CacheKey
        {
            get { return "KernelSearchDataSource"; }
        }

        override protected IDataSource<BingSchema> CreateDataSource()
        {
            return new KernelSearchDataSource(); // BingDataSource
        }

        override public bool IsRefreshVisible
        {
            get { return ViewType == ViewTypes.List; }
        }

        override protected void NavigateToSelectedItem()
        {
            var currentItem = GetCurrentItem();
            if (currentItem != null)
            {
                NavigationServices.NavigateTo(new Uri(currentItem.GetValue("Link")));
            }
        }
    }
}
