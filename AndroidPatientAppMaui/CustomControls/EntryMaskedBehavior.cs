using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.CustomControls
{
    public class EntryMaskedBehavior : Behavior<Entry>
    {
        int previousCursorPosition = 0;

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            entry.Focused += OnEntryFocused;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            entry.Focused -= OnEntryFocused;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            var text = e.NewTextValue;

            if (!string.IsNullOrEmpty(text))
            {
              //  Remove non-numeric characters
               text = new string(text.Where(c => char.IsDigit(c)).ToArray());

                var formattedText = "";
                var index = 0;

               // Apply phone number pattern to the text
                foreach (var patternChar in "(___) ___-____")
                {
                    if (patternChar == '_')
                    {
                        if (index < text.Length)
                        {
                            formattedText += text[index];
                            index++;
                        }
                        else
                        {
                            break; // Stop applying the pattern if we reach the end of the text
                        }
                    }
                    else
                    {
                        formattedText += patternChar;
                    }
                }

                entry.Text = formattedText;

               // Calculate the new cursor position
               var newCursorPosition = entry.Text.Length - (previousCursorPosition > formattedText.Length ? 1 : 0);

               // Restore the cursor position
                entry.CursorPosition = Math.Max(0, Math.Min(newCursorPosition, formattedText.Length));
            }
            else
            {
                entry.Text = ""; // Clear the text if empty

                //Reset the cursor position
                entry.CursorPosition = 0;
            }
        }

        void OnEntryFocused(object sender, FocusEventArgs e)
        {
            var entry = sender as Entry;
            if (entry != null && entry.Text != null)
            {
                entry.CursorPosition = entry.Text.Length; // Move the cursor to the end of the text when focused
            }
        }
    }


}