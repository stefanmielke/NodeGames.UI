using System;
using NodeGames.Core;

namespace NodeGames.UI
{
    internal static class HUDMathUtil
    {
        public const float PiOver2 = 1.570796f;
        
        public static float GetRadiansAngleFromTwoPoints(Point start, Point finish)
        {
            return ToRadians(GetDegreesAngleFromTwoPoints(start, finish));
        }

        private static float GetDegreesAngleFromTwoPoints(Point from, Point to)
        {
            float xDiff = to.X - from.X;
            float yDiff = to.Y - from.Y;

            return (float)(Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI);
        }
        
        private static float ToRadians(float degrees)
        {
            return degrees * ((float) Math.PI / 180f);
        }
    }
}
