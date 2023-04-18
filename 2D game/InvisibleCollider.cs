

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_game;

public class InvisibleCollider : Sprite
{
    public InvisibleCollider(Texture2D texture, Vector2 position, Vector2 scale, SpriteEffects effect) : 
        base(texture, position, scale, effect)
    {

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, Position, Color.White * 0f);
    }
}

