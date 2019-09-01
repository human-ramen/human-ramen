using HumanRamen.Entities.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace HumanRamen.Entities.Systems
{
    public class RenderSystem : EntityDrawSystem
    {
        private SpriteBatch _spriteBatch;
        private int _w;
        private int _h;

        private ComponentMapper<SpriteComponent> _spriteMapper;
        private ComponentMapper<SpriteFontComponent> _spriteFontMapper;

        // TODO: Maybe refactor all this mess?
        public RenderSystem(SpriteBatch spriteBatch) :
            base(Aspect.One(typeof(SpriteComponent), typeof(SpriteFontComponent)))
        {
            _spriteBatch = spriteBatch;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _spriteMapper = mapperService.GetMapper<SpriteComponent>();
            _spriteFontMapper = mapperService.GetMapper<SpriteFontComponent>();
        }

        public override void Draw(GameTime gameTime)
        {
            _w = _spriteBatch.GraphicsDevice.Viewport.Width;
            _h = _spriteBatch.GraphicsDevice.Viewport.Height;

            foreach (var entity in ActiveEntities)
            {

                drawSprite(_spriteMapper.Get(entity));

                drawSpriteFont(_spriteFontMapper.Get(entity));
            }
        }

        private void drawSprite(SpriteComponent sprite)
        {
            if (sprite == null || !sprite.IsVisible) return;

            // TODO: refactor
            if (sprite.IsBackground)
            {
                _spriteBatch.Draw(sprite.Texture,
                                  new Rectangle((int)sprite.Position.X, (int)sprite.Position.Y,
                                                _spriteBatch.GraphicsDevice.Viewport.Width,
                                                _spriteBatch.GraphicsDevice.Viewport.Height),
                                  null, sprite.Mask, 0.0f, Vector2.Zero, SpriteEffects.None, 0);

                return;
            }

            _spriteBatch.Draw(sprite.Texture, sprite.Position, null,
                              sprite.Mask, 0.0f, Vector2.Zero, SpriteEffects.None, sprite.Depth);
        }

        private void drawSpriteFont(SpriteFontComponent spriteFont)
        {
            if (spriteFont == null || !spriteFont.IsVisible) return;

            _spriteBatch.DrawString(spriteFont.SpriteFont, spriteFont.Text, spriteFont.Position, spriteFont.Color, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, spriteFont.Depth);
        }
    }
}
