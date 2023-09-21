using System.Collections;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using static consoleApp.Program;

static class RandomExtensions
{
    public static void Shuffle<T>(this Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
} //for å shuffle arrays, funker med magi
namespace consoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            playBlackjack();
        }
        public abstract class GeneralUser { public abstract void Contact(); }
        public class MessengerUser : GeneralUser { public override void Contact() { Console.WriteLine("sending messenger message"); } }
        public class SnapchatUser : GeneralUser { public override void Contact() { Console.WriteLine("sending snapchat message"); } }
        public class EmailUser : GeneralUser { public override void Contact() { Console.WriteLine("sending email message"); } }
        class Person
        {
            private String _firstName;
            private String _lastName;

            public String Fullname { get { return (_firstName + " " + _lastName); } }

            public String FirstName { get { return _firstName; } set { _firstName = value; } }
            public String LastName
            {
                get
                {
                    if (_lastName == null) { return "<last name not set>"; }
                    else return _lastName;
                }
                set
                {
                    _lastName = value.Trim();
                }
            }
            public Person(String firstName)
            {
                FirstName = firstName;
            }
            public Person(String firstName, String lastName)
            {
                FirstName = firstName;
                LastName = lastName;
            }

        }
        class Date
        {
            public int year;
            private int month;
            public int Month
            {
                get
                {
                    return month;
                }
                private set
                {
                    if (value < 0 || value > 12)
                    {
                        throw new ArgumentOutOfRangeException(nameof(value),
                        "not valid month");
                    }
                    month = value;
                }
            }
            public String MonthName
            {
                get
                {
                    return Enum.GetName(typeof(Date.monthEnum), month);

                }
            }
            public int day;
            public enum monthEnum
            {
                [Description("january")]
                january,
                february,
                march,
                april,
                may,
                june,
                july,
                august,
                september,
                october,
                november,
                december

            }

        }

        public class Deck
        {
            public Card[] cards = new Card[52];
            public Deck()
            {
                var random = new Random();
                int index = 0;
                for(int i = 0; i <= 3; i++)
                {
                    for (int j = 1; j <= 13; j++)
                    {
                        cards[index] = new Card(i,j);
                        index++;

                    }
                }
                random.Shuffle(cards);
                
            }

            public Card DrawCard()
            {
                Card card = cards[0];
                cards = cards.Skip(1).ToArray();
                return card;
            }
        }
        public class Card
        {
            public enum Suit
            {
                hearts,
                spades,
                diamonds,
                clubs
            }
            public enum Value
            {
                ace = 1,
                two,
                three,
                four,
                five,
                six,
                seven,
                eight,
                nine,
                ten,
                jack,
                queen,
                king
            }
        
            public Suit suit;
            public Value value;

            public Card(int suit, int value) {
                this.suit = (Suit)suit;
                this.value = (Value)value;
            }

            public override string ToString()
            {
                return value + " of " + suit;
            }


        }
        public class Player
        {
            int aces;
            String name;
            List<Card> hand = new List<Card>();
            int score = 0;

            public void AddCard(Card card)
            {
                hand.Add(card);
                CalculateScore();
                Console.WriteLine(name + " draws "+ card.value+ " current score is "+ score);
            }
            public void CalculateScore()
            {
                score = 0;
                aces = 0;
                for (int i = 0; i < hand.Count; i++)
                {
                    if((int)hand[i].value >= 10) { score += 10; }
                    else if ((int)hand[i].value == 1) { score += 11; aces++; }
                    else { score += (int)hand[i].value; }
                }
                while (score > 21 && aces > 0)  { score -= 10; aces -= 1; }

            }
            public override string ToString()
            {
                CalculateScore();
                String s = name+" has the ";
                String temp;
                for (int i = 0; i < hand.Count ; i++) { 
                    temp = hand[i].ToString();
                    s += temp;
                    if (i+1 != hand.Count) {  s += " and "; }
                }
                s += " in his hand";

                s += ". Total is " + score;

                return s;
            }

            public int getScore() {  return score; }
            public String getName() { return name; }

            public Player(String name){ this.name = name; }
        }
        public static void playBlackjack()
        {
            bool playing = true;
            while (playing)
            {
                blackjack();
                Console.WriteLine("press any button to start again or n to end game");
                string input= Console.ReadLine();
                if(input == "y") { }
                else if(input == "n") { playing = false; }
                else { Console.WriteLine("invalid input"); }

            }
        }

        public static void blackjack()
        {
            Deck deck = new Deck();
            Player winner = null;
            bool playing = true;
            bool playerDone = false;
            bool dealerDone = false;
            Player player = new Player("player");
            Player dealer = new Player("dealer");
            player.AddCard(deck.DrawCard());
            player.AddCard(deck.DrawCard());
            dealer.AddCard(deck.DrawCard());
            dealer.AddCard(deck.DrawCard());

            while(playing)
            {
                playerDone = PlayerTurn(player,deck);
                if(player.getScore() > 21) { Console.WriteLine("you busted !!"); playing = false; winner = dealer; endscreen(dealer); return; }
                DealerTurn(dealer, deck);
                while(playerDone == true && dealerDone == false) { dealerDone = DealerTurn(dealer, deck); }
                if (dealer.getScore() > 21) { Console.WriteLine("dealer busted !!"); playing = false; winner = player; endscreen(player); }

                if (playerDone == true && dealerDone == true && winner == null)
                {
                    CalculateWinner(player, dealer);
                    playing = false;
                }
            }

        }
        public static void CalculateWinner(Player player, Player dealer)
        {
            if (player.getScore() > dealer.getScore()) {Console.WriteLine( "you beat the dealer!!!"); }
            if (player.getScore() < dealer.getScore()) {Console.WriteLine( "you lost to the dealer!!!"); }
            if (player.getScore() == dealer.getScore()) {Console.WriteLine( "its a tie!!!"); }
        }
        
        public static bool PlayerTurn(Player player, Deck deck)
        {
            Console.WriteLine("your current score is " + player.getScore() + " do you want to hit or stand (y/n)");
            while (true) {
                String input = Console.ReadLine();
                if (input == "y") { player.AddCard(deck.DrawCard()); return false; }
                else if (input == "n") { return true; }
                else { Console.WriteLine("wrong input"); }
            }

        }
        public static bool DealerTurn(Player dealer, Deck deck)
        {
            if (dealer.getScore() >= 17) { Console.WriteLine("dealer has " + dealer.getScore() + " and stands"); return true; }
            else { dealer.AddCard(deck.DrawCard()); return false; }
        }
        public static void endscreen(Player winner) { Console.WriteLine(winner.getName() + " is the winner"); }

        static void extractEvenNumbersFromArray(int[] numbers)
        {
            ArrayList evenNumbers = new ArrayList();
            for (int i = 0; i < numbers.Length; i++)
            {
                if ((numbers[i] % 2) == 0) { evenNumbers.Add(numbers[i]); }
            }
            for (int i = 0; i < evenNumbers.Count; i++)
            {
                Console.WriteLine(evenNumbers[i]);
            }

        }
        static void categorizePeople()
        {
            Console.WriteLine("enter name of two people");
            String name1 = Console.ReadLine();
            String name2 = Console.ReadLine();
            int age1 = int.Parse(Console.ReadLine());
            int age2 = int.Parse(Console.ReadLine());

            if (age1 < age2)
            {
                Console.WriteLine(name1 + " is younger than " + name2);
            }
            else if (age1 == age2)
            {
                Console.WriteLine(name1 + " is the same age as " + name2);
            }
            else { Console.WriteLine(name1 + "is older than " + name2); }
        }
        static double converter()
        {
            Console.WriteLine("enter 1 if you want to convert inches to centimeters or enter anything else toconvert other way");
            String input = Console.ReadLine();
            Console.WriteLine("enter the length to convert");
            int length = int.Parse(Console.ReadLine());


            if (input == "1")
            {
                return length / 2.54;
            }
            else { return length * 2.54; }

        }
        static void fruitColor()
        {

            Console.WriteLine("enter a color");
            String input = Console.ReadLine();
            if (input == "yellow")
            {
                Console.WriteLine("banana");
            }
            else if (input == "red")
            {
                Console.WriteLine("apple");
            }
            else if (input == "orange")
            {
                Console.WriteLine("orange");
            }
            else { Console.WriteLine("unknown fruit"); }

        }
        static void loop()
        {
            int[] ints = new int[9];
            for (int i = 1; i <= ints.Length; i++)
            {
                ints[i - 1] = i;
            }
            for (int i = ints.Length - 1; i >= 0; i--)
            {
                Console.WriteLine(ints[i]);
            }
        }
        static void dice()
        {
            var random = new Random();
            int number = random.Next(1, 7);
            int total = 0;
            for (int i = 0; i <= 10; i++)
            {
                number = random.Next(1, 7);
                total += number;
                Console.WriteLine(number);
            }
            Console.WriteLine(total);
        }
        static void fibonacci()
        {
            Console.WriteLine("enter how many numbers of fibonacci you want to calculate");
            int input = int.Parse(Console.ReadLine());
            int prev = 0;
            int current = 1;
            int temp = 0;

            for (int i = 0; i <= input - 3; i++)
            {
                temp = current;
                Console.WriteLine(current = prev + current);
                prev = temp;

            }
        }
        static void countWords(string words)
        {
            String[] strings = words.Split(' ');
            Console.WriteLine(strings.Length);
        }
        static void inheritanceDemo()
        {
            List<GeneralUser> users = new();

            GeneralUser generalUser;

            generalUser = new EmailUser();
            users.Add(generalUser);
            generalUser = new MessengerUser();
            users.Add(generalUser);
            generalUser = new SnapchatUser();
            users.Add(generalUser);

            foreach (GeneralUser user in users)
            {
                user.Contact();
            }
        }
        
    }
}