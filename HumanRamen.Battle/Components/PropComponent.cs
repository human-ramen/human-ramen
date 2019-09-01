namespace HumanRamen.Battle.Components
{
    public class PropComponent
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }

        public PropComponent() { }

        public PropComponent(int str, int dex)
        {
            Strength = str;
            Dexterity = dex;
        }
    }
}
