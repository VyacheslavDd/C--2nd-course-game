﻿
using Game2D.BasicElements;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game2D.Core;

public class Guide : StandardSimpleMenuWindow
{
    public Guide(ContentManager content, GraphicsDevice graphics, USE_Game game, List<Component> components)
        : base(content, graphics, game, components)
    {
    }
}
