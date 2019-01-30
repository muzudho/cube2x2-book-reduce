using System;

[assembly: CLSCompliant(true)]
namespace Grayscale.Cube2X2BookReduce
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;
    using System.Globalization;

    class Program
    {
        private const string BOOK_PATH = "./book.txt";
        private const string BOOK_MIN_PATH = "./book-min.txt";

        /// <summary>
        /// 定跡。
        /// </summary>
        private static Dictionary<string, BookRecord> book;

        public static void Main(string[] args)
        {
            book = new Dictionary<string, BookRecord>();

            ReadBook();

            // 上書き保存。
            File.WriteAllText(Program.BOOK_MIN_PATH, ToBookText());
        }

        /// <summary>
        /// 定跡読込。
        /// </summary>
        public static void ReadBook()
        {
            book.Clear();
            if (File.Exists(Program.BOOK_PATH))
            {
                foreach (var line in File.ReadAllLines(Program.BOOK_PATH))
                {
                    var tokens = line.Split(' ');
                    var record = new BookRecord(
                        tokens[1],
                        int.Parse(tokens[2], CultureInfo.CurrentCulture),
                        int.Parse(tokens[3], CultureInfo.CurrentCulture));
                    book.Add(tokens[0], record);
                }
            }
        }

        /// <summary>
        /// 定跡全文。
        /// </summary>
        /// <returns>定跡。</returns>
        public static string ToBookText()
        {
            var builder = new StringBuilder();
            foreach (var record in book)
            {
                // 14手以下の定跡だけ残す。
                if (record.Value.Ply < 15)
                {
                    builder.AppendLine(string.Format(
                        CultureInfo.CurrentCulture,
                        "{0} {1}",
                        record.Key,
                        record.Value.Handle));
                }
            }

            return builder.ToString();
        }
    }
}
