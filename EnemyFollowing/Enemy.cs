using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkost
{
    public class Enemy
    {
        public Texture2D texture;
        public Rectangle enemyBox;
        public Vector2 position;
        public Vector2 speed;
        public int shootTimer;
        public int HP;
        public bool isHit = false;
        
        public Enemy(Texture2D _texture)
        {
            texture = _texture;
            speed = new Vector2(5, 0);
            HP = 50;
            shootTimer = 1;
        }

        public void Update(GameTime gameTime)
        {
            
            //Fiende rörelse
            foreach (Drifter drifter in Game1.enemyList)
            {
                drifter.position += drifter.speed;

                if (drifter.position.X >= 750)
                {
                    drifter.speed.X = -5;
                }
                else if (drifter.position.X <= -50)
                {
                    drifter.speed.X = 5;
                }

                if (drifter.position.X >= 700 && drifter.texture == textureForBoss)
                {
                    drifter.speed.X = -5;
                }
                else if (drifter.position.X <= -50 && drifter.texture == textureForBoss)
                {
                    drifter.speed.X = 5;
                }
            }
            //Fiende skott
            foreach (Drifter enemy in Game1.enemyList)
            {
                enemy.shootTimer--;

                if (enemy.shootTimer == 0)
                {
                    Game1.laserLjud.Play(0.3f, 0, 0);
                    Bullets newBullet = new Bullets(Game1.bulletTexture);
                    newBullet.speed = new Vector2(0, 5);
                    newBullet.position = new Vector2(enemy.position.X + Player.playerTexture.Width / 2.6f, enemy.position.Y + Player.playerTexture.Height);
                    Game1.enemyBulletList.Add(newBullet);
                    enemy.shootTimer = (Game1.rnd.Next(0, 100));
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void DrawHit(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.Red);
        }
    }

    public class Drifter : Enemy
    {
        public drifter(Texture2D t) : base(t)
        {
            texture = t;
        }
    }

    public class Boss : Enemy
    {

        public Boss(Texture2D t) : base(t)
        {
            t = texture;
            speed = new Vector2(1, 0);
            HP = 500;
            shootTimer = 5;
        }
    }
}
