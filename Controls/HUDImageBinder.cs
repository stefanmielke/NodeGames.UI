using System;
using NodeGames.Core;
using NodeGames.UI.Interfaces;

namespace NodeGames.UI.Controls
{
    internal class HUDImageBinder<TColor> : HUDControl<TColor>
    {
        private Rectangle _originalBox;
        private readonly Func<IBasicAnimation<TColor>> _animationBinder;
        private readonly float _scale;

        public HUDImageBinder(Rectangle box, Func<IBasicAnimation<TColor>> animationBinder, float scale = 1f) : base(box)
        {
            _animationBinder = animationBinder;
            _scale = scale;
        }

        public override void Init(Point outPosition)
        {
            base.Init(outPosition);

            _originalBox = Box;
        }

        protected override void Draw(IPainter<TColor> painter, Point currentCameraLocation)
        {
            var image = _animationBinder();

            var newLocation = new Point(_originalBox.X + (int)image.Center.X, _originalBox.Y + (int)image.Center.Y);
            Box = new Rectangle(newLocation, Box.Size);

            image.Draw(painter, Box.Location + currentCameraLocation, scale: _scale);

            base.Draw(painter, currentCameraLocation);
        }
    }
}
