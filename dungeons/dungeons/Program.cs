namespace dungeons
{
    class enemy
    {
        public int pos;
        public int health;
        public int damage;
        public enemy(int pos, int health, int damage)
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

            int pos = 1;
            int health = 100;
            int damage = 1;

            List<enemy> enemylist = new List<enemy>();
            enemylist.Add(new enemy(4, 5, 5));
            enemylist.Add(new enemy(4, 5, 20));

            int[] positions = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            
            void clearlist(int[] list)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    list[i] = 0;
                }
            }

            while (true)
            {
                clearlist(positions);
                if (Console.KeyAvailable)
                {
                    var input = Console.ReadKey(true);


                    if (input.Key == ConsoleKey.A) pos--;
                    if (input.Key == ConsoleKey.D) pos++;
                    
                    foreach (enemy meanie in enemylist)
                    {
                        if (input.Key == ConsoleKey.Z) meanie.pos--;
                        if (input.Key == ConsoleKey.C) meanie.pos++;

                        if (input.Key == ConsoleKey.Spacebar && pos == meanie.pos) meanie.health -= damage;
                    }
                }

                Console.SetCursorPosition(1,1);
                Console.Write(pos + 1);
                Console.SetCursorPosition(3, 1);
                Console.Write(health);
                foreach (enemy meanie in enemylist)
                {
                    positions[meanie.pos] = meanie.health;
                    if (meanie.health <= 0) enemylist.Remove(meanie);
                }
                for (int i = 0; i < positions.Count(); i++)
                {
                    if (i == pos) Console.ForegroundColor = ConsoleColor.Blue;
                    else Console.ForegroundColor = ConsoleColor.White;

                    Console.SetCursorPosition(i, 12);
                    Console.Write(positions[i]);
                }
            }

            



        }
    }
}
