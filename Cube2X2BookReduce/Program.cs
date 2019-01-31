using System;

[assembly: CLSCompliant(true)]
namespace Grayscale.Cube2X2BookReduce
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;

    /// <summary>
    /// プログラム。
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// 定跡ファイルへのパス。
        /// </summary>
        private const string BookPath = "./book.txt";

        /// <summary>
        /// サイズを小さくした定跡ファイルへのパス。
        /// </summary>
        private const string BookMinPath = "./book-min.txt";

        /// <summary>
        /// 定跡。
        /// </summary>
        private static Dictionary<string, BookRecord> book;

        /// <summary>
        /// エントリーポイント。
        /// </summary>
        /// <param name="args">コマンドライン引数。</param>
        public static void Main(string[] args)
        {
            book = new Dictionary<string, BookRecord>();

            ReadBook();

            // 上書き保存。
            File.WriteAllText(Program.BookMinPath, ToBookText());
        }

        /// <summary>
        /// 定跡読込。
        /// </summary>
        public static void ReadBook()
        {
            book.Clear();
            if (File.Exists(Program.BookPath))
            {
                int row = 0;
                foreach (var line in File.ReadAllLines(Program.BookPath))
                {
                    var tokens = line.Split(' ');

                    // 次の一手。
                    var move = int.Parse(tokens[2], CultureInfo.CurrentCulture);

                    // 現局面を正規化する。
                    var currentPositionNormalizer = new Normalizer();
                    (var normalizedCurrentPosition, var normalizedMove) = currentPositionNormalizer.Normalize(Position.Parse(tokens[0]), move);
                    tokens[0] = normalizedCurrentPosition.BoardText;

                    // 前局面を正規化する。
                    var previousPositionNormalizer = new Normalizer();
                    (var normalizedPreviousPosition, var unusedMove) = previousPositionNormalizer.Normalize(Position.Parse(tokens[1]), 0);
                    tokens[1] = normalizedPreviousPosition.BoardText;

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

                    Console.WriteLine(string.Format(
                        CultureInfo.CurrentCulture,
                        "Row: {0}.",
                        row));
                    row++;
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
