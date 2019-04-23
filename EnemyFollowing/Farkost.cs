using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace Farkost
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Spelare
        Player player;

        //BulletData
        public static Texture2D bulletTexture;
        public static List<Bullets> bulletList = new List<Bullets>();

        //Powerup
        Texture2D powerUpTexture;
        List<PowerUp> powerUpList = new List<PowerUp>();

        //Enemy
        public Texture2D enemyTexture;
        public static List<Enemy> enemyList = new List<Enemy>();
        List<Bullets> enemyBulletList = new List<Bullets>();

        //Stages
        LevelManager levelManager;

        //Övrigt
        Texture2D background;
        Vector2 backgroundPosition;
        Song music;
        public static SoundEffect laserLjud;
        public SoundEffect playerHit;
        public SoundEffect explosionLjud;
        public SoundEffect enemyHit;
        public KeyboardState keyboard;
        Random rnd = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = 400;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 600;   // set this value to the desired height of your window
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            backgroundPosition = new Vector2(0, 0);

            player = new Player();


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //Sprites
            player.playerTexture = Content.Load<Texture2D>("player");
            player.playerTurnRight = Content.Load<Texture2D>("playerTurnRight");
            player.playerTurnLeft = Content.Load<Texture2D>("playerTurnLeft");
            background = Content.Load<Texture2D>("BAKGRUNDEN");
            bulletTexture = Content.Load<Texture2D>("bullet");
            enemyTexture = Content.Load<Texture2D>("enemy");
            powerUpTexture = Content.Load<Texture2D>("powerup");

            //Musik och ljud
            music = Content.Load<Song>("music");
            laserLjud = Content.Load<SoundEffect>("skott");
            explosionLjud = Content.Load<SoundEffect>("explosion");
            enemyHit = Content.Load<SoundEffect>("hit");
            playerHit = Content.Load<SoundEffect>("playerhit");

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.Play(music);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            keyboard = Keyboard.GetState();

            //Uppdatera stages
            levelManager = new LevelManager(enemyTexture);
            levelManager.Update(gameTime);

            //Uppdatera spelaren
            player.Update(gameTime);

             //"Marken"
            if (player.playerPos.Y >= 600 - player.playerTexture.Height)
            {
                player.playerPos.Y = 600 - player.playerTexture.Height;
            }
            
            //Spelare hitbox
            Rectangle hitBox = new Rectangle((int)player.playerPos.X, (int)player.playerPos.Y, 80, 5);

            //När man dör så händer detta
            if(player.HP <= 0)
            {
                Exit();
            }

            /*//Skapa fiende
            enemySpawnTimer--;
            if (enemySpawnTimer == 0)
            {
                Enemy enemy = new Enemy(enemyTexture);
                enemyList.Add(enemy);
                enemy.position = new Vector2(50, 30);
                enemy.enemyShootTimer = 55;
                enemySpawnTimer = 100;
            }*/

            //Fiende rörelse
            foreach (Enemy enemy in enemyList)
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
            foreach (Enemy enemy in enemyList)
            {
                enemy.enemyShootTimer--;

                if (enemy.enemyShootTimer == 0)
                {
                    laserLjud.Play(0.3f, 0, 0);
                    Bullets newBullet = new Bullets(bulletTexture);
                    newBullet.speed = new Vector2(0, 5);
                    newBullet.position = new Vector2(enemy.position.X + player.playerTexture.Width / 2.6f, enemy.position.Y + player.playerTexture.Height);
                    enemyBulletList.Add(newBullet);
                    enemy.enemyShootTimer = 70;
                }
            }

            //Skapa hitbox för bullets
            foreach (Bullets bullet in bulletList)
            {
                bullet.position += bullet.speed;
                bullet.bulletBox = new Rectangle((int)bullet.position.X, (int)bullet.position.Y, 10, 10);
            }

            foreach (Bullets bullet in enemyBulletList)
            {
                bullet.position += bullet.speed;
                bullet.bulletBox = new Rectangle((int)bullet.position.X, (int)bullet.position.Y, 10, 1);
            }

            //Skapa hitboxar för enemies
            foreach (Enemy enemy in enemyList)
            {
                enemy.enemyBox = new Rectangle((int)enemy.position.X, (int)enemy.position.Y, 100, 25);
            }

            //Bullets träffar fiender
            foreach (Bullets bullet in bulletList) 
            {
                foreach (Enemy enemy in enemyList)
                {
                    for(int i = 0; i < enemyList.Count; i++)
                    {
                        if (bullet.bulletBox.Intersects(enemy.enemyBox))
                        {
                            enemyHit.Play(0.6f, 0, 0);
                            enemy.HP--;
                        }
                    }
                }
            }

            //När fiende HP når noll
            {
                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (enemyList[i].HP < 1)
                    {
                        explosionLjud.Play(0.3f, 0, 0);

                        if (rnd.Next(0, 2) == 0)
                        {
                            PowerUp powerUp = new PowerUp(powerUpTexture);
                            powerUpList.Add(powerUp);
                            powerUp.position.X = enemyList[i].position.X + 50;
                            powerUp.position.Y = enemyList[i].position.Y;
                        }

                        enemyList.RemoveAt(i);
                        i--;

                        
                    }
                    else if(enemyList[i].HP < 5)
                    {
                        enemyList[i].isHit = true;
                    }

                }
            }

            //Fiendebullets träffar spelare
            foreach (Bullets bullet in enemyBulletList)
            {
                if(bullet.bulletBox.Intersects(hitBox))
                {
                    player.HP--;
                    playerHit.Play(0.6f, 0, 0);
                }
            }



            //powerUp
            for (int i = 0; i < powerUpList.Count; i++)
            {
                powerUpList[i].Update(gameTime);

                Rectangle powerUpBox = new Rectangle((int)powerUpList[i].position.X, (int)powerUpList[i].position.Y, powerUpTexture.Width, powerUpTexture.Height);

                if (powerUpBox.Intersects(hitBox))
                {
                    powerUpList.RemoveAt(i);
                    PowerUp.timer = 1000;
                    i--;
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            //Rita ut bakgrund
            spriteBatch.Draw(background, backgroundPosition, Color.DarkGreen);

            //Rita ut spelare
            if(player.turningRight == true)
            {
                spriteBatch.Draw(player.playerTurnRight, player.playerPos, Color.White);
            }
            else if(player.turningLeft == true)
            {
                spriteBatch.Draw(player.playerTurnLeft, player.playerPos, Color.White);
            }
            else
            {
                spriteBatch.Draw(player.playerTexture, player.playerPos, Color.White);
            }

            //Rita ut fiende
            foreach (Enemy enemy in enemyList)
            {
                if(enemy.isHit == true)
                {
                    enemy.DrawHit(spriteBatch);
                }
                else
                {
                    enemy.Draw(spriteBatch);
                }
            }

            //Rita ut fiendens bullets
            foreach (Bullets enemyBullet in enemyBulletList)
            {
                enemyBullet.Draw(spriteBatch);
            }

            //Rita ut bullets
            foreach (Bullets bullet in bulletList)
            {
                bullet.Draw(spriteBatch);
            }

            //Rita ut powerups
            foreach (PowerUp powerUp in powerUpList)
            {
                powerUp.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
