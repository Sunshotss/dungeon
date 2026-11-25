namespace dungeons
{
    class card
    {
        public int pos;
        public int health;
        public int damage;
        public int blood;
        public bool dead = false;
        public card(int pos, int health, int damage, int blood)
        {
            this.pos = pos;
            this.health = health;
            this.damage = damage;
            this.blood = blood;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Random rnd = new Random();
            int boardselected = 1;
            int handselected = 1;
            int scale = 0;
            int bloodtemporary = 0;

            List<int> bloodtemporarycards = new();


            List<card> cardsonplayerside = new();
            List<card> cardsonenemyside = new();
            List<card> playerside = new();
            List<card> enemyside = new();

            List<card> deck = new();
            deck.Add(new card(0, 1, 8, 0));
            deck.Add(new card(0, 2, 7, 0));
            deck.Add(new card(0, 3, 6, 0));
            deck.Add(new card(0, 4, 5, 0));
            deck.Add(new card(0, 5, 4, 1));
            deck.Add(new card(0, 6, 3, 2));
            deck.Add(new card(0, 7, 2, 3));
            deck.Add(new card(0, 8, 1, 4));
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
                if (handselected >= hand.Count()) handselected--;

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
                            if (!enemyside[i].dead)enemyside[i].health -= playerside[i].damage;
                            if (enemyside[i].health <= 0) enemyside[i].health = 0;
                            if (enemyside[i].dead) scale += playerside[i].damage; enemyside[i] = new card(0, 0, 0, 0);

                            playerside[i].health -= enemyside[i].damage;
                            if (playerside[i].health <= 0) scale -= enemyside[i].damage; playerside[i] = new card(0, 0, 0, 0);


                            if (enemyside[i].health <= 0) enemyside[i].dead = true;
                        }
                    
                    }
                    
                    
                    //places card from hand
                    if (input.Key == ConsoleKey.D1)
                    {
                        if (hand[handselected].blood <= bloodtemporary)
                        {
                            cardsonplayerside.Add(new card(boardselected, hand[handselected].damage, hand[handselected].health, hand[handselected].blood));
                            hand.RemoveAt(handselected);
                            for (int i = 0; i < bloodtemporarycards.Count(); i++)
                            {
                                cardsonplayerside.RemoveAt(bloodtemporarycards[i]);
                                playerside.RemoveAt(bloodtemporarycards[i]);
                                //balances playerside out (I CANT CODE)
                                playerside.Add(new card(0, 0, 0, 0));
                                for (int j = 0; j < playerside.Count(); j++)
                                {
                                    Console.SetCursorPosition(j * 3 + 1, 25);
                                    Console.Write(" ");
                                    Console.SetCursorPosition(j * 3, 25);
                                    Console.Write(" ");
                                    Console.SetCursorPosition(j * 3 + 1, 24);
                                    Console.Write(" ");
                                }
                            }
                            bloodtemporarycards.Clear();
                            for (int i = 0; i < hand.Count() * 4 + 1; i++)
                            {
                                for (int j = 0; j < 2; j++)
                                {
                                    Console.Write(" ");
                                    Console.SetCursorPosition(i, 3 + j);
                                }
                            }
                        }
                        else
                        {
                            int color = rnd.Next(0,11);
                            if (color == 1) Console.ForegroundColor = ConsoleColor.Red;
                            else if (color == 2) Console.ForegroundColor = ConsoleColor.Blue;
                            else if (color == 3) Console.ForegroundColor = ConsoleColor.Yellow;
                            else if (color == 4) Console.ForegroundColor = ConsoleColor.Cyan;
                            else if (color == 5) Console.ForegroundColor = ConsoleColor.DarkBlue;
                            else if (color == 6) Console.ForegroundColor = ConsoleColor.DarkRed;
                            else if (color == 7) Console.ForegroundColor = ConsoleColor.Green;
                            else if (color == 8) Console.ForegroundColor = ConsoleColor.DarkGreen;
                            else if (color == 9) Console.ForegroundColor = ConsoleColor.Magenta;
                            else if (color == 10) Console.ForegroundColor = ConsoleColor.Gray;
                            Console.SetCursorPosition(10,2);
                            Console.Write("Need more blood!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    //places card from hand
                    //places enemy 1:1:1 (devtool)
                    if (input.Key == ConsoleKey.D2)
                    {
                        cardsonenemyside.Add(new card(boardselected, 1, 1, 1));
                    }
                    //places enemy 1:1:1 (devtool)
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

                    if (input.Key == ConsoleKey.Z)
                    {
                        bloodtemporary += 1;
                        bloodtemporarycards.Add(playerside[boardselected].pos);
                    }
                }
                Console.SetCursorPosition(1, 1);
                Console.Write(scale);
                Console.SetCursorPosition(1, 2);
                Console.Write(bloodtemporary);

                for (int i = 0; i < playerside.Count(); i ++)
                {
                    if (i == boardselected) Console.ForegroundColor = ConsoleColor.Blue;
                    else Console.ForegroundColor = ConsoleColor.DarkGreen;

                    Console.SetCursorPosition(i * 3 + 1, 25);
                    Console.Write(playerside[i].health);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(i * 3, 25);
                    Console.Write(playerside[i].damage);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.SetCursorPosition(i * 3 + 1, 24);
                    Console.Write(playerside[i].blood);
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.SetCursorPosition(i * 3 + 1, 21);
                    Console.Write(enemyside[i].health);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(i * 3, 21);
                    Console.Write(enemyside[i].damage);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.SetCursorPosition(i * 3 + 1, 20);
                    Console.Write(enemyside[i].blood);
                    Console.ForegroundColor = ConsoleColor.White;

                }

                for (int i = 0; i < hand.Count; i++)
                {
                    if (i == handselected) Console.ForegroundColor = ConsoleColor.Blue;
                    else Console.ForegroundColor = ConsoleColor.DarkGreen;

                    Console.SetCursorPosition(i * 3 + 1, 4);
                    Console.Write(hand[i].health);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(i * 3, 4);
                    Console.Write(hand[i].damage);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.SetCursorPosition(i * 3 + 1, 3);
                    Console.Write(hand[i].blood);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.SetCursorPosition(0, 6);
                Console.WriteLine(deck.Count());
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
