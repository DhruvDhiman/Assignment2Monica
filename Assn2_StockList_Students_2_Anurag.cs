using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_2
{
  public partial class StockList
  {
    //param   (StockList)listToMerge : second list to be merged 
    //summary      : merge two different list into a single result list
    //return       : merged list
    //return type  : StockList
    public StockList MergeList(StockList listToMerge)
    {
      StockList resultList = new StockList();

      // write your implementation here
      StockNode node = this.head;
      while(node != null)
		{
			resultList.AddLast(node.StockHolding);
			node = node.Next;
		}
		node = listToMerge.head;
		while(node != null)
		{
			resultList.AddLast(node.StockHolding);
			node = node.Next;
		}
		
      return resultList;
    }

    //param        : NA
    //summary      : finds the stock with most number of holdings
    //return       : stock with most shares
    //return type  : Stock
    public Stock MostShares()
    {
      Stock mostShareStock = null;

      StockNode stock = this.head;
      decimal maxHoldings = 0.0m;
      
      while(stock != null)
      {
      	if(stock.StockHolding.Holdings > maxHoldings)
      	{
      		maxHoldings = stock.StockHolding.Holdings;
      		mostShareStock = stock.StockHolding;
      	}
      	stock = stock.Next;
      }
     
      return mostShareStock;
    }

    //param        : NA
    //summary      : finds the number of nodes present in the list
    //return       : length of list
    //return type  : int
    public int Length()
    {
      int length = 0;

     StockNode sNode = this.head;
     
     while(sNode != null)
     {
     	length++;
     	sNode = sNode.Next;
     }
      return length;
    }
  }
}