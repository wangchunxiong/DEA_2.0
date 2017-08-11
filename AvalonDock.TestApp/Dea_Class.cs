using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DEA3
{
    public class Dea_Class : BaseEntityTree,INotifyPropertyChanged
    {

        private string _note = "";
        ///// <summary>  
        ///// ProjiectNote  
        ///// </summary>  
        public string Note
        {
            get { return _note; }
            set
            {
                if (!_note.Equals(value))
                {
                    _note = value;
                    OnChangedProperty("Note");
                }
            }
        }



        private BaseEntity _currentBaseEntity = null;
        /// <summary>  
        /// 当前节点对象  
        /// </summary>  
        public BaseEntity CurrentBaseEntity
        {
            get { return _currentBaseEntity; }
            set
            {
                if (this._currentBaseEntity == null || !this._currentBaseEntity.Equals(value))
                {
                    this._currentBaseEntity = value;
                    OnChangedProperty("CurrentBaseEntity");
                }
            }
        }

        private ObservableCollection<BaseEntityTree> _childrenEntityTree = new ObservableCollection<BaseEntityTree>();
        /// <summary>  
        /// 当前节点 包含的孩子节点  
        /// </summary>  
        public ObservableCollection<BaseEntityTree> ChildrenEntityTree
        {
            get
            {
                return _childrenEntityTree;
            }
            set
            {
                _childrenEntityTree = value;
            }
        }

        /// <summary>  
        /// 默认构造函数  
        /// </summary>  
        public Dea_Class()
        {
            this._currentBaseEntity = new BaseEntity();
        }

        /// <summary>  
        /// 赋值  
        /// </summary>  
        /// <param name="entity"></param>  
        public Dea_Class(BaseEntity entity)
        {
            this._currentBaseEntity = entity;
        }



        //监听方法
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnChangedProperty(string value)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(value));
            }
        }
    } 
}
