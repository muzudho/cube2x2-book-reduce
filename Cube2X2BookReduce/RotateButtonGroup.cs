namespace Grayscale.Cube2X2BookReduce
{
    /// <summary>
    /// 回転ボタン。
    /// </summary>
    public class RotateButtonGroup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RotateButtonGroup"/> class.
        /// </summary>
        public RotateButtonGroup()
        {
            this.LabelArray = new int[]
            {
                0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11,
            };
        }

        /// <summary>
        /// Gets or sets ラベルの配列。
        /// </summary>
        public int[] LabelArray { get; set; }

        /// <summary>
        /// 回転ボタンをずらします。
        /// </summary>
        /// <param name="handleA">回す箇所A。</param>
        /// <param name="handleB">回す箇所B。</param>
        /// <param name="handleC">回す箇所C。</param>
        /// <param name="handleD">回す箇所D。</param>
        public void Shift4(int handleA, int handleB, int handleC, int handleD)
        {
            var tempLabel = this.LabelArray[handleA];
            this.LabelArray[handleA] = this.LabelArray[handleB];
            this.LabelArray[handleB] = this.LabelArray[handleC];
            this.LabelArray[handleC] = this.LabelArray[handleD];
            this.LabelArray[handleD] = tempLabel;
        }

        /// <summary>
        /// 回転ボタンを回します。
        /// </summary>
        /// <param name="handle">回す箇所。0～5。</param>
        public void RotateView(int handle)
        {
            switch (handle)
            {
                case 0:

                    // +Y
                    this.Shift4(11, 9, 5, 3);
                    this.Shift4(10, 8, 4, 2);
                    break;
                case 1:

                    // +Z
                    this.Shift4(0, 4, 6, 10);
                    this.Shift4(1, 5, 7, 11);
                    break;
                case 2:

                    // +X
                    this.Shift4(8, 2, 6, 0);
                    this.Shift4(9, 7, 3, 1);
                    break;
                case 3:

                    // -Y
                    this.Shift4(2, 4, 8, 10);
                    this.Shift4(3, 5, 9, 11);
                    break;
                case 4:

                    // -Z
                    this.Shift4(0, 10, 6, 4);
                    this.Shift4(1, 11, 7, 5);
                    break;
                case 5:

                    // -X
                    this.Shift4(0, 2, 4, 8);
                    this.Shift4(1, 3, 5, 9);
                    break;
            }
        }
    }
}
