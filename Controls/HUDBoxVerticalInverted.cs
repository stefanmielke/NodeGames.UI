using NodeGames.Core;
using NodeGames.UI.Interfaces;

namespace NodeGames.UI.Controls
{
    internal class HUDBoxVerticalInverted<TColor> : HUDControl<TColor>
    {
        private readonly int _borderLeft;
        private readonly int _spaceHeight;
        protected int BorderBottom;

        public HUDBoxVerticalInverted(Rectangle box, int borderLeft, int borderBottom, int spaceHeight) : base(box)
        {
            _borderLeft = borderLeft;
            BorderBottom = borderBottom;
            _spaceHeight = spaceHeight;
        }

        public virtual void AddGridControl(HUDControl<TColor> control)
        {
            AddNewHUD(control);
        }

        protected override void Draw(IPainter<TColor> painter, Point currentCameraLocation)
        {
            var currentY = Box.Bottom - BorderBottom;

            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                currentY -= Controls[i].Box.Height;

                Controls[i].Box = new Rectangle(Box.X + _borderLeft, Box.Y + currentY, Controls[i].Box.Width, Controls[i].Box.Height);

                currentY -= _spaceHeight;
            }

            base.Draw(painter, currentCameraLocation);
        }
    }
}
