using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkipList2020;

namespace SkipListUnitTests
{
    [TestClass]
    public class SkipListUnitTest
    {
        [TestMethod]
        public void CountIncreaseAfterAddingTest()
        {
            int n = 10;
            var lib = new SkipList<int, int>();

            for(int i=0; i<n; i++)
            {
                lib.Add(i, i);
            }
            Assert.AreEqual(n, lib.Count);
        }

        [TestMethod]
        public void ItemsExistsAfterAddingTest()
        {
            var lib = new SkipList<int, int>();
            var nums = new List<int>(new[] { 44, 22, 1 , 56, 3, 90, 31, 15, 26 });
            int n = nums.Count;
            for (int i = 0; i < n; i++)
            {
                lib.Add(nums[i], i);
            }
            nums.Sort();
            int j = 0;
            foreach(var pair in lib)
            {
                Assert.AreEqual(nums[j], pair.Key);
                j++;
            }
            Assert.AreEqual(n, lib.Count);
        }

        [TestMethod]
        public void ContainsTest()
        {
            var lib = new SkipList<int, int>();
            var nums = new List<int>(new[] { 44, 22, 1, 56, 3, 90, 31, 15, 26 });
            int n = nums.Count;
            for (int i = 0; i < n; i++)
            {
                lib.Add(nums[i], i);
            }
            nums.Sort();
            for (int i = 0; i < n; i++)
            {
                Assert.IsTrue(lib.Contains(nums[i]));
            }
        }

        [TestMethod]
        public void RemoveTest()
        {
            var lib = new SkipList<int, int>();
            var nums = new List<int>(new[] { 44, 22, 1, 56, 3, 90, 31, 15, 26 });
            int n = nums.Count;
            for (int i = 0; i < n; i++)
            {
                lib.Add(nums[i], i);
            }
            nums.Sort();
            for (int i = 0; i < n; i += 3)
                lib.Remove(nums[i]);
            for (int i = 0; i < n; i++)
            {
                if(i%3==0)
                    Assert.IsFalse(lib.Contains(nums[i]));
                else
                    Assert.IsTrue(lib.Contains(nums[i]));
            }
        }

        [TestMethod]
        public void RandomItemsExistsAfterAddingTest()
        {
            var lib = new SkipList<int, int>();
            var nums = new HashSet<int>();
            var rd = new Random();
            int n = 100;
            while (nums.Count < n)
            {
                nums.Add(rd.Next(1, n * 3));
            }
            foreach(var item in nums)
            {
                lib.Add(item,1);
            }

            var a = nums.ToList();
            a.Sort();
            int j = 0;
            foreach (var pair in lib)
            {
                Assert.AreEqual(a[j], pair.Key);
                j++;
            }
            Assert.AreEqual(n, lib.Count);
        }
    }
}
