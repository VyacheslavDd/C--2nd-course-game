
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace _2D_game;

public class Sprite : Component
{
    protected Texture2D texture;

    public Vector2 Position { get; set; }

    public Vector2 Scale { get; set; }

    public SpriteEffects Effect { get; set; }

    public Rectangle Rectangle
    {
        get { return new Rectangle((int)Position.X, (int)Position.Y, (int)(texture.Width * Scale.X), (int)(texture.Height * Scale.Y)); }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, Position, Color.White);
    }

    public Sprite(Texture2D texture, Vector2 position, Vector2 scale, SpriteEffects effect)
    {
        this.texture = texture;
        Position = position;
        Scale = scale;
        Effect = effect;
    }

    public override void Update(GameTime gameTime)
    {

    }
}