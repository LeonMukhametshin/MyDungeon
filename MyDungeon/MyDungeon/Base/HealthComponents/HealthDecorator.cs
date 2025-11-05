namespace MyDungeon.MyDungeon.Base.HealthComponents
{
    public abstract class HealthDecorator : Health
    {
        protected readonly Health Decorable;

        public override bool HasArmor => Decorable.HasArmor;

        protected HealthDecorator(Health decorable) 
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
