// (c) Copyright Alexander G. Bykin, Russia 2011
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

namespace System.Windows.Controls
{
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Represents Text type of Column
    /// </summary>
    public class GridViewTextColumn : GridViewColumnBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets binding for column cell
        /// </summary>
        public Binding Binding { get; set; }

        #endregion

        #region Private Methods

        /// <summary>
        /// Generate content for cell by column binding
        /// </summary>
        /// <returns>TextBlock element</returns>
        internal override FrameworkElement GetGeneratedContent()
        {
            TextBlock result = new TextBlock();

            if (this.Binding != null)
            {
                result.SetBinding(TextBlock.TextProperty, this.Binding);
            }

            return result;
        }

        #endregion
    }
}
