using System.Threading.Tasks;
using System.Windows.Input;

using AppStudio.Data;
using AppStudio.Services;

namespace AppStudio
{
    public class MainViewModels : BindableBase
    {
       private GotKernelViewModel _gotKernelModel;
       private KernelSearchViewModel _kernelSearchModel;

        private ViewModelBase _selectedItem = null;

        public MainViewModels()
        {
            _selectedItem = GotKernelModel;
        }
 
        public GotKernelViewModel GotKernelModel
        {
            get { return _gotKernelModel ?? (_gotKernelModel = new GotKernelViewModel()); }
        }
 
        public KernelSearchViewModel KernelSearchModel
        {
            get { return _kernelSearchModel ?? (_kernelSearchModel = new KernelSearchViewModel()); }
        }

        public void SetViewType(ViewTypes viewType)
        {
            GotKernelModel.ViewType = viewType;
            KernelSearchModel.ViewType = viewType;
        }

        public ViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                UpdateAppBar();
            }
        }

        public bool IsAppBarVisible
        {
            get
            {
                if (SelectedItem == null || SelectedItem == GotKernelModel)
                {
                    return true;
                }
                return SelectedItem != null ? SelectedItem.IsAppBarVisible : false;
            }
        }

        public bool IsLockScreenVisible
        {
            get { return SelectedItem == null || SelectedItem == GotKernelModel; }
        }

        public bool IsAboutVisible
        {
            get { return SelectedItem == null || SelectedItem == GotKernelModel; }
        }

        public void UpdateAppBar()
        {
            OnPropertyChanged("IsAppBarVisible");
            OnPropertyChanged("IsLockScreenVisible");
            OnPropertyChanged("IsAboutVisible");
        }

        /// <summary>
        /// Load ViewModel items asynchronous
        /// </summary>
        public async Task LoadData(bool isNetworkAvailable)
        {
            var loadTasks = new Task[]
            { 
                GotKernelModel.LoadItems(isNetworkAvailable),
                KernelSearchModel.LoadItems(isNetworkAvailable),
            };
            await Task.WhenAll(loadTasks);
        }

        //
        //  ViewModel command implementation
        //
        public ICommand AboutCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("AboutThisAppPage");
                });
            }
        }

        public ICommand LockScreenCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    LockScreenServices.SetLockScreen("/Assets/LockScreenImage.png");
                });
            }
        }
    }
}
