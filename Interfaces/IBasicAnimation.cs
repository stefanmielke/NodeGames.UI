using System;
using NodeGames.Core;

namespace NodeGames.UI.Interfaces
{
    public interface IBasicAnimation<out TColor>
    {
        Point Center { get; }
        void Draw(IPainter<TColor> painter, Point location);
        void Draw(IPainter<TColor> painter, Point location, float scale);
        void Update(TimeSpan getElapsedTime);
    }
}
