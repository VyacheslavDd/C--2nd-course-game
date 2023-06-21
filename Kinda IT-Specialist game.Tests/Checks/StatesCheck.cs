
using Game2D;
using Game2D.BasicElements;
using Game2D.Core;
using Game2DTests.Additionals;
using System.Reflection;

namespace Game2DTests.Checks;

public class StatesCheck : IDisposable
{
    private USE_Game game;
    public StatesCheck()
    {
        game = new USE_Game();
    }

    private void StartGame(object game)
    {
        StaticMethods.InitializeGame(game);
        StaticMethods.InvokeMethod("StartNewGame", game, new object[] { });
    }

    [Fact]
    public void CheckIfInitialStateIsMenu()
    {
        GameStateData.ResetAllInfo();
        StaticMethods.InitializeGame(game);
        var state = StaticMethods.GetValue("currentState", game);
        Assert.NotNull(state);
        game.Dispose();
    }

    [Fact]
    public void CheckIfGameRegisterNewState()
    {
        GameStateData.ResetAllInfo();
        StartGame(game);
        var state = StaticMethods.GetValue("nextState", game);
        Assert.True(state.GetType() == typeof(GameProcess));
        game.Dispose();
    }

    [Fact]
    public void CheckIfGameReallyStarts()
    {
        GameStateData.ResetAllInfo();
        StartGame(game);
        game.SimplifiedUpdateForTests();
        var state = StaticMethods.GetValue("currentState", game);
        Assert.True(state.GetType() == typeof(GameProcess));
        game.Dispose();
    }

    [Fact]
    public void CheckIfGameGetsPaused()
    {
        GameStateData.ResetAllInfo();
        StartGame(game);
        game.SimplifiedUpdateForTests();
        GameStateData.Paused = true;
        game.SimplifiedUpdateForTests();
        game.SimplifiedUpdateForTests();
        var state = StaticMethods.GetValue("currentState", game);
        Assert.True(state.GetType() == typeof(Pause));
        game.Dispose();
    }

    [Fact]
    public void CheckIfGameGetsUnPaused()
    {
        GameStateData.ResetAllInfo();
        StartGame(game);
        game.SimplifiedUpdateForTests();
        GameStateData.Paused = true;
        game.SimplifiedUpdateForTests();
        game.SimplifiedUpdateForTests();
        GameStateData.Paused = false;
        game.SimplifiedUpdateForTests();
        game.SimplifiedUpdateForTests();
        var state = StaticMethods.GetValue("currentState", game);
        Assert.True(state.GetType() == typeof(GameProcess));
        game.Dispose();
    }

    [Fact]
    public void CheckIfGameGetsEnded()
    {
        GameStateData.ResetAllInfo();
        StartGame(game);
        game.SimplifiedUpdateForTests();
        GameStateData.GameOver = true;
        game.SimplifiedUpdateForTests();
        game.SimplifiedUpdateForTests();
        var state = StaticMethods.GetValue("currentState", game);
        Assert.True(state.GetType() == typeof(EndingWindow));
        game.Dispose();
    }

    [Fact]
    public void CheckDelayWorking()
    {
        GameStateData.ResetAllInfo();
        StartGame(game);
        game.SimplifiedUpdateForTests();
        GameStateData.Paused = true;
        game.SimplifiedUpdateForTests();
        Assert.True(GameStateData.IsDelayBeforeSwitchingStatesActive);
        game.Dispose();
    }

    [Fact]
    public void CheckDelayStopsWorking()
    {
        GameStateData.ResetAllInfo();
        StartGame(game);
        game.SimplifiedUpdateForTests();
        GameStateData.Paused = true;
        game.SimplifiedUpdateForTests();
        game.SimplifiedUpdateForTests();
        Assert.True(GameStateData.IsDelayBeforeSwitchingStatesActive);
        StaticMethods.SetValue("seconds", game, 0);
        game.SimplifiedUpdateForTests();
        Assert.False(GameStateData.IsDelayBeforeSwitchingStatesActive);
        game.Dispose();
    }

    [Fact]
    public void CheckRestartResetsGameInfo()
    {
        GameStateData.ResetAllInfo();
        StartGame(game);
        game.SimplifiedUpdateForTests();
        GameStateData.Paused = true;
        StaticMethods.InvokeMethod("StartNewGame", game, new object[] { });
        Assert.False(GameStateData.Paused);
        GameStateData.GameOver = true;
        GameStateData.Paused = true;
        StaticMethods.InvokeMethod("StartNewGame", game, new object[] { });
        Assert.False(GameStateData.GameOver);
        Assert.False(GameStateData.Paused);
        Assert.False(GameStateData.IsDelayBeforeSwitchingStatesActive);
        Assert.Equal(0, GameStateData.ResultScore);
        game.Dispose();
    }

    public void Dispose()
    {
        game.Dispose();
    }
}

