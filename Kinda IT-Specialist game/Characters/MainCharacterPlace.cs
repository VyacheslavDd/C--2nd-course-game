
using Game2D.BasicElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.CompilerServices;

namespace Game2D.Characters;

public class MainCharacterPlace : CharacterWithProblems
{
    public MainCharacterPlace(Texture2D texture, Vector2 position, Vector2 scale, SpriteEffects effect, double waitingTime = 10)
        : base(texture, position, scale, effect, waitingTime)
    {
        anyTimeProblems = ProblemSets.MainCharacterProblems;
        this.waitingTime = waitingTime;
        punishPercent = 0.002f;
        playFailingSound = false;
    }

    public void ForceGeneratingProblem()
    {
        if (HasProblem)
        {
            GameStateData.Lives += 1;
            PunishPlayer();
        }
        GenerateProblem();
    }

    public override void Update(GameTime gameTime)
    {
        timeSinceLastProblem = 0;
        base.Update(gameTime);
    }
}
