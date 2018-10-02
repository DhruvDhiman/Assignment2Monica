namespace Assignment_2
{
  public class Stock
  {
    public string Symbol { get; set; }
    public string Name { get; set; }
    public decimal Holdings { get; set; }
    public decimal CurrentPrice { get; set; }

    // default constructor
    public Stock()
    {
      Symbol = "NA";
      Name = "Invalid";
      Holdings = 0;
      CurrentPrice = -99;
    }

    // Constructor for initialization
    public Stock(string symbol, string name, decimal holdings, decimal currentPrice)
    {
      Symbol = symbol;
      Name = name;
      Holdings = holdings;
      CurrentPrice = currentPrice;
    }

    // overridden ToString method to customize the return value
    public override string ToString()
    {
      return Symbol + ", " + Name + ", " + Holdings + ", " + CurrentPrice;
    }
  }
}