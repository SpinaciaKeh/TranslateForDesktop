using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

namespace TranslateForDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point _pressedPosition;
        private bool _isDragMoved = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        // 窗口置顶
        private void Window_Deactivated(object sender, EventArgs e)
        {
            Window window = sender as Window;
            window.Topmost = true;
            // Console.WriteLine("put it in top level");
        }

        // 监视输入框的数据更新
        private void UpdateOriginData(object sender, TextChangedEventArgs args)
        {
            StringCollection originData = new StringCollection();
            originData = GetOriginData();
            CheckLanguage();
            Translate();
            ShowResult(originData);
        }
        
        // 获取输入框输入的内容
        private StringCollection GetOriginData()
        {
            StringCollection lines = new StringCollection();
            int lineCount = OriginBox.LineCount;

            for(int line = 0; line < lineCount; line++)
            {
                lines.Add(OriginBox.GetLineText(line));
            }

            return lines;
        }

        // 自动检测语言（中or英）
        private void CheckLanguage()
        {

        }

        // 翻译
        private void Translate()
        {
            var translator=new Goo
        }

        // 在输出框显示翻译结果
        private void ShowResult(StringCollection originData)
        {
            foreach(String s in originData)
            {
                OutputBox.Text = s;
            }
            // OutputBox.Text = "This is an English sequence.";
        }

        // 点击按钮切换翻译方式
        private void ExchangeBtn_Click(object sender, RoutedEventArgs e)
        {
            StringCollection str = new StringCollection();
            str.Add("test");
            ShowResult(str);
        }

        // 记录鼠标按下位置
        private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs s)
        {
            _pressedPosition = s.GetPosition(this);
        }

        private void Window_PreviewMouseMove(object sender, MouseEventArgs s)
        {
            if(Mouse.LeftButton==MouseButtonState.Pressed && _pressedPosition != s.GetPosition(this))
            {
                _isDragMoved = true;
                DragMove();
            }
        }

        private void Window_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isDragMoved)
            {
                _isDragMoved = false;
                e.Handled = true;
            }
        }
    }
}
