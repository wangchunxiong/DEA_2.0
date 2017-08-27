using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DEA3.Data;
using System.Data;

namespace DEA3.Control
{
    /// <summary>
    /// Window_ComSet.xaml 的交互逻辑
    /// </summary>
    public partial class Window_ComSet : Window
    {
        public Window_ComSet()
        {
            InitializeComponent();
        }
        private ComEntity _comNode;
        private bool _isExit = false;
        public Window_ComSet(ComEntity comNode)
        {
            InitializeComponent();
            _comNode = comNode; 

            ComboBoxItem item = new ComboBoxItem();
            item.Content = _comNode.NAME;
            grid_com_name.Items.Add(item);
            grid_com_name.SelectedIndex = 0;
        }

        private void is_ok_Click(object sender, RoutedEventArgs e)
        {
            if (grid_fault_site.Text != null &&
                grid_fault_stop.Text != null &&
                grid_spd.SelectedValue != null &&
                grid_bit.SelectedValue != null &&
                grid_sync_bit.SelectedValue != null &&
                grid_stop_bit.SelectedValue != null &&
                grid_protocol.SelectedValue != null &&
                grid_protocol_device.SelectedValue != null )
            { 
                _comNode.SITE_ADD = grid_fault_site.Text;
                _comNode.STOP_ADD = grid_fault_stop.Text;
                _comNode.SPD = grid_spd.SelectedValue.ToString();
                _comNode.BIT = grid_bit.SelectedValue.ToString();
                _comNode.SYNC_BIT = grid_sync_bit.SelectedValue.ToString();
                _comNode.STOP_BIT = grid_stop_bit.SelectedValue.ToString();
                _comNode.PROTOCOL = grid_protocol_device.SelectedValue.ToString();
                _comNode.IS_MAIN = Convert.ToInt32(grid_is_main.SelectedValue);
                this.Close(); 
            }
            else
            {
                MessageBox.Show("端口设置信息不完整!");
            }
        }

        public bool IsExit
        {
            get { return _isExit; } 
        }

        public ComEntity comSetNode
        {
            get { return _comNode; }
            set
            {
                if (!_comNode.Equals(value))
                {
                    _comNode = value;
                    OnChangedProperty("comSetNode");
                }
            }
        }

        #region 实现INotifyPropertyChanged 接口  
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnChangedProperty(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        private void is_cancel_Click(object sender, RoutedEventArgs e)
        {
            _isExit = true;
            this.Close();
        }
  
        private void grid_protocol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dictionary<int, String> _protocol_device = new Dictionary<int, string>();
            GetInfoService getDeviceInfo = new GetInfoService();
            DataTable _dt = new DataTable();
             
            int _key = Convert.ToInt32(((ComboBox)sender).SelectedValue); 

             _dt = getDeviceInfo.GetProtocolDeiveceList(_key);
              
            this.grid_protocol_device.ItemsSource = from dr in _dt.AsEnumerable()
                                                    where dr.Field<int>("ID") == _key
                                                    select new DeviceClass() {
                                                        ID = int.Parse(dr["ID"].ToString()),
                                                        NAME = dr["NAME"].ToString()
                                                    }; 
        }

        public class DeviceClass {
            public int ID { get; set; }
            public string NAME { get; set; }
        }
    }
}
