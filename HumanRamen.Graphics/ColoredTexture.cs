using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HumanRamen.Graphics
{
    public class ColoredTexture : Texture2D
    {
        public ColoredTexture(GraphicsDevice graphicsDevice, Color color) : base(graphicsDevice, 1, 1)
        {
            SetData(new[] { color });
        }
    }
}
