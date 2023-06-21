using Game2D.BasicElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Game2D.Characters;

public class MainPlayer : Sprite
{
    private float initialSpeed;

    private Texture2D actualTexture;

    private float speedMultiplier = 2.5f;

    private float animationSpeed = 0.1f;

    private Vector2 initialPosition;

    private Directions currentDirection;

    private Dictionary<Directions, bool> possibleMoves;

    private MainCharacterPlace place;
    public float Speed { get; set; }
    public float StaminaLeft { get; private set; }
    public bool IsRunning { get; private set; }
    public bool IsMoving { get; private set; }

    private List<Component> objectsToDetectCollisionsWith;

    private List<CharacterWithProblems> problematicCharacters;

    private Animator animator;

    public MainPlayer(Texture2D textureBase, Texture2D actualTexture, float speed, Vector2 initialPosition, Vector2 scale,
        Directions direction = Directions.None) : base(textureBase, initialPosition, scale, SpriteEffects.None)
    {
        this.actualTexture = actualTexture;
        StaminaLeft = 100;
        this.initialPosition = initialPosition;
        initialSpeed = speed;
        Speed = speed;
        currentDirection = direction;
        objectsToDetectCollisionsWith = new List<Component>();
        problematicCharacters = new List<CharacterWithProblems>();
        possibleMoves = new Dictionary<Directions, bool>();
        animator = new Animator();
        ResetPossibleMoves();
        AddAnimations();
    }

    private void AddAnimations()
    {
        animator.AddAnimation(Directions.Left, new Animation(actualTexture, Position, Scale, 13, 21, animationSpeed, 10, 9));
        animator.AddAnimation(Directions.Right, new Animation(actualTexture, Position, Scale, 13, 21, animationSpeed, 12, 9));
        animator.AddAnimation(Directions.Up, new Animation(actualTexture, Position, Scale, 13, 21, animationSpeed, 9, 9));
        animator.AddAnimation(Directions.Down, new Animation(actualTexture, Position, Scale, 13, 21, animationSpeed, 11, 9));
    }

    public void LoadColliders(List<Component> colliders)
    {
        objectsToDetectCollisionsWith = colliders;
    }

    public void SetProblematicCharacters(List<Component> characters)
    {
        foreach (var character in characters)
            problematicCharacters.Add((CharacterWithProblems) character);
        place = (MainCharacterPlace)problematicCharacters[problematicCharacters.Count - 1];
    }

    public override void Update(GameTime gameTime)
    {
        UpdateStamina();
        UpdatePossibleMoves();
        animator.Update(gameTime, currentDirection);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        animator.Draw(gameTime, spriteBatch, Position - new Vector2(7, 10));
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

    public void InteractWithProblematicCharacters()
    {
        foreach (var character in problematicCharacters)
        {
            if (Vector2.Distance(Position, character.Position) <= 45 && character.HasProblem)
            {
                character.RewardForSolving();
                if (character != place) place.ForceGeneratingProblem();
            }
        }
    }

    public void RestartPosition()
    {
        Position = initialPosition;
    }

    private void SetSpeedValues(float playerSpeed, float animationSpeed)
    {
        Speed = playerSpeed;
        animator.ChangeAnimationsSpeed(animationSpeed);
    }

    public void Run()
    {
        IsRunning = true;

        if (StaminaLeft > 0)
            SetSpeedValues(initialSpeed * speedMultiplier, animationSpeed / speedMultiplier);
        else
            SetSpeedValues(initialSpeed, animationSpeed);
    }

    public void Walk()
    {
        IsRunning = false;
        SetSpeedValues(initialSpeed, animationSpeed);
    }

    private void UpdateMoveData(ref Vector2 position, bool isX, float moveValue, Directions direction)
    {
        IsMoving = true;
        currentDirection = direction;
        if (isX) position.X += moveValue;
        else position.Y += moveValue;
    }

    public void Move(Directions direction, float elapsedSeconds)
    {

        var updatedTemp = Position;

        if (direction == Directions.Left && possibleMoves[Directions.Left])
            UpdateMoveData(ref updatedTemp, true, -Speed * elapsedSeconds, Directions.Left);
        if (direction == Directions.Right && possibleMoves[Directions.Right])
            UpdateMoveData(ref updatedTemp, true, Speed * elapsedSeconds, Directions.Right);
        if (direction == Directions.Up && possibleMoves[Directions.Up])
            UpdateMoveData(ref updatedTemp, false, -Speed * elapsedSeconds, Directions.Up);    
        if (direction == Directions.Down && possibleMoves[Directions.Down])
            UpdateMoveData(ref updatedTemp, false, Speed * elapsedSeconds, Directions.Down);
        if (direction == Directions.None)
        {
            IsMoving = false;
            currentDirection = direction;
        }

        Position = updatedTemp;
    }

    private void UpdateStamina()
    {
        if (IsRunning && StaminaLeft > 0) 
        {
            StaminaLeft -= 1;
            if (StaminaLeft < 0) StaminaLeft = 0;
        }
        if (!IsRunning && StaminaLeft < 100)
        {
            StaminaLeft += 0.5f;
            if (StaminaLeft > 100) StaminaLeft = 100;
        }
    }
}
