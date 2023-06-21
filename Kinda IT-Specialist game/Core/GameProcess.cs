
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System;
using Game2D.BasicElements;
using Game2D.UI;
using Game2D.Characters;
using Game2D.Different;

namespace Game2D.Core;

public class GameProcess : State
{
    private MainPlayer player;

    private List<Component> components;
    private List<Component> componentsToUpdate;

    private float increasingCheckpoint = 0;

    public GameProcess(ContentManager content, GraphicsDevice graphics, USE_Game game)
        : base(content, graphics, game)
    {
    }

    public void LoadGameProcess()
    {
        components = GameMap.CreateFunctioningLevel();
        GameUICreator.AddUIToGameProcess(components);

        player = (MainPlayer)components.Where(comp => comp.GetType() == typeof(MainPlayer)).First();
        ((StaminaBar)components.Where(comp => comp.GetType() == typeof(StaminaBar)).First()).SetPlayer(player);

        componentsToUpdate = components
            .Where(comp => { var sprite = (Sprite)comp; return sprite != null && sprite.MustBeUpdated; }).ToList();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(transformMatrix: USE_Game.Camera.Transform);

        foreach (var component in components)
            component.Draw(gameTime, spriteBatch);

        spriteBatch.End();
    }

    private void ChanceIncreasingCheck()
    {
        if (GameStateData.GameSeconds - GameStateData.RemainedSeconds - increasingCheckpoint >= GameStateData.ChanceIncreasingInterval)
        {
            GameStateData.ProblemChance += GameStateData.ProblemAddChance;
            increasingCheckpoint += GameStateData.ChanceIncreasingInterval;
        }
    }

    public override void Update(GameTime gameTime)
    {
        ChanceIncreasingCheck();

        USE_Game.ActualCenterOfGameWorld = player.Position - new Vector2(OffsetDefiner.GetXOffset(player), OffsetDefiner.GetYOffset(player));

        USE_Game.Camera.Follow(player);

        GameStateData.RemainedSeconds -= gameTime.ElapsedGameTime.TotalSeconds;

        if (GameStateData.RemainedSeconds <= 0)
            GameStateData.GameOver = true;

        if (GameStateData.Lives <= 0)
        {
            GameStateData.ResultScore = 0;
            GameStateData.GameOver = true;
        }

        foreach (var component in componentsToUpdate)
            component.Update(gameTime);

    }
}

