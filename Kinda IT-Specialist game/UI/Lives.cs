using Game2D.BasicElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2D.UI;

public class Lives : Label
{
    public Lives(Texture2D texture, Vector2 position, Vector2 scale, SpriteEffects effect,
        SpriteFont font, Color color, Vector2 delta, string text = "default")
        : base(texture, position, scale, effect, font, color, delta, text)
    {
        this.text = $"Remained Lives: {GameStateData.Lives}";
    }

    public Lives()
    {
        SimpleDraw();
        UpdateColor();
    }

    public override void Update(GameTime gameTime)
    {
        UpdateColor();
        PutGameInfoInRightPosition(USE_Game.ScreenWidth / 2, USE_Game.ScreenHeight / 2);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        SimpleDraw();
        base.Draw(gameTime, spriteBatch);
    }

    public void SimpleDraw()
    {
        text = $"Remained Lives: {GameStateData.Lives}";
    }

    public void UpdateColor()
    {
        if (GameStateData.Lives >= 4) color = Color.NavajoWhite;
        if (GameStateData.Lives < 4 && GameStateData.Lives >= 2) color = Color.Orange;
        if (GameStateData.Lives < 2) color = Color.Red;
    }


}