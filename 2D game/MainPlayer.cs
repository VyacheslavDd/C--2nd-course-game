using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace _2D_game;

public class MainPlayer : Sprite
{
    private Directions currentDirection;
    private Dictionary<Directions, bool> possibleMoves;

    public float Speed { get; set; }

    private List<Component> objectsToDetectCollisionsWith;

    private bool isTurnedLeft; 

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
        UpdateDirection();
        UpdateEffect();
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

    public void Move(Directions direction, float elapsedSeconds)
    {

        var updatedTemp = Position;

        if (direction == Directions.Left && possibleMoves[Directions.Left])
        {
            isTurnedLeft = true;
            updatedTemp.X -= Speed * elapsedSeconds;
        }
        if (direction == Directions.Right && possibleMoves[Directions.Right])
        {
            isTurnedLeft = false;
            updatedTemp.X += Speed * elapsedSeconds;
        }
        if (direction == Directions.Up && possibleMoves[Directions.Up]) updatedTemp.Y -= Speed * elapsedSeconds;
        if (direction == Directions.Down && possibleMoves[Directions.Down]) updatedTemp.Y += Speed * elapsedSeconds;

        Position = updatedTemp;
    }

    private void UpdateEffect()
    {
        if (currentDirection == Directions.Right) Effect = SpriteEffects.None;
        else Effect = SpriteEffects.FlipHorizontally;
    }

    private void UpdateDirection()
    {
        if (!isTurnedLeft) currentDirection = Directions.Right;
        else currentDirection = Directions.Left;
    }
}
