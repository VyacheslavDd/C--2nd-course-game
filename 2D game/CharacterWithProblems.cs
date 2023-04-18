
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_game;

public class CharacterWithProblems : Sprite
{
    public CharacterWithProblems(Texture2D texture, Vector2 position, Vector2 scale, SpriteEffects effect)
        : base(texture, position, scale, effect)
    {

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, Position, null, Color.White, 0, new Vector2(0, 0),
                Scale, Effect, 0);
    }
}
