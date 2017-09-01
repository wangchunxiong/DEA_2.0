using DEA3.Control;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace DEA3
{
    public class DeaEntity : BaseEntity
    { 
        private string _sn = "";
        ///// <summary>  
        ///// SN  
        ///// </summary>  
        public string SN
        {
            get { return _sn; }
            set
            {
                if (!_sn.Equals(value))
                {
                    _sn = value;
                    OnChangedProperty("SN");
                }
            }
        } 

        private string _mode_type = "";
        ///// <summary>  
        ///// MODE_TYPE  
        ///// </summary>  
        public string MODE_TYPE
        {
            get { return _mode_type; }
            set
            {
                if (!_mode_type.Equals(value))
                {
                    _mode_type = value;
                    OnChangedProperty("MODE_TYPE");
                }
            }
        }

        private int _main_exists ;
        ///// <summary>  
        ///// SN  
        ///// </summary>  
        public int MAIN_EXISTS
        {
            get { return _main_exists; }
            set
            {
                if (!_main_exists.Equals(value))
                {
                    _main_exists = value;
                    OnChangedProperty("MAIN_EXISTS");
                }
            }
        }

        private int _main_device_id;
        ///// <summary>  
        ///// SN  
        ///// </summary>  
        public int MAIN_DEVICE_ID
        {
            get { return _main_device_id; }
            set
            {
                if (!_main_device_id.Equals(value))
                {
                    _main_device_id = value;
                    OnChangedProperty("MAIN_DEVICE_ID");
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

        public override void AddNewItem(object currentNode)
        {
            DeaEntity _currentNode = currentNode as DeaEntity;
            ComEntity _childrenNode = new ComEntity();
            Window_ComSet ComSetWindow;
            int _nodeNum;
            if (_currentNode.ChildrenCount< 2 )
            { 
                    switch (_currentNode.DEPTI)
                    { 
                        case 1:
                            _nodeNum = _currentNode.ChildrenCount;
                            _childrenNode.NAME = "COM" + (_nodeNum + 1);
                            _childrenNode.PORT = _nodeNum + 1;
                            _childrenNode.DEPTI = _currentNode.DEPTI + 1;
                            _childrenNode.TYPE = NodeType.ComNode;
                            _childrenNode.AddIsEnabled = true;
                            _childrenNode.ChgIsEnabled = false;
                            _childrenNode.DelIsEnabled = true;
                            _childrenNode.Parent = this;

                            ComSetWindow = new Window_ComSet(_childrenNode);
                            ComSetWindow.Title = _childrenNode.NAME + "详细设置";
                            ComSetWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen; 
                            ComSetWindow.ShowDialog();

                            if (ComSetWindow.IsExit)
                            {
                                return;
                            } 
                            break;
                    }
                    Children.Add(_childrenNode);
              
            }
            else
            {
                MessageBox.Show("每台DEA只能设置2个COM口!","提示");
            }
        }

        public override void DelItem(object currentNode)
        {
            DeaEntity _currentNode = currentNode as DeaEntity;

            string message = "确定删除[ " + _currentNode.NAME + " ]下所有COM口配置数据 ?";
            string caption = "删除!";
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Question;

            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.OK)
            {
                switch (_currentNode.DEPTI)
                {
                    case 1:
                        _currentNode.MAIN_EXISTS = 0;
                        ((ProjectEntity)_currentNode.Parent).Children.Remove(this);
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

        public ObservableCollection<ComEntity> Children { get; set; } = new ObservableCollection<ComEntity>();

    }
}
