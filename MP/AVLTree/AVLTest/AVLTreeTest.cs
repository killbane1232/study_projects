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
            var list = new List<int>();
            var rand = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < 8; i++)
            {
                var ran = rand.Next() % 20;
                while(list.Contains(ran))
                    ran = rand.Next() % 20;
                list.Add(ran);
            }

            var blow = new AVLTree<int, int>();
            for (int i = 0; i < 8; i++)
                blow.Add(list[i], 0);
            int a = 5 + 7;
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

            Assert.Throws<InvalidOperationException>(()=>blow.Remove(1));
        }
    }
}
