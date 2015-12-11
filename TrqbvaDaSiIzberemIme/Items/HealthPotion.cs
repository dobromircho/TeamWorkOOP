using System;
namespace TrqbvaDaSiIzberemIme.Items
{
    class HealthPotion : Item
    {
        private const char healthPotionSymbol = '*';
        private int potion = 100;
        public HealthPotion(Position position, ItemState itemState)
            : base(position, healthPotionSymbol, itemState)
        {

        }
        public int Potion
        {
            get { return this.potion; }
        }
    }
}
