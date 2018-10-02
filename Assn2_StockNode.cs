using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_2
{
  public class StockNode
  {
    public Stock StockHolding;
    public StockNode Next;

    // default constructor
    public StockNode()
    {
      StockHolding = null;
      Next = null;
    }

    //constructor to initialize the variables of object
    public StockNode(Stock stockHolding)
    {
      StockHolding = stockHolding;
      Next = null;
    }
  }
}
