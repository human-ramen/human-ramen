using System;
using System.Collections.Generic;
using HumanRamen.Battle.Components;
using HumanRamen.Battle.Systems;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using Xunit;
using static HumanRamen.Battle.Components.TurnComponent;

namespace BattleSystem.Systems.Tests
{
    public class BattleSystemTest : IDisposable
    {
        private World _world;

        public BattleSystemTest()
        {
            _world = new WorldBuilder()
                .AddSystem(new AiSystem())
                .AddSystem(new ActionSystem())
                .AddSystem(new TurnSystem())
                .AddSystem(new ABattleSystem())
                .Build();
        }

        [Fact]
        public void Scenario()
        {
            var player = _world.CreateEntity();
            player.Attach(new PlayerComponent());
            player.Attach(new StatusComponent());
            player.Attach(new PropComponent());

            var playerActions = new ActionsComponent();

            player.Attach(playerActions);


            var enemies = new List<Entity>();

            for (var i = 0; i < 10; ++i)
            {
                var enemy = _world.CreateEntity();
                enemy.Attach(new StatusComponent());
                enemy.Attach(new PropComponent());
                enemy.Attach(new ActionsComponent());

                enemies.Add(enemy);
            }

            var battle = _world.CreateEntity();
            battle.Attach(new BattleComponent(enemies, player));

            // TODO: BattleSystem scenario
            for (var i = 0; i < 20; ++i)
            {
                _world.Update(new GameTime());

                var turn = battle.Get<TurnComponent>();
                if (turn != null && turn.State == Turn.Player)
                {
                    battle.Get<ActionQueueComponent>()
                      .Add(new ActionQueueItem(playerActions.DrinkCoffee, player));
                    battle.Attach(new TurnEndComponent());
                }
            }

        }

        void IDisposable.Dispose()
        {
            _world.Dispose();
        }
    }
}
