using MyDungeon.Core.Interfaces;

namespace MyDungeon.MyDungeon.Base.HealthComponents
{
    public class Health : IDamageable
    {
        private int _value;

        public virtual bool HasArmor => false;

        public int Value
        {
            get => _value; 
            protected set 
            {
                _value = value > 0 ? value : 0; 
            }
        }

        public Health(int healthpoint)
        {
            Value = healthpoint >= 0 
                ? healthpoint 
                : throw new ArgumentException($"Hp can't be negative {healthpoint}", nameof(healthpoint));
        }

        public virtual void TakeDamage(int damage)
        {
            _value -= Math.Max(0, damage);
        }
    }
}
