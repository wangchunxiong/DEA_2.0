using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DEA3
{
    /// <summary>  
    /// 视图模型类，用于绑定到当前数据上下文  
    /// </summary>  
    public class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<BaseEntityTree> _nodeCollection = new ObservableCollection<BaseEntityTree>();
        /// <summary>  
        /// 节点数据集合  
        /// </summary>  
        public ObservableCollection<BaseEntityTree> NodeCollection
        {
            get { return _nodeCollection; }
            set
            {
                _nodeCollection = value;
                OnChangedProperty("NodeCollection");
            }
        }


        private BaseEntityTree _currentSelectedEntityTree;
        /// <summary>  
        /// 当前选择EntityTree  
        /// </summary>  
        public BaseEntityTree CurrentSelecteEntityTree
        {
            get { return _currentSelectedEntityTree; }
            set
            {
                _currentSelectedEntityTree = value;
                OnChangedProperty("CurrentSelecteEntityTree");
            }
        }
        public ViewModel()
        {
            EntityTreeModel parentTreeModel = new EntityTreeModel()
            {
                Name = "工程"
            };
            NodeCollection.Add(new BaseEntityTree(parentTreeModel));
            // 组件临时数据  
            foreach (BaseEntityTree entityTree in NodeCollection)
            {
                for (int i = 0; i < 2; i++)
                {
                    EntityTreeModel model = new EntityTreeModel()
                    {
                        Name = "DEA" + i
                    };
                    entityTree.AddChildrenEntityTree(new BaseEntityTree(model));

                }
                foreach (BaseEntityTree seEntityTree in entityTree.ChildrenEntityTree)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        EntityTreeModel model = new EntityTreeModel()
                        {
                            Name = "COM" + j
                        };
                        seEntityTree.AddChildrenEntityTree(new BaseEntityTree(model));
                    }
                }
            }
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
