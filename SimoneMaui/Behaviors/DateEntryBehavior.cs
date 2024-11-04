namespace SimoneMaui.Behaviors
{
    public class DateEntryBehavior : Behavior<Entry>
    {
        private const int MaxLength = 10;
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEntryTextChanged;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            if (entry != null)
            {

                var cursorPosition = entry.CursorPosition;
                var text = entry.Text;

                if (text.Length > MaxLength)
                {
                    text = text.Substring(0, MaxLength);
                    entry.Text = text;
                }

                if (text.Length == 2 && !text.Contains('-'))
                {
                    entry.Text += "-";
                }

                else if (text.Length == 5 )
                {
                    entry.Text += "-";
                }
               
                entry.CursorPosition = cursorPosition;
            }
        }
    }
}
