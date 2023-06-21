using Game2D.BasicElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2D.UI;

public class GameScore : Label
{
    public GameScore(Texture2D texture, Vector2 position, Vector2 scale,
        SpriteEffects effect, SpriteFont font, Color color, Vector2 delta, string text = "default")
        : base(texture, position, scale, effect, font, color, delta, text)
    {
        this.text = $"Score : {GameStateData.ResultScore}";
    }

    public GameScore()
    {

    }

    public override void Update(GameTime gameTime)
    {
        PutGameInfoInRightPosition(USE_Game.ScreenWidth / 2, USE_Game.ScreenHeight / 2);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        SimpleDraw();
        base.Draw(gameTime, spriteBatch);
    }

    public void SimpleDraw()
    {
        text = $"Score : {GameStateData.ResultScore}";
    }
}
