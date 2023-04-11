
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
        get { return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height); }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, Position, Color.White);
    }

    public Sprite(Texture2D texture)
    {
        this.texture = texture;
        Position = Vector2.Zero;
        Scale = Vector2.One;
        Effect = SpriteEffects.None;
    }

    public override void Update(GameTime gameTime)
    {

    }
}