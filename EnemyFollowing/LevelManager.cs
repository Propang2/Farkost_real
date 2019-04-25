using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkost
{
    public class LevelManager
    {
        public int level;
        public Texture2D textureForEnemy;

        public LevelManager(Texture2D _textureForEnemy)
        {
            textureForEnemy = _textureForEnemy;
        }

        public void Update(GameTime gameTime)
        {
            //Fiende rörelse
            foreach (Enemy enemy in Game1.enemyList)
            {
                enemy.position += enemy.speed;

                if (enemy.position.X >= 350)
                {
                    enemy.speed.X = -5;
                }
                else if (enemy.position.X <= -50)
                {
                    enemy.speed.X = 5;
                }
            }
            //Fiende skott
            foreach (Enemy enemy in Game1.enemyList)
            {
                enemy.enemyShootTimer--;

                if (enemy.enemyShootTimer == 0)
                {
                    Game1.laserLjud.Play(0.3f, 0, 0);
                    Bullets newBullet = new Bullets(Game1.bulletTexture);
                    newBullet.speed = new Vector2(0, 5);
                    newBullet.position = new Vector2(enemy.position.X + Player.playerTexture.Width / 2.6f, enemy.position.Y + Player.playerTexture.Height);
                    Game1.enemyBulletList.Add(newBullet);
                    enemy.enemyShootTimer = (Game1.rnd.Next(0, 100));
                }
            }

            if (Game1.enemyList.Count <= 0)
            {
                level++;

                if (level == 1)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        Enemy enemy = new Enemy(textureForEnemy);
                        Game1.enemyList.Add(enemy);
                        enemy.position = new Vector2(50, 30);
                    }
                }
                else if (level == 2)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Enemy enemy = new Enemy(textureForEnemy);
                        Game1.enemyList.Add(enemy);
                        enemy.position = new Vector2(Game1.rnd.Next(50, 400), (Game1.rnd.Next(0, 200)));
                    }
                }
                else if (level == 3)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Enemy enemy = new Enemy(textureForEnemy);
                        Game1.enemyList.Add(enemy);
                        enemy.position = new Vector2(Game1.rnd.Next(50, 400), (Game1.rnd.Next(0, 200)));
                    }
                }
                else if (level == 4)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Enemy enemy = new Enemy(textureForEnemy);
                        Game1.enemyList.Add(enemy);
                        enemy.position = new Vector2(Game1.rnd.Next(50, 400), (Game1.rnd.Next(0, 200)));
                    }
                }

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in Game1.enemyList)
            {
                spriteBatch.Draw(textureForEnemy, enemy.position, Color.White);
            }
        }

    }
}
