using MyDungeon.Base;
using MyDungeon.MyDungeon.Base.HealthComponents;

namespace MyDungeon.Enemies
{
    public class Ghost : Enemy
    {
        public Ghost(int healthPoint, string name = "NoName")
            : base(healthPoint, name) { }

        public Ghost(Health health, string name = "NoName")
            : base(health, name) { }

        protected override void Die() => Console.WriteLine($"{Name} повержен!");

        public override void Move() => Console.WriteLine($"{Name} передвигается");

        public override string ToString() => base.ToString();
    }
}
