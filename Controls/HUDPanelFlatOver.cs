using NodeGames.Core;
using NodeGames.UI.Interfaces;

namespace NodeGames.UI.Controls
{
    internal class HUDPanelFlatOver<TColor> : HUDControl<TColor>
    {
        private TColor _currentColor;
        private readonly TColor _color;
        private readonly TColor _overColor;

        public HUDPanelFlatOver(Rectangle box, TColor color, TColor overColor) : base(box)
        {
            _currentColor = color;
            _color = color;
            _overColor = overColor;
        }

        protected override void OnMouseEnter()
        {
            base.OnMouseEnter();

            _currentColor = _overColor;
        }

        protected override void OnMouseLeave()
        {
            base.OnMouseLeave();

            _currentColor = _color;
        }

        protected override void Draw(IPainter<TColor> painter, Point currentCameraLocation)
        {
            HUDManager<TColor>.Painter.DrawRectangle(Box.Location + currentCameraLocation, Box.Size, _currentColor);

            base.Draw(painter, currentCameraLocation);
        }
    }
}
