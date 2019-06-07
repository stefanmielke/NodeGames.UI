using System;
using NodeGames.Core;
using NodeGames.UI.Interfaces;

namespace NodeGames.UI.Controls
{
    internal class HUDPanelColorBinder<TColor> : HUDControl<TColor>
    {
        private readonly Func<TColor> _outerColorBinder;
        private readonly Func<TColor> _innerColorBinder;

        public HUDPanelColorBinder(Rectangle box, Func<TColor> outerColorBinder, Func<TColor> innerColorBinder) : base(box)
        {
            _outerColorBinder = outerColorBinder;
            _innerColorBinder = innerColorBinder;
        }

        protected override void Draw(IPainter<TColor> painter, Point currentCameraLocation)
        {
            HUDManager<TColor>.Painter.DrawRectangle(Box.Location + currentCameraLocation, Box.Size, _innerColorBinder());
            HUDManager<TColor>.Painter.DrawRectangleOutline(Box.Location + currentCameraLocation, Box.Size, _outerColorBinder());

            base.Draw(painter, currentCameraLocation);
        }
    }
}
