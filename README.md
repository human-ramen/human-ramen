[![Build Status](https://travis-ci.org/human-ramen/human-ramen.svg?branch=master)](https://travis-ci.org/human-ramen/human-ramen)  
[WIP] Toolkit for Human Ramen games

# HumanRamen.Scenario
Lua scripts driven scenario tool. Code some talks, mutate some world's vars and start a fight.

[Here](https://github.com/human-ramen/human-ramen/blob/master/HumanRamen.Entities/Systems/ScenarioSystem.cs) you can see how it works. Ok, would work.

```lua
Characters:Add("Karen", "ChrKaren")
Background = "BgKitchen"

startNode = Scenario:CreateNode("Karen", "Hello, Human")

okNode = Scenario:CreateNode("Karen", "Hey :)")
notOkNode = Scenario:CreateNode("Karen", "Karen, of course!")

startNode:AddResponse("Hello", okNode)
startNode:AddResponse("Who are you?", notOkNode)

...
```

Shove it in a system and watch results.

# HumanRamen.Battle
Turn-based battle system. [Here](https://github.com/human-ramen/human-ramen/blob/master/HumanRamen.Battle.Tests/BattleTest.cs) battle setup. If everyone is ready just throw battle entity:
```c#
var battle = _world.CreateEntity();
battle.Attach(new BattleComponent(enemies, player));
```
And we're ready to dance.

Tests shows something like predefined fight.

# HumanRamen.UI
Barebone Visual Novel / JRPG style ui system. Has big black rectangle, can show letters and lights up some choices.

# HumanRamen.Entities
Musthave systems for showing some sprites and taken some keyboard presses.

## RenderSystem
Renders SpriteComponent and SpriteFontComponent. Doing some work with resizing and positioning stuff.

## ControlSystem
Parse lua script with keyboard->command maps and with presses throw commands through [HumanRamen.Essentials.Commander](https://github.com/human-ramen/human-ramen/blob/master/HumanRamen.Essentials/Commander.cs).

```lua
kbd.Q = "Exit"
kbd.Space = "Continue"
kbd.F = "Fullscreen"
kbd.D = "ToggleDebugConsole"
kbd.Enter = "Enter"
kbd.J = "Down"
kbd.K = "Up"
kbd.Down = "Down"
kbd.Up = "Up"
...
```
## ScenarioSystem
See HumanRamen.Scenario package. This system makes scenarios work.

# HumanRamen.Essentials
Essentials tools for everyone.

## Commander
Create commander and throw it in systems where you want to receive or send commands:
```c#
_commander = new Commander();

new SomeSystem(_commander);
```

If your system wants to receive commands it must be registered to it and implement ICommandHandler interface.
```c#
public class SomeSystem : ICommandHandler
{
    private Commander _commander;

    public SomeSystem(Commander commander)
    {
        _commander = commander;
    
        _commander.RegisterHandler("control", this)
    }
    
    public void HandleCommand(string topic, string command)
    {
        if (topic == "control" && command == "Shoot")
        {
            // do stuff
            
            return;
        }
    }
}
```

And you can command from everywhere with commander:
```c#
_commander.Command("control", "shoot")
```

# HumanRamen.Graphics

## ColoredTexture
SpriteBatch helper for creation simple 1x1 texture with one color.
```c#
Primary = new ColoredTexture(graphicDevice, Color.Blue);
```
