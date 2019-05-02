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
        public Texture2D textureForBoss;

        public LevelManager(Texture2D _textureForEnemy, Texture2D bossTexture)
        {
            textureForEnemy = _textureForEnemy;
            textureForBoss = bossTexture;
        }

        public void Update(GameTime gameTime)
        {

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
                    Enemy enemy = new Enemy(textureForEnemy);
                    Game1.enemyList.Add(enemy);
                    enemy.position = new Vector2(-50, 200);

                    Enemy enemy2 = new Enemy(textureForEnemy);
                    Game1.enemyList.Add(enemy2);
                    enemy2.position = new Vector2(350, 100);
                }
                else if (level == 3)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Enemy enemy = new Enemy(textureForEnemy);
                        Game1.enemyList.Add(enemy);
                        enemy.position = new Vector2(0, 100);

                        enemy.position.X += 50 * i;
                        enemy.position.Y += 50 * i;
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
                else if (level == 5)
                {
                    Boss boss1 = new Boss(textureForBoss);
                    Game1.enemyList.Add(boss1);
                    boss1.position = new Vector2(200, 3);
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
