using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivalGame
{
    public class ResourceType
    {
        public string Sprite { get; set; }

        public ResourceType(string Sprite)
        {
            this.Sprite = Sprite;
        }

        public static ResourceType Iron = new ResourceType("Iron");
        public static ResourceType Rocks = new ResourceType("Rocks");
        public static ResourceType Trees = new ResourceType("Trees");
        public static ResourceType Strawberries = new ResourceType("Strawberries");
        public static ResourceType Corn = new ResourceType("Corn");
        public static ResourceType Wheat = new ResourceType("Wheat");
        public static ResourceType Rice = new ResourceType("Rice");
        public static ResourceType Gold = new ResourceType("Gold");
        public static ResourceType Diamond = new ResourceType("Diamond");
        public static ResourceType Flowers = new ResourceType("Flowers");
    }
}
