
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace _2D_game;

public class Timer : Label
{
    private double secondsLeft;
    private string finished;

    public Timer(Texture2D texture, Vector2 position, Vector2 scale, SpriteEffects effect,
        SpriteFont font, Color color, double seconds, string text = "default")
        : base(texture, position, scale, effect, font, color, text)
    {
        secondsLeft = seconds;
        finished = "The exam is over!";
    }

    public override void Update(GameTime gameTime)
    {
        if (!GameStatesAndActions.GameOver)
        {
            secondsLeft -= gameTime.ElapsedGameTime.TotalSeconds;
            var center = USE_Game.ActualCenterOfGameWorld + new Vector2(20, 20);
            Position = new Vector2(center.X - USE_Game.ScreenWidth / 2, center.Y - USE_Game.ScreenHeight / 2);
            if (secondsLeft > 0)
                text = "Finishing in  " + TimeSpan.FromSeconds(secondsLeft).ToString(@"mm\:ss\.ff");
            else
            {
                text = finished;
                GameStatesAndActions.GameOver = true;
            }
        }
    }
}