using NodeGames.UI.Interfaces;

namespace NodeGames.UI
{
    public static class HUDManager<TColor>
    {
        public static IMouseLocator MouseLocator { get; private set; }
        public static ICameraLocator CameraLocator { get; private set; }
        public static IPainter<TColor> Painter { get; private set; }
        public static IGameTimer GameTimer { get; private set; }
        public static IInput Input { get; private set; }

        public static void SetDefaults(IPainter<TColor> painter, IGameTimer gameTimer, IMouseLocator mouseLocator, ICameraLocator cameraLocator)
        {
            Painter = painter;
            MouseLocator = mouseLocator;
            CameraLocator = cameraLocator;
        }
    }
}
