using DEA3.Control;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using static DEA3.Control.Window_DeviceSet;

namespace DEA3
{
    public class ComEntity : BaseEntity
    {
        private int _productId;
        ///// <summary>  
        ///// PROT
        ///// </summary>  
        public int PRODUCTID
        {
            get { return _productId; }
            set
            {
                if (!_productId.Equals(value))
                {
                    _productId = value;
                    OnChangedProperty("PRODUCTID");
                }
            }
        }

        private string _productName="";
        ///// <summary>  
        ///// PROT
        ///// </summary>  
        public string PRODUCTNAME
        {
            get { return _productName; }
            set
            {
                if (!_productName.Equals(value))
                {
                    _productName = value;
                    OnChangedProperty("PRODUCTNAME");
                }
            }
        }

        private int _port ;
        ///// <summary>  
        ///// PROT
        ///// </summary>  
        public int PORT
        {
            get { return _port; }
            set
            {
                if (!_port.Equals(value))
                {
                    _port = value;
                    OnChangedProperty("PORT");
                }
            }
        }

        private int _protocol_id ;
        ///// <summary>  
        ///// PROTOCOL  
        ///// </summary>  
        public int PROTOCOL_ID
        {
            get { return _protocol_id; }
            set
            {
                if (!_protocol_id.Equals(value))
                {
                    _protocol_id = value;
                    OnChangedProperty("PROTOCOL_ID");
                }
            }
        }

        private string _protocol = "";
        ///// <summary>  
        ///// PROTOCOL  
        ///// </summary>  
        public string PROTOCOL
        {
            get { return _protocol; }
            set
            {
                if (!_protocol.Equals(value))
                {
                    _protocol = value;
                    OnChangedProperty("PROTOCOL");
                }
            }
        }

        private int _spd ;
        ///// <summary>  
        ///// SPD  
        ///// </summary>  
        public int SPD
        {
            get { return _spd; }
            set
            {
                if (!_spd.Equals(value))
                {
                    _spd = value;
                    OnChangedProperty("SPD");
                }
            }
        }

        private int _bit ;
        ///// <summary>  
        ///// BIT  
        ///// </summary>  
        public int BIT
        {
            get { return _bit; }
            set
            {
                if (!_bit.Equals(value))
                {
                    _bit = value;
                    OnChangedProperty("BIT");
                }
            }
        }

        private int _sync_bit;
        ///// <summary>  
        ///// SYNC_BIT  
        ///// </summary>  
        public int SYNC_BIT
        {
            get { return _sync_bit; }
            set
            {
                if (!_sync_bit.Equals(value))
                {
                    _sync_bit = value;
                    OnChangedProperty("SYNC_BIT");
                }
            }
        }

        private int _stop_bit ;
        ///// <summary>  
        ///// STOP_BIT  
        ///// </summary>  
        public int STOP_BIT
        {
            get { return _stop_bit; }
            set
            {
                if (!_stop_bit.Equals(value))
                {
                    _stop_bit = value;
                    OnChangedProperty("STOP_BIT");
                }
            }
        }

        private bool _is_main ;
        ///// <summary>  
        ///// IS_MAIN  
        ///// </summary>  
        public bool IS_MAIN
        {
            get { return _is_main; }
            set
            {
                if (!_is_main.Equals(value))
                {
                    _is_main = value;
                    OnChangedProperty("IS_MAIN");
                }
            }
        }

        private string _site_add = "";
        ///// <summary>  
        ///// SITE_ADD  
        ///// </summary>  
        public string SITE_ADD
        {
            get { return _site_add; }
            set
            {
                if (!_site_add.Equals(value))
                {
                    _site_add = value;
                    OnChangedProperty("SITE_ADD");
                }
            }
        }

        private string _stop_add = "";
        ///// <summary>  
        ///// STOP_ADD  
        ///// </summary>  
        public string STOP_ADD
        {
            get { return _stop_add; }
            set
            {
                if (!_stop_add.Equals(value))
                {
                    _stop_add = value;
                    OnChangedProperty("STOP_ADD");
                }
            }
        }

        private int _divece_id ;
        ///// <summary>  
        ///// STOP_ADD  
        ///// </summary>  
        public int DIVECE_ID
        {
            get { return _divece_id; }
            set
            {
                if (!_divece_id.Equals(value))
                {
                    _divece_id = value;
                    OnChangedProperty("DIVECE_ID");
                }
            }
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

        public void set_copy_num(object sender, MyEventArgs e)
        {
            this.COPY_NUM = e._copy_num;
        }

        private DeaEntity _parentBaseEntityTree;
        /// <summary>  
        /// 当前节点的父节点  
        /// </summary>  
        public DeaEntity ParentBaseEntityTree
        {
            get { return _parentBaseEntityTree; }
            set
            {
                if (this._parentBaseEntityTree == null || !this._parentBaseEntityTree.Equals(value))
                {
                    this._parentBaseEntityTree = value;
                    OnChangedProperty("ParentBaseEntityTree");
                }
            }
        }

        /// <summary>  
        /// 当前节点 孩子节点数量  
        /// </summary>  
        public int ChildrenCount
        {
            get
            {
                if (this.Children != null)
                {
                    return this.Children.Count;
                }
                return 0;
            }
        }

        /// <summary>  
        /// 当前节点所在集合的索引  
        /// </summary>  
        public int? Index
        {

            get
            {
                if (ParentBaseEntityTree != null)
                {
                    return this.ParentBaseEntityTree.Children.IndexOf(this);
                }
                else
                { return null; }
            }
        }
         
        public override void AddNewItem( object currentNode)
        {
            ComEntity _currentNode = currentNode as ComEntity;
            DeviceEntity _childrenNode = new DeviceEntity();

            Window_DeviceSet DeviceSetWindow; 
            int _nodeNum;
            /// <summary>  
            ///新增设备 
            /// </summary>
            if (!_currentNode.IS_MAIN || _currentNode.ChildrenCount<1)
            { 
                    switch (_currentNode.DEPTI)
                    { 
                        case 2:
                            _nodeNum = _currentNode.ChildrenCount;
                            _childrenNode.NAME = (_nodeNum + 1) + "号设备";
                            _childrenNode.DEPTI = _currentNode.DEPTI + 1;
                            _childrenNode.TYPE = NodeType.DeviceNode;
                            _childrenNode.SITE_NUMBER = _nodeNum + 1 ;
                            _childrenNode.PROTOCOL = _currentNode.PROTOCOL;
                            _childrenNode.CURRENT_STAT = CurrentOpCType.Add;
                            _childrenNode.AddIsEnabled = false;
                            _childrenNode.ChgIsEnabled = true;
                            _childrenNode.DelIsEnabled = true;
                            _childrenNode.Parent = this;
                            DeviceSetWindow = new Window_DeviceSet(_childrenNode);
                            DeviceSetWindow.Title = _childrenNode.NAME + "详细设置";
                            DeviceSetWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            DeviceSetWindow.ShowDialog();
                            this.COPY_NUM = DeviceSetWindow.COPY_NUM;

                            if (DeviceSetWindow.IsExit)
                            {
                                return;
                            } 
                            break;
                    }
                    /// <summary>  
                    ///生成设备自动复制数量  
                    /// </summary> 
                    if (this.COPY_NUM > 0)
                    {
                        
                            int a, b;
                            string str;
                            _nodeNum = _currentNode.ChildrenCount;
                            str = _childrenNode.NAME.Substring(_childrenNode.NAME.IndexOf("号"));
                            for (int i = 1; i <= this.COPY_NUM; i++)
                            {
                                DeviceEntity _newDeviceNode = new DeviceEntity();
                                _newDeviceNode.DEPTI = _childrenNode.DEPTI;
                                _newDeviceNode.TYPE = _childrenNode.TYPE;
                                _newDeviceNode.PROTOCOL = _childrenNode.PROTOCOL;
                                _newDeviceNode.AddIsEnabled = false;
                                _newDeviceNode.ChgIsEnabled = true;
                                _newDeviceNode.DelIsEnabled = true;
                                _newDeviceNode.Parent = this;
                                _newDeviceNode.DEVICE_NAME_INFO = _childrenNode.DEVICE_NAME_INFO;
                                _newDeviceNode.DEVICE_ID_INFO = _childrenNode.DEVICE_ID_INFO;
                                _newDeviceNode.CHG_DATA_ARRY = _childrenNode.CHG_DATA_ARRY;

                                _newDeviceNode.NAME = (_nodeNum + i) + str;
                                _newDeviceNode.SITE_NUMBER = _nodeNum + i;
                                Children.Add(_newDeviceNode);
                            } 
                    }
                    else
                    { 
                        Children.Add(_childrenNode);
                    } 
            }
            else
            {
                MessageBox.Show("主站COM口只能设置1台设备!","提示");
                return;
            }
            this.COPY_NUM = 0;
        }

        public override void DelItem(object currentNode)
        {
            ComEntity _currentNode = currentNode as ComEntity;

            string message = "确定删除[ " + _currentNode.NAME + " ]下所有设备配置数据 ?";
            string caption = "删除!";
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Question;

            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.OK)
            {
                switch (_currentNode.DEPTI)
                {
                    case 0:
                        break;
                    case 2:
                        ((DeaEntity)_currentNode.Parent).Children.Remove(this);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                return;
            } 
        }

        public override void ChgItem(object currentNode)
        {
            ComEntity _currentNode = currentNode as ComEntity;
            _currentNode.CURRENT_STAT = CurrentOpCType.Modify;
            Window_ComSet ComSetWindow;
            int _nodeNum;
            /// <summary>  
            ///新增设备 
            /// </summary>
            if (_currentNode.NAME != null)
            {
                switch (_currentNode.DEPTI)
                {
                    case 2: 
                        ComSetWindow = new Window_ComSet(_currentNode);
                        ComSetWindow.Title = _currentNode.NAME + "【修改】详细设置";
                        ComSetWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        ComSetWindow.ShowDialog();
                        break;
                }
            }
            else
            {
                MessageBox.Show("不能修改!", "提示");
            }
        }
        public ObservableCollection<DeviceEntity> Children { get; set; } = new ObservableCollection<DeviceEntity>();
    }
}
