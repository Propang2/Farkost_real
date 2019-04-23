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
        public int enemyShootTimer;
        public int HP;
        public bool isHit = false;
        
        public Enemy(Texture2D _texture)
        {
            texture = _texture;
            speed = new Vector2(5, 0);
            HP = 8;
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
}
