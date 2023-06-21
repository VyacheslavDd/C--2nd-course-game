
using Game2D.BasicElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2D.UI;

public class ControlsInfoLabel : Label
{   
    public ControlsInfoLabel(Texture2D texture, Vector2 position, Vector2 scale, SpriteEffects effect,
        SpriteFont font, Color color, Vector2 delta)
        : base(texture, position, scale, effect, font, color, delta)
    {
        text = "Use E to interact.\nUse Shift to run, but it's limited.\n";
        this.color = Color.White;
    }

    public override void Update(GameTime gameTime)
    {
        var lengths = StringLengths;
        PutGameInfoInRightPosition((int)(-USE_Game.ScreenWidth / 2 + 10 + lengths.X), (int)(-USE_Game.ScreenHeight / 2 + lengths.Y + 110));
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (GameStateData.GameSeconds - GameStateData.RemainedSeconds <= 7) 
            base.Draw(gameTime, spriteBatch);
    }

}
