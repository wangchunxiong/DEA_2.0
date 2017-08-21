using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace DEA3
{
   public class DeviceEntity : BaseEntity
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

        private string _device_type = "";
        ///// <summary>  
        ///// DEVICE_TYPE  
        ///// </summary>  
        public string DEVICE_TYPE
        {
            get { return _device_type; }
            set
            {
                if (!_device_type.Equals(value))
                {
                    _device_type = value;
                    OnChangedProperty("DEVICE_TYPE");
                }
            }
        }
        private string _chg_device_add = "";
        ///// <summary>  
        ///// CHG_DEVICE_ADD  
        ///// </summary>  
        public string CHG_DEVICE_ADD
        {
            get { return _chg_device_add; }
            set
            {
                if (!_chg_device_add.Equals(value))
                {
                    _chg_device_add = value;
                    OnChangedProperty("CHG_DEVICE_ADD");
                }
            }
        }
        private string _chg_plc_add = "";
        ///// <summary>  
        ///// CHG_PLC_ADD  
        ///// </summary>  
        public string CHG_PLC_ADD
        {
            get { return _chg_plc_add; }
            set
            {
                if (!_chg_plc_add.Equals(value))
                {
                    _chg_plc_add = value;
                    OnChangedProperty("CHG_PLC_ADD");
                }
            }
        }
        private string _chg_type = "";
        ///// <summary>  
        ///// CHG_TYPE  
        ///// </summary>  
        public string CHG_TYPE
        {
            get { return _chg_type; }
            set
            {
                if (!_chg_type.Equals(value))
                {
                    _chg_type = value;
                    OnChangedProperty("CHG_TYPE");
                }
            }
        }



        private ComEntity _parentBaseEntityTree;
        /// <summary>  
        /// 当前节点的父节点  
        /// </summary>  
        public ComEntity ParentBaseEntityTree
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


        public override void DelItem(object currentNode)
        {
            DeviceEntity _currentNode = currentNode as DeviceEntity;

            string message = "确定删除设备[ " + _currentNode.NAME + " ]配置数据 ?";
            string caption = "删除!";
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Question;

            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.OK)
            {
                switch (_currentNode.DEPTI)
                {
                    case 0:
                        break;
                    case 3:
                        ((ComEntity)_currentNode.Parent).Children.Remove(this);
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

    }
}
