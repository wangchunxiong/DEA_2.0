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
        private DeaEntity _deaNode;
        private DeviceEntity _deviceNode;
        private bool _isExit = false;
        private GetInfoService getDeviceInfo = new GetInfoService();
        private Dictionary<int, String> _product_info = new Dictionary<int, string>();
        private Dictionary<int, String> _protocol_device = new Dictionary<int, string>();
        private DataTable _dt = new DataTable();

        private bool is_main = false;
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
            _deaNode = _comNode.Parent as DeaEntity;
            if (_deaNode.MAIN_EXISTS != 1 || !is_main)
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
                    _comNode.SPD =Convert.ToInt32(grid_spd.SelectedValue.ToString());
                    _comNode.BIT = Convert.ToInt32(grid_bit.SelectedValue.ToString());
                    _comNode.SYNC_BIT = Convert.ToInt32(grid_sync_bit.SelectedValue.ToString());
                    _comNode.STOP_BIT = Convert.ToInt32(grid_stop_bit.SelectedValue.ToString());
                    _comNode.PROTOCOL = grid_protocol_device.Text;
                    _comNode.PROTOCOL_ID = Convert.ToInt32(grid_protocol_device.SelectedValue);
                    _comNode.IS_MAIN = (bool)grid_is_main.SelectedValue;
                    _comNode.PRODUCTID = Convert.ToInt32(grid_protocol.SelectedValue);
                    _comNode.PRODUCTNAME = grid_protocol.Text;
                            if (is_main)
                                {
                                    _deaNode.MAIN_EXISTS = 1;
                                    _comNode.NAME = _comNode.NAME+" [主站]" ;
                                    _comNode.IS_MAIN = true;
                                }
                                   
                            this.Close(); 
                        }
                        else
                        {
                            MessageBox.Show("端口设置信息不完整!", "提示");
                        }
            }
            else
            {
                MessageBox.Show("同一台DEA只能设置一个COM口为主站!", "提示");
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
            //Dictionary<int, String> _protocol_device = new Dictionary<int, string>();
            //GetInfoService getDeviceInfo = new GetInfoService();
            //DataTable _dt = new DataTable();
             
            int _key = Convert.ToInt32(((ComboBox)sender).SelectedValue); 

             _dt = getDeviceInfo.GetProductProtocolList(_key);
              
            this.grid_protocol_device.ItemsSource = from dr in _dt.AsEnumerable()
                                                    where dr.Field<int>("PRODUCT_ID") == _key
                                                    select new ProtocolClass() {
                                                        ID = int.Parse(dr["ID"].ToString()),
                                                        PRODUCT_ID = int.Parse(dr["PRODUCT_ID"].ToString()),
                                                        NAME = dr["NAME"].ToString()
                                                    }; 
        }

        public class ProtocolClass {
            public int ID { get; set; }
            public int PRODUCT_ID { get; set; }
            public string NAME { get; set; }
        }

        private void grid_protocol_Loaded(object sender, RoutedEventArgs e)
        {
            if (grid_is_main.SelectedValue != null)
            {
                is_main = (bool)grid_is_main.SelectedValue;
                _dt = getDeviceInfo.GetProductListForDt(1);
                InitProdcutInfo();
            }
            
        }

        private void grid_is_main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((bool)grid_is_main.SelectedValue)
            {
                is_main = true;
            }
            else
            {
                is_main = false;
            }
            
        }

        private void grid_protocol_DropDownOpened(object sender, EventArgs e)
        { 
            _dt = getDeviceInfo.GetProductListForDt(1);
            InitProdcutInfo(); 
        }

        private void InitProdcutInfo()
        {
            if (is_main)
            {
                this.grid_protocol.ItemsSource = from dr in _dt.AsEnumerable()
                                                 where dr.Field<int>("IS_MAIN") == 1
                                                 select new ProtocolClass()
                                                 {
                                                     ID = int.Parse(dr["ID"].ToString()),
                                                     NAME = dr["NAME"].ToString()
                                                 };
            }
            else
            {
                this.grid_protocol.ItemsSource = from dr in _dt.AsEnumerable()
                                                 where dr.Field<int>("IS_MAIN") != 1
                                                 select new ProtocolClass()
                                                 {
                                                     ID = int.Parse(dr["ID"].ToString()),
                                                     NAME = dr["NAME"].ToString()
                                                 };
            }
        }

        private void grid_is_main_DropDownClosed(object sender, EventArgs e)
        {
            grid_protocol.SelectedIndex = -1;
        }

        private void grid_is_main_DropDownOpened(object sender, EventArgs e)
        {
          
        }
    }
}
