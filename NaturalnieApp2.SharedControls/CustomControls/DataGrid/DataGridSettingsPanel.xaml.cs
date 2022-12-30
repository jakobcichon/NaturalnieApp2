namespace NaturalnieApp2.SharedControls.CustomControls.DataGrid
{
    using NaturalnieApp2.Common.Binding;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    public class ColumnAndVisibility: BindableBase
    {
        private DataGridColumn dataColumn;
        private bool visibility;


        public ColumnAndVisibility(DataGridColumn dataColumn, bool visibility)
        {
            DataColumn = dataColumn;
            Visibility = visibility;
        }

        public DataGridColumn DataColumn
        {
            get { return dataColumn; }
            set { SetProperty(ref dataColumn, value); }
        }

        public bool Visibility
        {
            get { return visibility; }
            set { SetProperty(ref visibility, value); }
        }
    }

    /// <summary>
    /// Interaction logic for DataGridSettingsPanel.xaml
    /// </summary>
    public partial class DataGridSettingsPanel : UserControl
    {
        public DataGridSettingsPanel()
        {
            InitializeComponent();
            AllColumns = new();
        }

        public ObservableCollection<ColumnAndVisibility> AllColumns
        {
            get { return (ObservableCollection<ColumnAndVisibility>)GetValue(AllColumnsProperty); }
            set { SetValue(AllColumnsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllColumns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllColumnsProperty =
            DependencyProperty.Register("AllColumns", typeof(ObservableCollection<ColumnAndVisibility>), typeof(DataGridSettingsPanel), new PropertyMetadata(null));

        public DataGridCustom DataGridInstance
        {
            get { return (DataGridCustom)GetValue(DataGridInstanceProperty); }
            set { SetValue(DataGridInstanceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataGridInstance.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataGridInstanceProperty =
            DependencyProperty.Register("DataGridInstance", typeof(DataGridCustom), typeof(DataGridSettingsPanel), new FrameworkPropertyMetadata(null, OnDataGridChangeDp));

        private static void OnDataGridChangeDp(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataGridSettingsPanel? localSender = d as DataGridSettingsPanel;

            if(localSender is null)
            {
                return;
            }

            localSender.OnDataGridChange();
        }

        public void OnDataGridChange()
        {
            if(DataGridInstance is null)
            {
                return;
            }

            foreach (DataGridColumn element in DataGridInstance.Columns)
            {
                AllColumns.Add(new ColumnAndVisibility(element, true));
            }
        }

        private bool IsColumnVisiable(DataGridColumn column)
        {
            return DataGridInstance.Columns.Any(c => c.Equals(column));
        }

        private ColumnAndVisibility FindColumnByHeader(object header)
        {
            return AllColumns.Where(c => c.DataColumn.Header.Equals(header)).FirstOrDefault()!;
        }

        private void Button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(this.SettingsPopup.IsOpen)
            {
                this.SettingsPopup.IsOpen = false;
            }

            this.SettingsPopup.IsOpen = true;
        }

        private void SettingsPopup_MouseLeave(object sender, MouseEventArgs e)
        {
            this.SettingsPopup.IsOpen = false;
        }

        private void ColumnButton_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Button? localSender = sender as Button;
            if (localSender is null)
            {
                return;
            }

            ColumnAndVisibility data = FindColumnByHeader(localSender.Content);
            ToogleColumnVisibility(data);
        }

        private void ToogleColumnVisibility(ColumnAndVisibility columnData)
        {
            if(IsColumnVisiable(columnData.DataColumn))
            {
                HideColumn(columnData);
                return;
            }

            ShowColumn(columnData);
        }

        private void HideColumn(ColumnAndVisibility columnData)
        {
            DataGridInstance.Columns.Remove(columnData.DataColumn);
            columnData.Visibility = false;
        }

        private void ShowColumn(ColumnAndVisibility columnData)
        {
            int index = AllColumns.IndexOf(columnData);
            DataGridInstance.Columns.Insert(index, columnData.DataColumn);
            columnData.Visibility = true;
        }
    }
}
