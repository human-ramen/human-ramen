using System;
using System.Collections.Generic;

namespace HumanRamen.Scene
{
    public class Characters
    {
        public class Character
        {
            public string Name { get; set; }
            public string SpritePath { get; set; }

            public Character(string name, string spritePath)
            {
                Name = name;
                SpritePath = spritePath;
            }
        }

        public Dictionary<string, Character> Pool { get; private set; } = new Dictionary<string, Character>();

        public Character Get(string name)
        {

            if (!Pool.ContainsKey(name))
            {
                throw new ExceptionNoSuchCharacter(name);
            }

            return Pool[name];
        }

        public void Add(string name, string spritePath)
        {
            Pool.Add(name, new Character(name, spritePath));
        }
    }

    public class ExceptionNoSuchCharacter : Exception
    {
        public ExceptionNoSuchCharacter(string name) : base(String.Format("No character with name {0} in pool", name))
        {
        }
    }
}
