using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Comet
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ContentContainer contentLoader;
        Texture2D[] icons;
        SoundEffect[] sfxs;

        /// <summary>
        /// States of the Game.
        /// </summary>
        public enum GameState
        {
            /// <summary> The game is in a menu. </summary>
            Menu,
            /// <summary> The game is in a fight. </summary>
            Fight,
            /// <summary> The game is paused. </summary>
            Paused
        };
        /// <summary> Current state of the game. </summary>
        public GameState State { get; set; }

        InputManager input;
        DrawHelper drawHelper;

        Menu menu;
        Fight fight;
        Player[] players;

        SpriteFont fightFont;

        /// <summary> The game manager. </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            /*graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;*/

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
            #if DEBUG
                Window.Title = "Comet_d";
            #else
                Window.Title = "Comet";
#endif

            drawHelper = DrawHelper.GetInstance();
            drawHelper.Initialize(GraphicsDevice);
            input = InputManager.GetInstance();
            input.Initialize();
            players = new Player[2];

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            string contentFilename = "Content\\CometContent.json";
            Character[] characters = new Character[1];
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Read Content file and deserialize data.
            try
            {
                StreamReader sr = new StreamReader(contentFilename);
                characters = JsonConvert.DeserializeObject<List<Character>>(sr.ReadToEnd()).ToArray();
            }
            catch (FileNotFoundException e)
            {
                throw e.GetBaseException();
            }
            catch
            {
                throw new System.Exception("We found the file, but something else went wrong...");
            }
            
            icons = new Texture2D[characters.Length * 4];
            sfxs = new SoundEffect[characters.Length * 4];

            // Load all data
            // Fonts
            fightFont = Content.Load<SpriteFont>("test");

            // Character Icons and Sound Effects.
            foreach (Character chr in characters)
            {
                foreach (Skill skl in chr.skills)
                {
                    if(skl.iconName != null)
                    {
                        try
                        {
                            skl.icon = Content.Load<Texture2D>(skl.iconName);
                        }
                        catch { }
                    }

                    if (skl.sfxName != null)
                    {
                        try
                        {
                            skl.sfx = Content.Load<SoundEffect>(skl.sfxName);
                        }
                        catch { }
                    }
                }
            }

            /*
            skillIcons[0] = Content.Load<Texture2D>("assets/skill_icon_MightMakesRight");
            skillIcons[1] = Content.Load<Texture2D>("assets/skill_icon_Scorch");
            skillIcons[2] = Content.Load<Texture2D>("assets/skill_icon_Sentinel");
            skillIcons[3] = Content.Load<Texture2D>("assets/skill_icon_GraveyardBash");
            skillIcons[4] = Content.Load<Texture2D>("assets/skill_icon_Stalk");
            skillIcons[5] = Content.Load<Texture2D>("assets/skill_icon_DaggerStrike");
            skillIcons[6] = Content.Load<Texture2D>("assets/skill_icon_ShadowSomersault");
            skillIcons[7] = Content.Load<Texture2D>("assets/skill_icon_ShellShock");
            */

            contentLoader = new ContentContainer(characters);
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

            input.Update();

            if (State == GameState.Menu)
            {
                foreach(Player p in input.players)
                {
                    if(p != null && p.inputs.joinGame)
                    {
                        if (players[0] == null)
                            players[0] = p;
                        else if (p != players[0] && players[1] == null)
                            players[1] = p;
                    }
                }

                if (players[1] != null)
                {
                    fight = new Fight(players[0], players[1], contentLoader.GetParty(0), contentLoader.GetParty(1));
                }

                if (fight != null)
                {
                    State = GameState.Fight;
                }
            }

            if (State == GameState.Fight)
            {
                fight.Update(gameTime);

                if (fight.isOver)
                {
                    State = GameState.Menu;
                    fight = null;
                    players = new Player[2];
                }
            }

            if (State == GameState.Paused)
            {

            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            spriteBatch.Begin();
            if (State == GameState.Menu)
            {
                drawHelper.DrawLine(spriteBatch, Vector2.Zero, new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Red);
            }

            if (State == GameState.Fight)
            {
                fight.Draw(spriteBatch, fightFont);
            }

            if (State == GameState.Paused)
            {

            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
