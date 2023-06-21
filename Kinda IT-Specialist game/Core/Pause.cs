using Game2D.BasicElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game2D.Core;

public class Pause : StandardSimpleMenuWindow
{
    public Pause(ContentManager content, GraphicsDevice graphics, USE_Game game, List<Component> components)
        : base(content, graphics, game, components)
    {
    }

    public override void Update(GameTime gameTime)
    {
        USE_Game.ActualCenterOfGameWorld = new Vector2(USE_Game.ScreenWidth / 2, USE_Game.ScreenHeight / 2);

        foreach (var component in components)
        {
            component.Update(gameTime);
        }
    }
}
