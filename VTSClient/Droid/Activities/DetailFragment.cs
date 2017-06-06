using System;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;

namespace Droid.Activities
{
    public class DetailFragment : Fragment
    {
        private readonly Func<LayoutInflater, ViewGroup, Bundle, View> view;
        private ViewGroup _viewGroup;

        public DetailFragment(Func<LayoutInflater, ViewGroup, Bundle, View> view)
        {
            this.view = view;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            _viewGroup = container;
            return view(inflater, container, savedInstanceState);
        }
    }
}