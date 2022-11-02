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
        private string languageFrom = "en";
        private string languageTo = "zh";

        public MainWindow()
        {
            InitializeComponent();
        }

        // 窗口置顶
        private void Window_Deactivated(object sender, EventArgs e)
        {
            Window window = sender as Window;
            window.Topmost = true;
        }

        // 监视输入框的数据更新
        //private void UpdateOriginData(object sender, TextChangedEventArgs args)
        //{
        //    String originData = GetOriginData();
        //    CheckLanguage();
        //    String res = Translate(originData);
        //    ShowResult(res);
        //}

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                String originData = GetOriginData();
                CheckLanguage();
                String res = Translate(originData);
                ShowResult(res);
            }
        }
        
        // 获取输入框输入的内容
        private String GetOriginData()
        {
            return OriginBox.GetLineText(0);
        }

        // 自动检测语言（中or英）
        private void CheckLanguage()
        {

        }

        // 翻译
        private String Translate(String s)
        {
            Request req = new Request();
            String res = req.Translate(s, languageFrom, languageTo).trans_result[0].dst;
            return res;
        }

        // 在输出框显示翻译结果
        private void ShowResult(String s)
        {
            OutputBox.Text = s;
        }

        // 点击按钮切换翻译方式
        private void ExchangeBtn_Click(object sender, RoutedEventArgs e)
        {
            String stmp = languageTo;
            languageTo = languageFrom;
            languageFrom = stmp;
        }

        // 点击按钮清空输入框和输出框
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            OriginBox.Text = "";
            OutputBox.Text = "";
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
