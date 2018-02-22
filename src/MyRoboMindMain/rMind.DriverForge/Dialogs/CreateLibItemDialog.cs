using rMind.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace rMind.DriverForge.Dialogs
{
    public sealed class CreateLibItemDialog : ContentDialog
    {
        public CreateLibItemDialog()
        {
            this.DefaultStyleKey = typeof(CreateLibItemDialog);
        }

        ToggleSwitch toggleSwitch;
        TextBox ids;
        TextBox name;
        ComboBox templates;

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            toggleSwitch = GetTemplateChild("IsFolderSwitch") as ToggleSwitch;
            ids = GetTemplateChild("IDS") as TextBox;
            name = GetTemplateChild("NAME") as TextBox;

            templates = GetTemplateChild("templates") as ComboBox;

            var parents = new string[] { "NONE" };
            parents = parents.Union(DriverDB.Current().Drivers.Drivers.Select(x => x.IDS)).ToArray();
            
            templates.ItemsSource = parents;

            PrimaryButtonClick += async (s, args) =>
            {
                ContentDialogButtonClickDeferral deferral = args.GetDeferral();

                var exists = DriverDB.Current().Drivers.Drivers.Any(x => x.IDS.ToLower() == (ids?.Text.ToLower() ?? ""));
                if (exists)
                {
                    args.Cancel = true;
                }
                deferral.Complete();
            };
        }

        /// <summary> Create folder </summary>
        public bool IsFolder { get { return !toggleSwitch?.IsOn ?? false; } }

        /// <summary> IDS </summary>
        public string IDS { get { return ids?.Text.ToUpper() ?? ""; } }

        /// <summary> Name </summary>
        public string ElementName { get { return name?.Text ?? ""; } }

        /// <summary> Name </summary>
        public string ParentTemplate { get { return (string)(templates?.SelectedItem ?? ""); } }
    }
}
