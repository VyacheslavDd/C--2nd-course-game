using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2D.BasicElements;

public class MainCamera
{
    public Matrix Transform { get; private set; }

    public Vector2 Position { get; private set; }

    public void Follow(Sprite target)
    {
        Position = new Vector2(
        target.Position.X - USE_Game.ScreenWidth / 2 + 10,
            target.Position.Y - USE_Game.ScreenHeight / 2 + 20
        );

        var position = Matrix.CreateTranslation(
          -target.Position.X - target.Rectangle.Width / 2,
          -target.Position.Y - target.Rectangle.Height / 2,
          0);

        var offset = Matrix.CreateTranslation(
        USE_Game.ScreenWidth / 2 + OffsetDefiner.GetXOffset(target),
            USE_Game.ScreenHeight / 2 + OffsetDefiner.GetYOffset(target),
            0);

        Transform = position * offset;
    }

    public void ResetPosition()
    {
        Position = Vector2.Zero;
    }
}
