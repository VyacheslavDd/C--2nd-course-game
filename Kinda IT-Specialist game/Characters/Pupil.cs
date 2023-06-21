using Game2D.BasicElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace Game2D.Characters;

public class Pupil : CharacterWithProblems
{
    public Pupil(Texture2D texture, Vector2 position, Vector2 scale, SpriteEffects effect)
        : base(texture, position, scale, effect)
    {
        anyTimeProblems = ProblemSets.PupilProblems.Where(x => !x.OnlyAtStart).ToList();
        onlyAtStartProblems = ProblemSets.PupilProblems.Where(x => x.OnlyAtStart).ToList();
    }
}
