using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.FluentLayouts.Touch;
using Foundation;
using UIKit;

namespace iOS.Controllers
{
    class TestViewController: UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.White;
            var button = new UIButton(UIButtonType.System);
            button.SetTitle("Hello world", UIControlState.Normal);
            Add(button);
            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            View.AddConstraints(
                button.CenterY().EqualTo().CenterYOf(View),
                button.CenterX().EqualTo().CenterXOf(View), 
                button.Width().EqualTo().WidthOf(button), 
                button.Height().EqualTo().HeightOf(button)
                );


        }
    }
}