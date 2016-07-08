﻿using System;
using System.Collections.Generic;
using GildedRose.Console;
using NUnit.Framework;

namespace GildedRose.Tests
{
    public class ItemUpdaterTests
    {
        [Test]
        public void StandardItemShouldLowerQualityAndSellInByOne()
        {
            var item = new Item { Name = "+5 Dexterity Vest", SellIn = 3, Quality = 6, Type = ItemType.Normal};

            UpdateItem(item);

            Assert.AreEqual(5, item.Quality);
            Assert.AreEqual(2, item.SellIn);
        }

        [Test]
        public void StandardItemShouldLowerQualityTwiceAsFastWhenSellInIsNegative()
        {
            var item = new Item { Name = "+5 Dexterity Vest", SellIn = -2, Quality = 6, Type = ItemType.Normal};

            UpdateItem(item);

            Assert.AreEqual(4, item.Quality);
            Assert.AreEqual(-3, item.SellIn);
        }

        [Test]
        public void StandardItemShouldLowerQualityTwiceAsFastWhenSellInIsZero()
        {
            var item = new Item { Name = "+5 Dexterity Vest", SellIn = 0, Quality = 6, Type=ItemType.Normal };

            UpdateItem(item);

            Assert.AreEqual(4, item.Quality);
            Assert.AreEqual(-1, item.SellIn);
        }

        [Test]
        public void BackstagePassItemShouldIncreaseQualityTwiceAsFastWhenSellInLessThanElevenDays()
        {
            var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 6, Type=ItemType.Desirable };

            UpdateItem(item);

            Assert.AreEqual(8, item.Quality);
            Assert.AreEqual(9, item.SellIn);
        }

        [Test]
        public void BackstagePassItemShouldIncreaseQualityThreeTimesAsFastWhenSellInLessThanSixDays()
        {
            var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 6, Type = ItemType.Desirable };

            UpdateItem(item);

            Assert.AreEqual(9, item.Quality);
            Assert.AreEqual(4, item.SellIn);
        }

        [Test]
        public void BackstagePassItemShouldHaveZeroQualityWhenSellInBelowZero()
        {
            var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 6, Type = ItemType.Desirable };

            UpdateItem(item);

            Assert.AreEqual(0, item.Quality);
            Assert.AreEqual(-1, item.SellIn);
        }

        [Test]
        public void AgedBrieQualityIncreasesTwiceAsFastWhenSellInIsLessThanZero()
        {
            var item = new Item { Name = "Aged Brie", SellIn = 0, Quality = 6, Type = ItemType.Ageing };

            UpdateItem(item);

            Assert.AreEqual(8, item.Quality);
            Assert.AreEqual(-1, item.SellIn);
        }

        [Test]
        public void StandardItemQualityIsNeverNegative()
        {
            var item = new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 0, Type = ItemType.Normal };

            UpdateItem(item);

            Assert.AreEqual(0, item.Quality);
            Assert.AreEqual(9, item.SellIn);
        }

        [Test]
        public void SulfurasNeverDecreasesInQualityAndNeverHasToBeSold()
        {
            var item = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 80, Type = ItemType.Legendary };

            UpdateItem(item);

            Assert.AreEqual(80, item.Quality);
            Assert.AreEqual(10, item.SellIn);
        }

        [Test]
        public void AgedBrieQualityCanNeverBeMoreThanFifty()
        {
            var item = new Item { Name = "Aged Brie", SellIn = -1, Quality = 50, Type = ItemType.Ageing };

            UpdateItem(item);

            Assert.AreEqual(50, item.Quality);
            Assert.AreEqual(-2, item.SellIn);
        }

        [Test]
        public void ConjuredManaCakeQualityDecreasesTwiceAsFast()
        {
            var item = new Item { Name = "Conjured Mana Cake", SellIn = 6, Quality = 10, Type = ItemType.Conjured };

            UpdateItem(item);

            Assert.AreEqual(8, item.Quality);
            Assert.AreEqual(5, item.SellIn);
        }

        [Test]
        public void CubanCigarsDontChange()
        {
            var item = new Item { Name = "Cuban Cigars", SellIn = 0, Quality = 10, Type = ItemType.NoDrop };

            UpdateItem(item);

            Assert.AreEqual(10, item.Quality);
            Assert.AreEqual(0, item.SellIn);
        }

        [Test]
        public void PartiallyConstructedItemThrowsAnError()
        {
            var item = new Item { Name = "Test item", SellIn = 0, Quality = 10 };

            Assert.Throws<ArgumentOutOfRangeException>(() => UpdateItem(item));
        }

        [Test]
        public void EvilItemThrowsAnError()
        {
            var item = new Item { Name = "Evil", SellIn = 0, Quality = 10, Type = (ItemType)23 };

            Assert.Throws<ArgumentOutOfRangeException>(() => UpdateItem(item));
        }

        private void UpdateItem(Item item)
        {
            Program.UpdateQuality(new[] { item });
        }
    }
}