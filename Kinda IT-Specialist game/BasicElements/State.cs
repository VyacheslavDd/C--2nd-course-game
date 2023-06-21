using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game2D.BasicElements;

public abstract class State
{
    protected ContentManager content;

    protected GraphicsDevice graphics;

    protected Game game;

    public State(ContentManager content, GraphicsDevice graphics, USE_Game game)
    {
        this.content = content;
        this.graphics = graphics;
        this.game = game;
    }

    public abstract void Update(GameTime gameTime);

    public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
}
