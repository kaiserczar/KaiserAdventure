using System;

namespace AdventureClient {
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {

        public static String UserName = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            UserName = Microsoft.VisualBasic.Interaction.InputBox("Input your username.", "TEMPORARY", "Neophyte", -1, -1);

            if (UserName != "") {
                using (var game = new Main())
                    game.Run();
            }
        }
    }
#endif
}
