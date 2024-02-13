using System.Text.RegularExpressions;

namespace CommonLibraryCoreMaui.Helper
{
    public static class StringColorHelper
    {
        public static ColorText? GetColorText(string text)
        {
            string pattern = "{color:#(.*)}(.*){color}";
            Regex regex = new Regex(pattern);
            Match mx = regex.Match(text);
            if (mx.Groups.Count == 3)
            {
                return new ColorText() { Text = mx.Groups[2].Value, Color = mx.Groups[1].Value, RawText = regex.Replace(text, mx.Groups[2].Value) };
            }
            return null;
        }

        public static AllColorText? GetAllColorText(string text)
        {
            string pattern = "(?s)(.*){color:#(.*)}(.*){color}(.*)";
            var regex = new Regex(pattern);
            Match mx = regex.Match(text);
            if (mx.Groups.Count == 5)
            {
                return new AllColorText()
                {
                    WholeText = mx.Groups[0].Value,
                    FirstText = mx.Groups[1].Value,
                    SecondText = mx.Groups[3].Value,
                    ThirdText = mx.Groups[4].Value,
                    Color = mx.Groups[2].Value
                };
            }
            return null;
        }
    }

    public struct ColorText
    {
        public string Text { get; set; }
        public string Color { get; set; }
        public string RawText { get; set; }
    }

    public struct AllColorText
    {
        public string FirstText { get; set; }
        public string SecondText { get; set; }
        public string ThirdText { get; set; }
        public string WholeText { get; set; }
        public string Color { get; set; }
    }
}
