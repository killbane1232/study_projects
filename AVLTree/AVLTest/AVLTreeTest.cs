using System;
using Xunit;
using System.Collections.Generic;
using AVLTree;

namespace AVLTest
{
    public class AVLTreeTest
    {
        [Fact]
        public void TreeStructTestOnlyDebug()
        {
            var list = new List<int> { 5, 2, 7, 1, 3, 6, 8 };

            var blow = new AVLTree<int, int>();
            for (int i = 0; i < 7; i++)
                blow.Add(list[i], 0);
            Assert.Equal(3, blow.Head.MaxChildHeight(blow.Head));
            blow.Remove(5);
            Assert.Equal(3, blow.Head.MaxChildHeight(blow.Head));
        }

        [Fact]
        public void TreeBalanceTest()
        {
            var list = new List<int> { 5,2,7,1,3,6,8};

            var blow = new AVLTree<int, int>();
            for (int i = 0; i < 7; i++)
                blow.Add(list[i], 0);
            Assert.Equal(3, blow.Head.MaxChildHeight(blow.Head));
            blow.Remove(1);
            blow.Remove(3);
            blow.Remove(2);
            Assert.Equal(3, blow.Head.MaxChildHeight(blow.Head));
        }

        [Fact]
        public void HeadOnlyRemoveTest()
        {
            var blow = new AVLTree<int, int>();
            blow.Add(1, 0);
            blow.Remove(1);
            Assert.Equal(0, blow.Count);
            Assert.Null(blow.Head);
        }

        [Fact]
        public void AddTest()
        {
            var blow = new AVLTree<int, int>();
            for (int i = 0; i < 2; i++)
                blow.Add(i, 0);
            Assert.Equal(2, blow.Count);
            Assert.Equal(0, blow.Head.Key);
            Assert.NotNull(blow.Head.Right);
            Assert.Equal(1, blow.Head.Right.Key);
        }

        [Fact]
        public void RemoveTest()
        {
            var blow = new AVLTree<int, int>();
            for (int i = 0; i < 2; i++)
                blow.Add(i, 0);

            blow.Remove(0);

            Assert.Equal(1, blow.Count);
            Assert.Equal(1, blow.Head.Key);
            Assert.Null(blow.Head.Right);
            Assert.Null(blow.Head.Left);
            Assert.Null(blow.Head.Parent);
        }

        [Fact]
        public void ContainsTest()
        {
            var blow = new AVLTree<int, int>();
            for (int i = 0; i < 20; i++)
                blow.Add(i, 0);
            blow.Remove(14);

            Assert.True(blow.Contains(15));
            Assert.False(blow.Contains(30));
            Assert.False(blow.Contains(14));
        }

        [Fact]
        public void RemoveInEmptyThrowsExceptionTest()
        {
            var blow = new AVLTree<int, int>();

            Assert.Throws<InvalidOperationException>(() => blow.Remove(1));
        }

        [Fact]
        public void AddingTwoElementsWithEqualKeysThrowsExceptionTest()
        {
            var blow = new AVLTree<int, int>();
            blow.Add(1, 0);
            Assert.Throws<ArgumentException>(()=>blow.Add(1,0));
        }

        [Fact]
        public void ContainsInEmptyReturnsFalse()
        {
            var blow = new AVLTree<byte, int>();

            for (int i = byte.MinValue; i <= byte.MaxValue; i++)
                Assert.False(blow.Contains((byte)i));
        }
    }
}
