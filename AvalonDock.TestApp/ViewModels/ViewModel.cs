using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace DEA3
{
    /// <summary>  
    /// 视图模型类，用于绑定到当前数据上下文  
    /// </summary>  
    public class ViewModel : INotifyPropertyChanged
    { 
        private ObservableCollection<ProjectEntity> _nodeCollection = new ObservableCollection<ProjectEntity>();
        /// <summary>  
        /// 节点数据集合  
        /// </summary>  
        public ObservableCollection<ProjectEntity> NodeCollection
        {
            get { return _nodeCollection; }
            set
            {
                _nodeCollection = value;
                OnChangedProperty("NodeCollection");
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

        public RelayCommand<BaseEntity> AddNewItemCommand { get; set; }
        public RelayCommand<BaseEntity> DelItemCommand { get; set; }
        public RelayCommand<BaseEntity> ChgItemCommand { get; set; }

        public ViewModel()
        {
            NodeCollection = new ObservableCollection<ProjectEntity> {
            new ProjectEntity()
            {
                NAME = "工程",
                NOTE = "这是一个初始化备注",
                PROJEC_TNOTE = "四川XXx鞋厂工程",
                DEPTI = 0,
                TYPE = BaseEntity.NodeType.RootNode,
                AddIsEnabled = true,
                DelIsEnabled = false
            } };

            AddNewItemCommand = new RelayCommand<BaseEntity>(ExecuteAddNewItem);
            DelItemCommand = new RelayCommand<BaseEntity>(ExecuteDelItem);
            ChgItemCommand = new RelayCommand<BaseEntity>(ExecuteGchItem);
        }

        BaseEntity _currentNode;
        public void ExecuteAddNewItem(BaseEntity node)
        {  
            if (node == null)
                return;
             node.AddNewItem(node);
        }

        public void ExecuteDelItem(BaseEntity node)
        {
            if (node == null)
                return;

            node.DelItem(node); 
        }

        public void ExecuteGchItem(BaseEntity node)
        {
            if (node == null)
                return;

            node.ChgItem(node);
        }

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
