using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HumanRamen.Essentials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Input;
using NLua;

namespace HumanRamen.Entities.Systems
{
    public class ControlSystem : UpdateSystem
    {
        private readonly Logger _l = new Logger("ControlSystem");
        private readonly string _topic = "Control";

        private Commander _commander;
        private Dictionary<Keys, string> _kbdmap;

        public ControlSystem(Commander commander)
        {
            _commander = commander;

            var lua = new Lua();

            lua.NewTable("kbd");
            lua.DoFile("./Lua/controls.lua");

            var raw = lua.GetTableDict(lua.GetTable("kbd")) as Dictionary<object, object>;

            _kbdmap = new Dictionary<Keys, string>();
            foreach (var row in raw)
            {
				
                _kbdmap.Add((Keys) Enum.Parse(typeof(Keys), row.Key.ToString()), row.Value.ToString());
            }

            lua.Dispose();

            // _kbdmap.Add(Keys.Q, "Exit");
            // _kbdmap.Add(Keys.Space, "Continue");
            // _kbdmap.Add(Keys.F, "Fullscreen");
            // _kbdmap.Add(Keys.D, "ToggleDebugConsole");
        }

        public override void Update(GameTime gameTime)
        {
            var kbd = KeyboardExtended.GetState();

            Parallel.ForEach(_kbdmap, (m) =>
            {
                if (kbd.WasKeyJustDown(m.Key))
                {
                    _l.Debug(String.Format("{0}: {1}", _topic, m.Value));
                    _commander.Command(_topic, m.Value);
                }
            });
        }
    }
}
