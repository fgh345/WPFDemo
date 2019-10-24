using HKZControlLibrary;
using HKZControlLibrary.bean;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TangCeForWindow
{
    public partial class MainWindow : Window, ToolsBoxInterface
    {
        private DrawingAttributes drawingAttributes;//画板设置对象

        private bool isPaint = false; //标记是否已经有绘画轨迹

        private InkCanvasEditingMode? inkCanvasEditingModeSave = null;//记录画笔当前状态

        private bool isStartDropTools = false; //标记是否开始拖动工具栏

        private Point ToolBoxRePos; //点击时  鼠标或触摸点相对工具栏的坐标点

        private Point MouseRePos; //点击时  鼠标全局的坐标点

        private ObservableCollection<FLayer> StrokesList = new ObservableCollection<FLayer>();

        private int layerIndex = 0; //当前图层索引

        Stack<StrokeCollection> tempList = new Stack<StrokeCollection>();//记录操作

        // 拖动工具栏
        // NowPos 相对于ToolBoxContainer 点击 或触摸 的点 
        private void DragToolsBox(System.Windows.Point NowPos)
        {
            if (isStartDropTools)
            {
                var NLeft = NowPos.X - ToolBoxRePos.X;
                var NTop = NowPos.Y - ToolBoxRePos.Y;
                ToolsBox.SetValue(Canvas.TopProperty, NTop);
                ToolsBox.SetValue(Canvas.LeftProperty, NLeft);

            }
        }

        //颜色改变监听
        public void ColorChange(Brush brush)
        {
            Color BrushColor = ((SolidColorBrush)brush).Color;
            drawingAttributes.Color = BrushColor;
        }

        //画笔大小改变
        public void SizeChange(int size) {
            drawingAttributes.Width = size;
            drawingAttributes.Height = size;
        }

        //画笔模式
        public void PaintMode()
        {
            InkCanvasBoard.EditingMode = InkCanvasEditingMode.Ink;

            Grid_Body.IsManipulationEnabled = true;
        }

        //开始 拖动工具箱
        public void StartDropToolBox(Point point)
        {
            isStartDropTools = true;

            ToolBoxRePos = point;
        }

        //显示桌面
        public void WindowDesktopMode(bool flag) {
            if (flag)
            {
                Grid_Bk.Visibility = Visibility.Visible;
            }
            else
            {
                Grid_Bk.Visibility = Visibility.Collapsed;
            }
        }

        //工具箱 展开 折叠
        public void ToolsBoxToggle(bool flag) {
            if (flag)
            {
                InkCanvasBoard.Visibility = Visibility.Visible;
                StackPanel_Box.Visibility = Visibility.Visible;
            }
            else {
                InkCanvasBoard.Visibility = Visibility.Collapsed;
                StackPanel_Box.Visibility = Visibility.Collapsed;
            }
        }

        //橡皮擦模式 
        public void EraserMode()
        {
            InkCanvasBoard.EditingMode = InkCanvasEditingMode.EraseByPoint;

            InkCanvasBoard.EraserShape = new EllipseStylusShape(30, 30);

            Grid_Body.IsManipulationEnabled = true;
        }

        //垃圾箱模式 
        public void TrashMode(String flag)
        {
            
            if ("true".Equals(flag))
            {
                if (InkCanvasBoard.GetSelectedStrokes().Count > 0)
                {
                    InkCanvasBoard.Strokes.Remove(InkCanvasBoard.GetSelectedStrokes());
                }
                else
                {
                    InkCanvasBoard.Strokes.Clear();
                }

                StrokesList[layerIndex].FBitmap = SaveImage();//保存截图

                return;
            }

            InkCanvasBoard.EditingMode = InkCanvasEditingMode.Select;

            Grid_Body.IsManipulationEnabled = false;
        }

        //生成截图方法
        private RenderTargetBitmap SaveImage()
        {
            //Thread.Sleep(50);//干复杂的事情

            RenderTargetBitmap targetBitmap = new RenderTargetBitmap((int)Grid_Body.ActualWidth, (int)Grid_Body.ActualHeight, 0, 0, PixelFormats.Default);

            targetBitmap.Render(Grid_Body);

            return targetBitmap;
        }
    }
}
