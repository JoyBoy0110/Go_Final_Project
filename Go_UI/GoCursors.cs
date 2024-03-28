using System.IO;
using System.Windows;
using System.Windows.Input;


namespace Go_UI
{
    public class GoCursors
    {
        public static Cursor WhiteCursor = LoadCursor("Assets\\CursorW.cur");
        public static Cursor BlackCursor = LoadCursor("Assets\\CursorB.cur");

        public static Cursor LoadCursor(string filepath)
        {
            Stream stream = Application.GetResourceStream(new Uri(filepath, UriKind.Relative)).Stream;
            return new Cursor(stream, true);
        }

    }
}
