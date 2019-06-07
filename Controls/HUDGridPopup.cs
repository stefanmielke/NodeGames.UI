using NodeGames.Core;
using NodeGames.UI.Interfaces;

namespace NodeGames.UI.Controls
{
    internal class HUDGridPopup<TColor> : HUDPanel<TColor>
    {
        public readonly int BorderLeft;
        public readonly int BorderRight;
        public readonly int BorderTop;
        public readonly int SpaceWidth;
        public readonly int SpaceHeight;

        private int _currentX;
        private int _currentY;

        public HUDGridPopup(Rectangle box, int borderLeft, int borderRight, int borderTop, int spaceWidth, int spaceHeight,
            TColor outColor, TColor innerColor) : base(box, outColor, innerColor)
        {
            BorderLeft = borderLeft;
            BorderRight = borderRight;
            BorderTop = borderTop;
            SpaceWidth = spaceWidth;
            SpaceHeight = spaceHeight;

            _currentX = BorderLeft;
            _currentY = BorderTop;
        }

        public void AddGridControl(HUDControl<TColor> control)
        {
            control.Box = new Rectangle(control.Box.X + _currentX, control.Box.Y + _currentY, control.Box.Width, control.Box.Height);
            AddNewHUD(control);

            _currentX += control.Box.Width + SpaceWidth;
            if (_currentX + control.Box.Width + BorderRight > Box.Width)
            {
                _currentX = BorderLeft;
                _currentY += control.Box.Height + SpaceHeight;
            }
        }

        protected override void Draw(IPainter<TColor> painter, Point currentCameraLocation)
        {
            _currentX = BorderLeft;
            _currentY = BorderTop;

            foreach (var control in Controls)
            {
                control.Box = new Rectangle(Box.X + _currentX, Box.Y + _currentY, control.Box.Width, control.Box.Height);

                _currentX += control.Box.Width + SpaceWidth;
                if (_currentX + control.Box.Width + BorderRight > Box.Width)
                {
                    _currentX = BorderLeft;
                    _currentY += control.Box.Height + SpaceHeight;
                }
            }

            base.Draw(painter, currentCameraLocation);
        }
    }
}
