using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkost
{
    class PowerUp
    {
        public Vector2 position;
        public Vector2 speed;
        public Texture2D texture;

        //Fixa så den inte är static (eller Rensa bort powerups från lsitan)
        public static int timer;

        public PowerUp(Texture2D _texture)
        {
            texture = _texture;
            speed = new Vector2(0, 2);

        }

        public void Update(GameTime gameTime)
        {
            timer--;
            position += speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
