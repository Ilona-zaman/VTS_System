using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Foundation;
using MonoTouch.Dialog;
using UIKit;

namespace iOS.Controllers
{
    class MenuViewController : DialogViewController
    {
        private List<StringElement> menuItems;

        public event EventHandler<RowClickedEventArgs> RowClicked;

        public class RowClickedEventArgs : EventArgs
        {
            public int Item { get; set; }

            public RowClickedEventArgs(int item) : base()
            {
                this.Item = item;
            }
        }

        public MenuViewController() : base(null)
        {
            InitMenuItem();
            var section = new Section();
            StringElement stringElement = new StringElement("Arkadiy Dobkin");
            UIImage image = UIImage.FromBundle("Avatar_Ark");
            ImageStringElement imageStringElement = new ImageStringElement("", image);
            section.Add(imageStringElement);
            section.Add(stringElement);
            section.AddAll(menuItems);
            Root = new RootElement("....") { section };
        }

        private void InitMenuItem()
        {
            menuItems = new List<StringElement>();
            menuItems.Add(new StringElement("All",
                delegate
                {
                    if (RowClicked != null)
                    {
                        RowClicked(this, new RowClickedEventArgs(0));
                    }
                }));
            menuItems.Add(new StringElement("Approved",
                delegate
                {
                    if (RowClicked != null)
                    {
                        RowClicked(this, new RowClickedEventArgs(1));
                    }
                }));
            menuItems.Add(new StringElement("Closed",
                delegate
                {
                    if (RowClicked != null)
                    {
                        RowClicked(this, new RowClickedEventArgs(2));
                    }
                }));
        }

        public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
        {
            return true;
        }
    }
}