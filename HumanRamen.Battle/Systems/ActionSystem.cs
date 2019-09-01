using HumanRamen.Battle.Components;
using HumanRamen.Essentials;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace HumanRamen.Battle.Systems
{
    /// <summary>
    ///   ActionDoSystem do Action to Target with calculations and state changing.
    /// </summary>
    public class ActionSystem : EntityUpdateSystem
    {
        private readonly Logger _l = new Logger("ActionSystem");

        private ComponentMapper<ActionQueueComponent> _actionQueueMapper;
        private ComponentMapper<PropComponent> _propMapper;
        private ComponentMapper<StatusComponent> _statusMapper;

        public ActionSystem() : base(Aspect.One(typeof(ActionQueueComponent)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _actionQueueMapper = mapperService.GetMapper<ActionQueueComponent>();
            _propMapper = mapperService.GetMapper<PropComponent>();
            _statusMapper = mapperService.GetMapper<StatusComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in ActiveEntities)
            {
                var queue = _actionQueueMapper.Get(entity);

                while (!queue.Empty())
                {
                    var action = queue.Pop();
                    var targetProp = _propMapper.Get(action.Target);
                    var targetStatus = _statusMapper.Get(action.Target);

                    // TODO: Add damage/heal calculations.
                    switch (action.Action.Nature)
                    {
                        case Nature.Coffee:
                            _l.Info("Target was healed");
                            targetStatus.Health += action.Action.Amount;
                            break;
                        default:
                            _l.Info("Target taken damage");
                            targetStatus.Health -= action.Action.Amount;
                            break;
                    }

                }
            }
        }
    }
}
