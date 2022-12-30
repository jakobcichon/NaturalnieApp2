namespace NaturalnieApp2.SharedControls.CustomControls.DataGrid
{
    using NaturalnieApp2.Common.Extension_Methods;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;


    public class DataGridCustom : DataGrid
    {
        static DataGridCustom()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataGridCustom), new FrameworkPropertyMetadata(typeof(DataGridCustom)));
        }

        public DataGridCustom()
        {
            base.AutoGeneratingColumn += DataGridCustom_AutoGeneratingColumn;
            DataGridRow row = new DataGridRow();
        }



        #region Dependecy properties
        public Visibility SettingsPopupVisibility
        {
            get { return (Visibility)GetValue(SettingsPopupVisibilityProperty); }
            set { SetValue(SettingsPopupVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SettingsPopupVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SettingsPopupVisibilityProperty =
            DependencyProperty.Register("SettingsPopupVisibility", typeof(Visibility), typeof(DataGridCustom), new PropertyMetadata(Visibility.Hidden)); 
        #endregion



        private void DataGridCustom_AutoGeneratingColumn(object? sender, DataGridAutoGeneratingColumnEventArgs e)
        {       
            if (e.PropertyDescriptor is null)
            {
                return;
            }


            if (e.PropertyDescriptor is not PropertyDescriptor descriptor)
            {
                return;
            }

            List<PropertyInfo> displayProps = descriptor.ComponentType.GetDisplayableProperties();
            if (displayProps == null) 
            {
                return;
            }

            PropertyInfo? prop = displayProps.Where(p => p.Name.Equals(e.PropertyName)).FirstOrDefault();

            if(prop is null) 
            {
                e.Cancel = true;
                return;
            }

            e.Column.Header = prop.GetDisplayableName() ?? prop.Name;
            e.Column.IsReadOnly = prop.IsReadOnly();
        }
    }
}
