using Leaf.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaf.Shared.Converters
{
    public class AccountConverter
    {
        public static HomeItem CreateFrom(Home home)
        {
            HomeItem homeItem = new HomeItem
            {
                Id = home.Id,
                CreatedAt = home.CreatedAt,
                UpdatedAt = home.UpdatedAt,
                Version = home.Version,
                OwnerId = home.OwnerId,
                HomeId = home.HomeId,
                HasAccess = home.HasAccess,
                Name = home.Name,
                IsPrimary = home.IsPrimary,
                BackgroundUri = home.BackgroundUri,
                BackgroundOpacity = home.BackgroundOpacity,
                BackgroundBlur = home.BackgroundBlur,
                IsLocationAvailable = home.IsLocationAvailable,
                Location = home.Location,
                MenuItems = home.MenuItems,
                Deleted = home.Deleted
            };

            return homeItem;
        }
        public static Account CreateFrom(HomeItem homeItem)
        {
            Account account = new Account
            {
                Id = homeItem.Id,
                CreatedAt = homeItem.CreatedAt,
                UpdatedAt = homeItem.UpdatedAt,
                Version = homeItem.Version,
                OwnerId = homeItem.OwnerId,
                HomeId = homeItem.HomeId,
                HasAccess = homeItem.HasAccess,
                Name = homeItem.Name,
                IsPrimary = homeItem.IsPrimary,
                BackgroundUri = homeItem.BackgroundUri,
                BackgroundOpacity = homeItem.BackgroundOpacity,
                BackgroundBlur = homeItem.BackgroundBlur,
                IsLocationAvailable = homeItem.IsLocationAvailable,
                Location = homeItem.Location,
                MenuItems = homeItem.MenuItems,
                Deleted = homeItem.Deleted
            };

            return account;
        }
    }
}
