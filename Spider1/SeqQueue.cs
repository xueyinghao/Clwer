using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spider1
{
   public class SeqQueue<T>:IQueue<T>
    {
        private int maxsize; //循环顺序队列的容量
        private T[] data;    //数组，用于存储循环顺序队列中的数据元素
        private int front;   //队头
        private int rear;    //队尾
        //索引器
        public T this[int index]
        {
            get
            {
                return data[index];
            }
            set
            {
                data[index] = value;
            }
        }
        //容量属性
        public int Maxsize
        {
            get
            {
                return maxsize;
            }
            set
            {
                maxsize = value;
            }
        }
        //队头属性
        public int Front
        {
            get
            {
                return front;
            }
            set
            {
                front = value;
            }
        }
        //队尾属性
        public int Rear
        {
            get
            {
                return rear;
            }
            set
            {
                rear = value;
            }
        }
        //初始化队列
        public SeqQueue() { }
        public SeqQueue(int size)
        {
            data = new T[size];
            maxsize = size;
            front = rear = -1;
        }

        public int Count()
        {
            if (rear > front)
            {
                return rear - front;
            }
            else
            {
                return (rear - front + maxsize) % maxsize;
            }
        }

        //入队操作
        public void Enqueue(T elem)
        {
            if (IsFull())
            {
                MessageBox.Show("Queue is full");
                return;
            }
            rear = (rear + 1) % maxsize;
            data[rear] = elem;
        }
        //出队操作
        public T Dequeue()
        {
            if (IsEmpty())
            {
                MessageBox.Show("Queue is Empty");
                return default(T);
            }
            front = (front + 1) % maxsize;
            return data[front];
        }
        //获取队头数据元素
        public T GetFront()
        {
            if (IsEmpty())
            {
                MessageBox.Show("Queue is Empty");
                return default(T);
            }
            return data[(front + 1) % maxsize];
        }
        //求循环顺序队列的长度
        public int Getlength()
        {
            return (rear - front + maxsize) % maxsize;
        }


        //判断循环顺序队列是否为满
        public bool IsFull()
        {
            if ((front==-1&& rear==maxsize-1)||(rear+1)%maxsize==front)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //清空循环顺序队列
        public void Clear()
        {
            front = rear = -1;
        }
        //判断循环顺序队列是否为空
        public bool IsEmpty()
        {
            if (front==rear)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
