using Game2D.BasicElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game2D.UI;

public class ProblemAwaiter : Label
{
    private Color standardColor;

    public ProblemAwaiter(Texture2D texture, Vector2 position, Vector2 scale, SpriteEffects effect,
        SpriteFont font, Color color, Vector2 delta, string text = "default")
        : base(texture, position, scale, effect, font, color, delta, text)
    {
        standardColor = color;
    }

    public void DisplayTime(GameTime gameTime, SpriteBatch spriteBatch, double remainedSeconds)
    {
        text = TimeSpan.FromSeconds(remainedSeconds).ToString(@"mm\:ss\.ff");
        if (remainedSeconds >= 3 && color == Color.Red) color = standardColor;
        if (remainedSeconds < 3 && color != Color.Red) color = Color.Red;
        base.Draw(gameTime, spriteBatch);
    }
}
