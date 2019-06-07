using NodeGames.Core;

namespace NodeGames.UI.Controls
{
    internal class HUDGrid<TColor> : HUDPanel<TColor>
    {
        public int BorderLeft;
        public int BorderRight;
        public int BorderTop;
        public readonly int SpaceWidth;
        public readonly int SpaceHeight;

        private int _currentX;
        private int _currentY;

        public HUDGrid(Rectangle box, int borderLeft, int borderRight, int borderTop, int spaceWidth, int spaceHeight,
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

        public void ResetGrid()
        {
            _currentX = BorderLeft;
            _currentY = BorderTop;

            foreach (var control in Controls)
            {
                _currentX += control.Box.Width + SpaceWidth;
                if (_currentX + control.Box.Width + BorderRight > Box.Width)
                {
                    _currentX = BorderLeft;
                    _currentY += control.Box.Height + SpaceHeight;
                }
            }
        }
    }
}
