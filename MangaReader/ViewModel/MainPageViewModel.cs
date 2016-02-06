using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace MangaReader.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        private ICommand _catalogClick;
        private ICommand _hamburgerCommand;
        private bool _isOpenPanel;

        public String Test { get; set; }
        public MainPageViewModel()
        {
            IsOpenPanel = true;
            Test = "123123123";
        }
        public bool IsOpenPanel
        {
            get { return _isOpenPanel; }
            set
            {
                _isOpenPanel = value;
                RaisePropertyChanged(()=>IsOpenPanel);
            }
        }
        public ICommand CatalogClick
        {
            get
            {
                return _catalogClick ?? (_catalogClick = new RelayCommand(() =>
                {
                    new MessageDialog("").ShowAsync().GetResults();

                }));               
            }
        }

        public ICommand HamburgerCommand
        {
            get
            {
                return _hamburgerCommand ?? (_hamburgerCommand = new RelayCommand(() =>
                {
                    IsOpenPanel = !IsOpenPanel;

                }));
            }
        }
    }
}
