using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_2
{
    public partial class StockList
    {
        private StockNode head;

        //Constructor for initialization
        public StockList()
        {
            this.head = null;
        }

        //param        : NA
        //summary      : checks if the list is empty
        //return       : true if list is empty, false otherwise
        //return type  : bool
        public bool IsEmpty()
        {
            if (this.head == null)
            {
                return true;
            }
            return false;
        }

        //param (Stock)stock : stock that is to be added
        //summary      : Add node at first position in list
        //                This is done by creating a new node 
        //                  and pointing it to the current list 
        //return       : NA
        //return type  : NA
        public void AddFirst(Stock stock)
        {
            StockNode nodeToAdd = new StockNode(stock);
            nodeToAdd.Next = head;
            head = nodeToAdd;
        }

        //param (Stock)stock : stock that is to be added
        //summary      : Add mode at last position of list
        //  This is done by traversing the list till we reach the end
        // and pointing the last node to the new node
        // return       :
        // return type  :
        public void AddLast(Stock stock)
        {
            // for an empty list, we add the node at the top of the list
            if (this.IsEmpty())
                AddFirst(stock);
            else
            {
                // traverse the list till the end
                StockNode current = this.head;
                while (current.Next != null)
                    current = current.Next;

                // point the last node to the new node
                StockNode nodeToAdd = new StockNode(stock);
                current.Next = nodeToAdd;
            }
        }

        /// <summary>
        /// Add node in an alphabetically sorted manner, if stock is already present then set holdings to sum of existing and new stock
        ///   We assume that the list is always sorted in alphabetical order
        ///   The stock may be added either at:
        ///     the top of the list (if alphabetically lower than all nodes)
        ///   , middle of the list, in which case, we can either
        ///     Add to existing holdings (if the stock already exists in the list), or
        ///     Insert it at the right location in alphatecial order (if it does not already exist)
        ///   , or end of the list (if alphabetically greater than all existing nodes)
        /// </summary>
        /// <param name="stock">stock that is to be added</param>
        public void AddStock(Stock stock)
        {
            // for an empty list, we add the node at the top of the list
            if (this.IsEmpty())
                AddFirst(stock);
            else
            {
                // if the new node is alphabetically the first, again, we add it at the top of the list
                string nameOfStockToAdd = stock.Name;
                string headNodeData = (this.head.StockHolding).Name;
                if (headNodeData.CompareTo(nameOfStockToAdd) > 0)
                    AddFirst(stock);
                else
                {
                    // traverse the list to locate the stock
                    StockNode current = this.head;
                    StockNode previous = null;
                    string currentStockName = (current.StockHolding).Name;

                    while (current.Next != null && currentStockName.CompareTo(nameOfStockToAdd) < 0)
                    {
                        previous = current;
                        current = current.Next;
                        currentStockName = (current.StockHolding).Name;
                    }

                    // we have now traversed all stocks that are alphabetically less than the stock to be added
                    if (current.Next != null)
                    {
                        // if the stock already exists, add to holdings
                        if (currentStockName.CompareTo(nameOfStockToAdd) == 0)
                        {
                            decimal holdings = (current.StockHolding).Holdings + stock.Holdings;
                            current.StockHolding.Holdings = holdings;
                        }
                        else if (currentStockName.CompareTo(nameOfStockToAdd) > 0)
                        {
                            // insert the stock in the current position. This requires creating a new node,
                            //  pointing the new node to the next node
                            //    and pointing the previous node to the current node
                            //  QUESTION: what would happen if we flipped the sequence of assignments below?
                            StockNode newNode = new StockNode(stock);
                            newNode.Next = current;
                            previous.Next = newNode;
                        }
                    }
                    else
                    {
                        // we are at the end of the list, add the stock at the end
                        //  This is probably not the most efficient way to do it,
                        //  since AddLast traverses the list all over again
                        AddLast(stock);
                    }
                }
            }
        }

        //param  (Stock)stock : stock that is to be checked 
        //summary      : checks if list contains stock passed as parameter
        //                  This involves traversing the list until we find the stock
        //                    return null if we don't
        //return       : Reference of node with matching stock
        //return type  : StockNode if exists, null if not
        public StockNode Contains(Stock stock)
        {
            StockNode nodeReference = null;

            // if the list is empty, return null
            if (this.IsEmpty())
                return nodeReference;
            else
            {
                // traverse the list until we locate the stock,
                //  or, reach the end of the list
                StockNode current = this.head;
                StockNode previous = this.head;
                while (current.Next != null)
                {
                    Stock currentStock = current.StockHolding;

                    // found it! Return the node
                    if (currentStock.Equals(stock))
                    {
                        nodeReference = previous;
                        break;
                    }

                    // else, continue traversing
                    previous = current;
                    current = current.Next;
                }
            }

            return nodeReference;
        }

        /// <summary>
        /// swaps the node passed as argument with next node in list
        /// Sorting the list using the simple bubble sort algorithm requires repeatdely traversing
        ///   the list and pushing a node down the list until it falls in place
        ///     Pushing the node is essentially a swap operation, where we take the next node
        ///       and put it in the current position and move the current node to the next position on the list
        /// </summary>
        /// <param name="nodeOne">first node to be swapped</param>
        /// <returns>Reference to current node</returns>
        public StockNode Swap(Stock nodeOne)
        {
            StockNode prevNodeOne = null;
            StockNode currNodeOne = this.head;

            // traverse the list until we reach the node to swap
            while (currNodeOne != null && currNodeOne.StockHolding != nodeOne)
            {
                prevNodeOne = currNodeOne;
                currNodeOne = currNodeOne.Next;
            }

            // maintain references to the nodes to be swapped
            StockNode prevNodeTwo = currNodeOne;
            StockNode currNodeTwo = currNodeOne.Next;

            // handle corner cases, maybe we have reached the end of the list
            if (currNodeOne == null || currNodeTwo == null)
                return null;

            // perhaps the insertion is at the top of the list
            if (prevNodeOne != null)
                prevNodeOne.Next = currNodeTwo;
            else
                this.head = currNodeTwo;

            if (prevNodeTwo != null)
                prevNodeTwo.Next = currNodeOne;
            else
                this.head = currNodeOne;

            // normal case, swap nodes
            StockNode temp = currNodeOne.Next;
            currNodeOne.Next = currNodeTwo.Next;
            currNodeTwo.Next = temp;

            return currNodeTwo;
        }


        // FOR STUDENTS

        //param        : NA
        //summary      : Sort the list by descending number of holdings
        //return       : NA
        //return type  : NA
        public void SortByValue()
        {
            //Write your implementation here
            //We created local variables and used the method of bubblesorting to sort the local in descending order

            StockNode temp = this.head;
            int StockLen = 0;

            //The below function traverses the list to find the total number of stocks, stored as StockLen
            while (temp.Next != null)

            {
                StockLen++;
                temp = temp.Next;
            }
            //Console.WriteLine(StockLen);
            decimal[] holdings = new decimal[StockLen+1];
            decimal[] currentPrice = new decimal[StockLen+1];
            string[] symbol = new string[StockLen+1];
            string[] name = new string[StockLen+1];

            //This fucntion stores the incremental local variables
            int myctr = StockLen+1;
            temp = this.head;
            int ctr = 0;
            while (myctr != 0)

            {
                holdings[ctr] = temp.StockHolding.Holdings;
               
                currentPrice[ctr] = temp.StockHolding.CurrentPrice;
                symbol[ctr] = String.Copy(temp.StockHolding.Symbol);
                name[ctr] = String.Copy(temp.StockHolding.Name);
                //Console.WriteLine(name[ctr]);
                temp = temp.Next;
              
                ctr++;
               // Console.WriteLine(myctr);
                myctr--;
            }

            //name[ctr] = "aaaaa";

            //Console.WriteLine(name[ctr]);




            //Now we build comparision logic with nested loops

            for (int i = 0; i < name.Length; i++)
            {
                for (int j = 0; j < name.Length - 1; j++)
                {
                    if (holdings[j] < holdings[j + 1])

                    {
                        string nametemp = String.Copy(name[j+1]);
                        name[j+1] = String.Copy(name[j]);
                        name[j] = String.Copy(nametemp);

                        string symboltemp = String.Copy(symbol[j+1]);
                        symbol[j+1] = String.Copy(symbol[j]);
                        symbol[j] = String.Copy(symboltemp);

                        decimal holdingtemp = holdings[j+1];
                        holdings[j+1] = holdings[j];
                        holdings[j] = holdingtemp;

                        decimal currentprice = currentPrice[j+1];
                        currentPrice[j+1] = currentPrice[j];
                        currentPrice[j] = currentprice;
                    }

                }

            }
            
            /*myctr = StockLen + 1;
            while(myctr!=0)
            { Console.WriteLine(holdings[myctr - 1]);
                myctr--;
            }
            */
            //The existing list will be now updated with sorted array values
            temp = this.head;
            // ctr = 0;
            //while (temp.Next != null)
            myctr = StockLen + 1;
            for (int i = 0; i < myctr; i++)
            {

                temp.StockHolding.Holdings = holdings[i];
                temp.StockHolding.CurrentPrice = currentPrice[i];
                temp.StockHolding.Name = String.Copy(name[i]);
                temp.StockHolding.Symbol = String.Copy(symbol[i]);
                temp = temp.Next;
                //ctr++;
            }
        }
        //param        : NA
        //summary      : Sort the list alphabatically
        //return       : NA
        //return type  : NA
        public void SortByName()
        {
            // write your implementation here
            StockNode temp = this.head;
            int StockLen = 0;
            while (temp.Next != null)

            //Travering the list to find the total number of stocks as StockLen
            {
                StockLen++;
                temp = temp.Next;
            }

            //Creating local variables for run-time data

            decimal[] holdings = new decimal[StockLen+1];
            decimal[] currentPrice = new decimal[StockLen+1];
            string[] symbol = new string[StockLen+1];
            string[] name = new string[StockLen+1];
            temp = this.head;
            int myctr = StockLen + 1;
            int ctr = 0;
            while (myctr!=0)
            {
                //Populating the incremental local variables

                holdings[ctr] = temp.StockHolding.Holdings;
                currentPrice[ctr] = temp.StockHolding.CurrentPrice;
                symbol[ctr] = String.Copy(temp.StockHolding.Symbol);
                name[ctr] = String.Copy(temp.StockHolding.Name);
                temp = temp.Next;
                ctr++;
                myctr--;
            }

            //Creating comparision logic with nested loops using CompareTo

            for (int i = 0; i < name.Length; i++)
            {
                for (int j = 0; j < name.Length - 1; j++)
                {
                    if (name[j].CompareTo(name[j + 1]) > 0)
                    {

                        string nametemp = String.Copy(name[j+1]);
                        name[j+1] = String.Copy(name[j]);
                        name[j] = String.Copy(nametemp);

                        string symboltemp = String.Copy(symbol[j+1]);
                        symbol[j+1] = String.Copy(symbol[j]);
                        symbol[j] = String.Copy(symboltemp);

                        decimal holdingtemp = holdings[j+1];
                        holdings[j+1] = holdings[j];
                        holdings[j] = holdingtemp;

                        decimal currentprice = currentPrice[j+1];
                        currentPrice[j+1] = currentPrice[j];
                        currentPrice[j] = currentprice;
                    }
                }
            }

            //The existing list will be now updated with sorted array values
            myctr = StockLen + 1;
            /*while (myctr != 0)
            {
                Console.WriteLine(name[myctr - 1]);
                myctr--;
            }*/
           

           temp = this.head;
           // ctr = 0;
            //while (temp.Next != null)
            myctr = StockLen + 1;
            for(int i=0;i<myctr;i++)
            {

                temp.StockHolding.Holdings = holdings[i];
                temp.StockHolding.CurrentPrice = currentPrice[i];
                temp.StockHolding.Name = String.Copy(name[i]);
                temp.StockHolding.Symbol = String.Copy(symbol[i]);
                temp = temp.Next;
                //ctr++;
            }
        }

        public StockList MergeList(StockList listToMerge)
        {
            StockList resultList = new StockList();

            // write your implementation here
            StockNode node = this.head;
            while (node != null)
            {
                resultList.AddLast(node.StockHolding);
                node = node.Next;
            }
            node = listToMerge.head;
            while (node != null)
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

            while (stock != null)
            {
                if (stock.StockHolding.Holdings > maxHoldings)
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

            while (sNode != null)
            {
                length++;
                sNode = sNode.Next;
            }
            return length;
        }

        public decimal Value()
        {
            decimal value = 0.0m;

            // write your implementation here

            StockNode node = this.head;
            while (node != null)
            {

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

            while (node != null)
            {
                while (cNode != null)
                {
                    if (node.StockHolding.Symbol == cNode.StockHolding.Symbol)
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
            while (node != null)
            {
                Console.WriteLine(node.StockHolding.ToString());
                node = node.Next;
            }

        }
    }
}
