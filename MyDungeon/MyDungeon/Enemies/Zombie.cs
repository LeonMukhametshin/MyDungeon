using MyDungeon.Base;
using MyDungeon.MyDungeon.Base.HealthComponents;

namespace MyDungeon.Enemies
{
    public class Zombie : Enemy
    {
        public Zombie(int healthPoint, string name = "NoName")
            : base(healthPoint, name) { }

        public Zombie(Health health, string name = "NoName")
            : base(health, name) { }

        protected override void Die() => Console.WriteLine($"{Name} повержен");
       
        protected override void OnDamageApplied(int actualDamage)
        {
            if (IsAlive && actualDamage > 20)
            {
                Console.WriteLine($"{Name} впадает в ярость");
            }
        }

        public override void Move() => Console.WriteLine($"{Name} передвигается");

        public override string ToString() => base.ToString();
    }
}
