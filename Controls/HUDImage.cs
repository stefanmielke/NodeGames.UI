using NodeGames.Core;
using NodeGames.UI.Interfaces;

namespace NodeGames.UI.Controls
{
    internal class HUDImage<TColor> : HUDControl<TColor>
    {
        private IBasicAnimation<TColor> _image;

        public HUDImage(Rectangle box, IBasicAnimation<TColor> image) : base(box)
        {
            _image = image;
        }

        public override void Init(Point outPosition)
        {
            base.Init(outPosition);

            var newLocation = new Point(Box.X + (int)_image.Center.X, Box.Y + (int)_image.Center.Y);
            Box = new Rectangle(newLocation, Box.Size);
        }

        protected override void Draw(IPainter<TColor> painter, Point currentCameraLocation)
        {
            _image.Update(HUDManager<TColor>.GameTimer.GetElapsedTime());
            _image.Draw(painter, Box.Location + currentCameraLocation);

            base.Draw(painter, currentCameraLocation);
        }
    }
}
