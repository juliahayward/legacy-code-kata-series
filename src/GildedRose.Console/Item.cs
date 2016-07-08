namespace GildedRose.Console
{
    public enum ItemType
    {
        Normal = 1,
        Ageing,
        Desirable,
        Legendary,
        Conjured,
        NoDrop
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }

        public ItemType Type { get; set; }
    }
}