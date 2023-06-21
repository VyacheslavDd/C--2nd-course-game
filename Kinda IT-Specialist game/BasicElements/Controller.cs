using Game2D.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game2D.BasicElements;

public class Controller
{
    private MainPlayer player;

    private Action pause;

    private Action<GameTime> moveLeftPlayer;
    private Action<GameTime> moveRightPlayer;
    private Action<GameTime> moveUpPlayer;
    private Action<GameTime> moveDownPlayer;
    private Action<GameTime> noMovePlayer;
    private Action playerInteraction;
    private Action restartPosition;
    private Action run;
    private Action walk;


    private KeyboardState currentState;

    public KeyboardState CurrentState => currentState;

    public Controller()
    {
        pause = () =>
        {
            if (!GameStateData.IsDelayBeforeSwitchingStatesActive && !GameStateData.GameOver)
                GameStateData.Paused = !GameStateData.Paused;
        };
    }

    public void SetPlayer(MainPlayer player)
    {
        this.player = player;

        moveLeftPlayer = (gametime) => player.Move(Directions.Left, (float)gametime.ElapsedGameTime.TotalSeconds);
        moveRightPlayer = (gametime) => player.Move(Directions.Right, (float)gametime.ElapsedGameTime.TotalSeconds);
        moveUpPlayer = (gametime) => player.Move(Directions.Up, (float)gametime.ElapsedGameTime.TotalSeconds);
        moveDownPlayer = (gametime) => player.Move(Directions.Down, (float)gametime.ElapsedGameTime.TotalSeconds);
        noMovePlayer = (gametime) => player.Move(Directions.None, (float)gametime.ElapsedGameTime.TotalSeconds);
        playerInteraction = () => player.InteractWithProblematicCharacters();
        restartPosition = () => player.RestartPosition();
        run = () => player.Run();
        walk = () => player.Walk();
    }

    public void Update(GameTime gametime)
    {
        var state = Keyboard.GetState();
        currentState = state;

        if (player != null && !GameStateData.Paused)
        {
            if (state.IsKeyDown(Keys.Escape)) pause();

            if (state.GetPressedKeys().Length == 0) noMovePlayer(gametime);
            else
            {
                if (state.IsKeyDown(Keys.A)) moveLeftPlayer(gametime);
                if (state.IsKeyDown(Keys.D)) moveRightPlayer(gametime);
                if (state.IsKeyDown(Keys.W)) moveUpPlayer(gametime);
                if (state.IsKeyDown(Keys.S)) moveDownPlayer(gametime);
                if (state.IsKeyDown(Keys.E)) playerInteraction();
                if (state.IsKeyDown(Keys.R)) restartPosition();
                if (state.IsKeyDown(Keys.LeftShift) && player.IsMoving) run();
                if (state.IsKeyUp(Keys.LeftShift)) walk();
            }
        }
    }
}