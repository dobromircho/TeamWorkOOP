
namespace TrqbvaDaSiIzberemIme
{
     public abstract class GameObject
    {
         private Position position;
         private char objectSymbol;
         public  GameObject(Position position, char objectSymbol)
         {
             this.Position = position;
             this.ObjectSymbol = objectSymbol;

         }

         public Position Position
         {
             get { return this.position;  }
             set
             {
                 if (value.X < 0)
                 {
                     value.X = 0;
                 }
                if (value.Y < 0)
                {
                    value.Y = 0;
                }
                if (value.X >= GameEngine.Engine.mapSize)
                {
                    value.X = GameEngine.Engine.mapSize -1;
                }
                if (value.Y >= GameEngine.Engine.mapSize)
                {
                    value.Y = GameEngine.Engine.mapSize -1;
                }
                this.position = value;
             }
         }
         public char ObjectSymbol
         {
             get { return this.objectSymbol; }
             set
             {
                 
                 this.objectSymbol = value;
             }
         }
    }
}
