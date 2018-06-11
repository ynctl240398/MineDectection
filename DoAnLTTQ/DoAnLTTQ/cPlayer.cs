using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace DoAnLTTQ
{
    enum DirectOfMotion
    {
        Left, Right, Up, Down,
    }
    class cPlayer : DrawableGameComponent
    {
        Texture2D texturePlayer;
        Rectangle rectanglePlayer;
        DirectOfMotion directofmotion;
        int Speed = 3;
        Vector2 SizePlayer = new Vector2(65, 65);
        Point Position;
        Point Frame = new Point(0, 0);
        Point LimitFrame = new Point(4, 4);
        int Timer = 70;
        int LastTickCount = System.Environment.TickCount;
        bool Moving = false;
        bool walk = false;
        Point LimitMove1, LimitMove2;
        SoundEffect footstep;

        public cPlayer(Game game, Texture2D newTexture, Point newPosition, Point LimitMove, SoundEffect footstep)
            : base(game)
        {
            texturePlayer = newTexture;
            this.LimitMove2 = LimitMove;
            Position = newPosition;
            LimitMove1 = newPosition;
            rectanglePlayer = new Rectangle(Position.X, Position.Y, (int)SizePlayer.X, (int)SizePlayer.Y);
            this.footstep = footstep;
        }
        public bool getbound(Rectangle rect)
        {
            Point p1 = new Rectangle(Position.X, Position.Y, (int)SizePlayer.X, (int)SizePlayer.Y).Center;
            Vector2 player = new Vector2(p1.X, p1.Y);

            Point p2 = rect.Center;
            Vector2 vt2 = new Vector2(p2.X, p2.Y);
            return (Vector2.Distance(player, vt2) < 32);
        }
        private void UpdateState()
        {
            if (Moving)
            {
                if (Frame.X < LimitFrame.X - 1)
                {
                    Frame.X += 1;
                    if (Frame.X == 2)
                    {
                        walk = true;
                    }
                }
                else
                {
                    Frame.X = 0;
                }
            }
            else Frame.X = 0;
        }
        private void Move()
        {
            if (walk)
            {
                footstep.Play(1f, 1f, 1f);
                walk = false;
            }
            Moving = true;
            if (directofmotion == DirectOfMotion.Down)
            {
                Position.Y += Speed;
                Frame.Y = 0;
            }
            else if (directofmotion == DirectOfMotion.Left)
            {
                Position.X -= Speed;
                Frame.Y = 1;
            }
            else if (directofmotion == DirectOfMotion.Right)
            {
                Position.X += Speed;
                Frame.Y = 2;
            }
            else if (directofmotion == DirectOfMotion.Up)
            {
                Position.Y -= Speed;
                Frame.Y = 3;
            }
            check();
        }
        private void check()
        {
            int x = LimitMove2.X, y = LimitMove2.Y;
            if (Position.X + 15 < LimitMove1.X)
            {
                Position.X += Speed;
            }
            else if (Position.X + (int)SizePlayer.X - 18 > x)
            {
                Position.X -= Speed;
            }
            if (Position.Y + 10 < LimitMove1.Y)
            {
                Position.Y += Speed;
            }
            else if (Position.Y + (int)SizePlayer.Y - 5 > y)
            {
                Position.Y -= Speed;
            }
        }
        public void Update()
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (System.Environment.TickCount - LastTickCount > Timer - Speed)
            {
                LastTickCount = System.Environment.TickCount;
                UpdateState();
            }

            if (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right))
            {
                directofmotion = DirectOfMotion.Right;
                Move();
                return;
            }
            else if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left))
            {
                directofmotion = DirectOfMotion.Left;
                Move();
                return;
            }
            else if (keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down))
            {
                directofmotion = DirectOfMotion.Down;
                Move();
                return;
            }
            else if (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up))
            {
                directofmotion = DirectOfMotion.Up;
                Move();
                return;
            }

            Moving = false;
        }
        public void Draw(SpriteBatch sBatch)
        {
            sBatch.Draw(texturePlayer, new Rectangle(Position.X, Position.Y, (int)SizePlayer.X, (int)SizePlayer.Y), new Rectangle(Frame.X * (int)SizePlayer.X, Frame.Y * (int)SizePlayer.Y, (int)SizePlayer.X, (int)SizePlayer.Y), Color.White);
        }
    }
}
