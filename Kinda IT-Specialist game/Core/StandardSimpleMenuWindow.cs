﻿using Game2D.BasicElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game2D.Core;

public class StandardSimpleMenuWindow : State
{
    protected List<Component> components;

    public StandardSimpleMenuWindow(ContentManager content, GraphicsDevice graphics, USE_Game game, List<Component> components)
        : base(content, graphics, game)
    {
        this.components = components;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

        foreach (var component in components)
        {
            component.Draw(gameTime, spriteBatch);
        }

        spriteBatch.End();
    }

    public override void Update(GameTime gameTime)
    {
        if (USE_Game.Controller.CurrentState.IsKeyDown(Keys.Escape)) game.Exit();

        USE_Game.ActualCenterOfGameWorld = new Vector2(USE_Game.ScreenWidth / 2, USE_Game.ScreenHeight / 2);

        foreach (var component in components)
        {
            component.Update(gameTime);
        }
    }
}
