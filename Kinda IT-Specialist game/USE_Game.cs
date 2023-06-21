using Game2D.BasicElements;
using Game2D.UI;
using Game2D.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Game2D
{
    public class USE_Game : Game
    {
        public static int ScreenWidth { get; set; }
        public static int ScreenHeight { get; set; }
        public static Vector2 ActualCenterOfGameWorld { get; set; }

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static SpriteFont StandardFont;
        public static MainCamera Camera;
        public static RelativeMouse Mouse;
        public static Controller Controller;
        public static Random Random;

        private State currentState;
        private State nextState;

        private List<Component> menuComponents;
        private List<Component> guideComponents;
        private List<Component> pauseComponents;
        private List<Component> endingComponents;

        private Menu menuState;
        private Guide guideState;
        private GameProcess gameState;
        private Pause pauseState;
        private EndingWindow resultShowingState;

        private double seconds = 0.5;

        public USE_Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;
            ActualCenterOfGameWorld = Vector2.Zero;
            Window.Title = "IT Specialist Game";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            GameTextures.LoadTextures(Content);
            GameMusic.LoadMusicAndSounds(Content);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            StandardFont = Content.Load<SpriteFont>("Fonts\\serviceText");

            Random = new Random();
            Camera = new MainCamera();
            Mouse = new RelativeMouse(Camera);
            Controller = new Controller();

            menuComponents = GameUICreator.BuildMenu();
            guideComponents = GameUICreator.BuildGuide();
            pauseComponents = GameUICreator.BuildPause();
            endingComponents = GameUICreator.BuildEnding();

            menuState = new Menu(Content, _graphics.GraphicsDevice, this, menuComponents);
            currentState = menuState;
            guideState = new Guide(Content, _graphics.GraphicsDevice, this, guideComponents);
            pauseState = new Pause(Content, _graphics.GraphicsDevice, this, pauseComponents);
            resultShowingState = new EndingWindow(Content, _graphics.GraphicsDevice, this, endingComponents);

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(GameMusic.MenuMusic);
        }

        private void SwitchToStateWithDelayAfter(State state)
        {
            GameStateData.IsDelayBeforeSwitchingStatesActive = true;
            seconds = 0.5;
            nextState = state;
        }

        private void StatesCheck()
        {
            if (GameStateData.ShowGuide && currentState.GetType() != typeof(Guide))
            {
                currentState = guideState;
            }

            if (!GameStateData.ShowGuide && currentState.GetType() == typeof(Guide))
            {
                currentState = menuState;
            }

            if (GameStateData.MenuStateRequired && currentState.GetType() != typeof(Menu))
            {
                GameStateData.MenuStateRequired = false;
                MediaPlayer.Play(GameMusic.MenuMusic);
                currentState = menuState;
                Camera.ResetPosition();
            }

            if (GameStateData.GameOver && currentState.GetType() != typeof(EndingWindow))
            {
                MediaPlayer.Stop();
                Camera.ResetPosition();
                nextState = resultShowingState;
                var resultMessage = (ResultMessage)endingComponents.Find(x => x.GetType() == typeof(ResultMessage));
                UpdateResultMessage(resultMessage);
            }

            if (GameStateData.Paused && currentState.GetType() != typeof(Pause))
            {
                MediaPlayer.Pause();
                SwitchToStateWithDelayAfter(pauseState);
                Camera.ResetPosition();
            }

            if (!GameStateData.Paused && currentState.GetType() == typeof(Pause))
            {
                MediaPlayer.Resume();
                SwitchToStateWithDelayAfter(gameState);
            }
        }

        private void StartGameCheck()
        {
            if (GameStateData.NeedToStartGame)
            {
                StartNewGame();
                GameStateData.NeedToStartGame = false;
            }
        }

        private void DelayCheck(GameTime gameTime)
        {
            if (GameStateData.IsDelayBeforeSwitchingStatesActive)
            {
                seconds -= gameTime.ElapsedGameTime.TotalSeconds;
                if (seconds <= 0) GameStateData.IsDelayBeforeSwitchingStatesActive = false;
            }
        }

        private void UpdateState()
        {
            if (nextState != null)
            {
                currentState = nextState;
                nextState = null;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GameStateData.Quit) Exit();

            Controller.Update(gameTime);

            StartGameCheck();

            DelayCheck(gameTime);

            UpdateState();

            currentState.Update(gameTime);

            StatesCheck();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            currentState.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }

        private void StartNewGame()
        {
            MediaPlayer.Play(GameMusic.InGameMusic);
            GameStateData.ResetAllInfo();
            gameState = new GameProcess(Content, _graphics.GraphicsDevice, this);
            gameState.LoadGameProcess();
            nextState = gameState;
        }

        private void SetMessageWithData(ResultMessage message, SoundEffect effect, double scorePercentage, string text, Color color) 
        {
            effect.Play();
            message.SetMessage(Math.Round(scorePercentage, 2), text, color);
        }

        private void UpdateResultMessage(ResultMessage message)
        {
            var scorePercentage = GameStateData.ResultScore * 100.0 / GameStateData.IdealScore;

            if (scorePercentage <= GameStateData.BadScorePercentage)
                SetMessageWithData(message, GameMusic.BadResult, scorePercentage, GameStateData.FiredText, Color.Red);

            else if (scorePercentage > GameStateData.BadScorePercentage && scorePercentage <= GameStateData.NotSoBadScorePercentage)
                SetMessageWithData(message, GameMusic.NotSoBadResult, scorePercentage, GameStateData.ReplacementText, Color.Orange);

            else if (scorePercentage > GameStateData.NotSoBadScorePercentage && scorePercentage <= GameStateData.AverageScorePercentage)
                SetMessageWithData(message, GameMusic.GoodResult, scorePercentage, GameStateData.GreatPotentialText, Color.Yellow);

            else if (scorePercentage > GameStateData.AverageScorePercentage)
                SetMessageWithData(message, GameMusic.GreatResult, scorePercentage, GameStateData.LimitlessPotentialText, Color.Green);
        }

        public void SimplifiedUpdateForTests()
        {
            if (GameStateData.Quit) Exit();

            if (GameStateData.NeedToStartGame)
            {
                StartNewGame();
                GameStateData.NeedToStartGame = false;
            }

            if (GameStateData.IsDelayBeforeSwitchingStatesActive)
                if (seconds <= 0) GameStateData.IsDelayBeforeSwitchingStatesActive = false;

            if (nextState != null)
            {
                currentState = nextState;
                nextState = null;
            }

            if (GameStateData.GameOver && currentState.GetType() != typeof(EndingWindow))
            {
                nextState = resultShowingState;
            }

            if (GameStateData.Paused && currentState.GetType() != typeof(Pause))
            {
                SwitchToStateWithDelayAfter(pauseState);
            }

            if (!GameStateData.Paused && currentState.GetType() == typeof(Pause))
            {
                SwitchToStateWithDelayAfter(gameState);
            }
        }
    }
}