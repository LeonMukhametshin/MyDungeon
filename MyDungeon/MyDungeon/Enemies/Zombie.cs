using MyDungeon.Base;

namespace MyDungeon.Enemies
{
    public class Zombie : Enemy
    {
        //TODO: добавить конструкторы класса 
        public Zombie()
        {
            Name = "Zombie";
            HealthPoint = 110; 
            Armour = 12;        
            MobType = EnemyType.Zombie;
        }

        protected override int CalculateActualDamage(int damage) => damage;

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
