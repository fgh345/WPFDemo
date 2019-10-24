using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HKZControlLibrary;
using HKZControlLibrary.bean;

namespace TangCeForWindow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            ToolsBox.SetToolsBoxEvent(this);

            InitInkCanvas();
        }

        private void InitInkCanvas()
        {
            drawingAttributes = new DrawingAttributes();
            drawingAttributes.Color = Colors.Red;
            drawingAttributes.Width = 3;
            drawingAttributes.Height = 3;

            InkCanvasBoard.DefaultDrawingAttributes = drawingAttributes;
        }

        private void Grid_Body_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            int PointCount = e.Manipulators.Count();

            //Console.WriteLine("Grid_ManipulationDelta");

            StrokeCollection strokes = InkCanvasBoard.Strokes;//墨迹集合

            Matrix matrix = new Matrix();

            if (PointCount > 1 && isPaint == false)
            {

                if (inkCanvasEditingModeSave == null)
                {
                    inkCanvasEditingModeSave = InkCanvasBoard.EditingMode;
                    InkCanvasBoard.EditingMode = InkCanvasEditingMode.None;
                }

                if (PointCount == 2)
                {

                    // Rotate the Rectangle.
                    matrix.RotateAt(e.DeltaManipulation.Rotation,
                                        e.ManipulationOrigin.X,
                                         e.ManipulationOrigin.Y);

                    // Resize the Rectangle.  Keep it square 
                    // so use only the X value of Scale.
                    matrix.ScaleAt(e.DeltaManipulation.Scale.X,
                                        e.DeltaManipulation.Scale.X,
                                        e.ManipulationOrigin.X,
                                        e.ManipulationOrigin.Y);

                    // Move the Rectangle.
                    matrix.Translate(e.DeltaManipulation.Translation.X,
                                          e.DeltaManipulation.Translation.Y);
                }
                else if (PointCount > 2
                    && PointCount < 6)
                {
                    // Move the Rectangle.
                    matrix.Translate(e.DeltaManipulation.Translation.X,
                                          e.DeltaManipulation.Translation.Y);
                }

            }
            else
            {
                isPaint = true;
            }

            // Apply the changes to the Rectangle.
            strokes.Transform(matrix, false);
        }

        private void Grid_Body_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            isPaint = false;

            if (inkCanvasEditingModeSave != null) {
                InkCanvasBoard.EditingMode = inkCanvasEditingModeSave ?? InkCanvasEditingMode.None;
                inkCanvasEditingModeSave = null;
            }
            
        }

        private void CanvasControlPanel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MouseRePos = e.GetPosition(CanvasControlPanel);
        }

        private void CanvasControlPanel_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isStartDropTools = false;

            if(!MouseRePos.Equals(e.GetPosition(CanvasControlPanel)))//如果有移動 取消工具欄點擊事件
                ToolsBox.CancelBtnToolsClick();

            //Console.WriteLine("CanvasControlPanel_PreviewMouseUp");
        }

        private void CanvasControlPanel_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            DragToolsBox(e.GetPosition(CanvasControlPanel));
        }

        private void InkCanvasBoard_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            StrokesList[layerIndex].FBitmap = SaveImage();//保存截图
        }

        private void InkCanvasBoard_StrokeErased(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("InkCanvasBoard_StrokeErased:"+ InkCanvasBoard.Strokes.Count);

            StrokesList[layerIndex].FBitmap = SaveImage();//保存截图
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StrokesList.Add(new FLayer(InkCanvasBoard.Strokes, StrokesList.Count + 1 + "", SaveImage()));
            StrokesList.Add(new FLayer(new StrokeCollection(), "+", SaveImage()));
            StrokeLayerListView_Menu.ItemsSource = StrokesList;
            StrokeLayerListView_Menu.SelectedIndex = 0;
        }

        private void StrokeLayerListView_Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            StrokeLayerListView imgListView = sender as StrokeLayerListView;

            int index = imgListView.SelectedIndex;

            ObservableCollection<FLayer> fLayers = imgListView.ItemsSource as ObservableCollection<FLayer>;

            if (index == fLayers.Count - 1 && fLayers[index].FId == "+")
            {
                //添加新图层
                fLayers.Insert(index, new FLayer(new StrokeCollection(), StrokesList.Count + "", null));//创建新图层

                imgListView.SelectedIndex = index;

                fLayers[index].FBitmap = SaveImage();//给新图层 添加背景色  不然全是透明的

                InkCanvasBoard.Strokes = fLayers[index].FStrokes;//读取路径

            }
            else if (index != layerIndex)
            {
                InkCanvasBoard.Strokes = fLayers[index].FStrokes;//读取路径
            }

            layerIndex = StrokeLayerListView_Menu.SelectedIndex;
        }

        private void InkCanvasBoard_Selection(object sender, EventArgs e)
        {
            StrokesList[layerIndex].FBitmap = SaveImage();//保存截图
        }

    }
}
