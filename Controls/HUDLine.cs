using NodeGames.Core;
using NodeGames.UI.Interfaces;

namespace NodeGames.UI.Controls
{
    internal class HUDLine<TColor> : HUDControl<TColor>
    {
        private readonly Point _start;
        private readonly int _length;
        private readonly float _angle;
        private readonly TColor _color;

        public HUDLine(Point start, Point finish, TColor color) : base(new Rectangle(start, start))
        {
            _start = start;
            _color = color;

            _length = start.DistanceTo(finish);
            _angle = HUDMathUtil.GetRadiansAngleFromTwoPoints(start, finish);
        }

        protected override void Draw(IPainter<TColor> painter, Point currentCameraLocation)
        {
            base.Draw(painter, currentCameraLocation);

            HUDManager<TColor>.Painter.DrawLineImmediate(Box.Location + currentCameraLocation, _length, _angle, _color);
        }
    }
}
