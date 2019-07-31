using Leaf.Shared.Models;
using Leaf.Windows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaf.Windows.Controls
{
    public class NavMenuFlipView
    {
        public static List<NavigationMenuItem> startupFlipView = new List<NavigationMenuItem>
        {
            new NavigationMenuItem
            {
                Symbol = "\uEA80",
                Label = "Control  your devices",
                Description = "Control of all your smart home devices right at your fingertips",
                Arguments = "Next"
            },
            new NavigationMenuItem
            {
                Symbol = "\uE117",
                Label = "Sync across your devices",
                Description = "When you sign in, all of your devices and settings will sync to your other devices",
                Arguments = "Next"
            },
            new NavigationMenuItem
            {
                Symbol = "\uE125",
                Label = "Share with family",
                Description = "Share your smart home devices with family members and friends",
                Arguments = "Get Started"
            },
        };
    }
}
