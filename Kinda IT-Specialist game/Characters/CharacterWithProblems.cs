using Game2D.BasicElements;
using Game2D.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace Game2D.Characters;

public class CharacterWithProblems : Sprite
{
    public bool HasProblem { get; private set; }
    protected bool playFailingSound = true;

    protected double waitingTime;
    protected double remainedTime;
    protected double timeSinceLastProblem = 0;
    protected double punishPercent = 0.2;
    private int lerpAmount;

    private int minRandomValue;
    private int maxRandomValue;

    private Sprite problemSprite;
    private Problem currentProblem;
    private ProblemAwaiter awaitPlayerTimer;

    protected List<Problem> anyTimeProblems;
    protected List<Problem> onlyAtStartProblems;

    public CharacterWithProblems(Texture2D texture, Vector2 position, Vector2 scale, SpriteEffects effect, double waitingTime=20)
        : base(texture, position, scale, effect)
    {
        anyTimeProblems = new List<Problem>();
        onlyAtStartProblems = new List<Problem>();

        var imagePos = position + new Vector2(4, -38);
        problemSprite = new Sprite(null, imagePos, Vector2.One * 0.28f, SpriteEffects.None);
        awaitPlayerTimer = new ProblemAwaiter(null, new Vector2(imagePos.X-20, imagePos.Y + 57), Vector2.One, SpriteEffects.None,
            USE_Game.StandardFont, Color.Yellow, Vector2.Zero);

        this.waitingTime = USE_Game.Random.Next(7, 18);
        remainedTime = 0;
        lerpAmount = USE_Game.Random.Next(1, 10);
        minRandomValue = USE_Game.Random.Next(-2, 8);
        maxRandomValue = USE_Game.Random.Next(-5, 10);
    }

    private void ElapsedTimeOperating(double seconds)
    {
        if (HasProblem)
        {
            remainedTime -= seconds;
            if (remainedTime <= 0)
            {
                PunishPlayer();
            }
        }
        else
            timeSinceLastProblem += seconds;
    }

    protected void PunishPlayer()
    {
        HasProblem = false;
        if (currentProblem.IsFatal) GameStateData.Lives = 0;
        else
        {
            GameStateData.Lives -= 1;
            GameStateData.ResultScore -= (int)(GameStateData.ResultScore * punishPercent);
            if (GameStateData.ResultScore < 0) GameStateData.ResultScore = 0;
        }
        if (playFailingSound)
            GameMusic.FailedToCorrect.Play();
    }

    private void SetProblemFromArray(List<Problem> array)
    {
        currentProblem = array[USE_Game.Random.Next(0, array.Count)];
        currentProblem.AppearingMaxAmount -= 1;
        if (currentProblem.AppearingMaxAmount <= 0) array.Remove(currentProblem);
        GameMusic.ProblemAppear.Play();
    }

    protected void GenerateProblem()
    {
        var remainedTimePercent = GameStateData.RemainedSeconds * 100 / GameStateData.GameSeconds;

        if (remainedTimePercent >= 90 && onlyAtStartProblems.Count > 0 && USE_Game.Random.NextDouble() < GameStateData.ProblemChance)
            SetProblemFromArray(onlyAtStartProblems);
        else
            SetProblemFromArray(anyTimeProblems);

        GameStateData.IdealScore += currentProblem.PointsForSolving;
        problemSprite.Texture = currentProblem.Image;

        PrepareForProblem();
    }

    private void PrepareForProblem()
    {
        timeSinceLastProblem = 0;
        lerpAmount = USE_Game.Random.Next(1, 10);
        remainedTime = waitingTime;
        HasProblem = true;
    }

    private void CheckAboutGeneratingProblem()
    {
        if (timeSinceLastProblem >= GameStateData.MinTimeBetweenProblems)
        {
            var problemInterval = MathHelper.Lerp(GameStateData.MinTimeBetweenProblems + minRandomValue,
                GameStateData.MaxTimeBetweenProblems + maxRandomValue, lerpAmount);
            if (timeSinceLastProblem >= problemInterval && USE_Game.Random.NextDouble() < GameStateData.ProblemChance
                && GameStateData.RemainedSeconds - waitingTime > 0)
            {
                GenerateProblem();
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        ElapsedTimeOperating(gameTime.ElapsedGameTime.TotalSeconds);
        CheckAboutGeneratingProblem();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (HasProblem)
        {
            problemSprite.Draw(gameTime, spriteBatch);
            awaitPlayerTimer.DisplayTime(gameTime, spriteBatch, remainedTime);
        }
        if (Texture != null)
            spriteBatch.Draw(Texture, Position, null, Color.White, 0, new Vector2(0, 0), Scale, Effect, 0);
    }

    public void RewardForSolving()
    {
        HasProblem = false;
        GameStateData.ResultScore += currentProblem.PointsForSolving;
        GameMusic.CorrectedProblem.Play();
    }
}
