using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlappyBird
{
    class Bird : Drawable
    {
        //Physics
        public float Gravity = 0.3f;
        public float Acceleration = 0;
        public float Velocity = 0;


        
        public CircleShape shape = new CircleShape(20,20);
        public float X = 70;

        public bool Alive = true;
        public float Score = 0;
        public float Distance = 0;
        public Bird()
        {
            shape.Position = new Vector2f(X,200);
        }
        public void Up()
        {
            Acceleration += -10;
        }
        public void Update(RenderTarget target, RenderStates states)
        {
            Acceleration += Gravity;
            Velocity += Acceleration;
            shape.Position += new Vector2f(0, Velocity);
            Acceleration *= 0;
            CheckCollision(target,states);
        }
        public void Die()
        {
            Alive = false;
        }
        void CheckCollision(RenderTarget target, RenderStates states)
        {
            if (shape.Position.X >= target.Size.X || shape.Position.X <= 0)
            {
                Die();
            }
            if (shape.Position.Y >= target.Size.Y || shape.Position.Y <= 0)
            {
                Die();
            }
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(shape);
        }
    }
}
