using System;
using NodeGames.Core;
using NodeGames.UI.Interfaces;

namespace NodeGames.UI.Controls
{
    internal class HUDProgressBar<TColor> : HUDControl<TColor>
    {
        private readonly TColor _outColor;
        private readonly TColor _innerColor;
        private readonly Func<float> _currentPercentageBinder;
        private readonly Rectangle _outPanel;
        private Rectangle _innerPanel;

        public HUDProgressBar(Rectangle box, TColor outColor, TColor innerColor, Func<float> currentPercentageBinder) : base(box)
        {
            _outColor = outColor;
            _innerColor = innerColor;
            _currentPercentageBinder = currentPercentageBinder;

            _outPanel = box;
            _innerPanel = box;
        }

        protected override void Draw(IPainter<TColor> painter, Point currentCameraLocation)
        {
            var currentPercentage = _currentPercentageBinder();

            if (Math.Abs(currentPercentage - 1f) < 0.01)
            {
                _innerPanel.Width = _outPanel.Width;
                HUDManager<TColor>.Painter.DrawRectangle(Box.Location + currentCameraLocation, _innerPanel.Size, _innerColor);
            }
            else if (currentPercentage > 0)
            {
                _innerPanel.Width = (int)(((_outPanel.Width - 1)) * currentPercentage);
                HUDManager<TColor>.Painter.DrawRectangle(Box.Location + currentCameraLocation, _outPanel.Size, _outColor);
                HUDManager<TColor>.Painter.DrawRectangle(Box.Location + currentCameraLocation, _innerPanel.Size, _innerColor);
            }
            else
            {
                HUDManager<TColor>.Painter.DrawRectangle(Box.Location + currentCameraLocation, _outPanel.Size, _outColor);
                _innerPanel.Width = 0;
            }

            base.Draw(painter, currentCameraLocation);
        }
    }
}
