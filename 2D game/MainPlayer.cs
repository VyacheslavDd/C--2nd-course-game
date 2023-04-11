using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2D_game;

public class MainPlayer : Sprite
{
    private Directions currentDirection;

    public float Speed { get; set; }

    public MainPlayer(Texture2D texture, float speed, Vector2 initialPosition, Vector2 scale,
        Directions direction = Directions.Right) : base(texture)
    {
        Effect = SpriteEffects.None;
        Speed = speed;
        Position = initialPosition;
        Scale = scale;
        currentDirection = direction;
    }

    public override void Update(GameTime gameTime)
    {
        Move(Keyboard.GetState(), (float)gameTime.ElapsedGameTime.TotalSeconds);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, Position, null, Color.White, 0, new Vector2(0, 0),
                Scale, Effect, 0);
    }

    public void Move(KeyboardState key, float elapsedSeconds)
    {
        if (!IsMovementCaused(key)) return;

        var previousPos = Position;
        var updatedTemp = Position;

        if (key.IsKeyDown(Keys.A)) updatedTemp.X -= Speed * elapsedSeconds;
        if (key.IsKeyDown(Keys.D)) updatedTemp.X += Speed * elapsedSeconds;
        if (key.IsKeyDown(Keys.W)) updatedTemp.Y -= Speed * elapsedSeconds;
        if (key.IsKeyDown(Keys.S)) updatedTemp.Y += Speed * elapsedSeconds;
        Position = updatedTemp;

        UpdateDirection(previousPos);
        UpdateEffect();
    }

    private void UpdateEffect()
    {
        if (currentDirection == Directions.Right) Effect = SpriteEffects.None;
        else Effect = SpriteEffects.FlipHorizontally;
    }

    private bool IsMovementCaused(KeyboardState key)
    {
        return key.IsKeyDown(Keys.A) || key.IsKeyDown(Keys.D) ||
            key.IsKeyDown(Keys.W) || key.IsKeyDown(Keys.S);
    }

    private void UpdateDirection(Vector2 previousPos)
    {
        if (previousPos.X <= Position.X) currentDirection = Directions.Right;
        else currentDirection = Directions.Left;
    }
}
