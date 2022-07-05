using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyComputerManager.Controls
{
    public class ClippingBorder : Border
    {
        protected override void OnRender(DrawingContext dc)
        {
            OnApplyChildClip();
            base.OnRender(dc);
        }

        public override UIElement Child
        {
            get
            {
                return base.Child;
            }
            set
            {
                if (this.Child != value)
                {
                    if (this.Child != null)
                    {
                        // Restore original clipping
                        this.Child.SetValue(UIElement.ClipProperty, _oldClip);
                    }

                    if (value != null)
                    {
                        _oldClip = value.ReadLocalValue(UIElement.ClipProperty);
                    }
                    else
                    {
                        // If we dont set it to null we could leak a Geometry object
                        _oldClip = null;
                    }

                    base.Child = value;
                }
            }
        }

        protected virtual void OnApplyChildClip()
        {
            UIElement child = this.Child;
            if (child != null)
            {
                _clipRect.RadiusX = _clipRect.RadiusY = Math.Max(0.0, this.CornerRadius.TopLeft - (this.BorderThickness.Left * 0.5));
                Rect rect = new Rect(this.RenderSize);
                rect.Height -= (this.BorderThickness.Top + this.BorderThickness.Bottom);
                rect.Width -= (this.BorderThickness.Left + this.BorderThickness.Right);
                _clipRect.Rect = rect;
                child.Clip = _clipRect;
            }
        }

        public void Update() { OnApplyChildClip(); }

        private RectangleGeometry _clipRect = new RectangleGeometry();
        private object _oldClip;
    }
}
