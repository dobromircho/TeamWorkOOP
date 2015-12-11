
namespace TrqbvaDaSiIzberemIme.Characters
{
    [Enemy]
    class Griffon : Character
    {
        const int griffonDamage = 70;
        const int griffonHealth = 120;
        const char griffonSymbol = 'G';

        public Griffon(Position position) : base(position, griffonSymbol, griffonSymbol.ToString(), griffonDamage, griffonHealth)
        {
        }
    }
}
