namespace dungeons
{
    class card
    {
        public int pos;
        public int health;
        public int damage;
        public card(int pos, int health, int damage)
        {
            this.pos = pos;
            this.health = health;
            this.damage = damage;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;


            int selected = 1;

            List<card> playerside = new List<card>();
            List<card> enemyside = new List<card>();

            void initlist(List<card> list)
            {
                for (int i = 0; i < 4; i++)
                {
                    list.Add(new card(0,0,0));
                }
            }
            
            initlist(playerside);
            initlist(enemyside);

            playerside[0] = new card(0, 1, 1);
            enemyside[0] = new card(0, 1, 1);

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var input = Console.ReadKey(true);


                    if (input.Key == ConsoleKey.A) selected--;
                    if (input.Key == ConsoleKey.D) selected++;


                    if (input.Key == ConsoleKey.Spacebar)
                    {
                        for (int i = 0; i < playerside.Count(); i++)
                        {
                            playerside[i].health -= enemyside[i].damage;

                        }
                    }

                }

                Console.SetCursorPosition(1,1);
                Console.Write(selected + 1);


                for (int i = 0; i < playerside.Count(); i++)
                {

                    if (i == selected) Console.ForegroundColor = ConsoleColor.Blue;
                    else Console.ForegroundColor = ConsoleColor.White;

                    Console.SetCursorPosition(i, 10);
                    Console.Write(playerside[i].health);
                    Console.SetCursorPosition(i, 12);
                    Console.Write(enemyside[i].health);
                }

            }

            



        }
    }
}
