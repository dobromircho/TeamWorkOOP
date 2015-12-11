
namespace TrqbvaDaSiIzberemIme.Interfaces
{   
    using Characters;
    public interface IAttack
    {
        int Damage { get; set; }
        void Attack(ICharacter enemy);

    }
}
