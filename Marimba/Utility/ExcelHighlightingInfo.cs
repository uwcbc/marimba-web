using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marimba.Utility
{
    class ExcelHighlightingInfo
    {
        /// <summary>
        /// Column of a row to check
        /// </summary>
        public int column;

        /// <summary>
        /// Highlight a row if the value in the (column + 1)th entry matches this value
        /// </summary>
        public string matchExpression;

        /// <summary>
        /// The color to highlight the row with
        /// </summary>
        public System.Drawing.Color colour;
    }
}
