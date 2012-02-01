using System;

namespace DangerousGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Pokening game = new Pokening())
            {
                game.Run();
            }
        }
    }
#endif
}

