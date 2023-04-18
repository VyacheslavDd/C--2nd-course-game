using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace _2D_game;

public class MainPlayer : Sprite
{
    private Directions currentDirection;
    private Dictionary<Directions, bool> possibleMoves;

    public float Speed { get; set; }

    private List<Component> objectsToDetectCollisionsWith;

    public MainPlayer(Texture2D texture, float speed, Vector2 initialPosition, Vector2 scale,
        Directions direction = Directions.Right) : base(texture, initialPosition, scale, SpriteEffects.None)
    {
        Speed = speed;
        currentDirection = direction;
        objectsToDetectCollisionsWith = new List<Component>();

        possibleMoves = new Dictionary<Directions, bool>()
        {
            { Directions.Left, true },
            { Directions.Right, true },
            { Directions.Up, true },
            { Directions.Down, true },
        };
    }

    public void LoadColliders(List<Component> colliders)
    {
        objectsToDetectCollisionsWith = colliders;
    }

    public override void Update(GameTime gameTime)
    {
        UpdatePossibleMoves();
        Move(Keyboard.GetState(), (float)gameTime.ElapsedGameTime.TotalSeconds);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, Position, null, Color.White, 0, new Vector2(0, 0),
                Scale, Effect, 0);
    }

    private void UpdatePossibleMoves()
    {
        ResetPossibleMoves();
        foreach (var obj in objectsToDetectCollisionsWith)
        {
            var sprite = (Sprite)obj;
            if (Rectangle.Intersects(sprite.Rectangle))
            {
                var thisCenter = Rectangle.Center;
                var spriteRec = sprite.Rectangle;
                var spriteCenter = spriteRec.Center;

                if (Rectangle.Left < spriteRec.Right && thisCenter.X > spriteCenter.X) possibleMoves[Directions.Left] = false;
                if (Rectangle.Right > spriteRec.Left && thisCenter.X < spriteCenter.X) possibleMoves[Directions.Right] = false;
                if (Rectangle.Bottom > spriteRec.Top && thisCenter.Y < spriteCenter.Y) possibleMoves[Directions.Down] = false;
                if (Rectangle.Top < spriteRec.Bottom && thisCenter.Y > spriteCenter.Y) possibleMoves[Directions.Up] = false;
            }
        }
    }

    private void ResetPossibleMoves()
    {
        possibleMoves[Directions.Right] = true;
        possibleMoves[Directions.Left] = true;
        possibleMoves[Directions.Up] = true;
        possibleMoves[Directions.Down] = true;
    }

    private void Move(KeyboardState key, float elapsedSeconds)
    {
        if (!IsMovementCaused(key)) return;

        var previousPos = Position;
        var updatedTemp = Position;

        if (key.IsKeyDown(Keys.A) && possibleMoves[Directions.Left]) updatedTemp.X -= Speed * elapsedSeconds;
        if (key.IsKeyDown(Keys.D) && possibleMoves[Directions.Right]) updatedTemp.X += Speed * elapsedSeconds;
        if (key.IsKeyDown(Keys.W) && possibleMoves[Directions.Up]) updatedTemp.Y -= Speed * elapsedSeconds;
        if (key.IsKeyDown(Keys.S) && possibleMoves[Directions.Down]) updatedTemp.Y += Speed * elapsedSeconds;

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
