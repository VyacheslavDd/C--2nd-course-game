using Game2D.BasicElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2D.Different;

public class InvisibleCollider : Sprite
{
    public InvisibleCollider(Texture2D texture, Vector2 position, Vector2 scale, SpriteEffects effect) :
        base(texture, position, scale, effect, false)
    {

    }
}

