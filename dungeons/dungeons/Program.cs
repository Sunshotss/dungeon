namespace dungeons
{
    class card
    {
        public int pos;
        public int health;
        public int damage;
        public int blood;
        public card(int pos, int health, int damage, int blood)
        {
            this.pos = pos;
            this.health = health;
            this.damage = damage;
            this.blood = blood;
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
            int bloodtemporary = 0;




            List<card> cardsonplayerside = new();
            List<card> cardsonenemyside = new();
            List<card> playerside = new();
            List<card> enemyside = new();

            List<card> deck = new();
            deck.Add(new card(0, 1, 8, 1));
            deck.Add(new card(0, 2, 7, 1));
            deck.Add(new card(0, 3, 6, 1));
            deck.Add(new card(0, 4, 5, 1));
            deck.Add(new card(0, 5, 4, 1));
            deck.Add(new card(0, 6, 3, 1));
            deck.Add(new card(0, 7, 2, 1));
            deck.Add(new card(0, 8, 1, 1));
            List<card> hand = new();


            void initlist(List<card> list)
            {
                for (int i = 0; i < 4; i++)
                {
                    list.Add(new card(0, 0, 0, 0));
                }
            }
            
            initlist(playerside);
            initlist(enemyside);

            void drawhand(List<card> deck, List<card> hand)
            {
                for (int i = 0; i < 4; i++)
                {
                    int cardindex = rnd.Next(0, deck.Count());
                    hand.Add(new card(deck[cardindex].pos, deck[cardindex].health, deck[cardindex].damage, deck[cardindex].blood));
                    deck.RemoveAt(cardindex);
                }
            }
            drawhand(deck, hand);              


            while (true)
            {
                for (int i = 0; i < cardsonplayerside.Count; i++)
                {
                    playerside[cardsonplayerside[i].pos] = cardsonplayerside[i];
                    if (cardsonplayerside[i].health <= 0)
                    {
                        cardsonplayerside.RemoveAt(i);
                    }
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
                            if (enemyside[i].health > 0) enemyside[i].health -= playerside[i].damage;
                            else if (enemyside[i].health <= 0) scale += playerside[i].damage; enemyside[i] = new card(0, 0, 0, 0);
                            if (enemyside[i].health < 0) enemyside[i].health = 0;

                            if (playerside[i].health > 0) playerside[i].health -= enemyside[i].damage;
                            else if (playerside[i].health <= 0) scale -= enemyside[i].damage; playerside[i] = new card(0, 0, 0, 0);
                            if (playerside[i].health < 0) playerside[i].health = 0;
                        }
                    }
                    
                    
                    
                    if (input.Key == ConsoleKey.D1)
                    {
                        cardsonplayerside.Add(new card(boardselected, hand[handselected].damage, hand[handselected].health, hand[handselected].blood));
                        hand.RemoveAt(handselected);

                        for (int i = 0; i < hand.Count() + 1; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                Console.Write(" ");
                                Console.SetCursorPosition(i, 4 + j);
                            }
                        }
                    }
                    if (input.Key == ConsoleKey.D2)
                    {
                        cardsonenemyside.Add(new card(boardselected, 1, 1, 1));
                    }

                    if (input.Key == ConsoleKey.R)
                    {
                        drawhand(deck, hand);
                        for (int i = 0; i < deck.Count() + 4; i++)
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                Console.Write(" ");
                                Console.SetCursorPosition(i, 6 + j);
                            }
                        }
                    }
                }

                Console.SetCursorPosition(1,1);
                Console.Write(boardselected + 1);
                Console.SetCursorPosition(1, 2);
                Console.Write(scale);
                Console.SetCursorPosition(1, 3);
                Console.Write(bloodtemporary);

                for (int i = 0; i < playerside.Count(); i++)
                {
                    if (i == boardselected) Console.ForegroundColor = ConsoleColor.Blue;
                    else Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(i, 24);
                    Console.Write(playerside[i].health);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(i, 25);
                    Console.Write(playerside[i].damage);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(i, 26);
                    Console.Write(playerside[i].blood);
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.SetCursorPosition(i, 20);
                    Console.Write(enemyside[i].health);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(i, 21);
                    Console.Write(enemyside[i].damage);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(i, 22);
                    Console.Write(enemyside[i].blood);
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
                if (scale <= -5)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("You Lost"); break;
                }
                if (scale >= 5)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("you Win!!!"); break;
                }
            }
        }
    }
}
