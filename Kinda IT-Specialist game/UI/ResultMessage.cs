using Game2D.BasicElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection.Metadata.Ecma335;

namespace Game2D.UI;

public class ResultMessage : Label
{
    public void SetMessage(double scorePercent, string text, Color color)
    {
        this.text = $"Your score is {GameStateData.ResultScore}. It's {scorePercent}%!\n" + text;
        this.color = color;
    }

    public ResultMessage(Texture2D texture, Vector2 position, Vector2 scale, SpriteEffects effect,
        SpriteFont font, Color color, Vector2 delta, string text = "default")
        : base(texture, position, scale, effect, font, color, delta, text)
    {
        this.text = $"Nothing even happened. TOO TERRIBLE!";
    }


    public ResultMessage()
    {

    }
}

