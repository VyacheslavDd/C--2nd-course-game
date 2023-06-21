using Game2D;

namespace Game2DTests.Checks;

using Game2D.BasicElements;
using Game2D.UI;
using Game2DTests.Additionals;
using Microsoft.Xna.Framework;
using System.Reflection;

public class LivesCheck
{
    [Fact]
    public void CheckLivesAmountChanging()
    {
        GameStateData.ResetAllInfo();
        var startAmount = GameStateData.Lives;
        var lives = new Lives();
        GameStateData.Lives -= 1;
        lives.SimpleDraw();
        Assert.True(StaticMethods.GetValue("text", lives).ToString().Contains(GameStateData.Lives.ToString()));
        GameStateData.Lives -= 1;
        lives.SimpleDraw();
        Assert.True(StaticMethods.GetValue("text", lives).ToString().Contains(GameStateData.Lives.ToString()));
    }

    [Fact]
    public void CheckLivesColorChanging()
    {
        GameStateData.ResetAllInfo();
        GameStateData.Lives = 5;
        var lives = new Lives();
        Assert.Equal(Color.NavajoWhite, StaticMethods.GetValue("color", lives));
        GameStateData.Lives = 3;
        lives.UpdateColor();
        Assert.Equal(Color.Orange, StaticMethods.GetValue("color", lives));
        GameStateData.Lives = 1;
        lives.UpdateColor();
        Assert.Equal(Color.Red, StaticMethods.GetValue("color", lives));
    }
}