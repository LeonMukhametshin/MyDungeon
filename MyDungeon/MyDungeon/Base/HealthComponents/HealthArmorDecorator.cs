namespace MyDungeon.MyDungeon.Base.HealthComponents
{
    public abstract class HealthArmorDecorator : Health
    {
        protected readonly Health Decorable;
        protected int _armorValue;

        public override bool HasArmor => Decorable.HasArmor;

        protected HealthArmorDecorator(Health decorable) 
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
