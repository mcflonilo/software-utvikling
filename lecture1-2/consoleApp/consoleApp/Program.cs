﻿using System.Collections;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
static class arrayShuffler
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

    #region bases
    public abstract class Pizza
    {
        public string Name { get; set; } = "";
        protected double _price;
        protected string _description = "";
        public abstract double GetPrice();
        public abstract string GetDescription();
    }
    public abstract class PizzaDecorator : Pizza
    {
        private Pizza _pizza;

        protected PizzaDecorator(Pizza pizza)
        {
            _pizza = pizza;
        }

        public override string GetDescription()
        {
            return _pizza.GetDescription();
        }

        public override double GetPrice()
        {
            return _pizza.GetPrice();
        }
    }
    #endregion

    #region concreteComponents
    public class PlainPizza : Pizza
    {
        public PlainPizza(double price)
        {
            Name = "Margherita";
            _description = "Tomato sauce, mozzarella, oregano";
            _price = price;
        }
        // This method returns the price of the pizza object with all toppings.
        public override double GetPrice()
        {
            return _price;
        }
        // This method returns the description of the pizza object with all toppings.
        public override string GetDescription()
        {
            return _description;
        }
    }
    public class MeatLover : Pizza
    {
        public MeatLover(double price)
        {
            Name = "MeatLover";
            _description = "Tomato sauce, mozzarella, bacon, ham, pepperoni";
            _price = price;
        }
        // This method returns the price of the pizza object with all toppings.
        public override double GetPrice()
        {
            return _price;
        }
        // This method returns the description of the pizza object with all toppings.
        public override string GetDescription()
        {
            return _description;
        }
    }
    #endregion

    #region ConcreteDecorators

    class extraHam : PizzaDecorator
    {
        public extraHam(Pizza original) : base(original)
        {

        }

        public override string GetDescription()
        {
            return base.GetDescription()+ " + extra ham";
        }
        public override double GetPrice()
        {
            return base.GetPrice()+3;
        }
    }
    class extraPepperoni : PizzaDecorator
    {
        public extraPepperoni(Pizza original) : base(original)
        {

        }

        public override string GetDescription()
        {
            return base.GetDescription() + " + extra pepperoni";
        }
        public override double GetPrice()
        {
            return base.GetPrice() + 3;
        }
    }


    #endregion

    public class Program
    {
        static void Main(string[] args)
        {
            MeatLover meatlover = new MeatLover(10);
            Console.WriteLine(meatlover.GetDescription());
            extraHam meatloverExtra = new extraHam(meatlover);
            Console.WriteLine(meatloverExtra.GetDescription());



            UserHandler userHandler = new(new JSONRepository()) ;
            userHandler.PrintAllUsers();
        }


        public interface IRepository
        {
            List<User> GetUsers();
        }
        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; }
            public string Password { get; set; }
            public User(int id, String name)
            {
                Id = id;
                Name = name;
            }

        }
        public class UserHandler
        {
            private IRepository _repository;
            public UserHandler(IRepository repository)
            {
                _repository = repository;
            }
            public void PrintAllUsers()
            {
                List<User> users = _repository.GetUsers();
                foreach (User user in users)
                {
                    Console.WriteLine(user.Name);
                }
            }
        }
        public class MySQLRepository : IRepository
        {
            public List<User> GetUsers()
            {
                List<User> users = new();
                users.Add(new User(1, "sydgfy"));
                users.Add(new User(2, "efe"));
                return users;
            }
        }
        public class JSONRepository : IRepository
        {
            public List<User> GetUsers()
            {
                List<User> users = new();
                users.Add(new User(1, "sydgfy"));
                users.Add(new User(2, "efe"));
                return users;
            }
        }

        #region fraction and operator overloads
        public class Fraction
        {
            public int Numerator { get; set; }
            public int Denominator { get; set; }

            public Fraction(int numerator = 0, int denominator = 1)
            {
                if (denominator == 0) {throw new ArgumentOutOfRangeException(); }
                Numerator = numerator;

                Denominator = denominator;

                void reduce()
                {
                    static int __gcd(int a, int b)
                    {
                        if (b == 0) { return a; }
                        return __gcd(b, a % b);
                    }

                    int d;

                    d = __gcd(this.Numerator, this.Denominator);
                    this.Numerator = this.Numerator / d;
                    this.Denominator = this.Denominator / d;

                }
                reduce();
            }
            public override string ToString()
            {
                return $"{Numerator}/{Denominator}";
            }
            public double ToDouble()
            {
                return ((double) this.Numerator / this.Denominator);
            }

            #region operators

            public static Fraction operator +(Fraction a, Fraction b)
            {
                int newNumerator = a.Numerator * b.Denominator + a.Denominator * b.Numerator;
                int newDenominator = a.Denominator * b.Denominator;
                return new Fraction(newNumerator, newDenominator);
            }
            public override bool Equals(object obj)
            {
                if ((obj == null) || ! this.GetType().Equals(obj.GetType())) return false;
                else
                {
                    return this == (Fraction) obj;
                }
            }
            public static Fraction operator *(Fraction a, Fraction b)
            {
                int newNumerator = a.Numerator * b.Numerator;
                int newDenominator = a.Denominator * b.Denominator;
                return new Fraction(newNumerator, newDenominator);
            }
            public static Fraction operator /(Fraction a, Fraction b)
            {
                int newNumerator = a.Numerator * b.Denominator;
                int newDenominator = a.Denominator * b.Numerator;
                return new Fraction(newNumerator, newDenominator);
            }
            public static Fraction operator -(Fraction a, Fraction b)
            {
                int newNumerator = a.Numerator * b.Denominator - a.Denominator * b.Numerator;
                int newDenominator = a.Denominator * b.Denominator;
                return new Fraction(newNumerator, newDenominator);
            }
            public static bool operator ==(Fraction a, Fraction b)
            {
                if (a.Denominator == b.Denominator && a.Numerator == b.Numerator) { return true; }
                else return false;
            }
            public static bool operator !=(Fraction a, Fraction b)
            {
                if (a.Denominator == b.Denominator && a.Numerator == b.Numerator) { return !true; }
                else return !false;
            }
            public static bool operator <(Fraction a, Fraction b)
            {
                int aNum = a.Numerator * b.Denominator;
                int bNum = b.Numerator * a.Denominator;
                if (aNum < bNum) { return true; }
                else return false;
            }
            public static bool operator >(Fraction a, Fraction b)
            {
                int aNum = a.Numerator * b.Denominator;
                int bNum = b.Numerator * a.Denominator;
                if (aNum>bNum) { return true; }
                else return false;
            }

            public static implicit operator double(Fraction v)
            {
                return (double) v.Numerator/v.Denominator;
            }
            #endregion
        }
        #endregion

        #region inheritance example
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
            #endregion

        #region blackjack
            public class Deck
            {
                public Deck()
                {
                    var random = new Random();
                    int index = 0;
                    for (int i = 0; i <= 3; i++)
                    {
                        for (int j = 1; j <= 13; j++)
                        {
                            cards[index] = new Card(i, j);
                            index++;

                        }
                    }
                    random.Shuffle(cards);

                }
                public Card[] cards = new Card[52];
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

                public Card(int suit, int value)
                {
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
                    Console.WriteLine(name + " draws " + card.value + " current score is " + score);
                }
                public void CalculateScore()
                {
                    score = 0;
                    aces = 0;
                    for (int i = 0; i < hand.Count; i++)
                    {
                        if ((int)hand[i].value >= 10) { score += 10; }
                        else if ((int)hand[i].value == 1) { score += 11; aces++; }
                        else { score += (int)hand[i].value; }
                    }
                    while (score > 21 && aces > 0) { score -= 10; aces -= 1; }

                }
                public override string ToString()
                {
                    CalculateScore();
                    String s = name + " has the ";
                    String temp;
                    for (int i = 0; i < hand.Count; i++)
                    {
                        temp = hand[i].ToString();
                        s += temp;
                        if (i + 1 != hand.Count) { s += " and "; }
                    }
                    s += " in his hand";

                    s += ". Total is " + score;

                    return s;
                }

                public int getScore() { return score; }
                public String getName() { return name; }

                public Player(String name) { this.name = name; }
            }
            public static void playBlackjack()
            {
                bool playing = true;
                while (playing)
                {
                    blackjack();
                    Console.WriteLine("press any button to start again or n to end game");
                    string input = Console.ReadLine();
                    if (input == "y") { Console.Clear(); }
                    else if (input == "n") { playing = false; }
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

                while (playing)
                {
                    playerDone = PlayerTurn(player, deck);
                    if (player.getScore() > 21) { Console.WriteLine("you busted !!"); playing = false; winner = dealer; endscreen(dealer); return; }
                    DealerTurn(dealer, deck);
                    while (playerDone == true && dealerDone == false) { dealerDone = DealerTurn(dealer, deck); }
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
                if (player.getScore() > dealer.getScore()) { Console.WriteLine("you beat the dealer!!!"); }
                if (player.getScore() < dealer.getScore()) { Console.WriteLine("you lost to the dealer!!!"); }
                if (player.getScore() == dealer.getScore()) { Console.WriteLine("its a tie!!!"); }
            }

            public static bool PlayerTurn(Player player, Deck deck)
            {
                Console.WriteLine("your current score is " + player.getScore() + " do you want to hit or stand (y/n)");
                while (true)
                {
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
            #endregion blackjack

        #region basic tasks
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
            #endregion

        }
    }
