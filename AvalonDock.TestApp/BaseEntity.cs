using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DEA3
{
    /// <summary>  
    /// 基础数据类  
    /// </summary>  
    public class BaseEntity
    {
        private Guid _ID = Guid.Empty;
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        } 

        public BaseEntity()
        {
            this._ID = Guid.NewGuid();
        }
        public BaseEntity(Guid guid)
        {
            this._ID = guid;
        }
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
