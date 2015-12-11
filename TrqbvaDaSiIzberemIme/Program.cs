
namespace TrqbvaDaSiIzberemIme
{
    using Interfaces;
    using UI;
    using GameEngine;

    class Program
    {
        static void Main(string[] args)
        {
            IRenderer renderer = new ConsoleRenderer();
            IInputReader reader = new ConsoleInputReader();

            Engine engine = new Engine(reader, renderer);

            engine.Run();
        }
    }
}
