
using System.Runtime.CompilerServices;

namespace TrqbvaDaSiIzberemIme.GameEngine

{
    using System.Linq;
    using System.Collections.Generic;
    using Interfaces;
    using Characters;
    using Items;
    using System.IO;
    using System;
    using System.Reflection;
    using System.Text;

    public class Engine
    {
        public static int mapSize;
        int numberOfEnemies;
        int numberOfHealthPotions;
        private static readonly Random rand = new Random();

        private readonly IInputReader reader;
        private readonly IRenderer renderer;

        private readonly IList<ICharacter> characters;
        private readonly IList<Item> items;
        private IPlayer player;

        public Engine(IInputReader reader, IRenderer renderer)
        {
            this.reader = reader;
            this.renderer = renderer;
            this.characters = new List<ICharacter>();
            this.items = new List<Item>();
            
        }

        public bool IsRunning { get; set; }

        public void Run()
        {
            this.IsRunning = true;
            var playerName = GetPlayerName();
            PlayerType race = GetPlayerRace();
            mapSize = GetMapSize();
            this.numberOfEnemies = (mapSize * mapSize) / 10;
            this.numberOfHealthPotions = (mapSize * mapSize) / 8;
            this.player = new Player(new Position(0, 0), 'P', playerName, race);
            
            this.PopulateEnemies();
            this.PopulateItems();
            this.renderer.Clear();
            this.PrintMap();
            while (this.IsRunning)
            {
                ConsoleKeyInfo cki = new ConsoleKeyInfo();
                cki = Console.ReadKey();
                this.renderer.Clear();
                this.ExecuteCommand(cki);
                Console.WriteLine();
                this.PrintMap();
                
            }
        }

        private int GetMapSize()
        {
            this.renderer.WriteLine("Enter Map Size (10 - 30)");
            int size = 0;
            bool isValid = int.TryParse(this.reader.ReadLine(), out size);
            if (size < 10 || size > 30)
            {
                isValid = false;
            }
            
            while (!isValid)
            {
                this.renderer.WriteLine("Wrong entry!!!  Enter Map Size (10 - 30):");
                size = 0;
                isValid = int.TryParse(this.reader.ReadLine(), out size);
                if (size < 5 || size > 20)
                {
                    isValid = false;
                }
            }
            return size;
        }

        private GameObject CreateEnemies()
        {
            int currentX = rand.Next(1, mapSize);
            int currentY = rand.Next(1, mapSize);

            bool isExistingEnemeis = this.characters.Any(c => c.Position.X == currentX && c.Position.Y == currentY);

            while (isExistingEnemeis)
            {
                currentX = rand.Next(1, mapSize);
                currentY = rand.Next(1, mapSize);

                isExistingEnemeis = this.characters.Any(c => c.Position.X == currentX && c.Position.Y == currentY);
            }
            var typeOfEnemy = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.CustomAttributes
                .Any(a => a.AttributeType == typeof(EnemyAttribute))).ToArray();

            var type = typeOfEnemy[rand.Next(0, typeOfEnemy.Length)];
            GameObject character = Activator.CreateInstance(type, (new Position(currentX, currentY)))as GameObject;

            return character;
        }
        private Item CreateItem()
        {
            int currentX = rand.Next(1, mapSize);
            int currentY = rand.Next(1, mapSize);

            bool isExistingEnemeis = this.characters.Any(c => c.Position.X == currentX && c.Position.Y == currentY);
            bool isExistingItems = this.items.Any(c => c.Position.X == currentX && c.Position.Y == currentY);

            while (isExistingItems || isExistingEnemeis)
            {
                currentX = rand.Next(1, mapSize);
                currentY = rand.Next(1, mapSize);

                isExistingEnemeis = this.characters.Any(c => c.Position.X == currentX && c.Position.Y == currentY);
                isExistingItems = this.items.Any(c => c.Position.X == currentX && c.Position.Y == currentY);
            }
            return new HealthPotion(new Position(currentX,currentY), ItemState.Available);
        }

        private void PopulateEnemies()
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                GameObject enemy = this.CreateEnemies();
                this.characters.Add((ICharacter) enemy);
            }
        }

        private void PopulateItems()
        {
            for (int i = 0; i < numberOfHealthPotions; i++)
            {
                Item item = this.CreateItem();
                this.items.Add(item);
            }
        }

        private PlayerType GetPlayerRace()
        {
            this.renderer.WriteLine("Choose a player:");
            this.renderer.WriteLine("1.Zena - Damage: 100 Health: 90");
            this.renderer.WriteLine("2.Hercules - Damage: 120 Health = 80");
            this.renderer.WriteLine("3.Dworf - Damage: 80 Health: 120");

            string choice = this.reader.ReadLine();

            while (choice != "1" && choice != "2" && choice != "3")
            {
                this.renderer.WriteLine("Invalid choice of player.Please re-enter!");
                choice = this.reader.ReadLine();
            }

            PlayerType race = (PlayerType)int.Parse(choice);
            return race;
        }

        private string GetPlayerName()
        {
            this.renderer.WriteLine("Please enter the player's name:");
            string playerName = this.reader.ReadLine();

            while (string.IsNullOrWhiteSpace(playerName))
            {
                this.renderer.WriteLine("Player name cannot be empty. Please re-enter");
                playerName = this.reader.ReadLine();
            }
            return playerName;
        }

        private void ExecuteCommand(ConsoleKeyInfo cki)
        {
            switch (cki.Key)
            {
                
                case ConsoleKey.LeftArrow:
                    player.Position = new Position(this.player.Position.X - 1, this.player.Position.Y);
                    this.OnCollision();
                    break;
                case ConsoleKey.UpArrow:
                    this.player.Position = new Position(this.player.Position.X, this.player.Position.Y - 1);
                    this.OnCollision();
                    break;
                case ConsoleKey.RightArrow:
                    this.player.Position = new Position(this.player.Position.X + 1, this.player.Position.Y);
                    this.OnCollision();
                    break;
                case ConsoleKey.DownArrow:
                    this.player.Position = new Position(this.player.Position.X, this.player.Position.Y + 1);
                    this.OnCollision();
                    break;
                case ConsoleKey.P:
                    this.player.Energy();
                    break;
                case ConsoleKey.X:
                    this.IsRunning = false;
                    this.renderer.WriteLine("You have surrendered? Ok, run like a little girl. Bye, noob!");
                    break;
            }

        }

        private void PrintMap()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("-----Move player: Arrow keys------------Drink potion: P-----");
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendFormat("Player: {0}  - Damage: {1} -  Health: {2} - Enemy left: {3} -  Inventory: {4} items",         this.player.Name,
                this.player.Damage,
                this.player.Health,
                this.characters.Count,
                this.player.Inventory.Count());
            sb.AppendLine();
            for (int row = 0; row < mapSize; row++)
            {
                for (int col = 0; col < mapSize; col++)
                {
                    if (this.player.Position.X == col && this.player.Position.Y == row)
                    {
                        sb.Append('P');
                        continue;
                    }

                    Character character = (Character)characters.FirstOrDefault
                        (c => c.Position.X == col && c.Position.Y == row && c.Health > 0);
                    Item item = this.items.FirstOrDefault
                        (c => c.Position.X == col && c.Position.Y == row && c.ItemState == ItemState.Available);

                    if (character == null && item == null)
                    {
                        sb.Append('.');
                    }
                    else if (character != null)
                    {
                        sb.Append(character.ObjectSymbol);
                    }
                    else
                    {
                        sb.Append(item.ObjectSymbol);
                    }
                   
                }
                sb.AppendLine();
            }
            //this.renderer.WriteLine(sb.ToString());
            foreach (char ch in sb.ToString())
            {
                if (ch == 'P')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (ch == '.')
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                }
                else if (ch == '*')
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write(ch);
            }
        }

        private void ExecuteHelpCommand()
        {
            string helpInfo = File.ReadAllText("../../Help.txt");
            this.renderer.WriteLine(helpInfo);
        }

        private void OnCollision()
        {
            ICharacter enemy = this.characters.Cast<ICharacter>().FirstOrDefault(e => e.Position.X == this.player.Position.X && e.Position.Y == this.player.Position.Y && e.Health > 0);

            if (enemy != null)
            {
                this.EnterBattle(enemy);
                return;
            }

            Item item = this.items.Cast<Item>().FirstOrDefault(e => e.Position.X == this.player.Position.X && e.Position.Y == this.player.Position.Y && e.ItemState == ItemState.Available);

            if (item != null)
            {
                this.player.AddItemToInventory(item);
                this.player.Energy();
                item.ItemState = ItemState.Taken;
                this.renderer.WriteLine("Potion taken!");
            }
        }

        private void EnterBattle(ICharacter enemy)
        {
            this.player.Attack(enemy);
            if (enemy.Health <= 0)
            {
                this.renderer.WriteLine("You killed enemy!!!");
                this.characters.Remove(enemy);
                return;
            }
            else
            {
                this.renderer.WriteLine("You hit enemy!!!");
            }

            enemy.Attack(this.player);

            if (this.player.Health <= 0)
            {
                this.renderer.Clear();
                this.renderer.WriteLine("YOU ARE DEAD!!!");
                this.IsRunning = false;
            }
        }
    }
}
