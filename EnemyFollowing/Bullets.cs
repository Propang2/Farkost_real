using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkost
{
    public class Bullets
    {
        public Texture2D texture;
        public Rectangle bulletBox;
        public Vector2 position;
        public Vector2 speed;
        public float angle;

        public Bullets(Texture2D _texture)
        {
            texture = _texture;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
