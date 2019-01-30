namespace Grayscale.Cube2x2BookReduce
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;
    using System.Globalization;

    class Program
    {
        /// <summary>
        /// 定跡。
        /// </summary>
        private static Dictionary<string, BookRecord> book;

        public static void Main(string[] args)
        {
            book = new Dictionary<string, BookRecord>();

            ReadBook();

            // 上書き保存。
            File.WriteAllText("./book.txt", ToBookText());
        }

        /// <summary>
        /// 定跡読込。
        /// </summary>
        public static void ReadBook()
        {
            book.Clear();
            if (File.Exists("./book.txt"))
            {
                foreach (var line in File.ReadAllLines("./book.txt"))
                {
                    var tokens = line.Split(' ');
                    book.Add(tokens[0], new BookRecord(tokens[1], int.Parse(tokens[2]), int.Parse(tokens[3])));
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
                        record.Value.ToText()));
                }
            }

            return builder.ToString();
        }
    }
}
