using DEA3.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using static DEA3.Data.GetInfoService;
using static DEA3.DeviceEntity;

namespace DEA3.Control
{
    /// <summary>
    /// Window_DeviceSet.xaml 的交互逻辑
    /// </summary>
    public partial class Window_DeviceSet : Window
    {
        public Window_DeviceSet()
        {
            InitializeComponent();
        }

        public delegate void MyDelegate(object sender,MyEventArgs e);
        public event MyDelegate MyEvent;

        public class MyEventArgs : EventArgs
        {
            public int _copy_num;
        }

        private int _copy_num;
        ///// <summary>  
        ///// COPY_NUM  
        ///// </summary>  
        public int COPY_NUM
        {
            get { return _copy_num; }
            set
            {
                if (!_copy_num.Equals(value))
                {
                    _copy_num = value;
                    OnChangedProperty("COPY_NUM");
                }
            }
        }


        private ComEntity _comNode;
        /// <summary>
        /// _isExit是否取消新增
        /// </summary>
        private bool _isExit = false;
        private GetInfoService getDeviceInfo = new GetInfoService();
        private DeviceEntity _deviceNode;
        private DeaEntity _deaNode;
        int _protocol_id = 0;
        int _device_id;

        public Window_DeviceSet(DeviceEntity deviceNode)
        {
            InitializeComponent();

            _deviceNode = deviceNode;
            _comNode = _deviceNode.Parent as ComEntity; 
            /// <summary>
            /// 设置站号列表
            /// </summary>
            ComboBoxItem item = new ComboBoxItem();
            item.Content = _deviceNode.SITE_NUMBER;
            cob_site_number.Items.Add(item);
            cob_site_number.SelectedIndex = 0;
            /// <summary>
            /// 设置协议列表
            /// </summary>
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Content = _deviceNode.PROTOCOL;
            cob_protocol.Items.Add(item1);
            cob_protocol.SelectedIndex = 0;

            if (_comNode.IS_MAIN )
            {
                check_copyNum.IsEnabled = false; 
                group_box_add.IsEnabled = false;
            }
            else
            {
                if (_deviceNode.CURRENT_STAT != BaseEntity.CurrentOpCType.Modify)
                    check_copyNum.IsEnabled = true;
                else
                    check_copyNum.IsEnabled = false;
                group_box_add.IsEnabled = true;
            }
            //根据协议内容,填充设备列表
            InitDeviceInfoList(_deviceNode.PROTOCOL);

           
        }

        public void InitDeviceInfoList(string protocol_name) { 
            DataTable _dt_GetProductProtocolList = new DataTable();
            DataTable _dt_GetProductProtocolDeviceList = new DataTable();
            Dictionary<int, String> _dt_GetProductList = new Dictionary<int, String>();
            Dictionary<int, string> _device_name_list = new Dictionary<int, string>();
            //获得所有协议列表
            _dt_GetProductProtocolList = getDeviceInfo.GetProductProtocolList(1);
            //获得当前设备协议
            IEnumerable<DataRow> rows = from dr in _dt_GetProductProtocolList.AsEnumerable()
                                        where dr.Field<string>("NAME").Equals(protocol_name)
                                        select dr;

            foreach (DataRow i in rows)
            {
                _protocol_id = i.Field<int>("ID");
            }

            //获得产品大类对应所有产品列表
            _dt_GetProductProtocolDeviceList = getDeviceInfo.GetProductProtocolDeviceList(1);
            //获得所有产品大类列表
            _dt_GetProductList = getDeviceInfo.GetProductList();
            //获得当前设备协议对应所有产品
            IEnumerable<DataRow> rows2 = from p in _dt_GetProductProtocolDeviceList.AsEnumerable()
                                         where p.Field<int>("PRODUCTPROTOCOL_ID") == _protocol_id
                                         select p;

            string _list_item = "";
            int _product_id;
            string _device_name = "";

            foreach (DataRow item in rows2)
            {
                _product_id = item.Field<int>("PRODUCT_ID");
                _device_name = item.Field<string>("NAME");
                foreach (KeyValuePair<int, string> e in _dt_GetProductList)
                {
                    if (e.Key == _product_id)
                    {
                        _list_item = "[" + e.Value + "]" + "--" + "[" + _device_name + "]";
                    }
                }
                _device_name_list.Add(item.Field<int>("ID"), _list_item);
            }

            //给设备下拉加载所有当前设备支持的设备列表
            cob_device.ItemsSource = _device_name_list;

            
            if (_deviceNode.CURRENT_STAT == BaseEntity.CurrentOpCType.Modify)
            {
                //修改时恢复设备选项
                cob_device.SelectedIndex= _device_name_list.Keys.ToList<int>().IndexOf(_deviceNode.DEVICE_ID_INFO);

                //修改时恢复读取配置
                if (_deviceNode.CHG_DATA_ARRY != null)
                {
                    _device_id = Convert.ToInt32(cob_device.SelectedValue);
                    cob_chg_device_add_0.ItemsSource = GetBaseAddList(_device_id);
                    cob_chg_device_add_0.SelectedIndex = GetBaseAddList(_device_id).IndexOf(_deviceNode.CHG_DATA_ARRY[0]._divece_Add);

                    cob_chg_device_add_1.ItemsSource = GetBaseAddList(_device_id);
                    cob_chg_device_add_1.SelectedIndex = GetBaseAddList(_device_id).IndexOf(_deviceNode.CHG_DATA_ARRY[1]._divece_Add);

                    cob_chg_plc_add_0.ItemsSource = GetBaseAddList((_comNode.Parent as DeaEntity).MAIN_DEVICE_ID);
                    cob_chg_plc_add_0.SelectedIndex
                        = GetBaseAddList((_comNode.Parent as DeaEntity).MAIN_DEVICE_ID).IndexOf(_deviceNode.CHG_DATA_ARRY[0]._plc_Add);

                    cob_chg_plc_add_1.ItemsSource = GetBaseAddList((_comNode.Parent as DeaEntity).MAIN_DEVICE_ID);
                    cob_chg_plc_add_1.SelectedIndex
                        = GetBaseAddList((_comNode.Parent as DeaEntity).MAIN_DEVICE_ID).IndexOf(_deviceNode.CHG_DATA_ARRY[1]._plc_Add);

                    if (_deviceNode.CHG_DATA_ARRY[0]._chg_Type == "read")
                        cob_chg_device_direction_0.SelectedIndex = 0;
                    else
                        cob_chg_device_direction_0.SelectedIndex = 1;

                    if (_deviceNode.CHG_DATA_ARRY[1]._chg_Type == "read")
                        cob_chg_device_direction_1.SelectedIndex = 0;
                    else
                        cob_chg_device_direction_1.SelectedIndex = 1;
                }
                

            }



        }

        public List<string> GetBaseAddList(int device_id)
        {
            Dictionary<int, List<string>> myList = new Dictionary<int, List<string>>();
            myList = getDeviceInfo.GetBaseAddList(_device_id);
            List<string> _rusult = new List<string>();

            foreach (KeyValuePair<int, List<string>> e in myList)
            {
                if (e.Key == device_id)
                {
                    foreach (var item in e.Value)
                    {
                        _rusult.Add(item);
                    }
                }
            }
            return _rusult;
        }

        public bool IsExit
        {
            get { return _isExit; }
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

        public class DviceClass
        {
            public int ID { get; set; }
            public int PRODUCT_ID { get; set; }
            public int PRODUCTPROTOCOL_ID { get; set; }
            public string NAME { get; set; }
        }

        private void cob_device_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _device_id = Convert.ToInt32(cob_device.SelectedValue);
            cob_chg_device_add_0.ItemsSource = GetBaseAddList(_device_id);
            cob_chg_device_add_1.ItemsSource = GetBaseAddList(_device_id); 
            cob_chg_plc_add_0.ItemsSource = GetBaseAddList((_comNode.Parent as DeaEntity).MAIN_DEVICE_ID);
            cob_chg_plc_add_1.ItemsSource = GetBaseAddList((_comNode.Parent as DeaEntity).MAIN_DEVICE_ID);
        }

        private void is_ok_Click(object sender, RoutedEventArgs e)
        {
            
                ComEntity _comNode = new ComEntity();
              
             
            _comNode = _deviceNode.Parent as ComEntity;
            if (!_comNode.IS_MAIN || _comNode.ChildrenCount<1)
            { 
                if(_comNode.ChildrenCount + Convert.ToInt32(txt_copyNum.Text) < 18)
                { 
                    if (cob_device.SelectedItem != null  )
                    { 
                        List<ChangeDataClass> _list_chg_data = new List<ChangeDataClass>();
                        _deviceNode.DEVICE_ID_INFO =Convert.ToInt32(cob_device.SelectedValue); 
                        _deviceNode.DEVICE_NAME_INFO = cob_device.Text;
                        if(_deviceNode.CURRENT_STAT != BaseEntity.CurrentOpCType.Modify)
                             _deviceNode.NAME = _deviceNode.NAME + " " +cob_device.Text ;
                        //存储任务
                        for (int i = 0; i < 2; i++)
                        {
                            ChangeDataClass _chg_data_class = new ChangeDataClass();
                            foreach (UIElement element in this.Grid_Chg.Children)
                            {//遍历任务控件
                                if (element is System.Windows.Controls.ComboBox)
                                { 
                                    if (((ComboBox)element).Name.IndexOf("chg_device_add_"+i) > 0)
                                        _chg_data_class._divece_Add = ((ComboBox)element).Text;
                                    if (((ComboBox)element).Name.IndexOf("chg_device_direction_" + i) > 0)
                                        _chg_data_class._chg_Type = (string)((ComboBox)element).SelectedValue;
                                    if (((ComboBox)element).Name.IndexOf("chg_plc_add_" + i) > 0)
                                        _chg_data_class._plc_Add = ((ComboBox)element).Text;  
                                } 
                            }
                            _list_chg_data.Add(_chg_data_class);
                        }

                        _deviceNode.CHG_DATA_ARRY = _list_chg_data;

                        if (_comNode.IS_MAIN)
                            (_comNode.Parent as DeaEntity).MAIN_DEVICE_ID = _device_id;
                        if (txt_copyNum.Text != null && check_copyNum.IsChecked == true)
                            this.COPY_NUM = Convert.ToInt32(txt_copyNum.Text);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("设备信息不完整!", "提示");
                    }
                }
                else
                {
                    MessageBox.Show("同一个COM口下最多只能设置18台设备!", "提示");
                }
            }
            else
            {
                MessageBox.Show("一个主站COM端口只能设置一台设备!", "提示");
            }
        }

      
    }
}
