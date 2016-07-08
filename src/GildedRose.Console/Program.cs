using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRose.Console
{
    internal class Program
    {
        private IList<Item> Items;

        internal static void Main(string[] args)
        {
            UpdateAndPrintItems();

            System.Console.ReadKey();
        }

        internal static void UpdateAndPrintItems()
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program
            {
                Items = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20, Type = ItemType.Normal},
                    new Item {Name = "Aged Brie", SellIn = 2, Quality = 0, Type = ItemType.Ageing},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7, Type = ItemType.Normal},
                    new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80, Type = ItemType.Legendary},
                    new Item {Name = "Merlot Red Wine", SellIn = 0, Quality = 20, Type = ItemType.Ageing},
                    new Item {Name = "Stilton", SellIn=0, Quality=0, Type = ItemType.Ageing},
                    new Item {Name = "Gruyere", SellIn=0, Quality=0, Type = ItemType.Ageing},
                    new Item {Name = "Cuban Cigars", SellIn=0, Quality=10, Type = ItemType.NoDrop},
                    new Item {Name = "Artichoke", SellIn=5, Quality=5, Type = ItemType.Normal},
                    new Item {Name = "Yoghurt", SellIn=10, Quality=10, Type = ItemType.Normal},
                    new Item {Name = "Gourmet Dinner Tickets", SellIn=10, Quality=1, Type = ItemType.Desirable},
                    new Item {Name = "Wine Tasting Workshop", SellIn=42, Quality=1, Type = ItemType.Desirable},
                    new Item {Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 15,
                        Quality = 20, 
                        Type = ItemType.Desirable
                    },
                    new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6, Type = ItemType.Conjured}
                }
            };

            app.UpdateQuality();

            PrintItems(app);
        }

        private static void PrintItems(Program app)
        {
            foreach (var item in app.Items)
            {
                System.Console.WriteLine("{0}|{1}|{2}", item.Name, item.Quality, item.SellIn);
            }
        }

        public void UpdateQuality()
        {
            UpdateQuality(Items.ToArray());
        }

        public static void UpdateQuality(Item[] items)
        {
            foreach (var item in items)
            {
                if (item.Type == ItemType.Ageing ) 
                {
                    UpdateAgeingItem(item);
                }
                else if (item.Type == ItemType.Desirable)
                {
                    UpdateDesirableEventItem(item);
                }
                else if (item.Type == ItemType.Legendary)
                {
                    UpdateLegendaryItem(item);
                }
                else if (item.Type == ItemType.Conjured)
                {
                    UpdateConjuredItem(item);
                }
                else if (item.Type == ItemType.NoDrop)
                {
                    continue;
                }
                else if (item.Type==ItemType.Normal)
                {
                    UpdatePerishableItem(item);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("I don't know how to handle this item!");
                }
            }
        }

        private static void UpdateConjuredItem(Item item)
        {
            DecreaseQuality(item);
            DecreaseQuality(item);
            DecreaseSellIn(item);
        }

        private static void UpdateAgeingItem(Item item)
        {
            IncreaseQuality(item);
            DecreaseSellIn(item);

            if (HasPassedSellByDate(item))
            {
                IncreaseQuality(item);
            }
        }

        private static void UpdateDesirableEventItem(Item item)
        {
            // Tickets are more valuable when an event is closer
            if (item.SellIn <= 10)
            {
                IncreaseQuality(item);
            }

            // They increase in value much more the closer we are to the event
            if (item.SellIn <= 5)
            {
                IncreaseQuality(item);
            }

            IncreaseQuality(item);
            DecreaseSellIn(item);

            if (HasPassedSellByDate(item))
            {
                item.Quality = 0;
            }
        }

        private static void UpdateLegendaryItem(Item item)
        {
        }

        private static void UpdatePerishableItem(Item item)
        {
            DecreaseQuality(item);
            DecreaseSellIn(item);

            if (HasPassedSellByDate(item))
            {
                DecreaseQuality(item);
            }
        }

        private static bool HasPassedSellByDate(Item item)
        {
            return item.SellIn < 0;
        }

        private static void IncreaseQuality(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality = item.Quality + 1;
            }
        }

        private static void DecreaseQuality(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality = item.Quality - 1;
            }
        }

        private static void DecreaseSellIn(Item item)
        {
            item.SellIn = item.SellIn - 1;
        }
    }
}