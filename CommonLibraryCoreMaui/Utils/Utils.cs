using DotLiquid;
using System;
using System.Text.RegularExpressions;

namespace CommonLibraryCoreMaui
{
    public static class Utils
    {
        public static bool HighlightWaitingPatient(string span, int minutes = 5)
        {
            try
            {
                Regex regex = new Regex(@"^(?:(?:)?([0-5]?\d):)?([0-5]?\d)$");
                Match match = regex.Match(span);
                if (match.Success)
                {
                    //mm:ss
                    span = string.Concat("00:", span);
                }
                double seconds = TimeSpan.Parse(span).TotalSeconds;
                if (seconds > TimeSpan.FromMinutes(minutes).TotalSeconds) return true;
            }
            catch { }
            return false;
        }

        public static string ToDisplayDate(this DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue)
                return "MM/dd/yyy";
            return dateTime.ToString("MM-dd-yyyy");
        }

        //public class GlobalsStateHelper : IDisposable
        //{
        //    ISharedPreferences prefs;
        //    ISharedPreferencesEditor editor;

        //    public GlobalsStateHelper(Context context)
        //    {
        //        prefs = PreferenceManager.GetDefaultSharedPreferences(context);
        //    }

        //    public void Dispose()
        //    {
        //        if (prefs != null) prefs.Dispose();
        //        if (editor != null) editor.Dispose();
        //    }

        //    public GlobalState GetState()
        //    {
        //        string globals = prefs.GetString("globals", string.Empty);
        //        if (!string.IsNullOrEmpty(globals))
        //        {
        //            return Newtonsoft.Json.JsonConvert.DeserializeObject<GlobalState>(globals);
        //        }
        //        return new GlobalState();
        //    }

        //    public void SetState(GlobalState state)
        //    {
        //        if (editor is null) editor = prefs.Edit();
        //        editor.PutString("globals", Newtonsoft.Json.JsonConvert.SerializeObject(state));
        //        editor.Apply();
        //    }
        //}
    }
}
