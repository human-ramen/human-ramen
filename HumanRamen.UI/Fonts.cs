using Microsoft.Xna.Framework.Graphics;

namespace HumanRamen.UI
{
    public interface IGameContent
    {
        SpriteFont BrandFont { get; }
    }

    public class Fonts
    {
        public SpriteFont Brand { get; private set; }
        // TODO etc

        public Fonts(IGameContent _content)
        {
            Brand = _content.BrandFont;
        }
    }
}
