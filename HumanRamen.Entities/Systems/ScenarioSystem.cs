using System;
using System.Collections.Generic;
using HumanRamen.Entities.Components;
using HumanRamen.Essentials;
using HumanRamen.Scenario;
using HumanRamen.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using NLua;
using static HumanRamen.UI.Choices;

namespace InfinityDialogue.Systems
{
    // TODO: IGameContent
    public interface IGameContent { }

    public class ScenarioSystem : UpdateSystem, ICommandHandler
    {
        public enum State
        {
            Wait,
            Dialog,
            Choice,
            Battle,
        }

        private readonly Logger _l = new Logger("GameStateSystem");
        private readonly Lua _lua = new Lua();

        private readonly IGameContent _content;
        private readonly UISystem _ui;
        private readonly Commander _commander;

        private readonly string _startScript = "Lua/test_scenario.lua";

        private State _state;
        private Entity _scenarioEntity;

        private Scenario.Node _currentNode;
        private bool _currentNodeInited;

        private SpriteComponent _backgroundSprite;

        private Characters _characters;
        private Dictionary<string, Entity> _characterEntities = new Dictionary<string, Entity>();
        private Dictionary<string, SpriteComponent> _characterSprites = new Dictionary<string, SpriteComponent>();

        public ScenarioSystem(IGameContent content, UISystem ui, Commander commander)
        {
            _content = content;
            _ui = ui;
            _commander = commander;

            _commander.RegisterHandler("UI", this);
        }

        public override void Initialize(World world)
        {
            _scenarioEntity = world.CreateEntity();

            _lua.LoadCLRPackage();

            var characters = new Characters();
            _lua["Characters"] = characters;

            var scenario = new Scenario();
            _lua["Scenario"] = scenario;

            _lua.DoFile(_startScript);

            if (_lua.GetString("Background") != "")
            {
                _backgroundSprite = new SpriteComponent(_content.GetType().GetProperty(_lua.GetString("Background")).GetValue(_content) as Texture2D);
                _backgroundSprite.IsBackground = true;
                _scenarioEntity.Attach(_backgroundSprite);
            }

            // Create character's entity

            foreach (var chr in characters.Pool)
            {
                var name = chr.Value.Name;
                var spritePath = chr.Value.SpritePath;

                _l.Debug(String.Format("Add chr: name {0}, sprite {1}", name, spritePath));

                _characterEntities[name] = world.CreateEntity();

                var spriteComponent = new SpriteComponent(_content.GetType().GetProperty(spritePath).GetValue(_content) as Texture2D);
                // TODO: calculate position according to attributes
                // spriteComponent.Position = new Rectangle(150, 30, _content.ChrKaren.Width / 2, _content.ChrKaren.Height / 2);
                // spriteComponent.Depth = 0.0f;

                _characterSprites.Add(name, spriteComponent);
                _characterEntities[name].Attach(spriteComponent);
            }

            _currentNode = scenario.Start;
            // TODO: check node type
            _state = State.Dialog;
            _ui.IsDialogBackgroundVisible = true;
        }


        public override void Update(GameTime gameTime)
        {
            if (!_currentNodeInited && _state == State.Dialog)
            {
                _ui.UpdateDialog(new Dialog(_currentNode.DialogueName, _currentNode.DialogueText));

                _currentNodeInited = true;
            }

            if (!_currentNodeInited && _state == State.Choice)
            {
                var choicesList = new List<Choice>();

                foreach (var choice in _currentNode.Responses)
                {
                    choicesList.Add(new Choice(choice.Key, choice.Key));
                }

                var choices = new Choices(choicesList);
                _ui.UpdateChoices(choices);

                _currentNodeInited = true;
            }
        }

        public override void Dispose()
        {
            _lua.Dispose();
            base.Dispose();
        }

        public void HandleCommand(string topic, string command)
        {
            if (_state == State.Dialog && topic == "UI" && command == "Continue")
            {
                _state = State.Choice;
                _currentNodeInited = false;
                return;
            }

            if (_state == State.Choice && topic == "UI")
            {
                if (!_currentNode.Responses.ContainsKey(command))
                {
                    throw new ExceptionNullNode(command);
                }

                _currentNode = _currentNode.Responses[command];

                _state = State.Dialog;
                _currentNodeInited = false;
                return;
            }
        }
    }

    public class ExceptionNullNode : Exception
    {
        public ExceptionNullNode(string command) : base(String.Format("Node with key {0} is NULL", command))
        {
        }
    }
}
