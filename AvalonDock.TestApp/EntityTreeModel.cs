using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DEA3
{
    /// <summary>  
    /// 数据类，提供Name属性，显示节点信息  
    /// </summary>  
    public class EntityTreeModel : BaseEntity
    {
        public EntityTreeModel()
        {

        }

        public EntityTreeModel(string name)
        {
            this.Name = name;
        }
        /// <summary>
        /// name属性
        /// </summary>
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// project属性
        /// </summary>
        private string _projectNote;
        public string ProjectNote
        {
            get { return _projectNote; }
            set { _projectNote = value; }
        }

    }
}
