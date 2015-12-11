using System;
using System.Collections.Generic;

namespace TrqbvaDaSiIzberemIme.Characters
{
    using Interfaces;
    using Items;
    using System;
    using System.Linq;

    public class Player : Character, IPlayer
    {
        private List<Item> inventory;

        public Player(Position position, char objectSymbol, string name, PlayerType race)
            : base(position, objectSymbol, name, 0, 0)
        {
            this.Race = race;
            this.SetPlayer();
            this.inventory = new List<Item>();

        }

        private void SetPlayer()
        {
            switch(this.Race){
                case PlayerType.Zena:
                    this.Damage = 100;
                    this.Health = 90;
                break;
                case PlayerType.Hercules:
                this.Damage = 120;
                this.Health = 80;;
                    break;
                case PlayerType.Dworf:
                    this.Damage = 80;
                    this.Health = 120;
                    break;
                default:
                    throw new ArgumentException("Unknown player race");
            }
        }

        public PlayerType Race { get; set; }

        //public void Move(ConsoleKeyInfo cki)
        //{
        //    switch (cki.Key)
        //    {
        //        case ConsoleKey.LeftArrow:
        //            this.Position = new Position(this.Position.X - 1, this.Position.Y);
        //            break;
        //        case ConsoleKey.UpArrow:
        //            this.Position = new Position(this.Position.X, this.Position.Y - 1);
        //            break;
        //        case ConsoleKey.RightArrow:
        //            this.Position = new Position(this.Position.X + 1, this.Position.Y);
        //            break;
        //        case ConsoleKey.DownArrow:
        //            this.Position = new Position(this.Position.X, this.Position.Y + 1);
        //            break;
        //        default:
        //            break;
        //    }

        //}

        public IEnumerable<Item> Inventory
        {
            get { return this.inventory; }
        }

        public void AddItemToInventory(Item item)
        {
            this.inventory.Add(item);
        }

        public void Energy()
        {
            HealthPotion potion = this.inventory.Cast<HealthPotion>().FirstOrDefault();
            if (potion == null)
            {
                Console.WriteLine("Not enough potions!!!");
                return;
            }
            this.Health += potion.Potion;
            Console.WriteLine("Potion used!!!");
            //this.inventory.Remove(potion);
        }

        public int Experience
        {
            get { throw new NotImplementedException(); }
        }

        public void LevelUp()
        {
            throw new NotImplementedException();
        }

        public void Attack(Character enemy)
        {
            this.Damage += enemy.Damage; // returns the total Damage that the player has done to the enemies
            this.Health -= enemy.Damage; //decreasing the player's health when hitting the enemy
            enemy.Health -= this.Damage; //decreasing the enemy`s health
            
        }
        public void Attack(ICharacter enemy)
        {
            this.Damage += enemy.Damage;
            this.Health -= enemy.Damage;
            enemy.Health -= this.Damage;
        }

    }
}
