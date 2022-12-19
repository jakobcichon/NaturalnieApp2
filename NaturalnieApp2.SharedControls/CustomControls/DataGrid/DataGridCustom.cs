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

       

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
/*            base.OnItemsSourceChanged(oldValue, newValue);

            if (newValue is null) 
            {
                return;
            }
 
            Columns.Clear();

            Type modelType = GetEnumerableType(newValue.GetType());
            Collection<DataGridColumn> columns = DataGrid.GenerateColumns(new ItemPropertiesClass(modelType));

            foreach ( var column in columns) 
            {
                string headerAsString = column.Header as string;
                var prop = modelType.GetProperty(headerAsString);
                string displayableName = prop.GetDisplayableName() ?? prop.Name;
                column.Header = displayableName;
            }*/

        }

        private static Type GetEnumerableType(Type enumerableType)
        {
            if (enumerableType.IsGenericType)
            {

                return enumerableType.GetGenericArguments()[0];
            }

            return enumerableType;
        }

        private class ItemPropertiesClass : IItemProperties
        {
            public ReadOnlyCollection<ItemPropertyInfo> ItemProperties { get; }

            public ItemPropertiesClass(Type modelType)
            {
                ItemProperties = new(GetItemProperties(modelType));
            }

            private static IList<ItemPropertyInfo> GetItemProperties(Type modelType)
            {
                var props = modelType.GetDisplayableProperties();
                List<ItemPropertyInfo> retList = new();

                foreach (var prop in props)
                {
                    string name = prop.Name;

                    retList.Add(new ItemPropertyInfo(name, prop.PropertyType, prop));
                }

                return retList;
            }
        }

       

    }
}
