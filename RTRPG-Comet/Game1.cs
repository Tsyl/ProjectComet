﻿using Microsoft.Xna.Framework;
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

        Menu menu;
        Fight fight;
        Player[] players;

        SpriteFont fightFont;

        /// <summary> The game manager. </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            
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

            DrawHelper.graphicsDevice = GraphicsDevice;
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
            spriteBatch = new SpriteBatch(GraphicsDevice);

            fightFont = Content.Load<SpriteFont>("test");
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
                    fight = new Fight(players[0], players[1]);
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
