using HumanRamen.Battle.Components;
using HumanRamen.Essentials;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using static HumanRamen.Battle.Components.TurnComponent;

namespace HumanRamen.Battle.Systems
{
    /// <summary>
    ///   AiSystem controls enemies Ai.
    /// </summary>
    public class AiSystem : EntityUpdateSystem
    {
        private readonly Logger _l = new Logger("AiSystem");

        private ComponentMapper<AiComponent> _aiMapper;
        private ComponentMapper<TurnComponent> _turnMapper;
        private ComponentMapper<BattleComponent> _battleMapper;
        private ComponentMapper<ActionsComponent> _actionsManager;
        private ComponentMapper<ActionQueueComponent> _queueManager;
        private ComponentMapper<StatusComponent> _statusManager;

        public AiSystem() : base(Aspect.One(typeof(AiComponent)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _aiMapper = mapperService.GetMapper<AiComponent>();
            _turnMapper = mapperService.GetMapper<TurnComponent>();
            _battleMapper = mapperService.GetMapper<BattleComponent>();
            _actionsManager = mapperService.GetMapper<ActionsComponent>();
            _queueManager = mapperService.GetMapper<ActionQueueComponent>();
            _statusManager = mapperService.GetMapper<StatusComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in ActiveEntities)
            {
                var turn = _turnMapper.Get(entity);

                if (turn.State == Turn.Ai) { doTurn(entity); return; }
            }
        }

        private void doTurn(int entityId)
        {
            var entity = GetEntity(entityId);
            var battle = _battleMapper.Get(entity);
            var queue = _queueManager.Get(entity);

            // TODO: adv ai with bunch of ifelses, oh yeah
            foreach (var enemy in battle.Enemies)
            {
                _l.Info("Enemy decides what to do");

                var status = _statusManager.Get(enemy);
                var actions = _actionsManager.Get(enemy);

                if (status.Health >= 3)
                {
                    _l.Info("Enemy throwing a stepler");
                    queue.Add(new ActionQueueItem(actions.ThrowStaper, battle.Player, enemy));
                }
                else
                {
                    _l.Info("Enemy health is low, time to drink some COFFEE");
                    queue.Add(new ActionQueueItem(actions.DrinkCoffee, battle.Player, enemy));
                }
            }

            entity.Attach(new TurnEndComponent());
        }
    }
}
