using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;//用于加载dll
using System.IO;//用于获取多个文件名
using Microsoft.Win32;//用于打开文件选择对话框
using System.Windows.Forms; //用于读取文件

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        int count = 0;//第几个文件的序号
        int fileOpenFlag = 0;
        string[] fileSave;

        public MainWindow()
        {
            InitializeComponent();//初始化窗口
        }

        private void Button_Click(object sender, RoutedEventArgs e)//按钮事件
        {
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();//打开文件夹选择对话框

            if (result == System.Windows.Forms.DialogResult.Cancel)
                return;
            String path = m_Dialog.SelectedPath.Trim();//获取路径
            var files = Directory.GetFiles(path, "*.txt");//获取所有满足条件的文件名
            if (files == null)//如果获取失败
            {
                MessageBoxResult result1 = System.Windows.MessageBox.Show(path + "目录下没有txt文件", "注意", MessageBoxButton.YesNo);
                return;
            }
            if (files.Length <= 0)//如果没有满足条件的文件
            {
                MessageBoxResult result1 = System.Windows.MessageBox.Show(path + "目录下没有txt文件", "注意", MessageBoxButton.YesNo);
                if (result1 == MessageBoxResult.Yes)
                    return;
                else
                    return;
            }
            fileSave = files;//所有文件的名称存档
            fileOpenFlag = 1;
            string strReadFilePath = fileSave[0];
            count = 0;
            lblFileName.Text = strReadFilePath;//文本框显示内容：文件路径
            updateText(strReadFilePath);//调用自己实现的函数：更新文本框中文件内容
        }

        private void Button_Click1(object sender, RoutedEventArgs e)//按钮事件
        {
            if (fileOpenFlag == 0)
                return;
            if (count >= 1)
                count--;//文件序号减一
            else
                return;
            string strReadFilePath = fileSave[count];
            lblFileName.Text = strReadFilePath;
            updateText(strReadFilePath);
        }

        private void Button_Click2(object sender, RoutedEventArgs e)//按钮事件
        {
            if (fileOpenFlag == 0)
                return;
            if (count >= fileSave.Length - 1)
                return;
            count++;//文件序号加一
            string strReadFilePath = fileSave[count];
            lblFileName.Text = strReadFilePath;
            updateText(strReadFilePath);
        }
        
        public void updateText(string strReadFilePath)
        {
            if (strReadFilePath == null)
                return;
            if (strReadFilePath.Length <= 0)
                return;
            txts.Clear();

            //TextBlock是否能换行，Wrap为能换行  
            txts.TextWrapping = TextWrapping.Wrap;
            //字体大小  
            txts.FontSize = 14;
            //边距  
            txts.Margin = new Thickness(4);
            //字体颜色  
            txts.Foreground = Brushes.Black;
            //获取文本内容
            System.IO.StreamReader st;
            st = new System.IO.StreamReader(strReadFilePath, System.Text.Encoding.Default);//默认编码
            txts.Text = st.ReadToEnd();
            st.Close();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)//快捷键事件，前台定义在窗体中
        {
            try
            {
                if (e.Key == Key.Left)//如果是方向左键
                    Button_Click1(this, null);
                if (e.Key == Key.Right)
                    Button_Click2(this, null);
                if (e.Key == Key.Down)
                {
                    //int line = txts.GetLastVisibleLineIndex();
                    ////txts.ScrollToLine(line + 1);
                    txts.LineDown();
                }
                if (e.Key == Key.Up)
                {
                    //int line = txts.GetFirstVisibleLineIndex();
                    //txts.ScrollToLine(line - 1);     
                    txts.LineUp();
                }
            }
            catch (Exception){}
        }

    }
}

