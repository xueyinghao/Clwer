using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            CSeqQueue<int> queue = new CSeqQueue<int>(5);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            Console.WriteLine(queue);//front = -1       rear = 3        count = 4       data = 1,2,3,4
            queue.Dequeue();
            Console.WriteLine(queue);//front = 0        rear = 3        count = 3       data = 2,3,4
            queue.Dequeue();
            Console.WriteLine(queue);//front = 1        rear = 3        count = 2       data = 3,4
            queue.Enqueue(5);
            Console.WriteLine(queue);//front = 1        rear = 4        count = 3       data = 3,4,5
            queue.Enqueue(6);
            Console.WriteLine(queue);//front = 1        rear = 0        count = 4       data = 3,4,5,6
            queue.Enqueue(7);        //Queue is full
            Console.WriteLine(queue);//front = 1        rear = 0        count = 4       data = 3,4,5,6
            queue.Dequeue();
            queue.Enqueue(7);
            Console.WriteLine(queue);//front = 2        rear = 1        count = 4       data = 4,5,6,7

            queue.Clear();
            Console.WriteLine(queue);//queue is empty.

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            Console.WriteLine(queue);//front = -1       rear = 3        count = 4       data = 1,2,3,4
            queue.Enqueue(5);
            Console.WriteLine(queue);//front = -1       rear = 4        count = 5       data = 1,2,3,4,5
            queue.Enqueue(6);        //Queue is full
            Console.WriteLine(queue);//front = -1       rear = 4        count = 5       data = 1,2,3,4,5
            queue.Dequeue();
            queue.Dequeue();
            queue.Dequeue();
            queue.Dequeue();
            Console.WriteLine(queue);//front = 3        rear = 4        count = 1       data = 5
            queue.Dequeue();
            Console.WriteLine(queue);//queue is empty.
            queue.Enqueue(0);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);        //Queue is full
            Console.WriteLine(queue);//front = 4        rear = 3        count = 4       data = 0,1,2,3
            Console.WriteLine(queue.Peek());//0
            queue.Dequeue();
            Console.WriteLine(queue);//front = 0        rear = 3        count = 3       data = 1,2,3
            queue.Dequeue();
            Console.WriteLine(queue);//front = 1        rear = 3        count = 2       data = 2,3
            queue.Dequeue();
            Console.WriteLine(queue);//front = 2        rear = 3        count = 1       data = 3
            queue.Dequeue();
            Console.WriteLine(queue);//queue is empty.
            queue.Enqueue(9);
            Console.WriteLine(queue);//front = 3        rear = 4        count = 1       data = 9
            Console.ReadLine();
        }
    }
}
