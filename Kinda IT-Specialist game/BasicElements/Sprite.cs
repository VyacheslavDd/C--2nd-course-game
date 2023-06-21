
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game2D.BasicElements;

public class Sprite : Component
{
    public Texture2D Texture;

    public Vector2 Position { get; set; }

    public Vector2 Scale { get; set; }

    public SpriteEffects Effect { get; set; }

    public bool MustBeUpdated { get; set; }

    public Rectangle Rectangle
    {
        get { return new Rectangle((int)Position.X, (int)Position.Y, (int)(Texture.Width * Scale.X), (int)(Texture.Height * Scale.Y)); }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
    }

    public Sprite(Texture2D texture, Vector2 position, Vector2 scale, SpriteEffects effect, bool mustBeUpdated = true)
    {
        this.Texture = texture;
        Position = position;
        Scale = scale;
        Effect = effect;
        MustBeUpdated = mustBeUpdated;
    }

    public Sprite()
    {

    }

    public override void Update(GameTime gameTime)
    {

    }
}