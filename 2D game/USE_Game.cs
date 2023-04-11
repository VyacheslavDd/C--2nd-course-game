using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Security.AccessControl;

namespace _2D_game
{
    public class USE_Game : Game
    {
        public static int ScreenWidth;
        public static int ScreenHeight;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D map;
        private Texture2D mainCharacterSprite;

        private MainCamera camera;
        private MainPlayer player;
        private List<Component> gameComponents;

        public USE_Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 700;
            _graphics.PreferredBackBufferHeight = 450;
            _graphics.ApplyChanges();

            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;

            Window.Title = "IT Specialist Game";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            map = Content.Load<Texture2D>("map2");
            mainCharacterSprite = Content.Load<Texture2D>("only_man-transformed");

            camera = new MainCamera();
            player = new MainPlayer(mainCharacterSprite, 100f, new Vector2(180, 40), new Vector2(0.7f, 0.7f));

            gameComponents = new List<Component>()
            {
                new Sprite(map),
                player
            };
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            foreach (var component in gameComponents)
                component.Update(gameTime);

            camera.Follow(player);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            SpriteBatchDrawing(gameTime);

            base.Draw(gameTime);
        }

        private void SpriteBatchDrawing(GameTime gametime)
        {
            _spriteBatch.Begin(transformMatrix:camera.Transform);

            foreach (var component in gameComponents)
                component.Draw(gametime, _spriteBatch);

            _spriteBatch.End();
        }
    }
}