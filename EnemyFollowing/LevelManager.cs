using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkost
{
    class LevelManager
    {
        public int level;
        public List<Enemy> enemies = new List<Enemy>();
        public Texture2D textureForEnemy;

        public LevelManager(Texture2D _textureForEnemy)
        {
            textureForEnemy = _textureForEnemy;
        }

        public void Update(GameTime gameTime)
        {
            if(enemies.Count == 0)
            {
                level++;
            }

            if (level == 1)
            {
                for (int i = 0; i < 1; i++)
                {
                    Enemy enemy = new Enemy(textureForEnemy);
                    enemies.Add(enemy);
                    enemy.position = new Vector2(50, 30);
                    enemy.enemyShootTimer = 100;
                }
            }
            else if (level == 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    Enemy enemy = new Enemy(textureForEnemy);
                    enemies.Add(enemy);
                    enemy.position = new Vector2(50, 30);
                    enemy.enemyShootTimer = 100;
                }
            }
            else if (level == 3)
            {
                for (int i = 0; i < 4; i++)
                {
                    Enemy enemy = new Enemy(textureForEnemy);
                    enemies.Add(enemy);
                    enemy.position = new Vector2(50, 30);
                    enemy.enemyShootTimer = 100;
                }
            }
            else if (level == 4)
            {
                for (int i = 0; i < 8; i++)
                {
                    Enemy enemy = new Enemy(textureForEnemy);
                    enemies.Add(enemy);
                    enemy.position = new Vector2(50, 30);
                    enemy.enemyShootTimer = 100;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in enemies)
            {
                spriteBatch.Draw(textureForEnemy, enemy.position, Color.White);
            }
        }

    }
}
