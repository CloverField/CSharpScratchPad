using System;
using System.Linq;
using System.Threading.Tasks;

namespace Linked_List
{
    public class Program
    {
        static Random r = new Random();
        public static void Main(string[] args)
        {
            Console.WriteLine("Testing singly linked list");
            SinglyLinkedList<int> test = new SinglyLinkedList<int>();
            test.InsertAtFront(1);
            test.InsertAtBack(3);
            test.InsertAtFront(2);
            test.InsertAtBack(4);
            Console.WriteLine(test);

            Console.WriteLine("Testing doubly linked list");
            DoublyLinkedList<int> test2 = new DoublyLinkedList<int>();

            test2.InsertAtFront(1);
            test2.InsertAtFront(2);
            test2.InsertAtFront(3);
            test2.InsertAtFront(4);
            test2.InsertAt(2, 5);
            test2.RemoveAt(2);
            Console.WriteLine(test2);

            Console.WriteLine("Testing LinkListStack");
            LinkedListStack<int> test3 = new LinkedListStack<int>();
            test3.Push(1);
            test3.Push(2);
            test3.Push(3);
            test3.Push(4);
            test3.Remove(1);
            Console.WriteLine(test3);

            Console.WriteLine("Testing LinkListQueue");
            LinkedListQueue<int> test4 = new LinkedListQueue<int>();
            test4.Enqueue(1);
            test4.Enqueue(2);
            Console.WriteLine(test4);
            test4.Dequeue();
            test4.Remove(1);
            Console.WriteLine(test4);

            Console.WriteLine("Testing Chaining Hash Table");
            LinkedHashTable<int, string> test5 = new LinkedHashTable<int,string>();
            string toInsert = "test";
            test5.Add(toInsert.GetHashCode(), toInsert);
            toInsert = "test2";
            test5.Add(toInsert.GetHashCode(), toInsert);
            toInsert = "test3";
            test5.Add(toInsert.GetHashCode(), toInsert);
            toInsert = "test";
            test5.Add(toInsert.GetHashCode(), toInsert);
            Console.WriteLine(test5);

            Random randNum = new Random();
            var testarray = Enumerable
                .Repeat(0, 100)
                .Select(i => randNum.Next(0, 100));

            Console.WriteLine("Testing Chaining Hash Table with random numbers.");
            LinkedHashTable<int,int> table = new LinkedHashTable<int,int>();
            foreach (var item in testarray)
            {
                table.Add(item.GetHashCode(), item);
            }
            Console.WriteLine(table);

            Console.WriteLine("Testing Chaining Hash table with dates.");
            LinkedHashTable<int, DateTime> tableDT = new LinkedHashTable<int, DateTime>();
            var testDateTimeArray = Enumerable
                .Repeat(0, 100)
                .Select(i => RandomDayFunc().Invoke());

            foreach (var item in testDateTimeArray)
            {
                tableDT.Add(item.GetHashCode(), item);
            }
            Console.WriteLine(tableDT);
            
            Console.ReadLine();
        }

        static Func<DateTime> RandomDayFunc()
        {
            DateTime start = new DateTime(1995, 1, 1);
            //Random gen = new Random();
            int range = (DateTime.Today - start).Days;
            return () => start.AddDays(r.Next(range));
        }
    }
}
