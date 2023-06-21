
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game2D.BasicElements;

public class Animation
{
    private Texture2D texture;
    private List<Rectangle> sprites;
    private Vector2 scale;
    private int framesCount;
    private int currentFrame;
    private float frameTime;
    private float frameTimeLeft;
    private bool isActive = true;

    public Animation(Texture2D texture, Vector2 position, Vector2 scale, int framesX, int framesY, float frameTime, int row, int takeIn)
    {
        this.texture = texture;
        this.scale = scale;
        this.frameTime = frameTime;
        frameTimeLeft = this.frameTime;
        framesCount = takeIn;
        var frameWidth = this.texture.Width / framesX;
        var frameHeight = this.texture.Height / framesY;

        sprites = new List<Rectangle>();
        for (int i = 0; i < framesCount; i++)
        {
            sprites.Add(new Rectangle(i * frameWidth, (row - 1) * frameHeight, frameWidth, frameHeight));
        }
    }

    public void ChangeSpeed(float speed)
    {
        frameTime = speed;
    }

    public void Stop()
    {
        isActive = false;
    }

    public void Start()
    {
        isActive = true;
    }

    public void Reset()
    {
        currentFrame = 0;
        frameTimeLeft = frameTime;
    }

    public void Update(GameTime gameTime)
    {
        if (!isActive) return;

        frameTimeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (frameTimeLeft <= 0)
        {
            frameTimeLeft += frameTime;
            currentFrame = (currentFrame + 1) % framesCount;
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position)
    {
        spriteBatch.Draw(texture, position, sprites[currentFrame], Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
    }
}