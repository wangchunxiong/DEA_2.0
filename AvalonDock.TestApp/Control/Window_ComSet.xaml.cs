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
using static DEA3.BaseEntity; 

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

        public class ProductInfo
        {
            private int ID { get; set; }
            private int PRODUCT_ID { get; set; }
            private string NAME { get; set; } 
        }

        public Window_ComSet(ComEntity comNode)
        {
            
            InitializeComponent();
            _comNode = comNode;
            is_main = _comNode.IS_MAIN;
            List<DEA3.Data.GetInfoService.ProductInfo> _p_list = new List<DEA3.Data.GetInfoService.ProductInfo>();
            List<string> _result = new List<string>();
            Dictionary<int, String> a = new Dictionary<int, string>();
             ComboBoxItem item = new ComboBoxItem();
            item.Content = _comNode.NAME;
            grid_com_name.Items.Add(item);
            grid_com_name.SelectedIndex = 0;

            if (_comNode.CURRENT_STAT == CurrentOpCType.Modify)
            {  
                grid_fault_site.Text = _comNode.SITE_ADD;
                grid_fault_stop.Text = _comNode.STOP_ADD;
                grid_spd.SelectedIndex 
                    = getDeviceInfo.GetSpdList().IndexOf(_comNode.SPD.ToString()); //_comNode.SPD.ToString();
                grid_bit.SelectedIndex
                    = getDeviceInfo.GetBitList().IndexOf(_comNode.BIT.ToString()); //_comNode.BIT.ToString();
                grid_sync_bit.SelectedIndex
                    = getDeviceInfo.GetSyncBitList().Keys.ToList<int>().IndexOf(_comNode.SYNC_BIT);//_comNode.SYNC_BIT.ToString();
                grid_stop_bit.SelectedIndex
                    = getDeviceInfo.GetStopBitList().IndexOf(_comNode.STOP_BIT.ToString());//_comNode.STOP_BIT.ToString();

                //填充产品列表
                DataTable _dtt2 = new DataTable();
                _dtt2 = getDeviceInfo.GetProductListForDt(1);

                string str = "";
                foreach (var _var in _dtt2.AsEnumerable())
                {
                    str += _var.Field<string>("NAME");
                }
                //MessageBox.Show("填充产品列表TB内容:"+str);

                InitProdcutInfo(_dtt2);
                //MessageBox.Show("grid_protocol:"+_dt.Rows.Count);
                List<string> _list1 = new List<string>();
                if (is_main)
                { 
                    foreach (var i in _dtt2.AsEnumerable())
                    {
                        if (i.Field<int>("IS_MAIN") == 1)
                            _list1.Add(i.Field<string>("NAME"));
                    } 
                }
                else
                {
                    foreach (var i in _dtt2.AsEnumerable())
                    {
                        if (i.Field<int>("IS_MAIN") != 1)
                            _list1.Add(i.Field<string>("NAME"));
                    }
                }
                grid_protocol.SelectedIndex = _list1.IndexOf(_comNode.PRODUCTNAME);
                
                //MessageBox.Show("product_Id:"+grid_protocol.SelectedValue.ToString());
                //MessageBox.Show(((ComboBoxItem)grid_protocol.SelectedItem).Content.ToString());

                _list1.Clear();

                ////填充协议列表
                DataTable _dtt3 = new DataTable();
                int _i = Convert.ToInt32(grid_protocol.SelectedValue);
                _dtt3 = getDeviceInfo.GetProductProtocolList(1);
                //MessageBox.Show("grid_protocol_device:" + _dt.Rows.Count);
                this.grid_protocol_device.ItemsSource = from dr in _dtt3.AsEnumerable()
                                                        where dr.Field<int>("PRODUCT_ID") == _i
                                                        select new ProtocolClass()
                                                        {
                                                            ID = int.Parse(dr["ID"].ToString()),
                                                            PRODUCT_ID = int.Parse(dr["PRODUCT_ID"].ToString()),
                                                            NAME = dr["NAME"].ToString()
                                                        };

                 
                    foreach (var i in _dtt3.AsEnumerable())
                    {
                        if (i.Field<int>("PRODUCT_ID") == _i)
                            _list1.Add(i.Field<string>("NAME"));
                    } 

                  
                grid_protocol_device.SelectedIndex = _list1.IndexOf(_comNode.PROTOCOL);

                _list1.Clear();
                //grid_protocol_device.SelectedValue = _comNode.PROTOCOL_ID;

                //MessageBox.Show("grid_protocol_device_id:" + grid_protocol_device.SelectedValue.ToString());


                if (_comNode.IS_MAIN) grid_is_main.SelectedIndex = 0;
            } 
            
        }

        private void is_ok_Click(object sender, RoutedEventArgs e)
        {
            _deaNode = _comNode.Parent as DeaEntity;
            if (!Get_Exists_Main(_comNode)||_comNode.CURRENT_STAT == CurrentOpCType.Modify)
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
                                    //_deaNode.MAIN_EXISTS = 1;
                                    if (_comNode.NAME.IndexOf("[主站]") <= 0)
                                        _comNode.NAME = _comNode.NAME+"[主站]" ;
                                    //_comNode.IS_MAIN = true;
                                }
                                else
                                { 
                                    if(_comNode.NAME.IndexOf("[主站]")>0)
                                        _comNode.NAME =  _comNode.NAME.Replace("[主站]", "");
                                }
                                   
                            this.Close(); 
                        }
                        else
                        {
                            MessageBox.Show("端口设置信息不完整!", "提示");
                        }
            }
            else if(_comNode.CURRENT_STAT == CurrentOpCType.Add && grid_is_main.SelectedIndex != 0)
            {
                if (grid_fault_site.Text != null &&
                   grid_fault_stop.Text != null &&
                   grid_spd.SelectedValue != null &&
                   grid_bit.SelectedValue != null &&
                   grid_sync_bit.SelectedValue != null &&
                   grid_stop_bit.SelectedValue != null &&
                   grid_protocol.SelectedValue != null &&
                   grid_protocol_device.SelectedValue != null)
                {
                    _comNode.SITE_ADD = grid_fault_site.Text;
                    _comNode.STOP_ADD = grid_fault_stop.Text;
                    _comNode.SPD = Convert.ToInt32(grid_spd.SelectedValue.ToString());
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
                        _comNode.NAME = _comNode.NAME + "[主站]";
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

        public bool Get_Exists_Main(ComEntity currentNode)
        {
            bool is_Exists_main = false; 
            DeaEntity _deaNode = new DeaEntity();
            ComEntity _comNodes = new ComEntity(); 
            _comNodes = (ComEntity)currentNode;
            _deaNode = (DeaEntity)_comNodes.Parent;
            foreach (var item in _deaNode.Children)
            {
                if (item.IS_MAIN) 
                    is_Exists_main = true; 
            } 
            return is_Exists_main;
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

        /// <summary>  
        ///生成COM口中协议列表 
        /// </summary>
        private void grid_protocol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
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
            //DataTable _dtt1 = new DataTable();
            //if (grid_is_main.SelectedValue != null)
            //{
            //    is_main = (bool)grid_is_main.SelectedValue;
            //    _dtt1 = getDeviceInfo.GetProductListForDt(1);
            //    InitProdcutInfo(_dtt1);
            //    MessageBox.Show("产品列表框初始化加载方法!说明是主站."+ grid_is_main.SelectedValue);
            //} 

        }
         

        private void grid_protocol_DropDownOpened(object sender, EventArgs e)
        {
            if (_comNode.ChildrenCount > 0)
                MessageBox.Show("COM口下有设备,不能修改协议!", "提示");
            else
            {
                DataTable _dtt = new DataTable();
                _dtt = getDeviceInfo.GetProductListForDt(1);
                InitProdcutInfo(_dtt);
            }
        }
        /// <summary>  
        ///生成COM口中协议产品列表  
        /// </summary>
        private void InitProdcutInfo(DataTable dt)
        {
            if (is_main)
            {
                this.grid_protocol.ItemsSource = from dr in dt.AsEnumerable()
                                                 where dr.Field<int>("IS_MAIN") == 1
                                                 select new ProtocolClass()
                                                 {
                                                     ID = int.Parse(dr["ID"].ToString()),
                                                     NAME = dr["NAME"].ToString()
                                                 };
            }
            else
            {
                this.grid_protocol.ItemsSource = from dr in dt.AsEnumerable()
                                                 where dr.Field<int>("IS_MAIN") != 1
                                                 select new ProtocolClass()
                                                 {
                                                     ID = int.Parse(dr["ID"].ToString()),
                                                     NAME = dr["NAME"].ToString()
                                                 };
            }
        }

        private void grid_is_main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((bool)((ComboBox)sender).SelectedValue) 
                is_main = true; 
            else 
                is_main = false; 
        }

        private void grid_is_main_DropDownClosed(object sender, EventArgs e)
        {
            
        }

        private void grid_is_main_DropDownOpened(object sender, EventArgs e)
        {
            if (_comNode.ChildrenCount>0)
                MessageBox.Show("当前COM口下有设备设置,不能修改协议!", "提示");
            else
                grid_protocol.SelectedIndex = -1;
        }

        private void grid_protocol_GotFocus(object sender, RoutedEventArgs e)
        {
            //grid_protocol.IsEditable = false;
        }

        private void grid_protocol_device_GotFocus(object sender, RoutedEventArgs e)
        {
           //grid_protocol_device.IsEditable = false;
        }

        private void grid_protocol_device_Loaded(object sender, RoutedEventArgs e)
        { 
            //grid_protocol_device.IsEditable = true;
        }

        private void grid_protocol_device_DropDownOpened(object sender, EventArgs e)
        {
            if (_comNode.ChildrenCount > 0)
                MessageBox.Show("当前COM口下有设备设置,不能修改协议!", "提示");
           
        }
    }
}
