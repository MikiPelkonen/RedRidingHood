using System;

namespace RedRidingHood
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new RedRidingHoodGame())
                game.Run();
        }
    }
}
