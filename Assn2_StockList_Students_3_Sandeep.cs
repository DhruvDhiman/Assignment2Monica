using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_2
{
  public partial class StockList
  {
    //param        : NA
    //summary      : Calculate the value of each node by multiplying holdings with price, and returns the total value
    //return       : total value
    //return type  : decimal
    public decimal Value()
    {
      decimal value = 0.0m;

      // write your implementation here
      
      StockNode node = this.head;
      while(node != null){
      	
      	value += node.StockHolding.Holdings * node.StockHolding.CurrentPrice;
      	node = node.Next;
      }
      return value;
    }

    //param  (StockList) listToCompare     : StockList which has to comared for similarity index
    //summary      : finds the similar number of nodes between two lists
    //return       : similarty index
    //return type  : int
    public int Similarity(StockList listToCompare)
    {
      int similarityIndex = 0;

      // write your implementation here
      StockNode node = this.head;
      StockNode cNode = listToCompare.head;
      
      while(node != null)
		{
			while(cNode != null)
			{
				if(node.StockHolding.Symbol == cNode.StockHolding.Symbol)
				{
					similarityIndex++;
				}
				cNode = cNode.Next;
			}
			node = node.Next;
		}
      return similarityIndex;
    }

    //param        : NA
    //summary      : Print all the nodes present in the list
    //return       : NA
    //return type  : NA
    public void Print()
    {
      // write your implementation here
      
      StockNode node = this.head;
      while(node != null)
      {
      	Console.WriteLine(node.StockHolding.ToString());
      	node = node.Next;
      }

    }
  }
}