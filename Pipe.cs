using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlappyBird
{
    class Pipe : Drawable
    {
        public float Spacing = 35;
        public RectangleShape Top;
        public RectangleShape Bottom;

        public float width = 50;


        public float Speed = 5;
        public bool outOfScreen = false;
        public bool Scored = false;
        public bool isCurrent = false;
        public Pipe(float Y,RenderWindow window)
        {

            Top = new RectangleShape(new Vector2f(width, window.Size.Y*1.5f));
            Bottom = new RectangleShape(new Vector2f(width, window.Size.Y*1.5f));

            Top.Position = new Vector2f(window.Size.X, Y + -Top.Size.Y - Spacing*2);
            Top.FillColor = Color.White;
            Bottom.FillColor = Color.White;
            Bottom.Position = new Vector2f(window.Size.X, Y + Spacing*2);

        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Top);
            target.Draw(Bottom);
        }
        public void Update(RenderTarget target)
        {
            Top.Position -= new Vector2f(Speed, 0);
            Bottom.Position -= new Vector2f(Speed, 0);
            if (isCurrent)
            {
                Top.FillColor = new Color(240,25,255);
                Bottom.FillColor = new Color(240, 25, 255);
            }
            else 
            {
                Top.FillColor = Color.White;
                Bottom.FillColor = Color.White;
            }
            if (Top.Position.X < -width)
            {
                outOfScreen = true;
            }
        }
    }
}
