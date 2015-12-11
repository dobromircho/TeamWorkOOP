
namespace TrqbvaDaSiIzberemIme.Characters
{
    class Hercules : Player
    {
        private const PlayerType race = PlayerType.Hercules;

        public Hercules(Position position, char objectSymbol, string name) : base(position, objectSymbol, name, race)
        {
        }
    }
}
