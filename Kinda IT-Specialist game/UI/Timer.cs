using Game2D.BasicElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game2D.UI;

public class Timer : Label
{
    private static string finished;

    public Timer(Texture2D texture, Vector2 position, Vector2 scale, SpriteEffects effect,
        SpriteFont font, Color color, Vector2 delta, string text = "default")
        : base(texture, position, scale, effect, font, color, delta, text)
    {
        finished = "The exam is over!";
    }

    public override void Update(GameTime gameTime)
    {
        PutGameInfoInRightPosition(USE_Game.ScreenWidth / 2, USE_Game.ScreenHeight / 2);

        if (GameStateData.RemainedSeconds >= 0)
            text = "Finishing in  " + TimeSpan.FromSeconds(GameStateData.RemainedSeconds).ToString(@"mm\:ss\.ff");
        else
            text = finished;
    }
}