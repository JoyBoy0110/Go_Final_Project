using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Go_UI
{
    public class GoCursors
    {
        public static Cursor WhiteCursor = LoadCursor("Assets\\CursorW.cur");// a cursor that represents the white player
        public static Cursor BlackCursor = LoadCursor("Assets\\CursorB.cur");// a cursor that represents the black player

        /// <summary>
        /// a function that loads a new cursor by a path
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static Cursor LoadCursor(string filepath)
        {
            Stream stream = Application.GetResourceStream(new Uri(filepath, UriKind.Relative)).Stream;
            return new Cursor(stream, true);
        }

    }
}
