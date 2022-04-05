using System;
using Xunit;
using Queue;

namespace QueueTest
{
    public class QueueTest
    {
        [Fact]
        public void CountIncreases()
        {
            var queue = new Queue<int>();
            queue.Enqueue(1);
            Assert.Equal(1, queue.Count);
        }
        
        [Fact]
        public void CapacityIncreases()
        {
            var queue = new Queue<int>();
            for(int i=0;i<5;i++)
                queue.Enqueue(1);
            Assert.Equal(5, queue.Count);
            Assert.Equal(8, queue.Capacity);
        }

        [Fact]
        public void PopDecreasesCount()
        {
            var queue = new Queue<int>();
            
            for(int i =0;i<4;i++)
                queue.Enqueue(i);

            Assert.Equal(0, queue.Dequeue());
            Assert.Equal(3, queue.Count);
        }

        [Fact]
        public void ContainsWorking()
        {
            var queue = new Queue<int>();

            for (int i = 0; i < 4; i++)
                queue.Enqueue(i);

            for(int i =0;i<4;i++)
                Assert.True(queue.Contains(i));
        }

        [Fact]
        public void PopEmptyThrowsExeption()
        {
            var queue = new Queue<int>();

            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        }

        [Fact]
        public void PeekEmptyThrowsExeption()
        {
            var queue = new Queue<int>();

            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        }
    }
}
