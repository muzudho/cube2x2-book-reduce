namespace Grayscale.Cube2X2BookReduce
{
    using System.Drawing;

    /// <summary>
    /// 色に関するユーティリティー。
    /// </summary>
    public static class ColorHelper
    {
        /// <summary>
        /// 色をアルファベットに変換します。
        /// </summary>
        /// <param name="color">色。</param>
        /// <returns>アルファベット。</returns>
        public static string GetShort(Color color)
        {
            if (color == Color.Pink)
            {
                return "r";
            }
            else if (color == Color.Lime)
            {
                return "g";
            }
            else if (color == Color.SkyBlue)
            {
                return "b";
            }
            else if (color == Color.Orange)
            {
                return "y";
            }
            else if (color == Color.Violet)
            {
                return "v";
            }
            else if (color == Color.LightGray)
            {
                return "g";
            }

            return "?";
        }

        /// <summary>
        /// 数をアルファベットに変換します。
        /// </summary>
        /// <param name="color">色。</param>
        /// <returns>アルファベット。</returns>
        public static string GetShort(int color)
        {
            if (color == 0)
            {
                return "r";
            }
            else if (color == 1)
            {
                return "g";
            }
            else if (color == 2)
            {
                return "b";
            }
            else if (color == 3)
            {
                return "y";
            }
            else if (color == 4)
            {
                return "v";
            }
            else if (color == 5)
            {
                return "g";
            }

            return "?";
        }

        /// <summary>
        /// アルファベットを色に変換します。
        /// </summary>
        /// <param name="ch">アルファベット。</param>
        /// <returns>色。</returns>
        public static Color GetColor(char ch)
        {
            if (ch == 'r')
            {
                return Color.Pink;
            }
            else if (ch == 'g')
            {
                return Color.Lime;
            }
            else if (ch == 'b')
            {
                return Color.SkyBlue;
            }
            else if (ch == 'y')
            {
                return Color.Orange;
            }
            else if (ch == 'v')
            {
                return Color.Violet;
            }
            else if (ch == 'g')
            {
                return Color.LightGray;
            }

            return Color.Black;
        }

        /// <summary>
        /// アルファベットを色に変換します。
        /// </summary>
        /// <param name="ch">アルファベット。</param>
        /// <returns>色。</returns>
        public static int GetNumberFromAlphabet(char ch)
        {
            if (ch == 'r')
            {
                return 0;
            }
            else if (ch == 'g')
            {
                return 1;
            }
            else if (ch == 'b')
            {
                return 2;
            }
            else if (ch == 'y')
            {
                return 3;
            }
            else if (ch == 'v')
            {
                return 4;
            }
            else if (ch == 'g')
            {
                return 5;
            }

            return -1;
        }
    }
}
