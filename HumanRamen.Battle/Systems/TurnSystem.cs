using System;
using HumanRamen.Battle.Components;
using HumanRamen.Essentials;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using static HumanRamen.Battle.Components.TurnComponent;

namespace HumanRamen.Battle.Systems
{
    /// <summary>
    ///   TurnSystem saves and changing turn states.
    /// </summary>
    public class TurnSystem : EntityUpdateSystem
    {
        private Logger _l = new Logger("TurnSystem");
		private readonly Commander _commander;

        private Entity _entity = null;
        private TurnComponent _turn = null;

        private ComponentMapper<TurnComponent> _turnMapper;
        private ComponentMapper<TurnEndComponent> _turnEndMapper;

        public TurnSystem(Commander commander) : base(Aspect.One(typeof(TurnComponent)))
        {
			_commander = commander;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _turnMapper = mapperService.GetMapper<TurnComponent>();
            _turnEndMapper = mapperService.GetMapper<TurnEndComponent>();
        }

        // TODO: Add action points restrictions.
        public override void Update(GameTime gameTime)
        {
            if (_entity == null)
            {
                foreach (var entity in ActiveEntities)
                {
                    _turn = _turnMapper.Get(entity);

                    if (_turn != null) { initTurn(entity); return; }
                }
            }
            else
            {
                foreach (var entity in ActiveEntities)
                {
                    var turnEnd = _turnEndMapper.Get(entity);
                    if (turnEnd != null)
                    {
                        _turnEndMapper.Delete(entity);
                        toggleTurn();
                        return;
                    }
                }
            }
        }

        private void initTurn(int entity)
        {
            _entity = GetEntity(entity);

            // TODO: decide whos turn first

            _l.Info("Player turn first");
            _turn.State = Turn.Player;
        }

        private void toggleTurn()
        {
            if (_turn.State == Turn.Ai)
            {
                _l.Info("Pass turn to Player");
				_commander.Command("Turn", "Player");
                _turn.State = Turn.Player;
            }
            else if (_turn.State == Turn.Player)
            {
                _l.Info("Pass turn to AI");
				_commander.Command("Turn", "AI");
                _turn.State = Turn.Ai;
            }
            else
            {
                throw new Exception("Invalid state");
            }
        }

        private void toUnknownState()
        {
            _l.Info("Back to unknown state");
            _turn.State = Turn.Unknown;
            _turn = null;
            _entity = null;
        }
    }
}
