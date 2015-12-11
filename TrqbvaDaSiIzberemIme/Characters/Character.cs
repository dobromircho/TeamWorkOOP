using System;
using TrqbvaDaSiIzberemIme.Interfaces;

namespace TrqbvaDaSiIzberemIme.Characters
{
    
    public abstract class Character : GameObject, ICharacter
    {
        private string name;
        protected Character(Position position, char objectSymbol, string name, int damage, int health)
            : base(position, objectSymbol)
        {
            this.Name = name;
            this.Health = health;
            this.Damage = damage;
        }
        public string Name
        {
            get { return this.name; }
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("name", "Name cannot be null or empty");
                }
                this.name = value;
            }
        }

        public int Damage { get; set; }

        public int Health { get; set; }

        public void Attack(Character enemy)
        {
            enemy.Health -= this.Damage;
        }

        public void Attack(ICharacter enemy)
        {
            enemy.Health -= this.Damage;
        }
    }
}
