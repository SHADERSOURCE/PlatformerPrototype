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
    class Player
    {
        public Sprite sprite;
        public Vector2 Position;

        public Vector2 tl = Vector2.Zero;
        public Vector2 tr = Vector2.Zero;
        public Vector2 bl = Vector2.Zero;
        public Vector2 br = Vector2.Zero;


        public Rectangle Bounds;
        public Vector2 Speed;

        public bool[] checks = new bool[2];

        public float xFriction = 1;
        public float yFriction = 1;

        public float Rotation = 0;

        public int Width = 16;
        public int Height = 32;

        private BaseEngine BEngine;
        private Game1 game1;
        private int wallTimer;

        //--------------------------------------------

        public Player(ContentManager getContent)
        {
            Position = new Vector2(100, 50);

            sprite = new Sprite(getContent, "objects/player", Width, Height);

        }

        public void updateBounds(Vector2 Camera)
        {
            Bounds = new Rectangle((int)Position.X + (int)Camera.X, (int)Position.Y + (int)Camera.Y, Width, Height);
            tl = new Vector2(Bounds.X, Bounds.Y);
            tr = new Vector2(Bounds.X + Bounds.Width, Bounds.Y);
            bl = new Vector2(Bounds.X, Bounds.Y + Bounds.Height);
            br = new Vector2(Bounds.X + Bounds.Width, Bounds.Y + Bounds.Height);
        }

        public void Update(Game1 getGame1, BaseEngine getEngine)
        {
            BEngine = getEngine;
            game1 = getGame1;


            //Gravity--------------

            bool Checked = false;
            if (!Checked)
            {
                if (checks[0] == true)
                {
                    yFriction = 0.95f;
                    Checked = true;
                }
                else
                {
                    yFriction = 1;
                }
            }

            if (!Checked)
            {
                if (checks[1] == true)
                {
                    xFriction = 0.92f;
                    yFriction = 0.95f;
                    Checked = true;
                }
                else
                {
                    xFriction = 1;
                    yFriction = 1;
                }
            }

            if (checks[1] == false)
            {
                if (Speed.Y < 12)
                    Speed.Y += 0.2f;
                else
                    Speed.Y = 12;
            }
            else
            {
                if (Speed.Y < 2)
                    Speed.Y += 0.2f;
                else
                    Speed.Y = 2;
            }

            //---------------------


            //Controls--------------------------------
            if (Input.KeyboardPress(Keys.OemPlus))
                BEngine.tileSize += 1;

            if (Input.KeyboardPress(Keys.OemMinus))
                if (BEngine.tileSize > 1)
                    BEngine.tileSize -= 1;

            //Out of water
            if (checks[1] == false)
            {
                if (Input.KeyboardPress(Keys.W) || Input.KeyboardPress(Keys.Up))
                {
                    bool returner = false;

                    //Jump--------------

                    if (!returner)
                    {
                       
                        Position.Y += 1;
                        updateBounds(Camera.Position);
                        BEngine.updateHitboxes(Position, Bounds);

                        for (int i = 0; i < BEngine.Canvas.Length; i++)
                            if (Bounds.Intersects(BEngine.Canvas[i]))
                            {
                                Speed.Y = -7f;
                                returner = true;
                            }
                        if (checkAllLines(BEngine.tan1) == true)
                        {
                            Speed.Y = -7f;
                            returner = true;
                        }
                        if (checkAllLines(BEngine.tan2) == true)
                        {
                            Speed.Y = -7f;
                            returner = true;
                        }
                        if (checkAllLines(BEngine.tan3) == true)
                        {
                            Speed.Y = -7f;
                            returner = true;
                        }
                        if (checkAllLines(BEngine.tan4) == true)
                        {
                            Speed.Y = -7f;
                            returner = true;
                        }
                        if (checkAllLines(BEngine.tan5) == true)
                        {
                            Speed.Y = -7f;
                            returner = true;
                        }
                        if (checkAllLines(BEngine.tan6) == true)
                        {
                            Speed.Y = -7f;
                            returner = true;
                        }


                        Position.Y -= 1;

                    }
                    //--------------------

                    //Ladders----------------------------------------
                    if (!returner)
                    {
                        if (checks[0] == true)
                        {
                            wallTimer = 0;
                            Speed.Y = -4;
                        }
                    }

                    //Wall jumping-----------
                    wallTimer++;
                    if (wallTimer > 8)
                    {
                        if (!returner)
                        {
                            if (Input.KeyboardPressed(Keys.W))
                            {
                            Position.X += 1;
                            updateBounds(Camera.Position);

                            for (int i = 0; i < BEngine.Canvas.Length; i++)
                                if (Bounds.Intersects(BEngine.Canvas[i]))
                                {
                                    Rotation = -8;
                                    Speed.X = -7f;
                                    Speed.Y = -5f;
                                    returner = true;
                                }

                            Position.X -= 1;

                            Position.X -= 1;
                            updateBounds(Camera.Position);
                            BEngine.updateHitboxes(Position, Bounds);

                            for (int i = 0; i < BEngine.Canvas.Length; i++)
                                if (Bounds.Intersects(BEngine.Canvas[i]))
                                {
                                    Rotation = 8;
                                    Speed.X = 7f;
                                    Speed.Y = -5f;
                                    returner = true;
                                }

                            Position.X += 1;
                            }

                        }
                    }
                    //--------------------------

                  



                }
                if (Input.KeyboardPress(Keys.A) || Input.KeyboardPress(Keys.Left))
                    if (Speed.X > -3)
                        Speed.X -= 0.25f;
                    else
                        Speed.X = -3;

                if (Input.KeyboardPress(Keys.D) || Input.KeyboardPress(Keys.Right))
                    if (Speed.X < 3)
                        Speed.X += 0.25f;
                    else
                        Speed.X = 3;

                if (Input.KeyboardRelease(Keys.A) && Input.KeyboardRelease(Keys.D) && Input.KeyboardRelease(Keys.Left) && Input.KeyboardRelease(Keys.Right))
                    if (Math.Abs(Speed.X) > 1)
                        Speed.X *= 0.92f;
                    else
                        Speed.X = 0;
            }
            else
            {
                if (Input.KeyboardPress(Keys.W) || Input.KeyboardPress(Keys.Up))
                {
                    if (Speed.Y > -4)
                        Speed.Y -= 0.6f;
                    else
                        Speed.Y = -4;
                }
                if (Input.KeyboardPress(Keys.S) || Input.KeyboardPress(Keys.Down))
                {
                    if (Speed.Y < 4)
                        Speed.Y += 0.25f;
                    else
                        Speed.Y = 4;
                }
                if (Input.KeyboardPress(Keys.A) || Input.KeyboardPress(Keys.Left))
                    if (Speed.X > -4)
                        Speed.X -= 0.25f;
                    else
                        Speed.X = -4;

                if (Input.KeyboardPress(Keys.D) || Input.KeyboardPress(Keys.Right))
                    if (Speed.X < 4)
                        Speed.X += 0.25f;
                    else
                        Speed.X = 4;

                if (Input.KeyboardRelease(Keys.A) && Input.KeyboardRelease(Keys.D) && Input.KeyboardRelease(Keys.Left) && Input.KeyboardRelease(Keys.Right))
                    if (Math.Abs(Speed.X) > 1)
                        Speed.X *= 0.92f;
                    else
                        Speed.X = 0;
            }

            Speed.X *= xFriction;
            Speed.Y *= yFriction;

        }

        public void checkCollisionsX(Rectangle target)
        {
            if (Bounds.Intersects(target))
            {

                if (Speed.X > 0)
                    for (int i = 20; i > 0; i--)
                    {
                        updateBounds(Camera.Position);
                        BEngine.updateHitboxes(Position, Bounds);
                        if (Bounds.Intersects(target))
                            Position.X--;
                    }

                if (Speed.X < 0)
                    for (int i = 20; i > 0; i--)
                    {
                        updateBounds(Camera.Position);
                        BEngine.updateHitboxes(Position, Bounds);
                        if (Bounds.Intersects(target))
                            Position.X++;
                    }

                Speed.X = 0;
            }
        }

      

        public void checkCollisionsY(Rectangle target)
        {
            if (Bounds.Intersects(target))
            {

                if (Speed.Y > 0)
                    for (int i = 20; i > 0; i--)
                    {
                        updateBounds(Camera.Position);
                        BEngine.updateHitboxes(Position, Bounds);
                        if (Bounds.Intersects(target))
                        {
                            Position.Y--;
                            wallTimer = 0;
                            Rotation = 0;
                        }
                    }

                if (Speed.Y < 0)
                    for (int i = 20; i > 0; i--)
                    {
                        updateBounds(Camera.Position);
                        BEngine.updateHitboxes(Position, Bounds);
                        if (Bounds.Intersects(target))
                            Position.Y++;
                    }

                Speed.Y = 0;
            }
        }

        public void Draw(SpriteBatch sB)
        {

                sprite.Draw(sB, new Vector2(Bounds.X, Bounds.Y), Vector2.Zero, MathHelper.ToRadians(Rotation), SpriteEffects.None, Color.White);



            //Sprites
        }


        public void checkTollisionsX(Triangle target)
        {
            if (checkAllLines(target) == true)
            {

                if (Speed.X > 0)
                    for (int i = 20; i > 0; i--)
                    {
                        updateBounds(Camera.Position);
                        BEngine.updateHitboxes(Position, Bounds);
                        if (checkAllLines(target) == true)
                            Position.X--;
                    }

                if (Speed.X < 0)
                    for (int i = 20; i > 0; i--)
                    {
                        updateBounds(Camera.Position);
                        BEngine.updateHitboxes(Position, Bounds);
                        if (checkAllLines(target) == true)
                            Position.X++;
                    }

                Speed.X = 0;
            }
        }

        public void checkTollisionsY(Triangle target)
        {
            if (checkAllLines(target) == true)
            {
 

                if (Speed.Y > 0)
                    for (int i = 20; i > 0; i--)
                    {
                        updateBounds(Camera.Position);
                        BEngine.updateHitboxes(Position, Bounds);
                        if (checkAllLines(target) == true)
                        {
                            Position.Y--;
                            wallTimer = 0;
                            Rotation = 0;
                            
                        }
                    }

                if (Speed.Y < 0)
                    for (int i = 20; i > 0; i--)
                    {
                        updateBounds(Camera.Position);
                        BEngine.updateHitboxes(Position, Bounds);
                        if (checkAllLines(target) == true)
                            Position.Y++;
                    }

                Speed.Y = 0;
            }
        }

        public bool checkAllLines(Triangle triangle)
        {
            if (BEngine.lineTest(tl, tr, triangle.a, triangle.b) == true)
            {
                return true;
            }
            if (BEngine.lineTest(tl, tr, triangle.b, triangle.c) == true)
            {
                return true;
            }
            if (BEngine.lineTest(tl, tr, triangle.c, triangle.a) == true)
            {
                return true;
            }

            if (BEngine.lineTest(tl, bl, triangle.a, triangle.b) == true)
            {
                return true;
            }
            if (BEngine.lineTest(tl, bl, triangle.b, triangle.c) == true)
            {
                return true;
            }
            if (BEngine.lineTest(tl, bl, triangle.c, triangle.a) == true)
            {
                return true;
            }

            if (BEngine.lineTest(bl, br, triangle.a, triangle.b) == true)
            {
                return true;
            }
            if (BEngine.lineTest(bl, br, triangle.b, triangle.c) == true)
            {
                return true;
            }
            if (BEngine.lineTest(bl, br, triangle.c, triangle.a) == true)
            {
                return true;
            }

            if (BEngine.lineTest(tr, br, triangle.a, triangle.b) == true)
            {
                return true;
            }
            if (BEngine.lineTest(tr, br, triangle.b, triangle.c) == true)
            {
                return true;
            }
            if (BEngine.lineTest(tr, br, triangle.c, triangle.a) == true)
            {
                return true;
            }
            return false;
        }

    }
}
