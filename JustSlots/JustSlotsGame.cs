using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace JustSlots
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class JustSlotsGame : Game
    {
        GraphicsDeviceManager graphics;

        public TextureManager Textures { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public SlotsMachine SlotsMachine { get; private set; }
        public SoundManager SoundManager { get; private set; }

        public JustSlotsGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public T CreateComponent<T>() where T : IGameComponent, new()
        {
            T component = new T();
            component.Game = this;
            component.Initalize();
            return component;
        }

        protected override void Initialize()
        {
            base.Initialize();

            graphics.PreferredBackBufferWidth = 768;
            graphics.PreferredBackBufferHeight = 550;
            graphics.ApplyChanges();

            SlotsMachine = CreateComponent<SlotsMachine>();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            Textures = CreateComponent<TextureManager>();
            Textures.LoadContent();

            SoundManager = CreateComponent<SoundManager>();
            SoundManager.LoadContent();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            SlotsMachine.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SlotsMachine.Draw(gameTime);

            base.Draw(gameTime);
        }

        public static Random Random { get; private set; } = new Random();
    }
}
