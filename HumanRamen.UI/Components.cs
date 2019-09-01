using System.Collections.Generic;

namespace HumanRamen.UI
{
    public class Dialog
    {
        public string Name { get; private set; }
        public string Text { get; private set; }

        public Dialog(string name, string text)
        {
            Name = name;
            Text = text;
        }
    }

    public class Choices
    {
        public class Choice
        {
            public string Key { get; private set; }
            public string Text { get; private set; }

            public Choice(string key, string text)
            {
                Key = key;
                Text = text;
            }
        }

        public List<Choice> ChoicesList { get; private set; }

        public Choices(List<Choice> choicesList)
        {
            ChoicesList = choicesList;
        }
    }
}
