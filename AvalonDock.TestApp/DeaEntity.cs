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


        private ProjectEntity _parentBaseEntityTree;
        /// <summary>  
        /// 当前节点的父节点  
        /// </summary>  
        public ProjectEntity ParentBaseEntityTree
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
         

        public override void AddNewItem(object currentNode)
        {
            DeaEntity _currentNode = currentNode as DeaEntity;
            ComEntity _childrenNode = new ComEntity();
            int _nodeNum;
            switch (_currentNode.DEPTI)
            { 
                case 1:
                    _nodeNum = _currentNode.ChildrenCount;
                    _childrenNode.NAME = "COM" + (_nodeNum + 1);
                    _childrenNode.DEPTI = _currentNode.DEPTI + 1;
                    _childrenNode.TYPE = NodeType.ComNode;
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
