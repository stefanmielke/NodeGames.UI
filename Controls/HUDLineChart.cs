using System.Collections.Generic;
using NodeGames.Core;
using NodeGames.UI.Interfaces;

namespace NodeGames.UI.Controls
{
    internal class HUDLineChart<TColor> : HUDPanel<TColor>
    {
        private readonly TColor[] _lineColor;
        private readonly bool _rescale;

        private readonly List<float>[] _points;

        private readonly int _maxPoints;
        private float _xScale;
        private float _yScale;

        private float _maxValue;

        private readonly GraphPoints[,] _graphPoints;

        public HUDLineChart(Rectangle box, TColor outColor, TColor innerColor, float initialMaxValue, int maxPoints,
            int numberOfLines, TColor[] lineColor, bool rescale = true) : base(box, outColor, innerColor)
        {
            _lineColor = lineColor;
            _rescale = rescale;
            _maxValue = initialMaxValue;
            _maxPoints = maxPoints;

            _points = new List<float>[numberOfLines];
            for (int i = 0; i < _points.Length; i++)
            {
                _points[i] = new List<float>(_maxPoints + 1);
            }

            _xScale = box.Width / (float) _maxPoints;
            _yScale = box.Height / _maxValue;

            _graphPoints = new GraphPoints[numberOfLines, _maxPoints];
        }

        public void AddPoint(float value, int line = 0)
        {
            if (value > _maxValue && _rescale)
            {
                _maxValue = value;
                _xScale = Box.Width / (float) _maxPoints;
                _yScale = Box.Height / _maxValue;
            }

            _points[line].Add(value);

            if (_points[line].Count > _maxPoints)
            {
                _points[line].RemoveRange(0, _points[line].Count - _maxPoints);
            }

            RemakeGraph(line);
        }

        private void RemakeGraph(int line)
        {
            for (int i = 0; i < _points[line].Count - 1; i++)
            {
                var x1 = Box.X + 1 + (_xScale * i);
                var y1 = Box.Y - 1 + (_yScale * (_maxValue - _points[line][i]));
                var x2 = Box.X + 3 + (_xScale * (i + 1));
                var y2 = Box.Y - 1 + (_yScale * (_maxValue - _points[line][i + 1]));
                _graphPoints[line, i] = new GraphPoints(x1, y1, x2, y2);
            }
        }

        protected override void Draw(IPainter<TColor> painter, Point currentCameraLocation)
        {
            base.Draw(painter, currentCameraLocation);

            for (int i = 0; i < _graphPoints.GetLength(0); i++)
            {
                for (int j = 0; j < _graphPoints.GetLength(1); j++)
                {
                    float x1 = _graphPoints[i, j].X1 + currentCameraLocation.X;
                    float y1 = _graphPoints[i, j].Y1 + currentCameraLocation.Y;
                    float x2 = _graphPoints[i, j].X2 + currentCameraLocation.X;
                    float y2 = _graphPoints[i, j].Y2 + currentCameraLocation.Y;
                    painter.DrawLine(x1, y1, x2, y2, _lineColor[i]);
                }
            }
        }

        private struct GraphPoints
        {
            public readonly float X1;
            public readonly float Y1;
            public readonly float X2;
            public readonly float Y2;

            public GraphPoints(float x1, float y1, float x2, float y2)
            {
                X1 = x1;
                Y1 = y1;
                X2 = x2;
                Y2 = y2;
            }
        }
    }
}