
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_game;

public class Label : Sprite
{
    protected SpriteFont font;
    protected string text;
    protected Color color;

    public Label(Texture2D texture, Vector2 position, Vector2 scale, SpriteEffects effect,
        SpriteFont font, Color color, string text="default") 
        : base(texture, position, scale, effect)
    {
        this.font = font;
        this.text = text;
        this.color = color;
    }

    public override void Update(GameTime gameTime)
    {
        Position = USE_Game.ActualCenterOfGameWorld;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, text, Position, color, 0, Vector2.Zero, Scale, Effect, 0);
    }
}

