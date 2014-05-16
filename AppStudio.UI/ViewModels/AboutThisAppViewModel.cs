using System;
using System.Xml.Linq;
using System.Windows.Input;

using AppStudio.Services;

namespace AppStudio.Data
{
    public class AboutThisAppViewModel
    {
        static private AboutThisAppViewModel _current = null;
        static private ShareServices _shareService = new ShareServices();

        static public AboutThisAppViewModel Current
        {
            get { return _current ?? (_current = new AboutThisAppViewModel()); }
        }

        public string DeveloperName
        {
            get
            {
                return XDocument.Load("WMAppManifest.xml").Root.Element("App").Attribute("Author").Value;
            }
        }

        public string AppVersion
        {
            get
            {
                return XDocument.Load("WMAppManifest.xml").Root.Element("App").Attribute("Version").Value;
            }
        }

        public string AboutText
        {
            get
            {
                return "Simply a front end for the \"Got Kernel?\" blog on TechNet.";
            }
        }

        public ICommand ShareAppCommand
        {
            get
            {
                return new RelayCommand<string>((src) =>
                {
                    string appUri = String.Format("http://xap.winappstudio.com/Job/GetXap?ticket={0}", "10868.vamijf");
                    if (_shareService.AppExistInMarketPlace())
                    {
                        appUri = Windows.ApplicationModel.Store.CurrentApp.LinkUri.AbsoluteUri;
                    }
                    _shareService.Share("app", "message", appUri, string.Empty);
                });
            }
        }
    }
}
