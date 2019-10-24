using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HKZControlLibrary
{
   public interface ToolsBoxInterface
    {
        //设置画笔颜色
        void ColorChange(Brush brush);

        //设置画笔大小
        void SizeChange(int size);

        //画笔模式
        void PaintMode();

        //開始拖動工具欄
        void StartDropToolBox(System.Windows.Point point);

        //桌面模式
        void WindowDesktopMode(bool flag);

        //工具栏 展开 折叠
        void ToolsBoxToggle(bool flag);

        //橡皮模式
        void EraserMode();

        //垃圾箱模式
        void TrashMode(String flag);
    }
}
