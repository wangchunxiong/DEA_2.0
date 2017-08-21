using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DEA3
{
    public class ProjectEntity : BaseEntity
    { 
        private string _note = "";
        ///// <summary>  
        ///// NOTE  
        ///// </summary>  
        public string NOTE
        {
            get { return _note; }
            set
            {
                if (!_note.Equals(value))
                {
                    _note = value;
                    OnChangedProperty("NOTE");
                }
            }
        }

        private string _projec_tnote = "";
        ///// <summary>  
        ///// ProjiectNote  
        ///// </summary>  
        public string PROJEC_TNOTE
        {
            get { return _projec_tnote; }
            set
            {
                if (!_projec_tnote.Equals(value))
                {
                    _projec_tnote = value;
                    OnChangedProperty("PROJEC_TNOTE");
                }
            }
        }


        //private BaseEntity _currentBaseEntity = null;
        ///// <summary>  
        ///// 当前节点对象  
        ///// </summary>  
        //public BaseEntity CurrentBaseEntity
        //{
        //    get { return _currentBaseEntity; }
        //    set
        //    {
        //        if (this._currentBaseEntity == null || !this._currentBaseEntity.Equals(value))
        //        {
        //            this._currentBaseEntity = value;
        //            OnChangedProperty("CurrentBaseEntity");
        //        }
        //    }
        //}


        private BaseEntity _parentBaseEntityTree;
        /// <summary>  
        /// 当前节点的父节点  
        /// </summary>  
        public BaseEntity ParentBaseEntityTree
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
                if (this.Children!=null)
                {
                    return this.Children.Count;
                }
                return 0;
            }
        }

        public override void AddNewItem(object currentNode)
        {
            ProjectEntity _currentNode = currentNode as ProjectEntity;
            DeaEntity _childrenNode = new DeaEntity();
            int _nodeNum;
            switch (_currentNode.DEPTI)
            {
                case 0:
                    _nodeNum = _currentNode.ChildrenCount;
                    _childrenNode.NAME = (_nodeNum + 1) + "号DEA";
                    _childrenNode.DEPTI = _currentNode.DEPTI + 1;
                    _childrenNode.TYPE = NodeType.DeaNode;
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
         
    public ObservableCollection<DeaEntity> Children {  get;   set;  } = new ObservableCollection<DeaEntity>();



         
    }
}
