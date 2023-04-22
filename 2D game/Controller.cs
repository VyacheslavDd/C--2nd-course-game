using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace _2D_game;

public class Controller
{
    private MainPlayer player;

    private Action<GameTime> moveLeftPlayer;
    private Action<GameTime> moveRightPlayer;
    private Action<GameTime> moveUpPlayer;
    private Action<GameTime> moveDownPlayer;

    public Controller(MainPlayer player)
    {
        this.player = player;

        moveLeftPlayer = (gametime) => player.Move(Directions.Left, (float)gametime.ElapsedGameTime.TotalSeconds);
        moveRightPlayer = (gametime) => player.Move(Directions.Right, (float)gametime.ElapsedGameTime.TotalSeconds);
        moveUpPlayer = (gametime) => player.Move(Directions.Up, (float)gametime.ElapsedGameTime.TotalSeconds);
        moveDownPlayer = (gametime) => player.Move(Directions.Down, (float)gametime.ElapsedGameTime.TotalSeconds);
    }

    public void Update(GameTime gametime)
    {
        var state = Keyboard.GetState();

        if (state.IsKeyDown(Keys.A)) moveLeftPlayer(gametime);
        if (state.IsKeyDown(Keys.D)) moveRightPlayer(gametime);
        if (state.IsKeyDown(Keys.W)) moveUpPlayer(gametime);
        if (state.IsKeyDown(Keys.S)) moveDownPlayer(gametime);
    }
}