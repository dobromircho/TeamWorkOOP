
namespace TrqbvaDaSiIzberemIme.Items
{
    public abstract class Item : GameObject
    {
        protected Item(Position position, char itemSymbol, ItemState itemState)
            : base(position, itemSymbol)
        {
            this.ItemState = itemState;
        }
        public ItemState ItemState { get; set; }
       
    }
}
