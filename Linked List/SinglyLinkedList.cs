using System;
using System.Collections.Generic;
using System.Text;

namespace Linked_List
{
    public class SinglyLinkedListNode<T>
    {
        public SinglyLinkedListNode<T> Next { get; set; }
        public T Data { get; set; }

        public SinglyLinkedListNode(T data)
        : this(data, null)
        {
        }

        public SinglyLinkedListNode(T data, SinglyLinkedListNode<T> next)
        {
            Data = data;
            Next = next;
        }

        public override string ToString()
        {
            if (Data == null)
                return string.Empty;
            return Data.ToString();
        }
    }

    public class SinglyLinkedList<T> : ICollection<T>
    {
        private string strListName;
        public SinglyLinkedListNode<T> FirstNode { get; private set; }
        public SinglyLinkedListNode<T> LastNode { get; private set; }
        public int Count { get; private set; }
        public bool IsReadOnly { get { return false; } }

        public T this[int index]
        {
            get
            {
                if (index < 0)
                    throw new ArgumentOutOfRangeException();
                SinglyLinkedListNode<T> currentNode = FirstNode;
                for (int i = 0; i < index; i++)
                {
                    if (currentNode.Next == null)
                        throw new ArgumentOutOfRangeException();
                    currentNode = currentNode.Next;
                }
                return currentNode.Data;
            }
        }

        public bool IsEmpty
        {
            get
            {
                lock (this)
                {
                    return FirstNode == null;
                }
            }
        }

        public SinglyLinkedList(string strListName)
        {
            this.strListName = strListName;
            Count = 0;
            FirstNode = LastNode = null;
        }

        public SinglyLinkedList() : this("MyList") { }

        public override string ToString()
        {
            if (IsEmpty)
                return string.Empty;
            StringBuilder returnString = new StringBuilder();
            foreach (T item in this)
            {
                if (returnString.Length > 0)
                    returnString.Append("->");
                returnString.Append(item);
            }
            return returnString.ToString();
        }

        public void InsertAtFront(T item)
        {
            lock (this)
            {
                if (IsEmpty)
                    FirstNode = LastNode = new SinglyLinkedListNode<T>(item);
                else
                    FirstNode = new SinglyLinkedListNode<T>(item, FirstNode);
                Count++;
            }
        }

        public void InsertAtBack(T item)
        {
            lock (this)
            {
                if (IsEmpty)
                    FirstNode = LastNode = new SinglyLinkedListNode<T>(item);
                else
                    LastNode = LastNode.Next = new SinglyLinkedListNode<T>(item);
                Count++;
            }
        }

        public T RemoveFromFront()
        {
            lock (this)
            {
                if (IsEmpty)
                    throw new ApplicationException("list is empty");
                T removedData = FirstNode.Data;
                if (FirstNode == LastNode)
                    FirstNode = LastNode = null;
                else
                    FirstNode = FirstNode.Next;
                Count--;
                return removedData;
            }
        }

        public T RemoveFromBack()
        {
            lock (this)
            {
                if (IsEmpty)
                    throw new ApplicationException("list is empty");
                T removedData = LastNode.Data;
                if (FirstNode == LastNode)
                    FirstNode = LastNode = null;
                else
                {
                    SinglyLinkedListNode<T> currentNode = FirstNode;
                    while (currentNode.Next != LastNode)
                        currentNode = currentNode.Next;
                    LastNode = currentNode;
                    currentNode.Next = null;
                }
                Count--;
                return removedData;
            }
        }

        public void InsertAt(int index, T item)
        {
            lock (this)
            {
                if (index > Count || index < 0)
                    throw new ArgumentOutOfRangeException();
                if (index == 0)
                    InsertAtFront(item);
                else if (index == (Count - 1))
                    InsertAtBack(item);
                else
                {
                    SinglyLinkedListNode<T> currentNode = FirstNode;
                    for (int i = 0; i < index - 1; i++)
                    {
                        currentNode = currentNode.Next;
                    }
                    SinglyLinkedListNode<T> newNode = new SinglyLinkedListNode<T>(item, currentNode.Next);
                    currentNode.Next = newNode;
                    Count++;
                }
            }
        }

        public object RemoveAt(int index)
        {
            lock (this)
            {
                if (index > Count || index < 0)
                    throw new ArgumentOutOfRangeException();
                object removedData;
                if (index == 0)
                    removedData = RemoveFromFront();
                else if (index == (Count - 1))
                    removedData = RemoveFromBack();
                else
                {
                    SinglyLinkedListNode<T> currentNode = FirstNode;
                    for (int i = 0; i < index; i++)
                    {
                        currentNode = currentNode.Next;
                    }
                    removedData = currentNode.Data;
                    currentNode.Next = currentNode.Next.Next;
                    Count--;
                }
                return removedData;
            }
        }

        public bool Remove(T item)
        {
            if (FirstNode.Data.ToString().Equals(item.ToString()))
            {
                RemoveFromFront();
                return true;
            }
            else if (LastNode.Data.ToString().Equals(item.ToString()))
            {
                RemoveFromBack();
                return true;
            }
            else
            {
                SinglyLinkedListNode<T> currentNode = FirstNode;
                while (currentNode.Next != null)
                {
                    if (currentNode.Next.Data.ToString().Equals(item.ToString()))
                    {
                        currentNode.Next = currentNode.Next.Next;
                        Count--;
                        if (currentNode.Next == null)
                            LastNode = currentNode;
                        return true;
                    }
                    currentNode = currentNode.Next;
                }
            }
            return false;
        }

        public bool Update(T oldItem, T newItem)
        {
            lock (this)
            {
                SinglyLinkedListNode<T> currentNode = FirstNode;
                while (currentNode != null)
                {
                    if (currentNode.ToString().Equals(oldItem.ToString()))
                    {
                        currentNode.Data = newItem;
                        return true;
                    }
                    currentNode = currentNode.Next;
                }
                return false;
            }
        }

        public bool Contains(T item)
        {
            lock (this)
            {
                SinglyLinkedListNode<T> currentNode = FirstNode;
                while (currentNode != null)
                {
                    if (currentNode.Data.ToString().Equals(item.ToString()))
                    {
                        return true;
                    }
                    currentNode = currentNode.Next;
                }
                return false;
            }
        }

        public void Clear()
        {
            FirstNode = LastNode = null;
            Count = 0;
        }

        public void Reverse()
        {
            if (FirstNode == null || FirstNode.Next == null)
                return;
            LastNode = FirstNode;
            SinglyLinkedListNode<T> prevNode = null;
            SinglyLinkedListNode<T> currentNode = FirstNode;
            SinglyLinkedListNode<T> nextNode = FirstNode.Next;

            while (currentNode != null)
            {
                currentNode.Next = prevNode;
                if (nextNode == null)
                    break;
                prevNode = currentNode;
                currentNode = nextNode;
                nextNode = nextNode.Next;
            }
            FirstNode = currentNode;
        }

        public bool HasCycle()
        {
            SinglyLinkedListNode<T> currentNode = FirstNode;
            SinglyLinkedListNode<T> iteratorNode = FirstNode;
            for (; iteratorNode != null && iteratorNode.Next != null;
                iteratorNode = iteratorNode.Next)
            {
                if (currentNode.Next == null ||
                    currentNode.Next.Next == null) return false;
                if (currentNode.Next == iteratorNode ||
                    currentNode.Next.Next == iteratorNode) return true;
                currentNode = currentNode.Next.Next;
            }
            return false;
        }

        public SinglyLinkedListNode<T> GetMiddleItem()
        {
            SinglyLinkedListNode<T> currentNode = FirstNode;
            SinglyLinkedListNode<T> iteratorNode = FirstNode;
            for (; iteratorNode != null && iteratorNode.Next != null;
                iteratorNode = iteratorNode.Next)
            {
                if (currentNode.Next == null ||
                    currentNode.Next.Next == null) return iteratorNode;
                if (currentNode.Next == iteratorNode ||
                    currentNode.Next.Next == iteratorNode) return null;
                currentNode = currentNode.Next.Next;
            }
            return FirstNode;
        }

        public IEnumerator<T> GetEnumerator()
        {
            SinglyLinkedListNode<T> currentNode = FirstNode;
            while (currentNode != null)
            {
                yield return currentNode.Data;
                currentNode = currentNode.Next;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            InsertAtFront(item);
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array is null");
            if (!(array is T[] llArray))
                throw new ArgumentException();
            ((ICollection<T>)this).CopyTo(llArray, arrayIndex);
        }
    }
}
