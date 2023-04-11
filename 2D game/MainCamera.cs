
using Microsoft.Xna.Framework;

namespace _2D_game;

public class MainCamera
{
    public Matrix Transform { get; private set; }

    public void Follow(Sprite target)
    {
        var position = Matrix.CreateTranslation(
          -target.Position.X - (target.Rectangle.Width / 2),
          -target.Position.Y - (target.Rectangle.Height / 2),
          0);

        var offset = Matrix.CreateTranslation(
        USE_Game.ScreenWidth / 2,
            USE_Game.ScreenHeight / 2,
            0);

        Transform = position * offset;
    }
}
