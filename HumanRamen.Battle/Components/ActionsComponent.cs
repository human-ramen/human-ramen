using System.Collections.Generic;
using MonoGame.Extended.Entities;

namespace HumanRamen.Battle.Components
{
    public enum Nature
    {
        Attack,
        Damage,
        Wet,
        Heat,
        Poison,
        Coffee,
    }

    public class Requirements
    {
        public int Level { get; }
        public int Power { get; }

        public Requirements(int lvl)
        {
            Level = lvl;
        }

        public Requirements(int lvl, int pwr)
        {
            Level = lvl;
            Power = pwr;
        }
    }

    public class Action
    {
        public int Amount { get; set; }
        public Nature Nature { get; set; }
        public Requirements Reqs { get; set; }

        public Action() { }

        public Action(int amount)
        {
            Amount = amount;
            Nature = Nature.Damage;
        }

        public Action(int amount, Nature nature)
        {
            Amount = amount;
            Nature = nature;
        }

        public Action(int amount, Nature nature, Requirements reqs)
        {
            Amount = amount;
            Nature = nature;
            Reqs = reqs;
        }
    }

    public class ActionQueueItem
    {
        public Action Action { get; }
        public Entity Target { get; }
        public Entity Emitter { get; }

        public ActionQueueItem(Action action, Entity target, Entity emitter)
        {
            Action = action;
            Target = target;
            Emitter = emitter;
        }

        public ActionQueueItem(Action action, Entity self)
        {
            Action = action;
            Target = self;
            Emitter = self;
        }
    }

    public class ActionQueueComponent
    {
        private Queue<ActionQueueItem> _queue = new Queue<ActionQueueItem>();

        public void Add(ActionQueueItem item)
        {
            _queue.Enqueue(item);
        }

        public bool Empty()
        {
            return _queue.Count == 0;
        }

        public ActionQueueItem Pop()
        {
            return _queue.Dequeue();
        }

        public void Clear()
        {
            _queue.Clear();
        }
    }

    // TODO: refactor
    public class ActionsComponent
    {
        public Action StartBattle = new Action();
        public Action ThrowStaper = new Action(3);
        public Action DrinkCoffee = new Action(2, Nature.Coffee, new Requirements(2, 10));
    }
}
