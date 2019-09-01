using System.Collections.Generic;
using MonoGame.Extended.Entities;

namespace HumanRamen.Battle.Components
{
    public class BattleComponent
    {
        public List<Entity> Enemies { get; set; }
        public Entity Player { get; set; }

        public BattleComponent(List<Entity> enemies, Entity player)
        {
            Enemies = enemies;
            Player = player;
        }
    }
}
