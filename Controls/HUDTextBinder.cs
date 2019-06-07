using System;
using NodeGames.Core;
using NodeGames.UI.Interfaces;

namespace NodeGames.UI.Controls
{
    internal class HUDTextBinder<TColor> : HUDControl<TColor>
    {
        private readonly FontSize _iFontSize;
        private readonly TColor _color;
        private readonly Func<string> _stringBinder;
        private readonly TextAlign _align;

        public HUDTextBinder(Point location, FontSize fontSize, TColor color, Func<string> stringBinder, TextAlign align = TextAlign.Left) : base(new Rectangle(location, Point.Zero))
        {
            _iFontSize = fontSize;
            _color = color;
            _stringBinder = stringBinder;
            _align = align;
        }

        protected override void Draw(IPainter<TColor> painter, Point currentCameraLocation)
        {
            var text = _stringBinder();

            var textAdjustPosition = new Point();
            if (_align != TextAlign.Left)
            {
                var size = HUDManager<TColor>.Painter.GetTextSize(_iFontSize, text);
                switch (_align)
                {
                    case TextAlign.Center:
                        textAdjustPosition.X = size.X / 2;
                        break;
                    case TextAlign.Right:
                        textAdjustPosition.X = size.X;
                        break;
                }
            }

            HUDManager<TColor>.Painter.DrawString(text, _iFontSize, Box.Location + currentCameraLocation - textAdjustPosition, _color);

            base.Draw(painter, currentCameraLocation);
        }
    }
}
