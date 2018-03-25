using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Autofac;
using KP.Import.WPApp.Common;
using KP.Import.WPApp.Pages.Appartment;
using KP.Import.WPApp.Pages.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace KP.Import.WPApp.Pages.Start
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartPage : Page
    {
        private readonly NavigationHelper _navigationHelper;

        public StartPage()
        {
            ViewModel = App.Container.Resolve<StartViewModel>();
            ViewModel.GoToAppartment += ViewModel_OnGoToAppartment;

            InitializeComponent();

            _navigationHelper = new NavigationHelper(this);
        }

        public StartViewModel ViewModel { get; set; }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);
        }

        private void ViewModel_OnGoToAppartment(object sender, SelectAppartmentArgs e)
        {
            Frame.Navigate(typeof (AppartmentPage), e);
        }
    }
}
