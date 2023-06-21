using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game2D.BasicElements;

public class RelativeMouse
{
    private MainCamera camera;

    public RelativeMouse(MainCamera camera = null)
    {
        this.camera = camera;
    }

    public Rectangle MouseRectangle
    {
        get
        {
            var state = CurrentState.Position.ToVector2();
            var mousePos = state + camera.Position;
            return new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1);
        }
    }

    public MouseState CurrentState => Mouse.GetState();

}
