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
    using System.Diagnostics;

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
        /// 正規化をする。
        /// つまり、64個の局面を作り、そのうち代表的な１つを選ぶ。
        /// </summary>
        public static Position Normalize(Position posSource)
        {
            var isomorphic = new Position[64];

            for (int i = 0; i < 64; i++)
            {
                var pos = Position.Clone(posSource);
                isomorphic[i] = pos;

                if (i % 4 == 1)
                {
                    // +X
                    pos.RotateView(2);
                }
                else if (i % 4 == 2)
                {
                    // +X
                    pos.RotateView(2);
                    pos.RotateView(2);
                }
                else if (i % 4 == 3)
                {
                    // +X
                    pos.RotateView(2);
                    pos.RotateView(2);
                    pos.RotateView(2);
                }

                if (i / 4 % 4 == 1)
                {
                    // +Y
                    pos.RotateView(0);
                }
                else if (i / 4 % 4 == 2)
                {
                    // +Y
                    pos.RotateView(0);
                    pos.RotateView(0);
                }
                else if (i / 4 % 4 == 3)
                {
                    // +Y
                    pos.RotateView(0);
                    pos.RotateView(0);
                    pos.RotateView(0);
                }

                if (i / 16 % 4 == 1)
                {
                    // +Z
                    pos.RotateView(1);
                }
                else if (i / 4 % 4 == 2)
                {
                    // +Z
                    pos.RotateView(1);
                    pos.RotateView(1);
                }
                else if (i / 4 % 4 == 3)
                {
                    // +Z
                    pos.RotateView(1);
                    pos.RotateView(1);
                    pos.RotateView(1);
                }
            }

            var isomorphicText = new string[64];
            for (int i = 0; i < 64; i++)
            {
                isomorphicText[i] = isomorphic[i].GetBoardText();
            }

            // ソートする。
            System.Array.Sort(isomorphicText);

            /*
            // 確認表示。
            for (int i = 0; i < 64; i++)
            {
                Trace.WriteLine(string.Format(
                    CultureInfo.CurrentCulture,
                    "{0} {1}",
                    i,
                    isomorphicText[i]));
            }
            */

            return Position.Parse(isomorphicText[0]);
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

                    // 現局面を正規化する。
                    tokens[0] = Normalize(Position.Parse(tokens[0])).GetBoardText();

                    // 前局面を正規化する。
                    tokens[1] = Normalize(Position.Parse(tokens[1])).GetBoardText();

                    // 次の一手。
                    var move = int.Parse(tokens[2], CultureInfo.CurrentCulture);

                    // 手数。
                    var ply = int.Parse(tokens[3], CultureInfo.CurrentCulture);

                    var record = new BookRecord(tokens[1], move, ply);

                    // 既に追加されているやつがあれば、手数を比較する。
                    if (book.ContainsKey(tokens[0]))
                    {
                        if (ply < book[tokens[0]].Ply)
                        {
                            // 短くなっていれば更新する。
                            book[tokens[0]] = record;
                        }
                    }
                    else
                    {
                        book.Add(tokens[0], record);
                    }
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
