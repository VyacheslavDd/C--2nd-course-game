using Game2D.BasicElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2D.UI;

public class Label : Sprite
{
    protected SpriteFont font;
    protected string text;
    protected Color color;
    protected Vector2 centerDelta;

    public Vector2 StringLengths
    {
        get
        {
            return new Vector2(font.MeasureString(text).X * Scale.X, font.MeasureString(text).Y * Scale.Y);
        }
    }

    public Label(Texture2D texture, Vector2 position, Vector2 scale, SpriteEffects effect,
        SpriteFont font, Color color, Vector2 delta, string text = "default")
        : base(texture, position, scale, effect)
    {
        this.font = font;
        this.text = text;
        this.color = color;
        centerDelta = delta;
    }

    public Label()
    {

    }

    public override void Update(GameTime gameTime)
    {
        var lengths = StringLengths;
        PutGameInfoInRightPosition((int)lengths.X / 2, (int)lengths.Y / 2);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, text, Position, color, 0, Vector2.Zero, Scale, Effect, 0);
    }

    protected void PutGameInfoInRightPosition(int xDelta, int yDelta)
    {
        var center = USE_Game.ActualCenterOfGameWorld + centerDelta;
        Position = new Vector2(center.X - xDelta, center.Y - yDelta);
    }
}

