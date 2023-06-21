
using Game2D.BasicElements;
using Game2D.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2D.UI;

public class StaminaBar : Sprite
{
    private float initialTextureWidth;

    private MainPlayer player;

    public StaminaBar(Texture2D texture, Vector2 position, Vector2 scale, SpriteEffects effect)
        : base(texture, position, scale, effect)
    {
        Texture = texture;
        Position = position;
        Scale = scale;
        Effect = effect;

        initialTextureWidth = texture.Width;
    }

    public void SetPlayer(MainPlayer player)
    {
        this.player = player;
    }

    public override void Update(GameTime gameTime)
    {
        Position = USE_Game.ActualCenterOfGameWorld + new Vector2(USE_Game.ScreenWidth / 2, -USE_Game.ScreenHeight / 2)
            + new Vector2(-20 - initialTextureWidth-40, 40);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (player.IsRunning || player.StaminaLeft < 100)
        {
            var rectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)player.StaminaLeft, 30);
            spriteBatch.Draw(Texture, rectangle, Color.White);
        }
    }
}
