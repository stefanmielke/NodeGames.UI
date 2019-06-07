using NodeGames.Core;
using NodeGames.UI.Interfaces;

namespace NodeGames.UI.Controls
{
    internal class HUDText<TColor> : HUDControl<TColor>
    {
        private readonly string _text;
        private readonly FontSize _fontSize;
        protected TColor Color;

        public HUDText(Point location, string text, FontSize fontSize, TColor color, TextAlign align = TextAlign.Left) : base(new Rectangle(location, Point.Zero))
        {
            _text = text;
            _fontSize = fontSize;
            Color = color;

            var textSize = HUDManager<TColor>.Painter.GetTextSize(fontSize, text);

            switch (align)
            {
                case TextAlign.Center:
                    Box.X -= textSize.X / 2;
                    break;
                case TextAlign.Right:
                    Box.X -= textSize.X;
                    break;
            }
        }

        protected override void Draw(IPainter<TColor> painter, Point currentCameraLocation)
        {
            HUDManager<TColor>.Painter.DrawString(_text, _fontSize, Box.Location + currentCameraLocation, Color);

            base.Draw(painter, currentCameraLocation);
        }
    }
}
