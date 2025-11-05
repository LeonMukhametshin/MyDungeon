namespace MyDungeon.MyDungeon.Base.HealthComponents
{
    public abstract class HealthDodgeDecorator : Health
    {
        protected readonly Health Decorable;

        protected float _dodgeChanceValue;
        protected static readonly Random _random = new();

        protected HealthDodgeDecorator(Health decorable)
          : base(decorable.Value)
        {
            Decorable = decorable ?? throw new ArgumentNullException(nameof(decorable));
        }

        public sealed override void TakeDamage(int damage)
        {
            Decorable.TakeDamage(AffectDamage(damage));

            Value = Decorable.Value;
        }

        protected abstract int AffectDamage(int damage);
    }
}
