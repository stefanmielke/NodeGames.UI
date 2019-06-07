using System.Text;
using NodeGames.Core;
using NodeGames.UI.Interfaces;

namespace NodeGames.UI.Controls
{
    internal class HUDTextbox<TColor> : HUDControl<TColor>
    {
        private readonly int _maxCharacters;
        private readonly FontSize _iFontSize;
        private readonly TColor _IFontColor;
        private readonly TColor _borderColor;
        private readonly TColor _innerColor;
        private readonly TColor _borderColorFocus;
        private readonly TColor _innerColorFocus;
        private readonly StringBuilder _currentText;
        private bool _onFocus;
        private bool _startedTyping;

        public HUDTextbox(Rectangle box, int maxCharacters, FontSize fontSize, TColor fontColor, TColor borderColor,
            TColor innerColor, TColor borderColorFocus, TColor innerColorFocus) : base(box)
        {
            _maxCharacters = maxCharacters;
            _iFontSize = fontSize;
            _IFontColor = fontColor;
            _borderColor = borderColor;
            _innerColor = innerColor;
            _borderColorFocus = borderColorFocus;
            _innerColorFocus = innerColorFocus;

            _currentText = new StringBuilder();
            _onFocus = false;

            _startedTyping = false;
        }

        public string GetText()
        {
            return _currentText.ToString();
        }

        protected override void Draw(IPainter<TColor> painter, Point currentCameraLocation)
        {
            base.Draw(painter, currentCameraLocation);

            if (_onFocus)
            {
                if (_startedTyping)
                {
                    UpdateTextForms();
                }

                if (!_startedTyping)
                    _startedTyping = true;
            }

            var currentString = _currentText.ToString();

            if (_onFocus)
            {
                HUDManager<TColor>.Painter.DrawRectangle(Box.Location + currentCameraLocation, Box.Size, _innerColorFocus);
                HUDManager<TColor>.Painter.DrawRectangleOutline(Box.Location + currentCameraLocation, Box.Size,
                    _borderColorFocus);

                var textSize = HUDManager<TColor>.Painter.GetTextSize(_iFontSize, currentString);

                var textStartLocation = Box.Location + new Point(2, 2) + currentCameraLocation + new Point(2, 0);
                textStartLocation.X += textSize.X;

                HUDManager<TColor>.Painter.DrawLineImmediate(textStartLocation, Box.Height - 3, HUDMathUtil.PiOver2,
                    _IFontColor);
            }
            else
            {
                HUDManager<TColor>.Painter.DrawRectangle(Box.Location + currentCameraLocation, Box.Size, _innerColor);
                HUDManager<TColor>.Painter.DrawRectangleOutline(Box.Location + currentCameraLocation, Box.Size, _borderColor);
            }

            HUDManager<TColor>.Painter.DrawString(currentString, _iFontSize, Box.Location + new Point(2, 2) + currentCameraLocation,
                _IFontColor);
        }

        private void UpdateTextForms()
        {
            foreach (var character in HUDManager<TColor>.Input.GetEnteredText())
            {
                if (character >= 32)
                    AppendText(character.ToString());
            }

            if (HUDManager<TColor>.Input.HasPressedEndTextKey())
                FinishedText();

            if (HUDManager<TColor>.Input.HasPressedEraseTextKey() && _currentText.Length > 0)
                _currentText.Remove(_currentText.Length - 1, 1);
        }

        private void AppendText(string key)
        {
            if (_currentText.Length < _maxCharacters)
                _currentText.Append(key);
        }

        protected override bool Pressed()
        {
            EnterTextMode();

            return true;
        }

        public void ResetText()
        {
            _currentText.Clear();
        }

        public void EnterTextMode()
        {
            HUDManager<TColor>.Input.EnterTextMode();

            _onFocus = true;
            _startedTyping = false;
        }

        protected virtual void FinishedText()
        {
            _onFocus = false;
            HUDManager<TColor>.Input.ExitTextMode();
        }
    }
}