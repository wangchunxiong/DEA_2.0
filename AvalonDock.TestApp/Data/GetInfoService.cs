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
        string _ProductInfo_path = @"../../config_file/ProductInfo";

        string _ProductProtocol_path = @"../../config_file/ProductProtocol";

        string _ProductDevice_path = @"../../config_file/ProductDevice";

        string _DeviceBaseAdd_path = @"../../config_file/DeviceBaseAdd";

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
        public Dictionary<string,string> GetReadWriteList()
        {
            Dictionary<string, String> mydic = new Dictionary<string, string>()
            {
                {"read","-->" }, {"write","<--" }
            };

            return mydic;
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
        public Dictionary<bool, string> GetIsMainList()
        {
            Dictionary<bool, string> mydic = new Dictionary<bool, string>() {
                { true, "是"},
                 { false, "否"}};
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
        public Dictionary<int, String> GetProductList()
        {
            string ReadLine;
            string[] array;
            string[] _array_string;
            Dictionary<int, String> tasklist = new Dictionary<int, String>();
            //string Path = Directory.GetCurrentDirectory() + "/config_file/protocol";

            if (File.Exists(_ProductInfo_path))
            {
                StreamReader reader = new StreamReader(_ProductInfo_path,
                                  System.Text.Encoding.GetEncoding("GB2312"));

                reader.ReadLine();//先读取一行(列头)

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
        public DataTable GetProductListForDt(int key)
        {
            string ReadLine;
            string[] array;
            string[] _array_string;
            DataTable _dt = new DataTable("mydt");
            _dt.Columns.Add("ID", typeof(int));
            _dt.Columns.Add("NAME", typeof(string)); 
            _dt.Columns.Add("IS_MAIN", typeof(int));
            _dt.AcceptChanges();
            //string Path = Directory.GetCurrentDirectory() + "/config_file/protocol";

            if (File.Exists(_ProductInfo_path))
            {
                StreamReader reader = new StreamReader(_ProductInfo_path,
                                  System.Text.Encoding.GetEncoding("GB2312"));

                reader.ReadLine();//先读取一行(列头)

                while (reader.Peek() >= 0)
                {
                    try
                    {
                        ReadLine = reader.ReadLine();
                        if (ReadLine != "")
                        {
                            array = ReadLine.Split('\n');
                            if (array.Length == 0)
                            {
                                //MessageBox.Show("协议档案格式错误！", "提示");
                                return _dt;
                            }
                            _array_string = array[0].Split(',');
                            _dt.Rows.Add(Convert.ToInt32(_array_string[0]), _array_string[1], _array_string[2]);
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.ToString());
                        return _dt;
                    }
                }
                return _dt;
            }
            else
            {
                //MessageBox.Show("协议档案不存在！", "提示");
                return _dt;
            }

        }

        //加载明细协议列表
        public DataTable GetProductProtocolList(int key)
        {
            string ReadLine;
            string[] array;
            string[] _array_string; 
            DataTable _dt = new DataTable("mydt"); 
            _dt.Columns.Add("ID",typeof(int));
            _dt.Columns.Add("PRODUCT_ID", typeof(int));
            _dt.Columns.Add("NAME", typeof(string));
            _dt.AcceptChanges();
            //string Path = Directory.GetCurrentDirectory() + "/config_file/protocol";

            if (File.Exists(_ProductProtocol_path))
            {
                StreamReader reader = new StreamReader(_ProductProtocol_path,
                                  System.Text.Encoding.GetEncoding("GB2312"));
                 
                reader.ReadLine();//先读取一行(列头)

                while (reader.Peek() >= 0)
                {
                    try
                    {
                        ReadLine = reader.ReadLine();
                        if (ReadLine != "")
                        { 
                            array = ReadLine.Split('\n'); 
                            if (array.Length == 0)
                            {
                                //MessageBox.Show("协议档案格式错误！", "提示");
                                return _dt;
                            }
                            _array_string = array[0].Split(',');
                            _dt.Rows.Add(Convert.ToInt32(_array_string[0]), _array_string[1], _array_string[2]);
                         } 
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.ToString());
                        return _dt;
                    } 
                } 
                return _dt;
            }
            else
            {
                //MessageBox.Show("协议档案不存在！", "提示");
                return _dt;
            }

        }

        //加载明细协议列表
        public DataTable GetProductProtocolDeviceList(int key)
        {
            string ReadLine;
            string[] array;
            string[] _array_string;
            DataTable _dt = new DataTable("mydt"); 
            _dt.Columns.Add("ID", typeof(int));
            _dt.Columns.Add("PRODUCT_ID", typeof(int));
            _dt.Columns.Add("PRODUCTPROTOCOL_ID", typeof(int));
            _dt.Columns.Add("NAME", typeof(string));
            _dt.AcceptChanges();
            //string Path = Directory.GetCurrentDirectory() + "/config_file/protocol";

            if (File.Exists(_ProductDevice_path))
            {
                StreamReader reader = new StreamReader(_ProductDevice_path,
                                  System.Text.Encoding.GetEncoding("GB2312"));

                reader.ReadLine();//先读取一行(列头)

                while (reader.Peek() >= 0)
                {
                    try
                    {
                        ReadLine = reader.ReadLine();
                        if (ReadLine != "")
                        {
                            array = ReadLine.Split('\n');
                            if (array.Length == 0)
                            {
                                //MessageBox.Show("协议档案格式错误！", "提示");
                                return _dt;
                            }
                            _array_string = array[0].Split(',');
                            _dt.Rows.Add(Convert.ToInt32(_array_string[0]), _array_string[1], _array_string[2], _array_string[3]);
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.ToString());
                        return _dt;
                    }
                }
                return _dt;
            }
            else
            {
                //MessageBox.Show("协议档案不存在！", "提示");
                return _dt;
            }

        }


        //
        public Dictionary<int, List<string>> GetBaseAddList(int device_id)
        {
            string ReadLine;
            string[] array;
            string[] _array_string;
            Dictionary<int,List<string>> tasklist = new Dictionary<int, List<string>>();
            List<string> _result_add_List = new List<string>();
            //string Path = Directory.GetCurrentDirectory() + "/config_file/protocol";

            if (File.Exists(_DeviceBaseAdd_path))
            {
                StreamReader reader = new StreamReader(_DeviceBaseAdd_path,
                                  System.Text.Encoding.GetEncoding("GB2312"));

                reader.ReadLine();//先读取一行(列头)

                while (reader.Peek() >= 0)
                {
                    try
                    {
                        ReadLine = reader.ReadLine();
                        if (ReadLine != "")
                        {
                            array = ReadLine.Split('\n'); 
                            if (array.Length == 0)
                            { 
                                //MessageBox.Show("协议档案格式错误！", "提示");
                                return tasklist;
                            }
                            _array_string = array[0].Split(',');
                            _result_add_List = SplitAdd(_array_string[4], _array_string[3]);

                            //foreach (var item in _result_add_List)
                            //{
                            //每个设备所有地址加入集合
                                tasklist.Add(Convert.ToInt32(_array_string[1]), _result_add_List);
                            //} 
                        }
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

        private List<string> SplitAdd(string addStr,string start_str)
        {
            List<string> myList = new List<string>();
            string[] _add ;

            if (addStr.Contains("|"))
            {
                 myList = new List<string>(addStr.Split('|'));
            }
            else if (addStr.Contains("-"))
            {
                int start_num;
                int end_num;
                _add = addStr.Split('-');
                start_num = Convert.ToInt32(_add[0].Substring(_add[0].IndexOf(start_str)+1, _add[0].Length - start_str.Length));
                end_num = Convert.ToInt32(_add[1].Substring(_add[1].IndexOf(start_str) + 1, _add[1].Length - start_str.Length));
                for (int i = start_num; i <=end_num; i++)
                {
                    myList.Add(start_str + i);
                    //MessageBox.Show("ADD:"+start_str + i);
                }
            }
            else
            {
                myList = new List<string>() {addStr};
            }
            return myList;
        }



    }
}
