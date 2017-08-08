using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DEA3
{
    /// <summary>  
    /// 基础树类  
    /// </summary>  
    public class BaseEntityTree : INotifyPropertyChanged
    {

        private int _depti = 1;
        /// <summary>  
        /// 当前节点  在树中的深度  
        /// </summary>  
        public int Depti
        {
            get { return _depti; }
            set
            {
                if (!_depti.Equals(value))
                {
                    _depti = value;
                    OnChangedProperty("Depti");
                }
            }
        }


        ///// <summary>  
        ///// 当前节点  名称  
        ///// </summary>  
        public string Name
        {
            get
            {
                if (ParentBaseEntityTree != null)
                {
                    return ((EntityTreeModel)(this.CurrentBaseEntity)).Name;
                }
                else
                { return "工程"; }
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

        private BaseEntityTree _parentBaseEntityTree;
        /// <summary>  
        /// 当前节点的父节点  
        /// </summary>  
        public BaseEntityTree ParentBaseEntityTree
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
        /// 当前节点 同级上节点  
        /// </summary>  
        private BaseEntityTree _prevBaseEntityTree;
        public BaseEntityTree PrevBaseEntityTree
        {
            get { return _prevBaseEntityTree; }
            set
            {
                if (this._prevBaseEntityTree == null || !this._prevBaseEntityTree.Equals(value))
                {
                    this._prevBaseEntityTree = value;
                    OnChangedProperty("PrevBaseEntityTree");
                }
            }
        }

        private BaseEntityTree _nextBaseEntityTree;
        /// <summary>  
        /// 当前节点 同级下节点  
        /// </summary>  
        public BaseEntityTree NextBaseEntityTree
        {
            get { return _nextBaseEntityTree; }
            set
            {
                if (this._nextBaseEntityTree == null || !this._nextBaseEntityTree.Equals(value))
                {
                    this._nextBaseEntityTree = value;
                    OnChangedProperty("NextBaseEntityTree");
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
        public BaseEntityTree()
        {
            this._currentBaseEntity = new BaseEntity();
        }

        /// <summary>  
        /// 赋值  
        /// </summary>  
        /// <param name="entity"></param>  
        public BaseEntityTree(BaseEntity entity)
        {
            this._currentBaseEntity = entity;
        }

        /// <summary>  
        /// 当前节点 孩子节点数量  
        /// </summary>  
        public int ChildrenCount
        {
            get
            {
                return this.ChildrenEntityTree.Count;
            }
        }

        /// <summary>  
        /// 当前节点所在集合的索引  
        /// </summary>  
        public int Index
        {
            
            get
            {
                if (ParentBaseEntityTree != null)
                {
                    return this.ParentBaseEntityTree.ChildrenEntityTree.IndexOf(this);
                }
                else
                {  return 0;  }
            }
        }

        private const string NULLMESSAGE = "传入对象为空";

        /// <summary>  
        /// 添加孩子节点  
        /// </summary>  
        /// <param name="entityTree"></param>  
        /// <returns>string</returns>  
        public string AddChildrenEntityTree(BaseEntityTree entityTree)
        {
            string message = "";
            try
            {
                // 判断是否为空  
                if (entityTree == null)
                    return NULLMESSAGE;
                // 设置新添加孩子节点的深度  
                entityTree.Depti = this.Depti + entityTree.Depti;
                // 设置新添加孩子节点的父节点为当前BaseEntityTree  
                entityTree.ParentBaseEntityTree = this;

                // 添加到集合  
                if (this.ChildrenEntityTree != null && this.ChildrenEntityTree.Count > 0)
                {
                    // 获取孩子节点集合中的最后一个  
                    BaseEntityTree lastBaseEntityTree = this.ChildrenEntityTree.Last();
                    // 设置lastBaseEntityTree的同级下节点为当前 新添加孩子节点  
                    if (lastBaseEntityTree != null)
                    {
                        lastBaseEntityTree.NextBaseEntityTree = entityTree;
                        // 设置当前新添节点的同级上节点为lastBaseEntityTree  
                        entityTree.PrevBaseEntityTree = lastBaseEntityTree;
                    }
                }
                // 添加到当前集合  
                this.ChildrenEntityTree.Add(entityTree);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }
        /// <summary>  
        /// 移除当前选中的节点，同时需要更新处理当前节点的同级上节点，下节点  
        /// </summary>  
        /// <param name="entityTree">选择的节点</param>  
        /// <returns>string</returns>  
        public string RemoveChildrenEntityTree(BaseEntityTree entityTree)
        {
            string message = "";
            try
            {
                if (entityTree == null)
                    return NULLMESSAGE;
                if (this.ChildrenEntityTree == null || this.ChildrenEntityTree.Count > 0)
                {
                    // 如果传入节点的同级上节点不为null  
                    if (entityTree.PrevBaseEntityTree != null)
                    {

                        // 删除的最后一个  
                        if (ChildrenEntityTree.IndexOf(entityTree) == ChildrenCount - 1)
                        {
                            entityTree.PrevBaseEntityTree.NextBaseEntityTree = null; // 同级上节点的下节点为null  
                        }
                        else
                        {
                            entityTree.PrevBaseEntityTree.NextBaseEntityTree = entityTree.NextBaseEntityTree;
                            entityTree.NextBaseEntityTree.PrevBaseEntityTree = entityTree.PrevBaseEntityTree;
                        }
                    }
                    this.ChildrenEntityTree.Remove(entityTree);
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
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
