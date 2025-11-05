using MyDungeon.Core.Interfaces;
using MyDungeon.MyDungeon.Base.Armour;
using MyDungeon.MyDungeon.Base.HealthComponents;

namespace MyDungeon.Base
{
    public abstract class Enemy : IMovable, IDamageable
    {
        public int HealthPoint => _health.Value;
        public string Name { get; protected set; } 
        public EnemyType MobType { get; protected set; }

        private Health _health;

        public Health HealthComponent
        {
            get => _health;
            set => _health = value ?? throw new ArgumentException(nameof(value));
        }

        public bool IsAlive => HealthPoint > 0;

        protected Enemy(int healthpoint, string name = "NoName")
            : this(new Health(healthpoint), name) { }

        protected Enemy(Health health, string name = "NoName")
        {
            Name = name;
            _health = health ?? throw new ArgumentNullException(nameof(health));
        }

        private bool CanTakeDamage() => IsAlive;

        public void TakeDamage(int damage)
        {
            if(CanTakeDamage())
            {
                _health.TakeDamage(damage);
                OnDamageApplied(damage);

                if (!IsAlive)
                {
                    Die();
                }
            }
            else
            {
                OnDamagePrevented();
            }
        }

        protected abstract void Die();

        protected virtual void OnDamageApplied(int actualDamage) { }

        protected virtual void OnDamagePrevented() { }

        public virtual void Move() { }

        public override string ToString() =>
             $"{Name} | HP: {HealthPoint}/100 | Status: {(IsAlive ? "Alive" : "Dead")}";
    }

    public enum EnemyType
    {
        Unknown,
        Zombie,
        Ghost,
    }
}