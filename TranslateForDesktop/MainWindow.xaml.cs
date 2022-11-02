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
            OriginHint.Text = languageFrom;
            OutputHint.Text = languageTo;
        }

        // 监视输入框的数据更新
        // data update in OriginData TextBox
        private void UpdateOriginData(object sender, TextChangedEventArgs args)
        {
            if (OriginBox.Text == "")
            {
                OriginHint.Text = languageFrom;
            }
            else
            {
                OriginHint.Text = "";
            }
        }

        // 监视输出框的数据更新
        // data update in OutputData TextBox
        private void UpdateOutputData(object sender, TextChangedEventArgs args)
        {
            if (OutputBox.Text == "")
            {
                OutputHint.Text = languageTo;
            }
            else
            {
                OutputHint.Text = "";
            }
        }

        // 键盘事件
        // respond to keyboard events
        // Enter - translate;
        // Tab - exchange between English and Chinese;
        // Escape - clear both OriginData TextBox and OutputData TextBox;
        // F2 - close the window;
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                String originData = GetOriginData();
                CheckLanguage();
                String res = Translate(originData);
                ShowResult(res);
            }
            else if (e.Key == Key.Tab)
            {
                String stmp = languageTo;
                languageTo = languageFrom;
                languageFrom = stmp;
                OriginBox.Text = "";
                OutputBox.Text = "";
                OriginHint.Text = languageFrom;
                OutputHint.Text = languageTo;
            }
            else if (e.Key == Key.Escape)
            {
                OriginBox.Text = "";
                OutputBox.Text = "";
            }
            else if(e.Key == Key.F2)
            {
                this.Close();
            }
        }
        
        // 获取输入框输入的内容
        // get data from OriginData TextBox
        private String GetOriginData()
        {
            String originData = "";
            for(int line = 0; line < OriginBox.LineCount; line++)
            {
                originData += "" + OriginBox.GetLineText(line);
            }
            return originData;
        }

        // 自动检测语言（中or英）
        // detect language automatically
        private void CheckLanguage()
        {

        }

        // 翻译
        // translate from origin string s to output string res
        private String Translate(String s)
        {
            Request req = new Request();
            String res = req.Translate(s, languageFrom, languageTo).trans_result[0].dst;
            return res;
        }

        // 在输出框显示翻译结果
        // show result in OutputData TextBox
        private void ShowResult(String s)
        {
            OutputBox.Text = s;
        }

        // 点击按钮切换翻译方式
        // change between English and Chinese
        private void ExchangeBtn_Click(object sender, RoutedEventArgs e)
        {
            String stmp = languageTo;
            languageTo = languageFrom;
            languageFrom = stmp;
            OriginBox.Text = "";
            OutputBox.Text = "";
            OriginHint.Text = languageFrom;
            OutputHint.Text = languageTo;
        }

        // 点击按钮清空输入框和输出框
        // clear OriginData TextBox and OutputData TextBox
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            OriginBox.Text = "";
            OutputBox.Text = "";
        }

        // 记录鼠标按下位置
        // get the position of mouse done
        private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs s)
        {
            _pressedPosition = s.GetPosition(this);
        }

        // drag the window with mouse move
        private void Window_PreviewMouseMove(object sender, MouseEventArgs s)
        {
            if(Mouse.LeftButton==MouseButtonState.Pressed && _pressedPosition != s.GetPosition(this))
            {
                _isDragMoved = true;
                DragMove();
            }
        }

        // stop drag
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
