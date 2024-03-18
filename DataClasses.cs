﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmbyRefreshLogos
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ImageTags
    {
        public string Primary { get; set; }
    }

    public class Item
    {
        public string Name { get; set; }
        public string ServerId { get; set; }
        public string Id { get; set; }
        public int SortIndexNumber { get; set; }
        public string ChannelNumber { get; set; }
        public string Type { get; set; }
        public ImageTags ImageTags { get; set; }
        public List<object> BackdropImageTags { get; set; }
        public bool Disabled { get; set; }
        public string ManagementId { get; set; }
        public string ListingsProviderId { get; set; }
        public string ListingsChannelId { get; set; }
        public string ListingsPath { get; set; }
        public string ListingsChannelName { get; set; }
        public string ListingsChannelNumber { get; set; }
    }

    public class Root
    {
        public List<Item> Items { get; set; }
    }


}
