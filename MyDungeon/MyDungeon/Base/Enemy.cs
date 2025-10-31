using MyDungeon.Core.Interfaces;

namespace MyDungeon.Base
{
    public abstract class Enemy() : IMovable, IDamageable
    {
        #region Поля и свойства 
        public int HealthPoint { get; protected set; }
        public string Name { get; protected set; } 
        public int Armour { get; protected set; } 

        public EnemyType MobType { get; protected set; }

        public bool IsAlive => HealthPoint > 0;
        #endregion

        private bool CanTakeDamage() => IsAlive;

        public void TakeDamage(int damage)
        {
            if (!CanTakeDamage() || damage <= 0)
            {
                OnDamagePrevented();
                return;
            }

            int actualDamage = CalculateActualDamage(damage);
            ApplyDamage(actualDamage);
            OnDamageApplied(actualDamage);
        }

        private void ApplyDamage(int actualDamage)
        {
            HealthPoint -= actualDamage;

            if (IsAlive)
            {
                HealthPoint = 0;
                Die();
            }
        }

        protected virtual int CalculateActualDamage(int damage)
        {
            //TODO: магические числа
            float reductionPercent = Armour * 0.05f;
            return (int)(damage * (1 - reductionPercent));
        }

        public override string ToString() =>
             $"{Name} | HP: {HealthPoint}/100 | Armour: {Armour} | Status: {(IsAlive ? "Alive" : "Dead")}";

        protected abstract void Die();

        protected virtual void OnDamageApplied(int actualDamage) { }

        protected virtual void OnDamagePrevented() { }

        public virtual void Move() { }
    }

    public enum EnemyType
    {
        Unknown,
        Zombie,
        Ghost,
    }
}