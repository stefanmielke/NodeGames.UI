using NodeGames.Core;
using NodeGames.UI.Interfaces;

namespace NodeGames.UI.Controls
{
    internal class HUDTexture<TColor> : HUDControl<TColor>
    {
        private readonly ITexture2D _texture;

        public HUDTexture(Rectangle box, ITexture2D texture) : base(box)
        {
            _texture = texture;
        }

        protected override void Draw(IPainter<TColor> painter, Point currentCameraLocation)
        {
            painter.Draw(_texture, (Box.Location + currentCameraLocation));

            base.Draw(painter, currentCameraLocation);
        }
    }
}