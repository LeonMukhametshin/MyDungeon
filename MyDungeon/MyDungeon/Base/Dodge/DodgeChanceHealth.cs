using MyDungeon.MyDungeon.Base.HealthComponents;

namespace MyDungeon.MyDungeon.Base.Dodge
{
    public sealed class DodgeChanceHealth : HealthDecorator
    {
        private readonly float _dodgeChanceValue;
        private static readonly Random _random = new();

        public DodgeChanceHealth(Health decorator, float dodgeChanceValue) : base(decorator)
        {
            if (dodgeChanceValue < 0 || dodgeChanceValue > 1f)
            {
                throw new ArgumentOutOfRangeException(nameof(dodgeChanceValue));
            }

            _dodgeChanceValue = dodgeChanceValue;
        }

        protected override int AffectDamage(int damage) => IsDodged() ? 0 : damage;

        private bool IsDodged() => _random.NextDouble() < _dodgeChanceValue;
    }
}
