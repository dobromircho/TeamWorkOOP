
namespace TrqbvaDaSiIzberemIme.Characters
{
    class Dworf : Player
    {
        private const PlayerType race = PlayerType.Dworf;

        public Dworf(Position position, char objectSymbol, string name) : base(position, objectSymbol, name, race)
        {
        }
    }
}
