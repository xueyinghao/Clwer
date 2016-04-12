using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider_New
{
    public class Node<T>
    {
        private T data;
        private Node<T> next;
        public Node(T data, Node<T> next)
        {
            this.data = data;
            this.next = next;
        }
        public Node(Node<T> next)
        {
            this.next = next;
            this.data = default(T);
        }
        public Node(T data)
        {
            this.data = data;
            this.next = null;
        }
        public Node()
        {
            this.data = default(T);
            this.next = null;
        }
        public T Data {
            get { return this.data; }
            set { this.data = value; }
        }
        public Node<T> Next
        {
            get { return next; }
            set { next = value; }
        }
    }
}
