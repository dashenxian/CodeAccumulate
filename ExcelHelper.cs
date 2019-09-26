  public class ExcelHelper
    {
        /// <summary>
        /// excel单元格地址转行（从1开始）、列（从1开始）号
        /// </summary>
        /// <param name="address">单元格地址</param>
        /// <returns>行号（从1开始）列号（从1开始）</returns>
        public static (int row, int col) CalcRowCol(string address)
        {
            var upAd = address.ToUpper();

            if (Regex.IsMatch(upAd, @"[^A-Z0-9]+"))
            {
                throw new ArgumentException($"参数{nameof(address)}单元格地址不合法");
            }
            int row = 0, col = 0;
            
            string rowStr = "";
            for (int i = 0; i < upAd.Length; i++)
            {
                var item = upAd[i];

                if (char.IsDigit(item))
                {
                    rowStr += item;
                }
                else
                {
                    col = col * 26 + (((byte)item) - 64);
                    //colStr += item;
                }
            }
            int.TryParse(rowStr, out row);

            return (row, col);
        }
        /// <summary>
        /// excel单元格行（从1开始）、列（从1开始）号转地址
        /// </summary>
        /// <param name="row">行号（从1开始）</param>
        /// <param name="col">列号（从1开始）</param>
        /// <returns>单元格地址</returns>
        public static string CalcAddress(int row, int col)
        {
            var rowAd = row + "";
            var colAd = "";
            var quotient = col;
            do
            {
                var remainder = quotient % 26;
                remainder = remainder == 0 ? 26 : remainder;
                quotient = (quotient - (remainder / 26)) / 26;
                colAd = (char)(remainder + 64) + colAd;
            } while (quotient > 0);

            return colAd + rowAd;
        }
    }
