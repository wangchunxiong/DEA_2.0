using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace DEA3.Data
{
    public class GetInfoService
    {
        //获得任务号列表
        List<String> tasklist = new List<String>();

        //调试时使用
        string protocol_file_path = @"../../config_file/protocol";

        string protocoldevice_file_path = @"../../config_file/protocolDevice";
        //发布时使用
        //string protocol_file_path = Directory.GetCurrentDirectory()+"\\config_file\\protocol";

        public List<String> GetTaskNumList()
        {
            tasklist.Clear();
            for (int i = 1; i < 50; i++)
            {
                tasklist.Add("任务" + i);
            }
            return tasklist;

        }

        //获得任务端口列表
        public List<String> GetComNumList()
        {
            tasklist.Clear();
            for (int i = 1; i < 20; i++)
            {
                tasklist.Add("COM" + i);
            }
            return tasklist;

        }

        //获得任务读写下拉
        public List<String> GetReadWriteList()
        {
            tasklist.Clear();
            tasklist.Add("读");
            tasklist.Add("写");
            return tasklist;
        }

        //获得任务站号列表
        public List<String> GetDeviceNumList()
        {
            tasklist.Clear();
            for (int i = 1; i < 50; i++)
            {
                tasklist.Add(i + "号站");
            }
            return tasklist;
        }

        //获得端口设置站号列表
        public List<String> GetSpdList()
        {
            tasklist.Clear();
            tasklist.Add("2400");
            tasklist.Add("9600");
            tasklist.Add("38400");
            tasklist.Add("4800");
            tasklist.Add("19200");
            return tasklist;
        }

         

        //获得端口设置位长列表
        public List<String> GetBitList()
        {
            tasklist.Clear();
            tasklist.Add("7");
            tasklist.Add("8");
            return tasklist;
        }
        //获得端口设置同步位列表
        public Dictionary<int, String> GetSyncBitList()
        {
            Dictionary<int, String> mydic = new Dictionary<int, string>() {
                { 0, "N"},
                { 1, "O"},
                { 2, "E"} };
            return mydic;
        }

        //是否主站列表
        public Dictionary<int, String> GetIsMainList()
        {
            Dictionary<int, String> mydic = new Dictionary<int, string>() {
                { 1, "是"},
                 { 0, "否"}};
            return mydic;
        }
        //获得端口设置停止位列表
        public List<String> GetStopBitList()
        {
            tasklist.Clear();
            tasklist.Add("1");
            tasklist.Add("2");
            return tasklist;
        }

        //获得故障记录列表
        public List<String> GetSiteAddList()
        {
            tasklist.Clear();
            tasklist.Add("100");
            tasklist.Add("102");
            return tasklist;
        }

        //加载端口中协议列表
        public Dictionary<int, String> GetProtocolList()
        {
            string ReadLine;
            string[] array;
            string[] _array_string;
            Dictionary<int, String> tasklist = new Dictionary<int, String>();
            //string Path = Directory.GetCurrentDirectory() + "/config_file/protocol";

            if (File.Exists(protocol_file_path))
            {
                StreamReader reader = new StreamReader(protocol_file_path,
                                  System.Text.Encoding.GetEncoding("GB2312"));
                while (reader.Peek() >= 0)
                {
                    try
                    {
                        ReadLine = reader.ReadLine();
                        if (ReadLine != "")
                        {
                            array = ReadLine.Split('\n');
                            //array = ReadLine.Split('\n');
                            if (array.Length == 0)
                            {
                                //MessageBox.Show("协议档案格式错误！", "提示");
                                return tasklist;
                            }
                            _array_string = array[0].Split(',');
                            tasklist.Add(Convert.ToInt32(_array_string[0]), _array_string[1]);
                        }
                        //return tasklist;

                        //_DataGrid_DeviceSet_ComBox_Protocol.ItemsSource = protocolData2;

                        //_DataGrid_DeviceSet_ComBox_Protocol.SelectedValuePath//SelectedValueBinding//SelectedItemBinding
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        return tasklist;
                    }

                }
                return tasklist;
            }
            else
            {
                //MessageBox.Show("协议档案不存在！", "提示");
                return tasklist;
            }

        }

        //加载明细协议列表
        public DataTable GetProtocolDeiveceList(int key)
        {
            string ReadLine;
            string[] array;
            string[] _array_string; 
            DataTable _dt = new DataTable("mydt");
            DataTable _dt2 = new DataTable();
            _dt.Columns.Add("ID",typeof(int));
            _dt.Columns.Add("NAME", typeof(string));
            _dt.AcceptChanges();
            //string Path = Directory.GetCurrentDirectory() + "/config_file/protocol";

            if (File.Exists(protocol_file_path))
            {
                StreamReader reader = new StreamReader(protocoldevice_file_path,
                                  System.Text.Encoding.GetEncoding("GB2312"));
                while (reader.Peek() >= 0)
                {
                    try
                    {
                        ReadLine = reader.ReadLine();
                        if (ReadLine != "")
                        { 
                            array = ReadLine.Split('\n');
                            //array = ReadLine.Split('\n'); 
                            if (array.Length == 0)
                            {
                                //MessageBox.Show("协议档案格式错误！", "提示");
                                return _dt;
                            }
                            _array_string = array[0].Split(',');
                            _dt.Rows.Add(Convert.ToInt32(_array_string[0]), _array_string[1]);
                            //DataRow dr = _dt.NewRow();
                            //dr["ID"] = _array_string[0];
                            //dr["NAME"] = _array_string[1];
                            //_dt.Rows.Add(dr);
                            //tasklist.Add( _array_string[0]); 
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.ToString());
                        return _dt;
                    }

                }
                //var query = from dr in _dt.AsEnumerable()
                //            where dr.Field<int>("ID") == key
                //            select dr;

                //List<string> a = new List<string>();
                //a = query.ToList<string>();
                //foreach (var item in query.ToList)
                //{
                //    MessageBox.Show(query.First().Field<string>("NAME"));
                //}
                
                //MessageBox.Show(query.ToString());
                return _dt;
            }
            else
            {
                //MessageBox.Show("协议档案不存在！", "提示");
                return _dt;
            }

        }

    }
}
