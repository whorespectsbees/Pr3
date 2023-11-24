using System;
using System.Collections.Generic;
using System.Linq;

class Color
{
    private int yellow;
    private int blue;
    private int orange;

    public int Yellow
    {
        get { return yellow; }
        set { yellow = NormalizeColorValue(value); }
    }

    public int Blue
    {
        get { return blue; }
        set { blue = NormalizeColorValue(value); }
    }

    public int Orange
    {
        get { return orange; }
        set { orange = NormalizeColorValue(value); }
    }

    public Color(int yellow, int blue, int orange)
    {
        Yellow = yellow;
        Blue = blue;
        Orange = orange;
    }

    public void DisplayColor()
    {
        Console.WriteLine($"Yellow: {Yellow}, Blue: {Blue}, Orange: {Orange}");
    }

    private int NormalizeColorValue(int colorValue)
    {
        if (colorValue > 255)
            return 255;
        if (colorValue < 0)
            return 0;
        return colorValue;
    }
}

class SmsMessage
{
    private string messageText;
    private double price;

    public string MessageText
    {
        get { return messageText; }
        set
        {
            messageText = value.Length > MaxLength ? value.Substring(0, MaxLength) : value;
            CalculatePrice();
        }
    }

    public double Price { get; private set; }

    public int MaxLength { get; set; }
    public double InitialPrice { get; set; }
    public double PricePerExtraCharacter { get; set; }

    public SmsMessage(string messageText, int maxLength, double initialPrice, double pricePerExtraCharacter)
    {
        MessageText = messageText;
        MaxLength = maxLength;
        InitialPrice = initialPrice;
        PricePerExtraCharacter = pricePerExtraCharacter;
    }

    private void CalculatePrice()
    {
        int messageLength = MessageText.Length;
        double standardPrice = InitialPrice + (messageLength <= MaxLength ? 0 : (messageLength - MaxLength) * PricePerExtraCharacter);
        Price = standardPrice > 0 ? standardPrice : 0;
    }
}

class SQLCommand
{
    private string commandText;

    public string CommandText
    {
        get { return commandText; }
        set { commandText = value.ToUpper(); }
    }

    public SQLCommand(string commandText)
    {
        CommandText = commandText;
    }
}

class MyIntList
{
    private List<int> numbersList = new List<int>();

    public int MaxLength { get; set; }
    public double InitialPrice { get; set; }
    public double PricePerExtraCharacter { get; set; }

    public MyIntList(int maxLength, double initialPrice, double pricePerExtraCharacter)
    {
        MaxLength = maxLength;
        InitialPrice = initialPrice;
        PricePerExtraCharacter = pricePerExtraCharacter;
    }

    public void AddNumber(int number)
    {
        if (numbersList.Count < MaxLength)
            numbersList.Add(number);
    }

    public void RemoveNumber(int number)
    {
        numbersList.Remove(number);
    }

    private double CalculateAverage()
    {
        int sum = numbersList.Sum();
        return numbersList.Count > 0 ? sum / (double)numbersList.Count : 0;
    }

    public double Average => CalculateAverage();
}

class RandomNumberGenerator
{
    private List<int> randomNumbers;
    private bool isCached;

    public int SequenceLength { get; private set; }

    public RandomNumberGenerator(int sequenceLength)
    {
        SequenceLength = sequenceLength;
        GenerateRandomNumbers();
        isCached = false;
    }

    public List<int> RandomNumbers
    {
        get
        {
            if (!isCached)
            {
                CalculateStatistics();
            }
            return randomNumbers;
        }
    }

    public double Average
    {
        get
        {
            if (!isCached)
            {
                CalculateStatistics();
            }
            return randomNumbers.Average();
        }
    }

    public double Variance
    {
        get
        {
            if (!isCached)
            {
                CalculateStatistics();
            }
            return randomNumbers.Average(num => Math.Pow(num - Average, 2));
        }
    }

    public double StandardDeviation => Math.Sqrt(Variance);

    public double Median
    {
        get
        {
            if (!isCached)
            {
                CalculateStatistics();
            }
            int mid = randomNumbers.Count / 2;
            return (randomNumbers.Count % 2 != 0) ? randomNumbers[mid] : (randomNumbers[mid - 1] + randomNumbers[mid]) / 2.0;
        }
    }

    private void GenerateRandomNumbers()
    {
        randomNumbers = new List<int>();
        Random random = new Random();
        for (int i = 0; i < SequenceLength; i++)
        {
            randomNumbers.Add(random.Next(1, 101)); // generate random numbers from 1 to 100
        }
    }

    private void CalculateStatistics()
    {
        isCached = true;
    }
}

class Program
{
    static void Main()
    {
        Color color1 = new Color(255, 0, 0);
        color1.DisplayColor();

        Color color2 = new Color(0, 0, 255);
        color2.DisplayColor();

        Color color3 = new Color(255, 165, 0);
        color3.DisplayColor();

        SmsMessage sms = new SmsMessage("This is a long message that exceeds the maximum length.", 160, 1.5, 0.5);
        Console.WriteLine($"Message Text: {sms.MessageText}");
        Console.WriteLine($"Price: {sms.Price:C}");

        SQLCommand sqlCommand = new SQLCommand("select * from users");
        Console.WriteLine($"SQL Command Text: {sqlCommand.CommandText}");

        MyIntList intList = new MyIntList(5, 1.5, 0.5);
        intList.AddNumber(1);
        intList.AddNumber(2);
        intList.AddNumber(3);
        intList.AddNumber(4);
        intList.AddNumber(5);

        Console.WriteLine($"Average: {intList.Average}");

        RandomNumberGenerator generator = new RandomNumberGenerator(10);
        Console.WriteLine($"Random Numbers: {string.Join(", ", generator.RandomNumbers)}");
        Console.WriteLine($"Average: {generator.Average}");
        Console.WriteLine($"Variance: {generator.Variance}");
        Console.WriteLine($"Standard Deviation: {generator.StandardDeviation}");
        Console.WriteLine($"Median: {generator.Median}");
    }
}
