﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace Platformer_Prototype
{
    class Sprite
    {
        public Texture2D Texture;
        public int Rows;
        public int Columns;
        public float CurrentFrame;
        public float TotalFrames;
        public Rectangle sourceRectangle;
        public Rectangle destinationRectangle;
        public int Width;
        public int Height;

        public Sprite(ContentManager getContent, string getTexture, int getWidth, int getHeight)
        {
            Texture = getContent.Load<Texture2D>(getTexture);
            Width = getWidth;
            Height = getHeight;
        }
        public Sprite(ContentManager getContent, string getTexture, int getWidth, int getHeight, int getRows, int getColumns)
        {
            Texture = getContent.Load<Texture2D>(getTexture);
            Width = getWidth;
            Height = getHeight;
            Rows = getRows;
            Columns = getColumns;
            CurrentFrame = 0;
            TotalFrames = Rows * Columns;
        }

        public void UpdateAnimation(float getDelay)
        {
            CurrentFrame += getDelay;
            if (CurrentFrame >= TotalFrames)
            {
                CurrentFrame = 0;
            }
        }

        public void Draw(SpriteBatch sB, Vector2 getPosition)
        {
            destinationRectangle = new Rectangle((int)getPosition.X, (int)getPosition.Y, Width, Height);

            sB.Draw(Texture,
                destinationRectangle,
                null,
                Color.White,
                0,
                new Vector2(destinationRectangle.Width / 2, destinationRectangle.Height / 2),
                SpriteEffects.None,
                0);
        }
        public void Draw(SpriteBatch sB, Vector2 getPosition, float getRotation)
        {
            int sourceWidth = Texture.Width / Columns;
            int sourceHeight = Texture.Height / Rows;

            int row = (int)((float)CurrentFrame / (float)Columns);
            int column = (int)CurrentFrame % Columns;

            sourceRectangle = new Rectangle(sourceWidth * column, sourceHeight * row, sourceWidth, sourceHeight);
            destinationRectangle = new Rectangle((int)getPosition.X, (int)getPosition.Y, Width, Height);

            sB.Draw(Texture,
                destinationRectangle,
                sourceRectangle,
                Color.White,
                getRotation,
                new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2),
                SpriteEffects.None,
                0);
        }

    }
}