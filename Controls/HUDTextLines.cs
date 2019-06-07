using System.Collections.Generic;
using NodeGames.Core;
using NodeGames.UI.Interfaces;

namespace NodeGames.UI.Controls
{
    internal class HUDTextLines<TColor> : HUDControl<TColor>
    {
        private readonly string _text;
        private readonly FontSize _fontSize;
        private readonly TColor _color;

        public HUDTextLines(Rectangle box, IEnumerable<string> text, FontSize fontSize, TColor color) : base(box)
        {
            _text = string.Join("\n", text);
            _fontSize = fontSize;
            _color = color;
        }

        protected override void Draw(IPainter<TColor> painter, Point currentCameraLocation)
        {
            HUDManager<TColor>.Painter.DrawString(_text, _fontSize, Box.Location + currentCameraLocation, _color);

            base.Draw(painter, currentCameraLocation);
        }
    }
}
