﻿using System;
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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace rMind.DriverForge.Views
{
    using rMind.Driver;
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class DriverView : Page
    {
        Driver current;

        public DriverView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            current = e.Parameter as Driver;
            DataContext = current;

            driverContent.Navigate(typeof(DriverViewMain), current);
        }

        private void OnChangeView(object sender, SelectionChangedEventArgs e)
        {
            var page = ((string)((sender as ListBox)?.SelectedItem as ListBoxItem)?.Content).ToUpper();

            if (page == "MAIN") driverContent.Navigate(typeof(DriverViewMain), current);
            if (page == "METHODS") driverContent.Navigate(typeof(DriverViewMethods), current);
        }
    }
}
