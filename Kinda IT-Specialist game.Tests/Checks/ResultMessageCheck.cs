using Game2D;
using Game2D.BasicElements;
using Game2D.UI;
using Game2DTests.Additionals;
using Microsoft.Xna.Framework;
using NVorbis;

namespace Game2DTests.Checks;

public class ResultMessageCheck : IDisposable
{
    private USE_Game game;

    public ResultMessageCheck()
    {
        game = new USE_Game();
    }

    private void SetResultScoreAndCheck(int result, int idealResult, Color colorToBe)
    {
        GameStateData.ResetAllInfo();
        StaticMethods.InitializeGame(game);
        var resultMessage = new ResultMessage();
        GameStateData.ResultScore = result;
        GameStateData.IdealScore = idealResult;
        StaticMethods.InvokeMethod("UpdateResultMessage", game, new object[] { resultMessage });
        Assert.Equal(colorToBe, StaticMethods.GetValue("color", resultMessage));
        game.Dispose();
    }

    [Fact]
    public void CheckBadResult()
    {
        SetResultScoreAndCheck(10, 60, Color.Red);
        game.Dispose();
    }

    [Fact]
    public void CheckNotSoBadResult()
    {
        SetResultScoreAndCheck(25, 60, Color.Orange);
        game.Dispose();
    }

    [Fact]
    public void CheckGoodResult()
    {
        SetResultScoreAndCheck(45, 60, Color.Yellow);
        game.Dispose();
    }

    [Fact]
    public void CheckGreatResult()
    {
        SetResultScoreAndCheck(55, 60, Color.Green);
        game.Dispose();
    }

    public void Dispose()
    {
        game.Dispose();
    }
}

