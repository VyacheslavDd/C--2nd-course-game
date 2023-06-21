
namespace Game2D.BasicElements;

public static class OffsetDefiner
{
    private static int yDownSide = 527;
    private static int yUpSide = 352;
    private static int xLeftSide = 679;
    private static int xRightSide = 1101;

    private static float GetOffset(float positionCoord, int firstSide, int secondSide)
    {
        var firstOffset = positionCoord - firstSide;
        if (firstOffset > 0) return firstOffset;
        var secondOffset = secondSide - positionCoord;
        if (secondOffset > 0) return -secondOffset;
        return 0;
    }

    public static float GetYOffset(Sprite target)
    {
        return GetOffset(target.Position.Y, yDownSide, yUpSide);
    }

    public static float GetXOffset(Sprite target)
    {
        return GetOffset(target.Position.X, xRightSide, xLeftSide);
    }
}
