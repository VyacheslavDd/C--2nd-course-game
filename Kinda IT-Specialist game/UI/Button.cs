using Game2D.BasicElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game2D.UI;

public class Button : Sprite
{
    private MouseState previousState;
    private MouseState currentState;

    private Vector2 centerDelta;

    private Label label;

    private bool isHovered;

    private Color backgroundColor;
    private Color onHoveredBgColor;

    public event EventHandler Click;
    public bool Clicked { get; private set; }


    public Button(Texture2D texture, Vector2 position, Vector2 scale, SpriteEffects effect, Vector2 delta,
        Label label, Color bgColor)
        : base(texture, position, scale, effect)
    {
        this.label = label;
        backgroundColor = bgColor;
        onHoveredBgColor = Color.Orange;
        centerDelta = delta;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        var center = USE_Game.ActualCenterOfGameWorld + centerDelta;
        Position = new Vector2(center.X - Rectangle.Width / 2, center.Y - Rectangle.Height / 2);

        var labelLengths = label.StringLengths;
        label.Position = new Vector2(Position.X + Rectangle.Width / 2 - labelLengths.X / 2,
    Position.Y + Rectangle.Height / 2 - labelLengths.Y / 2);

        spriteBatch.Draw(Texture, Position, isHovered ? onHoveredBgColor : backgroundColor);
        label.Draw(gameTime, spriteBatch);
    }

    public override void Update(GameTime gameTime)
    {
        previousState = currentState;
        currentState = USE_Game.Mouse.CurrentState;

        var isIntersected = USE_Game.Mouse.MouseRectangle.Intersects(Rectangle);

        if (isIntersected && !isHovered)
        {
            GameMusic.ButtonHover.Play();
            isHovered = true;
        }
        else if (!isIntersected) isHovered = false;

        if (previousState.LeftButton == ButtonState.Pressed && currentState.LeftButton == ButtonState.Released && isHovered)
        {
            GameMusic.ButtonClick.Play();
            Click?.Invoke(this, new EventArgs());
        }
    }
}
