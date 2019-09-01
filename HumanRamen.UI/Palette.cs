using HumanRamen.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HumanRamen.UI
{
    public class Palette
    {
        public ColoredTexture Primary { get; set; }
        public ColoredTexture Secondary { get; set; }
        public ColoredTexture Success { get; set; }
        public ColoredTexture Danger { get; set; }
        public ColoredTexture Warning { get; set; }
        public ColoredTexture Info { get; set; }
        public ColoredTexture Light { get; set; }
        public ColoredTexture Dark { get; set; }

        public Palette(GraphicsDevice graphics)
        {
            Primary = new ColoredTexture(graphics, Color.Blue);
            Secondary = new ColoredTexture(graphics, Color.DarkCyan);
            Success = new ColoredTexture(graphics, Color.Green);
            Danger = new ColoredTexture(graphics, Color.Red);
            Warning = new ColoredTexture(graphics, Color.Yellow);
            Info = new ColoredTexture(graphics, Color.LightBlue);
            Light = new ColoredTexture(graphics, Color.White);
            Dark = new ColoredTexture(graphics, Color.Black);
        }
    }
}
