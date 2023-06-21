
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game2D.BasicElements;

public class Animator
{
    private Dictionary<Directions, Animation> animations;
    private Directions lastDirection;

    public Animator()
    {
        animations = new Dictionary<Directions, Animation>();
    }

    public void AddAnimation(Directions direction, Animation animation)
    {
        animations.Add(direction, animation);
        lastDirection = direction;
    }

    public void ChangeAnimationsSpeed(float speed)
    {
        foreach (var anim in animations.Values)
        {
            anim.ChangeSpeed(speed);
        }
    }

    public void Update(GameTime gameTime, Directions key)
    {
        if (animations.TryGetValue(key, out Animation value))
        {
            value.Start();
            animations[key].Update(gameTime);
            lastDirection = key;
        }
        else
        {
            animations[lastDirection].Stop();
            animations[lastDirection].Reset();
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position)
    {
        animations[lastDirection].Draw(gameTime, spriteBatch, position);
    }
}
