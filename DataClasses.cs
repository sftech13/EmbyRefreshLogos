namespace EmbyRefreshLogos
{
    public class ImageTags
    {
        public string Primary { get; set; } = string.Empty;
    }

    public class Item
    {
        public string Name { get; set; } = string.Empty;
        public string ServerId { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public int SortIndexNumber { get; set; } = 0;
        public string ChannelNumber { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public ImageTags ImageTags { get; set; } = new ImageTags();
        public List<object> BackdropImageTags { get; set; } = new List<object>();
        public bool Disabled { get; set; } = false;
        public string ManagementId { get; set; } = string.Empty;
        public string ListingsProviderId { get; set; } = string.Empty;
        public string ListingsChannelId { get; set; } = string.Empty;
        public string ListingsPath { get; set; } = string.Empty;
        public string ListingsChannelName { get; set; } = string.Empty;
        public string ListingsChannelNumber { get; set; } = string.Empty;
    }

    public class Root
    {
        public List<Item> Items { get; set; } = new List<Item>();
    }
}
