using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DoAnLTTQ
{
    class cExplosion : DrawableGameComponent
    {
        private Point Position;
        private SpriteBatch sBatch;
        private Point Frame = new Point(0, 0);
        private Point LimitFrame = new Point(5, 5);
        private Texture2D texture;
        private const int SizeExplosion = 64;
        private int LastTickCount;
        private const int Timer = 50;

        public cExplosion(Game game, Point Position, Texture2D texture)
            : base(game)
        {
            this.Position = Position;
            this.texture = texture;
            LastTickCount = System.Environment.TickCount;
            sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
        }

        private void UpdateStatus()
        {
            if (Frame.Y < LimitFrame.Y)
            {
                if (Frame.X < LimitFrame.X - 1)
                {
                    Frame.X += 1;
                }
                else
                {
                    Frame.X = 0;
                    Frame.Y += 1;
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            if (System.Environment.TickCount - LastTickCount > Timer)
            {
                LastTickCount = System.Environment.TickCount;
                UpdateStatus();
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            sBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            sBatch.Draw(texture, new Rectangle(Position.X, Position.Y, SizeExplosion, SizeExplosion), new Rectangle(Frame.X * SizeExplosion, Frame.Y * SizeExplosion, SizeExplosion, SizeExplosion), Color.White);
            sBatch.End();

            base.Draw(gameTime);
        }
    }
}
