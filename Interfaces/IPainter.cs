using NodeGames.Core;
using NodeGames.UI.Controls;

namespace NodeGames.UI.Interfaces
{
    public interface IPainter<in TColor>
    {
        Point GetTextSize(FontSize fontSize, string text);
        void DrawString(string text, FontSize fontSize, Point location, TColor color);
        void Draw(ITexture2D texture, Point location);
        void Draw(ITexture2D texture, Point location, TColor color);
        void Draw(ITexture2D texture, Point location, float scale, TColor white);
        void DrawRectangle(Point location, Point size, TColor color);
        void DrawRectangleOutline(Point location, Point size, TColor color, int thickness = 1);
        void DrawLineImmediate(Point location, int length, float angle, TColor color);
        void DrawLine(float x1, float y1, float x2, float y2, TColor color);
    }
}
