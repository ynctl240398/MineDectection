using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace DoAnLTTQ
{
    class cButton
    {
        Texture2D textureButton;
        Point Position;
        Vector2 SizeButton;
        Rectangle rectangleButton;
        bool down, click = true;
        public bool IsClicked;
        Color colour = new Color(255, 255, 255, 255);
        SoundEffect magicstep;
        public cButton(Game game, Texture2D newTexture, Point newPosition, Vector2 newSizeButton, SoundEffect magicstep)
        {
            textureButton = newTexture;
            Position = newPosition;
            SizeButton = newSizeButton;
            this.magicstep = magicstep;
            rectangleButton = new Rectangle(Position.X, Position.Y, (int)SizeButton.X, (int)SizeButton.Y);
        }

        public void Update(MouseState mouse)
        {
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);
            if (mouseRectangle.Intersects(rectangleButton))
            {
                if (colour.A == 255) down = false;
                if (colour.A == 0) down = false;
                if (down) colour.A += 3;
                else colour.A -= 3;
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    IsClicked = true;
                    if (click)
                    {
                        magicstep.Play();
                        click = false;
                    }
                }
            }
            else if (colour.A < 255)
            {
                colour.A += 3;
                IsClicked = false;
                click = true;
            }
        }

        public void Draw(SpriteBatch sBatch)
        {
            sBatch.Draw(textureButton, rectangleButton, colour);
        }

        public Rectangle Getbounds()
        {
            return new Rectangle(Position.X, Position.Y, (int)SizeButton.X, (int)SizeButton.Y);
        }
    }
}
