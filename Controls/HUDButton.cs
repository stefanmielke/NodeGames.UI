using NodeGames.Core;
using NodeGames.UI.Interfaces;

namespace NodeGames.UI.Controls
{
    internal class HUDButton<TColor> : HUDControl<TColor>
    {
        private readonly TColor _outColor;
        private readonly TColor _innerColor;
        private readonly TColor _outColorHovered;
        private readonly TColor _innerColorHovered;

        private bool _isHovered;

        public HUDButton(Rectangle box, TColor outColor, TColor innerColor, TColor outColorHovered, TColor innerColorHovered) : base(box)
        {
            _outColor = outColor;
            _innerColor = innerColor;
            _outColorHovered = outColorHovered;
            _innerColorHovered = innerColorHovered;

            _isHovered = false;
        }

        protected override void OnMouseEnter()
        {
            base.OnMouseEnter();

            _isHovered = true;
        }

        protected override void OnMouseLeave()
        {
            base.OnMouseLeave();

            _isHovered = false;
        }

        protected override void Draw(IPainter<TColor> painter, Point currentCameraLocation)
        {
            HUDManager<TColor>.Painter.DrawRectangle(Box.Location + currentCameraLocation, Box.Size, _isHovered ? _innerColorHovered : _innerColor);
            HUDManager<TColor>.Painter.DrawRectangleOutline(Box.Location + currentCameraLocation, Box.Size, _isHovered ? _outColorHovered : _outColor);

            base.Draw(painter, currentCameraLocation);
        }
    }
}
