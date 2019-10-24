using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HKZControlLibrary
{
    /// <summary>
    /// ToolsBox.xaml 的交互逻辑
    /// </summary>
    public partial class ToolsBox : UserControl
    {

        private ToolsBoxInterface toolsBoxEvent;

        private bool isCancelClick = false;//是否取消主工具按钮点击事件

        public ToolsBox()
        {
            InitializeComponent();
        }

        public void SetToolsBoxEvent(ToolsBoxInterface tevent) {
            toolsBoxEvent = tevent;
        }

        public void CancelBtnToolsClick() {
            isCancelClick = true;
        }
        /// <summary>
        /// 主工具按钮点击事件
        /// </summary>
        private void BtnTools_Click(object sender, RoutedEventArgs e)
        {

            //Console.WriteLine("BtnTools_Click");

            if (isCancelClick) {
                isCancelClick = false;
                return;
            }

            Button button = (sender as Button);
            String tag = button.Tag.ToString();

            if (tag.Equals("open"))
            {

                //折叠
                MoveAnimation(BtnDeskTop, 0, 82, 82, 82, 0.4);
                MoveAnimation(BtnConf, 41, 82, 153, 82, 0.4);
                MoveAnimation(BtnTrash, 123, 82, 153, 82, 0.4);
                MoveAnimation(BtnEraser, 164, 82, 82, 82, 0.4);
                MoveAnimation(BtnWhiteBoard, 41, 82, 11, 82, 0.4);
                MoveAnimation(GridBtnPencil, 123, 82, 11, 82, 0.4);

                RotateTransform rtf = new RotateTransform();
                rtf.CenterX = Canvas_Menu_Fun.ActualWidth / 2;
                rtf.CenterY = Canvas_Menu_Fun.ActualHeight / 2;
                Canvas_Menu_Fun.RenderTransform = rtf;
                DoubleAnimation dbAscending = new DoubleAnimation(0, -50, new Duration(TimeSpan.FromSeconds(0.4)));
                rtf.BeginAnimation(RotateTransform.AngleProperty, dbAscending);

                button.Tag = "close";

                toolsBoxEvent.ToolsBoxToggle(false);
            }
            else if (tag.Equals("close"))
            {
                //展开
                MoveAnimation(BtnDeskTop, 82, 0, 82, 82, 0.4);
                MoveAnimation(BtnConf, 82, 41, 82, 153, 0.4);
                MoveAnimation(BtnTrash, 82, 123, 82, 153, 0.4);
                MoveAnimation(BtnEraser, 82, 164, 82, 82, 0.4);
                MoveAnimation(BtnWhiteBoard, 82, 41, 82, 11, 0.4);
                MoveAnimation(GridBtnPencil, 82, 123, 82, 11, 0.4);

                RotateTransform rtf = new RotateTransform();
                rtf.CenterX = Canvas_Menu_Fun.ActualWidth / 2;
                rtf.CenterY = Canvas_Menu_Fun.ActualHeight / 2;
                Canvas_Menu_Fun.RenderTransform = rtf;
                DoubleAnimation dbAscending = new DoubleAnimation(-50, 0, new Duration(TimeSpan.FromSeconds(0.4)));
                rtf.BeginAnimation(RotateTransform.AngleProperty, dbAscending);

                button.Tag = "open";

                toolsBoxEvent.ToolsBoxToggle(true);
            }
            else if (tag.Equals("select"))
            {
                //收起
                MoveAnimation(GridBtnPencil, 123, 82, 11, 82, 0.3);

                DoubleAnimation ColorHide = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.4)));
                ColorHide.Completed += ColorHideCompleted;
                Canvas_Menu_Color.BeginAnimation(Canvas.OpacityProperty, ColorHide);

                button.Tag = "select_no";

                toolsBoxEvent.ToolsBoxToggle(false);
            }
            else if (tag.Equals("select_no")) {

                //展开

                MoveAnimation(GridBtnPencil, 82, 123, 82, 11, 0.2);

                Canvas_Menu_Color.Visibility = Visibility.Visible;

                DoubleAnimation ColorShow = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.4)));
                Canvas_Menu_Color.BeginAnimation(Canvas.OpacityProperty, ColorShow);

                button.Tag = "select";

                toolsBoxEvent.ToolsBoxToggle(true);
            }

            }

        /// <summary>
        /// 按钮位置动画
        /// </summary>
        private void MoveAnimation(FrameworkElement panel, double fromValue1, double toValue1, double fromValue2, double toValue2, double duration)
        {

            DoubleAnimation doubleAnimation1 = new DoubleAnimation(fromValue1, toValue1, new Duration(TimeSpan.FromSeconds(duration)));

            DoubleAnimation doubleAnimation2 = new DoubleAnimation(fromValue2, toValue2, new Duration(TimeSpan.FromSeconds(duration)));

            panel.BeginAnimation(Canvas.TopProperty, doubleAnimation1);

            panel.BeginAnimation(Canvas.LeftProperty, doubleAnimation2);
        }

        /// <summary>
        /// 画笔按钮点击事件
        /// </summary>
        private void BtnPencil_Click(object sender, RoutedEventArgs e)
        {
            Button button = (sender as Button);
            String tag = button.Tag.ToString();

            if (tag.Equals("true"))
            {

                Canvas_Menu_Color.Visibility = Visibility.Visible;

                DoubleAnimation ColorShow = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.4)));
                Canvas_Menu_Color.BeginAnimation(Canvas.OpacityProperty, ColorShow);


                MoveAnimation(BtnDeskTop, 0, 82, 82, 82, 0.4);
                MoveAnimation(BtnConf, 41, 82, 153, 82, 0.4);
                MoveAnimation(BtnTrash, 123, 82, 153, 82, 0.4);
                MoveAnimation(BtnEraser, 164, 82, 82, 82, 0.4);
                MoveAnimation(BtnWhiteBoard, 41, 82, 11, 82, 0.4);

                button.Tag = "select";
                BtnTools.Tag = "select";

            }
            else if (tag.Equals("select"))
            {
                DoubleAnimation ColorHide = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.4)));
                ColorHide.Completed += ColorHideCompleted;
                Canvas_Menu_Color.BeginAnimation(Canvas.OpacityProperty, ColorHide);


                MoveAnimation(BtnDeskTop, 82, 0, 82, 82, 0.4);
                MoveAnimation(BtnConf, 82, 41, 82, 153, 0.4);
                MoveAnimation(BtnTrash, 82, 123, 82, 153, 0.4);
                MoveAnimation(BtnEraser, 82, 164, 82, 82, 0.4);
                MoveAnimation(BtnWhiteBoard, 82, 41, 82, 11, 0.4);

                button.Tag = "true";
                BtnTools.Tag = "open";
            }
            else {
                button.Tag = "true";
                //进入画笔模式

                toolsBoxEvent.PaintMode();
                BtnEraser.Tag = "false";
                BtnTrash.Tag = "false";
            }

            
        }

        private void ColorHideCompleted(object sender, EventArgs e)
        {
            Canvas_Menu_Color.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// 颜色选择
        /// </summary>
        private void RadioButton_Checked_Color(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            String color = radioButton.Tag.ToString();

            BrushConverter brushConverter = new BrushConverter();
            Brush brush = (Brush)brushConverter.ConvertFromString(color);

            this.RadioButton_Size2.Tag = color;
            this.RadioButton_Size3.Tag = color;
            this.RadioButton_Size5.Tag = color;
            this.RadioButton_Size8.Tag = color;
            this.RadioButton_Size12.Tag = color;


            Ellipse_Pen_Color.Fill = brush;

            if (toolsBoxEvent!=null) {
                toolsBoxEvent.ColorChange(brush);
            }
        }

        /// <summary>
        /// 画笔大小选择
        /// </summary>
        private void RadioButton_Checked_Size(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            int size = int.Parse(radioButton.DataContext.ToString());

            Ellipse_Pen_Color.Width = size;
            Ellipse_Pen_Color.Height = size;


            toolsBoxEvent.SizeChange(size);
        }

        /// <summary>
        /// 主工具按钮按下事件
        /// </summary>
        private void BtnTools_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            toolsBoxEvent.StartDropToolBox(e.GetPosition(GridBox));
        }

        /// <summary>
        /// 桌面按钮点击事件
        /// </summary>
        private void BtnDeskTop_Click(object sender, RoutedEventArgs e)
        {
            toolsBoxEvent.WindowDesktopMode(!(sender as ToggleButton).IsChecked??false);
        }

        /// <summary>
        /// 橡皮按钮点击事件
        /// </summary>
        private void BtnEraser_Click(object sender, RoutedEventArgs e)
        {
            toolsBoxEvent.EraserMode();
            BtnEraser.Tag = "true";
            BtnPencil.Tag = "false";
            BtnTrash.Tag = "false";
        }

        /// <summary>
        /// 垃圾桶按钮点击事件
        /// </summary>
        private void BtnTrash_Click(object sender, RoutedEventArgs e)
        {
            toolsBoxEvent.TrashMode(BtnTrash.Tag.ToString());
            BtnTrash.Tag = "true";
            BtnPencil.Tag = "false";
            BtnEraser.Tag = "false";
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RadioButton_Color_Red.IsChecked = true;
            RadioButton_Size3.IsChecked = true;
        }
    }
}
