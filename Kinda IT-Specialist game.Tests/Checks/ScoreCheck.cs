using Game2D;
using Game2D.BasicElements;
using Game2D.UI;
using Game2DTests.Additionals;

namespace Game2DTests.Checks;

public class ScoreCheck
{
    [Fact]
    public void CheckScoreAfterAddingPositiveNumber()
    {
        GameStateData.ResetAllInfo();
        var score = new GameScore();
        GameStateData.ResultScore += 10;
        score.SimpleDraw();
        Assert.True(StaticMethods.GetValue("text", score).ToString().Contains(GameStateData.ResultScore.ToString()));
    }

    [Fact]
    public void CheckScoreAfterAddingNegativeNumber()
    {
        GameStateData.ResetAllInfo();
        var score = new GameScore();
        GameStateData.ResultScore -= 10;
        score.SimpleDraw();
        Assert.True(StaticMethods.GetValue("text", score).ToString().Contains(GameStateData.ResultScore.ToString()));
    }

    [Fact]
    public void CheckScoreAfterAddingFewNumbers()
    {
        GameStateData.ResetAllInfo();
        var score = new GameScore();
        GameStateData.ResultScore += 10;
        GameStateData.ResultScore += 30;
        GameStateData.ResultScore += -1;
        score.SimpleDraw();
        Assert.True(StaticMethods.GetValue("text", score).ToString().Contains(GameStateData.ResultScore.ToString()));
    }
}
