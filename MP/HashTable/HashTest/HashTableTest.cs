using System;
using Xunit;
using HashTable;

namespace HashTest
{
    public class HashTableTest
    {
        [Fact]
        public void CountIncreasesTest()
        {
            var table = new ChainHashTable<int, int>();
            table.Add(1, 2);
            table.Add(3, 4);

            Assert.Equal(2, table.Count);
        }

        [Fact]
        public void AddingItemsWithEqualsKeysThrowsExeptionTest()
        {
            var table = new ChainHashTable<int, int>();
            table.Add(3, 2);

            Assert.Throws<ArgumentException>(() => table.Add(3, 4));
        }

        [Fact]
        public void AddingItemsMoreThanPrimeNumberIncreasesCapacityTest()
        {
            var table = new ChainHashTable<int, int>();
            for(int i =0;i<500;i++)
                table.Add(i, i);

            Assert.Equal(500, table.Count);
        }

        [Fact]
        public void RemoveDecreasesCountTest()
        {
            var table = new ChainHashTable<int, int>();
            for (int i = 0; i < 500; i++)
                table.Add(i, i);

            table.Remove(0);
            Assert.Equal(499, table.Count);
        }

        [Fact]
        public void ContainsTest()
        {
            var table = new ChainHashTable<int, int>();
            table.Add(1, 2);
            table.Add(3, 4);

            Assert.True(table.ContainsKey(1));
            Assert.False(table.ContainsKey(2));
            Assert.True(table.ContainsKey(3));
            Assert.False(table.ContainsKey(4));
        }

        [Fact]
        public void RemoveTest()
        {
            var table = new ChainHashTable<int, int>();
            table.Add(1, 2);
            table.Add(3, 4);

            table.Remove(1);

            Assert.False(table.ContainsKey(1));
            Assert.False(table.ContainsKey(2));
            Assert.True(table.ContainsKey(3));
            Assert.False(table.ContainsKey(4));
        }

        [Fact]
        public void IndexerTest()
        {
            var table = new ChainHashTable<int, int>();
            table.Add(1, 2);
            table.Add(3, 4);

            Assert.Equal(4, table[3]);
        }

        [Fact]
        public void ColisionTest()
        {
            var table = new ChainHashTable<int, int>(3);
            table.Add(0, 2);
            table.Add(3, 4);

            Assert.Equal(2, table[0]);
            Assert.Equal(4, table[3]);
        }
    }
}
