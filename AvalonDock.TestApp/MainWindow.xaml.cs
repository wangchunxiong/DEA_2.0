using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Windows.Threading;
using Xceed.Wpf.AvalonDock.Layout;
using System.Diagnostics;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Xceed.Wpf.AvalonDock.Layout.Serialization;
using System.Resources;
using System.Collections;
using Xceed.Wpf.AvalonDock;
using System.Runtime.InteropServices;
using Xceed.Wpf.AvalonDock.Themes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;

namespace DEA3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //声明数据源对象
        ViewModel _viewModel = new ViewModel();
        string message = "";

        public RelayCommand<BaseEntity> AddNewItemCommand { get; set; }

        public MainWindow()
        {
            InitializeComponent(); 

            //_viewModel.NodeCollection = new ObservableCollection<ProjectEntity> {
            //new ProjectEntity()
            //{
            //    NAME = "ROOT工程",
            //    NOTE = "这是一个初始化备注",
            //    PROJEC_TNOTE = "四川XXx鞋厂工程",
            //    DEPTI = 0,
            //    TYPE = BaseEntity.NodeType.RootNode,
            //    AddIsEnabled = true,
            //    DelIsEnabled = false
            //} };

            this.TreeView_Project.DataContext = _viewModel;
             
        }

        //public void ExecuteAddNewItem(BaseEntity node)
        //{
        //    if (node == null)
        //        return;

        //    int _depti = _viewModel.CurrentBaseEntity.DEPTI;
        //    int _nodeNum;
        //    display.Text = "_depti:" + _depti;
        //    MessageBox.Show("新增节点,addNewItem方法!");
        //    switch (_depti)
        //    {
        //        case 0:
        //            _nodeNum = ((ProjectEntity)_viewModel.CurrentBaseEntity).ChildrenCount;
        //            //AddTreeViewNode();
        //            node.AddNewItem((_nodeNum + 1) + "号DEA", _currentNode, _depti);
        //            break;
        //        case 1:
        //            MessageBox.Show("节点深度为:" + _depti);
        //            _nodeNum = ((DeaEntity)_viewModel.CurrentBaseEntity).ChildrenCount;
        //            //AddTreeViewNode("COM1" + (_nodeNum + 1), _currentNode, _depti);
        //            node.AddNewItem("COM1" + (_nodeNum + 1), _currentNode, _depti);
        //            break;
        //        case 2:
        //            _nodeNum = ((ComEntity)_viewModel.CurrentBaseEntity).ChildrenCount;
        //            //AddTreeViewNode((_nodeNum + 1) + "号设备", _currentNode, _depti);
        //            node.AddNewItem((_nodeNum + 1) + "号设备", _currentNode, _depti);
        //            break;
        //        default:
        //            break;
        //    }
        //    ExpandTree();
             
        //    //MessageBox.Show("addNewItem方法!节点情况:"+node.NAME);
           
          
            
        //}

        private void Menu_Sys_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Tool_But_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Dp_Project_close_but_Click(object sender, RoutedEventArgs e)
        {
            //Dp_Project.Visibility = Visibility.Hidden;

            System.Windows.MessageBox.Show(Dp_Main.Width.ToString());
            //Dp_work_arer.Width = 2000;
            //dg1.Width = 300;
        }

        private void Tool_But_Project_Click(object sender, RoutedEventArgs e)
        {
            //Dp_Project.Visibility = Visibility.Visible;
        }
        //动态增加选项卡
        int clickCount = 0;
        private void item_AddNew_Click(object sender, RoutedEventArgs e)
        {
            clickCount++;
            LayoutAnchorable layOutAnc = new LayoutAnchorable() { Title = "新选项卡" + clickCount };
            layOutAnc.Content = new System.Windows.Controls.TextBox() { Text = "这是第" + clickCount + "个新选项卡" };
            layOutPaneContent.Children.Add(layOutAnc);
            layOutAnc.Closing += Tab_Close;//添加退出事件处理句柄=>添加的代码
        }
        //添加退出事件处理句柄
        private void Tab_Close(object sender, CancelEventArgs e)
        {
            if (true)
            {
                if (System.Windows.MessageBox.Show("还没有保存，是否要退出？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        //外观方法
        private void miGeneric_Click_1(object sender, RoutedEventArgs e)
        {
            dockingManager.Theme = new GenericTheme();
        }
        private void miDark_Click_1(object sender, RoutedEventArgs e)
        {
            dockingManager.Theme = new ExpressionDarkTheme();
        }
        private void miLight_Click_1(object sender, RoutedEventArgs e)
        {
            dockingManager.Theme = new ExpressionLightTheme();
        }
        private void miMetro_Click_1(object sender, RoutedEventArgs e)
        {
            dockingManager.Theme = new MetroTheme();
        }
        private void miVS_Click_1(object sender, RoutedEventArgs e)
        {
            dockingManager.Theme = new VS2010Theme();
        }


        //
        //
        //以下处理键盘钩子程序
        //
        /// 声明回调函数委托  
        /// </summary>  
        /// <param name="nCode"></param>  
        /// <param name="wParam"></param>  
        /// <param name="lParam"></param>  
        /// <returns></returns>  
        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);

        /// <summary>  
        /// 委托实例  
        /// </summary>  
        HookProc KeyboardHookProcedure;

        /// <summary>  
        /// 键盘钩子句柄  
        /// </summary>  
        static int hKeyboardHook = 0;

        //装置钩子的函数   
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll", EntryPoint = "GetKeyboardState")]
        public static extern int GetKeyboardState(byte[] pbKeyState); 

        //卸下钩子的函数   
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        //获取某个进程的句柄函数  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        /// <summary>  
        /// 普通按键消息  
        /// </summary>  
        private const int WM_KEYDOWN = 0x100;
        /// <summary>  
        /// 系统按键消息  
        /// </summary>  
        private const int WM_SYSKEYDOWN = 0x104;

        //鼠标常量   
        public const int WH_KEYBOARD_LL = 13;

        //声明键盘钩子的封送结构类型   
        [StructLayout(LayoutKind.Sequential)]
        public class KeyboardHookStruct
        {
            public int vkCode; //表示一个在1到254间的虚似键盘码   
            public int scanCode; //表示硬件扫描码   
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        //大小写状态
        public static bool CapsLockStatus
        {
            get
            {
                byte[] bs = new byte[256];
                GetKeyboardState(bs); 
                return (bs[0x14] == 1);
            }
        }

        //数值打开状态
        public static bool NumLockStatus
        {
            get
            {
                byte[] bs = new byte[256];
                GetKeyboardState(bs);
                return (bs[0x90] == 1);
            }
        }

        //插入覆盖状态
        public static bool InsertLockStatus
        {
            get
            {
                byte[] bs = new byte[256];
                GetKeyboardState(bs);
                return (bs[0x2D] == 1);
            }
        }

        /// <summary>  
        /// 截取全局按键，发送新按键，返回  
        /// </summary>  
        /// <param name="nCode"></param>  
        /// <param name="wParam"></param>  
        /// <param name="lParam"></param>  
        /// <returns></returns>  
        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN)
            {
                KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                System.Windows.Forms.Keys keyData = (System.Windows.Forms.Keys)MyKeyboardHookStruct.vkCode;

                switch (MyKeyboardHookStruct.vkCode)
                {
                    case 20:
                        if (CapsLockStatus) { statusBar_CapsLock.Text = "Caps Lock:[OFF]"; }
                        else { statusBar_CapsLock.Text = "Caps Lock:[ON]"; }
                        break;
                    case 144:
                        if (NumLockStatus) { statusBar_NumLock.Text = "Num Lock:[OFF]"; }
                        else { statusBar_NumLock.Text = "Num Lock:[ON]"; }
                        break;
                    case 45:
                        if (InsertLockStatus) { statusBar_InsertLock.Text = "Insert Lock:[OFF]"; }
                        else { statusBar_InsertLock.Text = "Insert Lock:[ON]"; }
                        break;
                    default: //MessageBox.Show(MyKeyboardHookStruct.vkCode.ToString());
                        break;
                } 
            }
            return 0;
        }
         
        private void MainAppWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            bool retKeyboard = true;

            if (hKeyboardHook != 0)
            {
                retKeyboard = UnhookWindowsHookEx(hKeyboardHook);
                hKeyboardHook = 0;
            }
            //如果卸下钩子失败   
            if (!(retKeyboard)) throw new Exception("卸下钩子失败！");
        }

        private void MainAppWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //实例化委托  
            //启动键盘钩子   
            if (hKeyboardHook == 0)
            {
                //实例化委托  
                KeyboardHookProcedure = new HookProc(KeyboardHookProc);
                Process curProcess = Process.GetCurrentProcess();
                ProcessModule curModule = curProcess.MainModule;
                hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, GetModuleHandle(curModule.ModuleName), 0);
            }
        }


        ////////////////////////////////////////////////////////////////////
        ///////////////////////树型生成即操作///////////////////////////
        ////////////////////////////////////////////////////////////////////
        BaseEntity _currentNode;
        /// <summary>  
        /// 鼠标右键功能菜单
        /// </summary>  
        /// <param name="_nodeName"></param>   
        private void TreeView_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {  //设置当前选择节点

            _viewModel.CurrentBaseEntity =  TreeView_Project.SelectedItem as BaseEntity;
            _currentNode = TreeView_Project.SelectedItem as BaseEntity;
             
        }

        /// <summary>  
        /// 树型加载方法
        /// </summary>  
        /// <param name="_nodeName"></param>   
        private void TreeView_Project_Loaded(object sender, RoutedEventArgs e)
        {
            //this.TreeView_Project.DataContext = _viewModel;
        }

        //展开所有节点
        private void ExpandTree()
        {
            if (this.TreeView_Project.Items != null && this.TreeView_Project.Items.Count > 0)
            {
                foreach (var item in this.TreeView_Project.Items)
                {
                    DependencyObject dependencyObject = this.TreeView_Project.ItemContainerGenerator.ContainerFromItem(item);
                    if (dependencyObject != null)//第一次打开程序，dependencyObject为null，会出错
                    {
                        ((TreeViewItem)dependencyObject).ExpandSubtree();
                    }
                }
            }
        }
        /// <summary>
        /// 菜单测试功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Test_Click(object sender, RoutedEventArgs e)
        { 
            ExpandTree();
            //display.Text = "";
            //_viewModel.NodeCollection[0].Name = "根目录";
            //_viewModel.NodeCollection[0].ProjectNote = "根目录信息备注";
            //display.Text = _viewModel.NodeCollection[0].Children[0].NAME;

           

        } 
 
    }
}
