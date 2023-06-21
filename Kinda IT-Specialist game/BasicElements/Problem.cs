
using Microsoft.Xna.Framework.Graphics;

namespace Game2D.BasicElements;

public class Problem
{
    public Texture2D Image { get; private set; }
    public int AppearingMaxAmount { get; set; }
    public int PointsForSolving { get; private set; }
    public bool IsFatal { get; private set; }
    public bool OnlyAtStart { get; private set; }

    public Problem(Texture2D image, int points, int maxAppearingAmount, bool isFatal, bool shouldBeFirst)
    {
        Image = image;
        PointsForSolving = points;
        AppearingMaxAmount = maxAppearingAmount;
        IsFatal = isFatal;
        OnlyAtStart = shouldBeFirst;
    }
}