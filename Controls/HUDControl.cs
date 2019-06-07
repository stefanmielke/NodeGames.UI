using System.Collections.Generic;
using System.Linq;
using NodeGames.Core;
using NodeGames.UI.Interfaces;

namespace NodeGames.UI.Controls
{
    internal class HUDControl<TColor>
    {
        public bool Destroyed { get; private set; }
        public HUDControl<TColor> Parent { get; set; }
        public Rectangle Box;
        public List<HUDControl<TColor>> Controls { get; }

        public bool IsVisible { get; set; }

        public bool IsMouseInside { get; private set; }

        /// <summary>
        /// Creates a new HUD.
        /// </summary>
        /// <param name="box">Position on the screen and size of the control</param>
        public HUDControl(Rectangle box)
        {
            Box = box;
            Controls = new List<HUDControl<TColor>>();

            IsVisible = true;

            IsMouseInside = false;
        }

        /// <summary>
        /// Add a new uninitialized hud
        /// </summary>
        /// <param name="hudControl"></param>
        public void AddNewHUD(HUDControl<TColor> hudControl)
        {
            Controls.Add(hudControl);
            hudControl.Parent = this;
        }

        /// <summary>
        /// Add a new uninitialized hud and immediatly initializes it.
        /// </summary>
        /// <param name="hudControl"></param>
        public void AddNewHUDAndInit(HUDControl<TColor> hudControl)
        {
            Controls.Add(hudControl);
            hudControl.Parent = this;

            hudControl.Init(Box.Location);
        }

        public virtual void Init(Point outPosition)
        {
            var newBox = Box;
            newBox.Offset(outPosition);
            Box = newBox;

            for (int i = 0; i < Controls.Count; i++)
            {
                Controls[i].Init(Box.Location);
            }
        }

        public void Update()
        {
            if (!IsVisible)
            {
                return;
            }

            Controls.RemoveAll(c => c.Destroyed);

            if (Box.Contains(GetMouseCenterPoint()))
            {
                if (!IsMouseInside)
                {
                    OnMouseEnter();
                }

                OnMouseInside();
                IsMouseInside = true;
            }
            else
            {
                if (IsMouseInside)
                {
                    OnMouseLeave();
                }

                OnMouseOutside();
                IsMouseInside = false;
            }

            foreach (var control in Controls)
            {
                control.Update();
            }
        }

        public void Clear()
        {
            Controls.ForEach(c => c.Destroy());
            Controls.Clear();
        }

        /// <summary>
        /// Method called when the mouse enters the box of the control.
        /// </summary>
        protected virtual void OnMouseEnter()
        {

        }

        /// <summary>
        /// Called every frame while the mouse is inside the control.
        /// </summary>
        protected virtual void OnMouseInside()
        {
        }

        /// <summary>
        /// Method called when the mouse exits the box of the control.
        /// </summary>
        protected virtual void OnMouseLeave()
        {
        }

        /// <summary>
        /// Called every frame while the mouse is outside the control.
        /// </summary>
        protected virtual void OnMouseOutside()
        {
        }

        protected virtual bool Pressed()
        {
            return false;
        }

        protected virtual void Released()
        {
        }

        protected virtual void Dismantled()
        {
        }

        public void MainDraw(IPainter<TColor> painter, Point currentCameraLocation)
        {
            if (!IsVisible)
            {
                return;
            }

            Draw(painter, currentCameraLocation);
        }

        protected virtual void Draw(IPainter<TColor> painter, Point currentCameraLocation)
        {
            foreach (var control in Controls)
            {
                control.MainDraw(painter, currentCameraLocation);
            }
        }

        public bool OnPressed()
        {
            if (!IsVisible)
                return false;

            bool pressed = false;
            if (Box.Contains(GetMouseCenterPoint()))
            {
                pressed = Pressed();

                for (int i = 0; i < Controls.Count; i++)
                {
                    if (Controls[i].OnPressed())
                        return true;
                }
            }

            return pressed;
        }

        private static Point GetMouseCenterPoint()
        {
            var mousePosition = HUDManager<TColor>.MouseLocator.GetMousePosition();

            var cameraLocation = HUDManager<TColor>.CameraLocator?.GetCameraLocation();
            if (cameraLocation.HasValue)
            {
                mousePosition -= cameraLocation.Value;
            }

            return mousePosition;
        }

        public void OnReleased()
        {
            if (!IsVisible)
                return;

            var mouseRectangle = HUDManager<TColor>.MouseLocator.GetMousePosition();

            if (Box.Contains(mouseRectangle))
            {
                Released();

                for (int i = 0; i < Controls.Count; i++)
                {
                    Controls[i].OnReleased();
                }
            }
        }

        public void OnDismantled()
        {
            if (!IsVisible)
                return;

            if (Box.Contains(GetMouseCenterPoint()))
            {
                Dismantled();

                for (int i = 0; i < Controls.Count; i++)
                {
                    Controls[i].OnDismantled();
                }
            }
        }

        public void Destroy()
        {
            Destroyed = true;
            foreach (var control in Controls)
            {
                control.Destroy();
            }
        }

        public int TotalControls()
        {
            return Controls.Count + Controls.Sum(c => c.TotalControls());
        }
    }
}
