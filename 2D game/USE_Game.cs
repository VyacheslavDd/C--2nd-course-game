using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private Texture2D background;
        private Texture2D mainCharacterSprite;
        private Texture2D teacher;
        private Texture2D pupilOrangeMan;
        private Texture2D pupilBlueMan;
        private Texture2D pupilGreenWoman;
        private Texture2D pupilBlueWoman;

        private Texture2D table_horizontal_col;
        private Texture2D table_vertical_col;
        private Texture2D chair_horizontal_col;
        private Texture2D chair_vertical_col;
        private Texture2D horizontal_wall;
        private Texture2D vertical_wall;

        private MainCamera camera;
        private MainPlayer player;

        private List<Component> colliders;
        private List<Component> pupilsAndTeachers;
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

        private void LoadMapAndCharacters()
        {
            map = Content.Load<Texture2D>("map2");
            background = Content.Load<Texture2D>("cosmos");
            mainCharacterSprite = Content.Load<Texture2D>("only_man-transformed");
            teacher = Content.Load<Texture2D>("teacher");
            pupilOrangeMan = Content.Load<Texture2D>("only_citizen1-transformed");
            pupilBlueMan = Content.Load<Texture2D>("only_citizen2-transformed");
            pupilGreenWoman = Content.Load<Texture2D>("only_citizen3-transformed");
            pupilBlueWoman = Content.Load<Texture2D>("only_citizen4-transformed");
        }

        private void LoadColliders()
        {
            table_horizontal_col = Content.Load<Texture2D>("table_collider");
            chair_horizontal_col = Content.Load<Texture2D>("chair_collider");
            table_vertical_col = Content.Load<Texture2D>("table_col_vertical");
            chair_vertical_col = Content.Load<Texture2D>("chair_col_vertical");
            horizontal_wall = Content.Load<Texture2D>("up_left_wall");
            vertical_wall = Content.Load<Texture2D>("vertical_wall");
        }

        private void LoadFonts()
        {
        }

        private void CreateObjects()
        {
            camera = new MainCamera();
            player = new MainPlayer(mainCharacterSprite, 200f, new Vector2(160, 40), new Vector2(0.7f, 0.7f));

            CreateAdditionalCharacters();
        }

        private void CreateAdditionalCharacters()
        {
            pupilsAndTeachers = new List<Component>()
            {
                new CharacterWithProblems(pupilBlueMan, new Vector2(68, 106), new Vector2(2.1f, 2.1f), SpriteEffects.FlipHorizontally),
                new CharacterWithProblems(pupilGreenWoman, new Vector2(68, 226), new Vector2(2.1f, 2.1f), SpriteEffects.FlipHorizontally),
                new CharacterWithProblems(pupilOrangeMan, new Vector2(68, 342), new Vector2(2.1f, 2.1f), SpriteEffects.FlipHorizontally),
                new CharacterWithProblems(pupilBlueWoman, new Vector2(68, 462), new Vector2(2.1f, 2.1f), SpriteEffects.FlipHorizontally),
                new CharacterWithProblems(pupilBlueWoman, new Vector2(706, 106), new Vector2(2.1f, 2.1f), SpriteEffects.None),
                new CharacterWithProblems(pupilOrangeMan, new Vector2(706, 226), new Vector2(2.1f, 2.1f), SpriteEffects.None),
                new CharacterWithProblems(pupilGreenWoman, new Vector2(706, 342), new Vector2(2.1f, 2.1f), SpriteEffects.None),
                new CharacterWithProblems(pupilBlueMan, new Vector2(706, 462), new Vector2(2.1f, 2.1f), SpriteEffects.None),
                new CharacterWithProblems(pupilOrangeMan, new Vector2(168, 490), new Vector2(2.1f, 2.1f), SpriteEffects.None),
                new CharacterWithProblems(pupilBlueMan, new Vector2(284, 485), new Vector2(2.1f, 2.1f), SpriteEffects.None),
                new CharacterWithProblems(pupilGreenWoman, new Vector2(400, 483), new Vector2(2.1f, 2.1f), SpriteEffects.None),
                new CharacterWithProblems(pupilBlueWoman, new Vector2(516, 482), new Vector2(2.1f, 2.1f), SpriteEffects.None),
                new CharacterWithProblems(pupilOrangeMan, new Vector2(632, 490), new Vector2(2.1f, 2.1f), SpriteEffects.None),
                new CharacterWithProblems(teacher, new Vector2(559, 40), new Vector2(0.28f, 0.28f), SpriteEffects.None)
            };
        }

        private void CreateColliders()
        {
            colliders = new List<Component>()
            {
                new InvisibleCollider(table_horizontal_col, new Vector2(203, 69), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(chair_horizontal_col, new Vector2(216, 53), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(table_horizontal_col, new Vector2(550, 69), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(chair_horizontal_col, new Vector2(567, 53), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(horizontal_wall, new Vector2(-3, 16), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(horizontal_wall, new Vector2(-3, 557), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(vertical_wall, new Vector2(-3, 10), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(vertical_wall, new Vector2(780, -7), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(table_vertical_col, new Vector2(47, 104), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(table_vertical_col, new Vector2(47, 223), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(table_vertical_col, new Vector2(47, 340), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(table_vertical_col, new Vector2(47, 458), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(chair_vertical_col, new Vector2(70, 111), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(chair_vertical_col, new Vector2(70, 231), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(chair_vertical_col, new Vector2(70, 351), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(chair_vertical_col, new Vector2(70, 468), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(table_vertical_col, new Vector2(730, 104), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(table_vertical_col, new Vector2(730, 223), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(table_vertical_col, new Vector2(730, 340), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(table_vertical_col, new Vector2(730, 458), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(chair_vertical_col, new Vector2(711, 111), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(chair_vertical_col, new Vector2(711, 231), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(chair_vertical_col, new Vector2(711, 351), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(chair_vertical_col, new Vector2(711, 468), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(table_horizontal_col, new Vector2(153, 513), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(table_horizontal_col, new Vector2(270, 513), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(table_horizontal_col, new Vector2(387, 513), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(table_horizontal_col, new Vector2(504, 513), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(table_horizontal_col, new Vector2(621, 513), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(chair_horizontal_col, new Vector2(168, 500), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(chair_horizontal_col, new Vector2(284, 500), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(chair_horizontal_col, new Vector2(403, 500), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(chair_horizontal_col, new Vector2(521, 500), Vector2.One, SpriteEffects.None),
                new InvisibleCollider(chair_horizontal_col, new Vector2(637, 500), Vector2.One, SpriteEffects.None)
            };
        }

        private void AddGameComponents()
        {
            gameComponents = new List<Component>()
            {
                new Sprite(background, Vector2.Zero, Vector2.One, SpriteEffects.None),
                new Sprite(map, Vector2.Zero, Vector2.One, SpriteEffects.None),
                player
            };
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadMapAndCharacters();
            LoadColliders();
            LoadFonts();

            CreateObjects();
            CreateColliders();

            AddGameComponents();

            gameComponents.AddRange(colliders);
            gameComponents.AddRange(pupilsAndTeachers);

            player.LoadColliders(colliders.Union(pupilsAndTeachers).ToList());
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