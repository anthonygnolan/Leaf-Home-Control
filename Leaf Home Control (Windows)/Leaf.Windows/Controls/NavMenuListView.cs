using Leaf.Windows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace Leaf.Windows.Controls
{
    /// <summary>
    /// A specialized ListView to represent the items in the navigation menu.
    /// </summary>
    /// <remarks>
    /// This class handles the following:
    /// 1. Sizes the panel that hosts the items so they fit in the hosting pane.  Otherwise, the keyboard
    ///    may appear cut off on one side b/c the Pane clips instead of affecting layout.
    /// 2. Provides a single selection experience where keyboard focus can move without changing selection.
    ///    Both the 'Space' and 'Enter' keys will trigger selection.  The up/down arrow keys can move
    ///    keyboard focus without triggering selection.  This is different than the default behavior when
    ///    SelectionMode == Single.  The default behavior for a ListView in single selection requires using
    ///    the Ctrl + arrow key to move keyboard focus without triggering selection.  Users won't expect
    ///    this type of keyboarding model on the nav menu.
    /// </remarks>
    public class NavMenuListView : ListView
    {
        private SplitView splitViewHost;

        public NavMenuListView()
        {
            this.SelectionMode = ListViewSelectionMode.Single;
            this.SingleSelectionFollowsFocus = false;
            this.IsItemClickEnabled = true;
            this.ItemClick += ItemClickedHandler;

            // Locate the hosting SplitView control
            this.Loaded += (s, a) =>
            {
                var parent = VisualTreeHelper.GetParent(this);
                while (parent != null && !(parent is SplitView))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }

                if (parent != null)
                {
                    this.splitViewHost = parent as SplitView;

                    splitViewHost.RegisterPropertyChangedCallback(SplitView.IsPaneOpenProperty, (sender, args) =>
                    {
                        this.OnPaneToggled();
                    });

                    splitViewHost.RegisterPropertyChangedCallback(SplitView.DisplayModeProperty, (sender, args) =>
                    {
                        this.OnPaneToggled();
                    });

                    // Call once to ensure we're in the correct state
                    this.OnPaneToggled();
                }
            };
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // Remove the entrance animation on the item containers.
            for (int i = 0; i < this.ItemContainerTransitions.Count; i++)
            {
                if (this.ItemContainerTransitions[i] is EntranceThemeTransition)
                {
                    this.ItemContainerTransitions.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Mark the <paramref name="item"/> as selected and ensures everything else is not.
        /// If the <paramref name="item"/> is null then everything is unselected.
        /// </summary>
        /// <param name="item"></param>
        public void SetSelectedItem(ListViewItem item)
        {
            int index = -1;
            if (item != null)
            {
                index = this.IndexFromContainer(item);
            }

            for (int i = 0; i < this.Items.Count; i++)
            {
                var lvi = (ListViewItem)this.ContainerFromIndex(i);
                if (i != index)
                {
                    lvi.IsSelected = false;
                }
                else if (i == index)
                {
                    lvi.IsSelected = true;
                }
            }
        }

        /// <summary>
        /// Occurs when an item has been selected
        /// </summary>
        public event EventHandler<ListViewItem> ItemInvoked;

        /// <summary>
        /// Custom keyboarding logic to enable movement via the arrow keys without triggering selection 
        /// until a 'Space' or 'Enter' key is pressed. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyRoutedEventArgs e)
        {
            var focusedItem = FocusManager.GetFocusedElement();

            switch (e.Key)
            {
                case VirtualKey.Up:
                    this.TryMoveFocus(FocusNavigationDirection.Up);
                    e.Handled = true;
                    break;

                case VirtualKey.Down:
                    this.TryMoveFocus(FocusNavigationDirection.Down);
                    e.Handled = true;
                    break;

                case VirtualKey.Space:
                case VirtualKey.Enter:
                    // Fire our event using the item with current keyboard focus
                    this.InvokeItem(focusedItem);
                    e.Handled = true;
                    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        /// <summary>
        /// This method is a work-around until the bug in FocusManager.TryMoveFocus is fixed.
        /// </summary>
        /// <param name="direction"></param>
        private void TryMoveFocus(FocusNavigationDirection direction)
        {
            if (direction == FocusNavigationDirection.Next || direction == FocusNavigationDirection.Previous)
            {
                FocusManager.TryMoveFocus(direction);
            }
            else
            {
                Control control = FocusManager.FindNextFocusableElement(direction) as Control;
                if (control != null)
                {
                    control.Focus(FocusState.Programmatic);
                }
            }
        }

        private void ItemClickedHandler(object sender, ItemClickEventArgs e)
        {
            // Triggered when the item is selected using something other than a keyboard
            var item = this.ContainerFromItem(e.ClickedItem);
            this.InvokeItem(item);
        }

        private void InvokeItem(object focusedItem)
        {
            this.SetSelectedItem(focusedItem as ListViewItem);
            this.ItemInvoked?.Invoke(this, focusedItem as ListViewItem);

            if (this.splitViewHost.IsPaneOpen && (
                this.splitViewHost.DisplayMode == SplitViewDisplayMode.CompactOverlay ||
                this.splitViewHost.DisplayMode == SplitViewDisplayMode.Overlay))
            {
                this.splitViewHost.IsPaneOpen = false;
            }

            if (focusedItem is ListViewItem)
            {
                ((ListViewItem)focusedItem).Focus(FocusState.Programmatic);
            }
        }

        /// <summary>
        /// Re-size the ListView's Panel when the SplitView is compact so the items
        /// will fit within the visible space and correctly display a keyboard focus rect.
        /// </summary>
        private void OnPaneToggled()
        {
            if (this.splitViewHost.IsPaneOpen)
            {
                this.ItemsPanelRoot.ClearValue(FrameworkElement.WidthProperty);
                this.ItemsPanelRoot.ClearValue(FrameworkElement.HorizontalAlignmentProperty);
            }
            else if (this.splitViewHost.DisplayMode == SplitViewDisplayMode.CompactInline ||
                this.splitViewHost.DisplayMode == SplitViewDisplayMode.CompactOverlay)
            {
                this.ItemsPanelRoot.SetValue(FrameworkElement.WidthProperty, this.splitViewHost.CompactPaneLength);
                this.ItemsPanelRoot.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Left);
            }
        }

        public static List<NavMenuItem> navlistone = new List<NavMenuItem>
        {
            new NavMenuItem()
            {
                Symbol = "\uE10F",
                Label = "Overview",
                IsSelected = true,
                SelectedVis = Visibility.Visible,
                DestPage = typeof(Views.Overview.MainPage)
            },
            new NavMenuItem()
            {
                Symbol = "\uE744",
                Label = "Zones",
                SelectedVis = Visibility.Visible,
                DestPage = typeof(Views.Zones.MainPage)
             },
            new NavMenuItem()
            {
                Symbol = "\uF246",
                Label = "Rooms",
                SelectedVis = Visibility.Visible,
                DestPage = typeof(Views.Rooms.MainPage)
             },
        };

        public static List<NavMenuItem> navlisttwo = new List<NavMenuItem>
            {
                new NavMenuItem()
                {
                    Symbol = "\uEA80",
                    Label = "Lighting",
                    SelectedVis = Visibility.Visible,
                    DestPage = typeof(Views.Lighting.MainPage)
                },
                new NavMenuItem()
                {
                    Symbol = "\uEA6C",
                    Label = "Appliances",
                    SelectedVis = Visibility.Collapsed,
                    DestPage = typeof(Views.Appliances.MainPage)
                },
                new NavMenuItem()
                 {
                    Symbol = "\uE72E",
                    Label = "Security",
                    SelectedVis = Visibility.Collapsed,
                    DestPage = typeof(Views.Security.MainPage)
                },
                new NavMenuItem()
                {
                    Symbol = "\uE9CA",
                    Label = "Climate",
                    SelectedVis = Visibility.Collapsed,
                    DestPage = typeof(Views.Climate.MainPage)
                }
            };

        public static List<NavMenuItem> navlistthree = new List<NavMenuItem>
        {
            new NavMenuItem()
            {
                Symbol = "\uED39",
                Label = "Scenes",
                SelectedVis = Visibility.Collapsed,
                DestPage = typeof(Views.Scenes.MainPage)
            },
            new NavMenuItem()
            {
                Symbol = "\uE121",
                Label = "Routines",
                SelectedVis = Visibility.Collapsed,
                DestPage = typeof(Views.Routines.MainPage)
            }
        };

        public static List<NavMenuItem> navlistfour = new List<NavMenuItem>
        {
            new NavMenuItem()
            {
                Symbol = "\uE115",
                Label = "Settings",
                SelectedVis = Visibility.Visible,
                DestPage = typeof(Views.Settings.MainPage)
             },
        };

        public void LoadAppMenu()
        {
            //menuItems = Newtonsoft.Json.JsonConvert.DeserializeObject<List<bool>[]>(Home.MenuItems);

            //MyHome_ListView.ItemsSource = AddMenuItems(menuItems[0], navlistone);
            //MyDevices_ListView.ItemsSource = AddMenuItems(menuItems[1], navlisttwo);
            //RoutinesAndScenes_ListView.ItemsSource = AddMenuItems(menuItems[2], navlistthree);
            //Settings_ListView.ItemsSource = AddMenuItems(menuItems[3], navlistfour);

            //if (MyDevices_ListView.Items.Count == 0)
            //{
            //    DividerTwo.Visibility = Visibility.Collapsed;
            //}
            //if (RoutinesAndScenes_ListView.Items.Count == 0)
            //{
            //    DividerThree.Visibility = Visibility.Collapsed;
            //}
            //navlistone[0].IsSelected = true;
            //MyHome_ListView.SelectedIndex = 0;
        }

        private List<NavMenuItem> AddMenuItems(List<bool> MenuList, List<NavMenuItem> MenuItems)
        {
            List<NavMenuItem> list = new List<NavMenuItem>();
            int count = MenuItems.Count();
            for (int i = 0; i < count; i++)
            {
                if (MenuList[i] == true)
                {
                    list.Add(MenuItems[i]);
                }
            }
            return list;
        }
    }
}

