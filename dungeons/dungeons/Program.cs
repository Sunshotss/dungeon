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

            Random rnd = new Random();
            int boardselected = 1;
            int handselected = 1;
            int scale = 0;


            List<card> cardsonplayerside = new List<card>();
            List<card> cardsonenemyside = new List<card>();

            List<card> playerside = new List<card>();
            List<card> enemyside = new List<card>();

            List<card> deck = new List<card>();
            deck.Add(new card(0, 1, 1));
            deck.Add(new card(0, 2, 1));
            deck.Add(new card(0, 3, 4));
            deck.Add(new card(0, 4, 4));
            deck.Add(new card(0, 5, 1));
            List<card> hand = new List<card>();


            void initlist(List<card> list)
            {
                for (int i = 0; i < 4; i++)
                {
                    list.Add(new card(0, 0, 0));
                }
            }
            
            initlist(playerside);
            initlist(enemyside);

            void drawhand(List<card> deck, List<card> hand)
            {
                for (int i = 0; i < 4; i++)
                {
                    int cardindex = rnd.Next(0, deck.Count());
                    hand.Add(new card(deck[cardindex].pos, deck[cardindex].health, deck[cardindex].damage));
                    deck.RemoveAt(cardindex);
                }
            }
            drawhand(deck, hand);              


            while (true)
            {
                for (int i = 0; i < cardsonplayerside.Count; i++)
                {
                    playerside[cardsonplayerside[i].pos] = cardsonplayerside[i];
                    if (cardsonplayerside[i].health <= 0) cardsonplayerside.RemoveAt(i);
                }
                for (int i = 0; i < cardsonenemyside.Count; i++)
                {
                    enemyside[cardsonenemyside[i].pos] = cardsonenemyside[i];
                }

                if (Console.KeyAvailable)
                {
                    var input = Console.ReadKey(true);


                    if (input.Key == ConsoleKey.A) boardselected--;
                    if (input.Key == ConsoleKey.D) boardselected++;

                    if (input.Key == ConsoleKey.W) handselected++;
                    if (input.Key == ConsoleKey.S) handselected--;


                    if (input.Key == ConsoleKey.Spacebar)
                    {
                        for (int i = 0; i < playerside.Count(); i++)
                        {
                            if (playerside[i].health > 0) playerside[i].health -= enemyside[i].damage;
                            else if (playerside[i].health <= 0) scale -= enemyside[i].damage; playerside[i] = new card(0, 0, 0);
                            if (playerside[i].health < 0) playerside[i].health = 0;
                        }
                    }
                    
                    
                    
                    if (input.Key == ConsoleKey.D1)
                    {
                        cardsonplayerside.Add(new card(boardselected, hand[handselected].damage, hand[handselected].health));
                        hand.RemoveAt(handselected);
                    }


                    if (input.Key == ConsoleKey.D2)
                    {
                        cardsonenemyside.Add(new card(boardselected, 1, 1));
                    }






                }

                Console.SetCursorPosition(1,1);
                Console.Write(boardselected + 1);
                Console.SetCursorPosition(1, 2);
                Console.Write(scale);
                Console.SetCursorPosition(1, 3);
                Console.Write(cardsonplayerside.Count());

                for (int i = 0; i < playerside.Count(); i++)
                {


                    if (i == boardselected) Console.ForegroundColor = ConsoleColor.Blue;
                    else Console.ForegroundColor = ConsoleColor.White;

                    Console.SetCursorPosition(i, 11);
                    Console.Write(playerside[i].health);
                    Console.SetCursorPosition(i, 14);
                    Console.Write(enemyside[i].health);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(i, 10);
                    Console.Write(playerside[i].damage);
                    Console.SetCursorPosition(i, 15);
                    Console.Write(enemyside[i].damage);
                    Console.ForegroundColor = ConsoleColor.White;
                }

                for (int i = 0; i < hand.Count; i++)
                {
                    if (i == handselected) Console.ForegroundColor = ConsoleColor.Blue;
                    else Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(i, 4);
                    Console.WriteLine(hand[i].health);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(i, 5);
                    Console.WriteLine(hand[i].damage);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                for (int i = 0; i < deck.Count; i++)
                {
                    Console.SetCursorPosition(i, 6);
                    Console.WriteLine(deck[i].health);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(i, 7);
                    Console.WriteLine(deck[i].damage);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                if (scale <= -5) break;

            }
            Console.SetCursorPosition (0,0);
            Console.WriteLine("youlost");
        }
    }
}
