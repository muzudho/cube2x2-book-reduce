namespace Grayscale.Cube2X2BookReduce
{
    /// <summary>
    /// 正規化します。
    /// </summary>
    public class Normalizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Normalizer"/> class.
        /// </summary>
        public Normalizer()
        {
            this.IsomorphicPosition = new Position[ArraySize];
            this.IsomorphicX = new int[ArraySize];
            this.IsomorphicY = new int[ArraySize];
            this.IsomorphicZ = new int[ArraySize];
        }

        /// <summary>
        /// Gets 配列の要素数。
        /// </summary>
        public static int ArraySize
        {
            get
            {
                return 4 * 4 * 4;
            }
        }

        /// <summary>
        /// Gets 同型の局面は 24個。
        /// </summary>
        public Position[] IsomorphicPosition { get; private set; }

        /// <summary>
        /// Gets X方向に 90°回した回数。
        /// </summary>
        public int[] IsomorphicX { get; private set; }

        /// <summary>
        /// Gets Y方向に 90°回した回数。
        /// </summary>
        public int[] IsomorphicY { get; private set; }

        /// <summary>
        /// Gets Z方向に 90°回した回数。
        /// </summary>
        public int[] IsomorphicZ { get; private set; }

        /// <summary>
        /// 同型を作ります。
        /// </summary>
        /// <param name="sourcePosition">元となる局面。</param>
        /// <param name="isomorphicIndex">同型の要素番号。</param>
        /// <param name="rotateX">+Xの方へ何回。</param>
        /// <param name="rotateY">+Yの方へ何回。</param>
        /// <param name="rotateZ">+Zの方へ何回。</param>
        /// <returns>局面。</returns>
        public Position CreateIsomorphic(Position sourcePosition, int isomorphicIndex, int rotateX, int rotateY, int rotateZ)
        {
            var pos = Position.Clone(sourcePosition);

            for (int i = 0; i < rotateX; i++)
            {
                // +X
                pos.RotateView(2);
                this.IsomorphicX[isomorphicIndex] += 1;
            }

            for (int i = 0; i < rotateY; i++)
            {
                // +Y
                pos.RotateView(0);
                this.IsomorphicY[isomorphicIndex] += 1;
            }

            for (int i = 0; i < rotateZ; i++)
            {
                // +Z
                pos.RotateView(1);
                this.IsomorphicZ[isomorphicIndex] += 1;
            }

            return pos;
        }

        /// <summary>
        /// 正規化をする。
        /// つまり、24個の局面を作り、そのうち代表的な１つを選ぶ。
        /// </summary>
        /// <param name="sourcePosition">元となる局面。</param>
        /// <param name="handle">回す箇所。</param>
        /// <returns>局面と、回す箇所。</returns>
        public (Position, int) Normalize(Position sourcePosition, int handle)
        {
            int isomorphicIndex = 0;

            for (int rotateZ = 0; rotateZ < 4; rotateZ++)
            {
                for (int rotateY = 0; rotateY < 4; rotateY++)
                {
                    for (int rotateX = 0; rotateX < 4; rotateX++)
                    {
                        this.IsomorphicPosition[isomorphicIndex] = this.CreateIsomorphic(sourcePosition, isomorphicIndex, rotateX, rotateY, rotateZ);
                        isomorphicIndex++;

                        // Console.WriteLine("isomorphicIndex: " + isomorphicIndex);
                    }
                }
            }

            var isomorphicText = new string[ArraySize];
            for (int i = 0; i < ArraySize; i++)
            {
                isomorphicText[i] = this.IsomorphicPosition[i].BoardText;
            }

            // 辞書順ソートする。
            System.Array.Sort(isomorphicText);

            /*
            // 確認表示。
            for (int i = 0; i < StateSize; i++)
            {
                Trace.WriteLine(string.Format(
                    CultureInfo.CurrentCulture,
                    "{0} {1}",
                    i,
                    isomorphicText[i]));
            }
            */

            // 代表の局面。
            var normalizedBoardText = isomorphicText[0];

            // 何番目のものか。
            int normalizedIndex = 0;
            for (; normalizedIndex < ArraySize; normalizedIndex++)
            {
                if (normalizedBoardText == isomorphicText[normalizedIndex])
                {
                    break;
                }
            }

            // 今回 回そうと思っていたハンドルを変更します。
            var normalizedHandle = this.RotateView(normalizedIndex, handle);

            return (Position.Parse(normalizedBoardText), normalizedHandle);
        }

        /// <summary>
        /// 今回 回そうと思っていたハンドルを変更します。
        /// </summary>
        /// <param name="isomorphicIndex">同型の要素番号。</param>
        /// <param name="handle">回す箇所。</param>
        /// <returns>視角を変えたあとの、回す箇所。</returns>
        public int RotateView(int isomorphicIndex, int handle)
        {
            var rotateButtonGroup = new RotateButtonGroup();
            for (int iView = 0; iView < this.IsomorphicX[isomorphicIndex]; iView++)
            {
                // +X
                rotateButtonGroup.RotateView(2);
            }

            for (int iView = 0; iView < this.IsomorphicY[isomorphicIndex]; iView++)
            {
                // +Y
                rotateButtonGroup.RotateView(0);
            }

            for (int iView = 0; iView < this.IsomorphicZ[isomorphicIndex]; iView++)
            {
                // +Z
                rotateButtonGroup.RotateView(1);
            }

            // 視角を変えたので、対応するハンドルの位置が変わる。
            return rotateButtonGroup.LabelArray[handle];
        }
    }
}
