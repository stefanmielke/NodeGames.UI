using System.Collections.Generic;

namespace NodeGames.UI.Interfaces
{
    public interface IInput
    {
        IEnumerable<char> GetEnteredText();
        bool HasPressedEndTextKey();
        bool HasPressedEraseTextKey();
        void EnterTextMode();
        void ExitTextMode();
    }
}