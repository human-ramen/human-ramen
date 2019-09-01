using System;
using System.Collections.Generic;

namespace HumanRamen.Scenario
{
    public class Scenario
    {

        public class Node
        {
            public string DialogueName { get; set; }
            public string DialogueText { get; set; }
            public Dictionary<string, Node> Responses { get; set; }

            public Node AddResponse(string text, Node node)
            {
                if (node == null)
                {
                    throw new Exception("Node is null");
                }

                Responses[text] = node;

                return this;
            }
        }

        public Node Start { get; private set; }

        public Node CreateNode(string name, string text)
        {
            var node = new Node();
            node.DialogueName = name;
            node.DialogueText = text;
            node.Responses = new Dictionary<string, Node>();

            if (Start == null) Start = node;

            return node;
        }
    }

}
