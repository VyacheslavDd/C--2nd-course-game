using Game2D.BasicElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game2D.Core;

public class Menu : StandardSimpleMenuWindow
{
    public Menu(ContentManager content, GraphicsDevice graphics, USE_Game game, List<Component> components)
        : base(content, graphics, game, components)
    {
    }
}

