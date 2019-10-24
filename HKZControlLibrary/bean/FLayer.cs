using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HKZControlLibrary.bean
{
    public class FLayer: INotifyPropertyChanged
    {
        public FLayer(StrokeCollection strokes, string id, RenderTargetBitmap bitmap)
        {
            FStrokes = strokes;
            FId = id;
            FBitmap = bitmap;
        }

        public string FId { get; set; }

        private bool fAdd;
        public bool FAdd { get {
                return FId == "+";
            } }

        public StrokeCollection FStrokes {get;set;}

        private RenderTargetBitmap fBitmap;

        public RenderTargetBitmap FBitmap
        {
            set
            {
                UpdateProperty(ref fBitmap, value);
            }
            get
            {
                return fBitmap;
            }
        }


        private void UpdateProperty<T>(ref T properValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (object.Equals(properValue, newValue))
            {
                return;
            }
            properValue = newValue;

            OnPropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
