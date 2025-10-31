using MyDungeon.Base;

namespace MyDungeon.Enemies
{
    public class Ghost : Enemy
    {
        //TODO: добавить конструкторы класса 
        public Ghost()                     
        {
            Name = "Ghost";
            HealthPoint = 70;
            Armour = 40;
            MobType = EnemyType.Ghost;
        }

        protected override int CalculateActualDamage(int damage) => (int) (damage* 0.4f);

        protected override void Die() => Console.WriteLine($"{Name} повержен!");

        public override void Move() => Console.WriteLine($"{Name} передвигается");

        public override string ToString() => base.ToString();
    }
}
