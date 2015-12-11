
namespace TrqbvaDaSiIzberemIme.Characters
{
    [Enemy]
    public class Dragon : Character
    {
        const int dragonDamage = 70;
        const int dragonHealth = 150;
        const char dragonSymbol = 'D';

        public Dragon(Position position) : base(position, dragonSymbol, dragonSymbol.ToString(), dragonDamage, dragonHealth)
        {

        }
    }
}
