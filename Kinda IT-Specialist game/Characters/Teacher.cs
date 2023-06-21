
using Game2D.BasicElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2D.Characters;

public class Teacher : CharacterWithProblems
{
    public Teacher(Texture2D texture, Vector2 position, Vector2 scale, SpriteEffects effect)
        : base(texture, position, scale, effect)
    {
        anyTimeProblems = ProblemSets.TeacherProblems;
    }
}
