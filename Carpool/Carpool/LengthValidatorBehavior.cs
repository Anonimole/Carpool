using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Carpool
{
    public class LengthValidatorBehavior : Behavior<Entry>

    {
        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(LengthValidatorBehavior), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            IsValid = entry.Text.Length>0? true : false;
            ((Entry)sender).TextColor = IsValid ? Color.Green : Color.Red;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;

        }
    }
}
