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
    class Item
    {
        public Sprite sprite;
        public Vector2 Position;
        public bool IsDrawn = true;

        public Item(ContentManager getContent, string getTexture, int getWidth, int getheight)
        {
            sprite = new Sprite(getContent, getTexture, getWidth, getheight);
        }

        public void Draw(SpriteBatch sB, BaseEngine getBengine)
        {
            if (IsDrawn)
            {
                Position = new Vector2(getBengine.tileDraw.X, getBengine.tileDraw.Y);
                sprite.Draw(sB, Position, 0, SpriteEffects.None);
            }
        }

    }
}