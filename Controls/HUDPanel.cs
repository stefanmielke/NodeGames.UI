using NodeGames.Core;
using NodeGames.UI.Interfaces;

namespace NodeGames.UI.Controls
{
    internal class HUDPanel<TColor> : HUDControl<TColor>
    {
        private readonly TColor _outColor;
        private readonly TColor _innerColor;

        public HUDPanel(Rectangle box, TColor outColor, TColor innerColor) : base(box)
        {
            _outColor = outColor;
            _innerColor = innerColor;
        }

        protected override void Draw(IPainter<TColor> painter, Point currentCameraLocation)
        {
            HUDManager<TColor>.Painter.DrawRectangle(Box.Location + currentCameraLocation, Box.Size, _innerColor);
            HUDManager<TColor>.Painter.DrawRectangleOutline(Box.Location + currentCameraLocation, Box.Size, _outColor);

            base.Draw(painter, currentCameraLocation);
        }
    }
}
