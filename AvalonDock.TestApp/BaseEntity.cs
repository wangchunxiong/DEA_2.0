using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DEA3
{
    /// <summary>  
    /// 基础数据类  
    /// </summary>  
    public class BaseEntity :  INotifyPropertyChanged
    { 
        public BaseEntity()
        {
            this.ID = Guid.NewGuid();
            //this._currentBaseEntity = new BaseEntity();
        }
        public BaseEntity(BaseEntity entity)
        {
            this.ID = new Guid();
            //this._currentBaseEntity = entity;
        }


        private Guid _ID = Guid.Empty;
        public Guid ID
        {
            get { return _ID; }
            set
            {
                if (!_ID.Equals(value))
                {
                    _ID = value;
                    OnChangedProperty("ID");
                }
            }
        }

        private int _depti ;
        /// <summary>  
        /// 当前节点  在树中的深度  
        /// </summary>  
        public int DEPTI
        {
            get { return _depti; }
            set
            {
                if (!_depti.Equals(value))
                {
                    _depti = value;
                    OnChangedProperty("DEPTI");
                }
            }
        }

        private string _name = "";
        ///// <summary>  
        ///// 当前节点  名称  
        ///// </summary>  
        public string NAME
        {
            get { return _name; }
            set
            {
                if (!_name.Equals(value))
                {
                    _name = value;
                    OnChangedProperty("NAME");
                }
            }
        }


        private bool _addIsEnabled ;
        ///// <summary>  
        ///// 当前节点菜单新增是否可用  
        ///// </summary>  
        public bool AddIsEnabled
        {
            get { return _addIsEnabled; }
            set
            {
                if (!_addIsEnabled.Equals(value))
                {
                    _addIsEnabled = value;
                    OnChangedProperty("AddIsEnabled");
                }
            }
        }

        private bool _chgIsEnabled;
        ///// <summary>  
        ///// 当前节点菜单修改是否可用  
        ///// </summary>  
        public bool ChgIsEnabled
        {
            get { return _chgIsEnabled; }
            set
            {
                if (!_chgIsEnabled.Equals(value))
                {
                    _chgIsEnabled = value;
                    OnChangedProperty("ChgIsEnabled");
                }
            }
        }

        private bool _delIsEnabled;
        ///// <summary>  
        ///// 当前节点菜单删除是否可用  
        ///// </summary>  
        public bool DelIsEnabled
        {
            get { return _delIsEnabled; }
            set
            {
                if (!_delIsEnabled.Equals(value))
                {
                    _delIsEnabled = value;
                    OnChangedProperty("DelIsEnabled");
                }
            }
        }

        private NodeType _type ;
        ///// <summary>  
        ///// 当前节点  名称  
        ///// </summary>  
        public NodeType TYPE
        {
            get { return _type; }
            set
            {
                if (!_type.Equals(value))
                {
                    _type = value;
                    OnChangedProperty("TYPE");
                }
            }
        }

        private CurrentOpCType _current_stat;
        ///// <summary>  
        ///// CURRENT_STAT
        ///// </summary>  
        public CurrentOpCType CURRENT_STAT
        {
            get { return _current_stat; }
            set
            {
                if (!_current_stat.Equals(value))
                {
                    _current_stat = value;
                    OnChangedProperty("CURRENT_STAT");
                }
            }
        }

        public enum NodeType
        {
            RootNode,//根节点
            DeaNode,
            ComNode,
            DeviceNode
        }

        public enum CurrentOpCType
        {
            Add, 
            Modify,
            Delete 
        }

        public BaseEntity Parent { get; set; }

        public ObservableCollection<BaseEntity> BaseChildren { get; set; } = new ObservableCollection<BaseEntity>();

        public virtual void AddNewItem(object currentNode) { }

        public virtual void DelItem(object currentNode) { }

        public virtual void ChgItem(object currentNode) { }




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
    }
}
