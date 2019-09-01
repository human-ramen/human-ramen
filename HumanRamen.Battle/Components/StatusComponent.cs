namespace HumanRamen.Battle.Components
{
    public class StatusComponent
    {
        public int Health { get; set; } = 100;
        public int Power { get; set; } = 100;

        public StatusComponent() { }

        public StatusComponent(int health, int power)
        {
            Health = health;
            Power = power;
        }
    }
}
