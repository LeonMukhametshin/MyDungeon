using MyDungeon.MyDungeon.Base.HealthComponents;

namespace MyDungeon.MyDungeon.Base.Armour
{
    public sealed class ArmorHealth : HealthArmorDecorator
    {
        public override bool HasArmor => true;

        public ArmorHealth(Health decorable, int armour) : base(decorable)
        {
            if(decorable.HasArmor)
            {
                throw new InvalidOperationException("Враг уже имеет броню");
            }
            _armorValue = armour;
        }

        protected override int AffectDamage(int damage) => damage - _armorValue;
    }
}