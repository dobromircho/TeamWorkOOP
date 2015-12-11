
namespace TrqbvaDaSiIzberemIme.Characters
{
    
    [Enemy]
    public class Orc : Character
    {
        const int orcDamage = 70;
        const int orcHealth = 100;
        const char orcSymbol = 'O';

        public Orc(Position position) : base(position, orcSymbol, orcSymbol.ToString(), orcDamage, orcHealth)
        {
        }
    }
}
