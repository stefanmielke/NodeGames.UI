using NodeGames.Core;
using NodeGames.UI.Interfaces;

namespace NodeGames.UI.Controls
{
    internal class HUDPanelFlat<TColor> : HUDControl<TColor>
    {
        private readonly TColor _color;
        
        public HUDPanelFlat(Rectangle box, TColor color) : base(box)
        {
            _color = color;
        }

        protected override void Draw(IPainter<TColor> painter, Point currentCameraLocation)
        {
            HUDManager<TColor>.Painter.DrawRectangle(Box.Location + currentCameraLocation, Box.Size, _color);

            base.Draw(painter, currentCameraLocation);
        }
    }
}
