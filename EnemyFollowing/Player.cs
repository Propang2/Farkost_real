using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkost
{
    class Player
    {   
        //Player variables
        public static Texture2D playerTexture;
        public Texture2D playerTurnRight;
        public Texture2D playerTurnLeft;
        public Vector2 playerPos;
        public Vector2 playerSpeed;
        public bool turningRight;
        public bool turningLeft;
        public int HP;
        int shootTimer;

        KeyboardState keyboard;

        public Player()
        {
            playerPos = new Vector2(400, 900);
            playerSpeed = new Vector2(0, 0);
            HP = 50000;
            shootTimer = 5;
        }

        public void TrippleMachineGun()
        {
            Game1.laserLjud.Play();
            
            Bullets newBullet = new Bullets(Game1.bulletTexture);
            //newBullet.speed = new Vector2(0, -30);
            newBullet.position = new Vector2(playerPos.X + playerTexture.Width / 2.6f, playerPos.Y);
            newBullet.angle = 100 * 0.01745f;
            newBullet.speed.X = (float)Math.Cos(newBullet.angle);
            newBullet.speed.Y = -(float)Math.Sin(newBullet.angle);
            newBullet.speed *= 30;
            Game1.bulletList.Add(newBullet);

            newBullet = new Bullets(Game1.bulletTexture);
            //newBullet.speed = new Vector2(0, -30);
            newBullet.position = new Vector2(playerPos.X + playerTexture.Width / 2.6f, playerPos.Y);
            newBullet.angle = 80 * 0.01745f;
            newBullet.speed.X = (float)Math.Cos(newBullet.angle);
            newBullet.speed.Y = -(float)Math.Sin(newBullet.angle);
            newBullet.speed *= 30;
            Game1.bulletList.Add(newBullet);

            newBullet = new Bullets(Game1.bulletTexture);
            //newBullet.speed = new Vector2(0, -30);
            newBullet.position = new Vector2(playerPos.X + playerTexture.Width / 2.6f, playerPos.Y);
            newBullet.angle = 90 * 0.01745f;
            newBullet.speed.X = (float)Math.Cos(newBullet.angle);
            newBullet.speed.Y = -(float)Math.Sin(newBullet.angle);
            newBullet.speed *= 30;
            Game1.bulletList.Add(newBullet);

            playerPos.Y = playerPos.Y - -5;
        }

        public void BulletHell()
        {
            Game1.laserLjud.Play();

            for (float a = 0; a < 360; a += 10)
            {
                Bullets newBullet = new Bullets(Game1.bulletTexture);
                //newBullet.speed = new Vector2(0, -30);
                newBullet.position = new Vector2(playerPos.X + playerTexture.Width / 2.6f, playerPos.Y);
                newBullet.angle = a * 0.01745f;
                newBullet.speed.X = (float)Math.Cos(newBullet.angle);
                newBullet.speed.Y = -(float)Math.Sin(newBullet.angle);
                newBullet.speed *= 30;
                Game1.bulletList.Add(newBullet);
            }

            playerPos.Y = playerPos.Y - -5;
        }

        public void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();
            
            //spelare rörelse
            playerPos += playerSpeed;

            if (keyboard.IsKeyDown(Keys.Up))
            {
                playerSpeed.Y = -8f;
            }
            else if (keyboard.IsKeyDown(Keys.Down))
            {
                playerSpeed.Y = 8f;
            }
            else
            {
                playerSpeed.Y = 0f;
            }

            if (keyboard.IsKeyDown(Keys.Left) && keyboard.IsKeyUp(Keys.Right))
            {
                playerSpeed.X = -8f;
                turningLeft = true;
            }
            else if (keyboard.IsKeyDown(Keys.Right) && keyboard.IsKeyUp(Keys.Left))
            {
                playerSpeed.X = 8f;
                turningRight = true;
            }
            else
            {
                playerSpeed.X = 0f;
                turningRight = false;
                turningLeft = false;
            }

            //Shoot
            shootTimer--;

            if (keyboard.IsKeyDown(Keys.Space) && shootTimer <= 0)
            {
                if (PowerUp.timer > 0)
                {
                    shootTimer = 10;
                    BulletHell();
                }
                else
                {
                    shootTimer = 5;
                    TrippleMachineGun();
                }

            }


            //X-rullning
            /*if (playerPos.X < -playerTexture.Width)
            {
                playerPos = new Vector2(800, playerPos.Y);
            }
            else if (playerPos.X > 800)
            {
                playerPos = new Vector2(-playerTexture.Width, playerPos.Y);
            }
            */
        }

        void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
