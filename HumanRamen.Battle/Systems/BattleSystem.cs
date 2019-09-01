using HumanRamen.Battle.Components;
using HumanRamen.Essentials;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace HumanRamen.Battle.Systems
{
    /// <summary>
    ///   Accumulates ActionDoSystem, TurnSystem and AiSystem.
    /// </summary>
    public class ABattleSystem : EntityUpdateSystem
    {
        private readonly Logger _l = new Logger("BattleSystem");

        private enum state
        {
            Unknown,
            InProcess,
            Win,
            Lose,
        }
        private state _state;
        private BattleComponent _battle = null;
        private Entity _entity = null;

        private ComponentMapper<BattleComponent> _battleMapper;
        private ComponentMapper<StatusComponent> _statusMapper;

        public ABattleSystem() : base(Aspect.One(typeof(BattleComponent)))
        {
            _l = new Logger("BattleSystem");
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _battleMapper = mapperService.GetMapper<BattleComponent>();
            _statusMapper = mapperService.GetMapper<StatusComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            // unknown => inprocess
            if (_state == state.Unknown)
            {
                foreach (var entity in ActiveEntities)
                {
                    _battle = _battleMapper.Get(entity);

                    if (_battle != null) { setInProcessState(entity); return; }
                }
            }
            else // inprocess => won/lose => unknown
                if (_state == state.InProcess)
            {
                if (isAllEnemiesDead()) { setWinState(); }
                else
                if (isPlayerDead()) { setLoseState(); }
            }
        }

        private void setInProcessState(int entity)
        {
            _l.Info("Battle started");
            _state = state.InProcess;

            _entity = GetEntity(entity);
            _entity.Attach(new TurnComponent());
            _entity.Attach(new AiComponent());
            _entity.Attach(new ActionQueueComponent());
        }

        private void setWinState()
        {
            _l.Info("All enemies is dead. Win!");
            _state = state.Win;

            // TODO: do some fancy-pancy win shit

            setUnknownState();
        }

        private void setLoseState()
        {
            _l.Info("Player is dead. Lose!");
            _state = state.Lose;

            // TODO: sorry screen

            setUnknownState();
        }

        private void setUnknownState()
        {
            _l.Info("Back to Unknown state");
            _state = state.Unknown;

            DestroyEntity(_entity.Id);

            _battle = null;
            _entity = null;
        }

        private bool isAllEnemiesDead()
        {
            foreach (var enemy in _battle.Enemies)
            {
                var status = _statusMapper.Get(enemy);

                if (status.Health >= 0)
                {
                    return false;
                }
            }

            return true;
        }

        private bool isPlayerDead()
        {
            var status = _statusMapper.Get(_battle.Player);

            return status.Health <= 0;
        }
    }
}
