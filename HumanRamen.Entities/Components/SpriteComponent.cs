using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HumanRamen.Entities.Components
{
    public interface IVisible
    {
        bool IsVisible { get; set; }
    }

    public class SpriteComponent : IVisible
    {
        public Texture2D Texture { get; set; }
        public Rectangle Position { get; set; }
        public Color Mask { get; set; } = Color.White;
        public float Depth { get; set; } = 0.5f;
        public bool IsVisible { get; set; } = true;
        public bool IsBackground { get; set; }

        public SpriteComponent(Texture2D texture)
        {
            Texture = texture;
            Position = new Rectangle(0, 0, texture.Width, texture.Height);
        }
    }

    public class SpriteFontComponent : IVisible
    {
        public SpriteFont SpriteFont { get; set; }
        public StringBuilder Text { get; set; } = new StringBuilder();
        public Color Color { get; set; } = Color.Black;
        public Vector2 Position { get; set; } = Vector2.Zero;
        public float Depth { get; set; } = 1;
        public bool IsVisible { get; set; } = true;

        public SpriteFontComponent(SpriteFont spriteFont)
        {
            SpriteFont = spriteFont;
        }
    }
}
