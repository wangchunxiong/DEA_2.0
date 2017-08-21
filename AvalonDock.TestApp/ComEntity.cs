using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace DEA3
{
    public class ComEntity : BaseEntity
    {
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

        private string _spd = "";
        ///// <summary>  
        ///// SPD  
        ///// </summary>  
        public string SPD
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

        private string _bit = "";
        ///// <summary>  
        ///// BIT  
        ///// </summary>  
        public string BIT
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

        private string _sync_bit = "";
        ///// <summary>  
        ///// SYNC_BIT  
        ///// </summary>  
        public string SYNC_BIT
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

        private string _stop_bit = "";
        ///// <summary>  
        ///// STOP_BIT  
        ///// </summary>  
        public string STOP_BIT
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

        private bool _is_main = false;
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
            int _nodeNum;
            switch (_currentNode.DEPTI)
            { 
                case 2:
                    _nodeNum = _currentNode.ChildrenCount;
                    _childrenNode.NAME = (_nodeNum + 1) + "号设备";
                    _childrenNode.DEPTI = _currentNode.DEPTI + 1;
                    _childrenNode.TYPE = NodeType.DeviceNode;
                    _childrenNode.AddIsEnabled = true;
                    _childrenNode.ChgIsEnabled = false;
                    _childrenNode.DelIsEnabled = true;
                    _childrenNode.Parent = this;
                    break;
                default:
                    break;
            }
            Children.Add(_childrenNode);
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
        public ObservableCollection<DeviceEntity> Children { get; set; } = new ObservableCollection<DeviceEntity>();
    }
}
