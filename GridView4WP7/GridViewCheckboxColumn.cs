// (c) Copyright Alexander G. Bykin, Russia 2011
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

namespace System.Windows.Controls
{
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Represents Text Checkbox of Column
    /// </summary>
    public class GridViewCheckboxColumn : GridViewColumnBase
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
        /// <returns>Checkbox element</returns>
        internal override FrameworkElement GetGeneratedContent()
        {
            CheckBox result = new CheckBox();

            if (this.Binding != null)
            {
                result.SetBinding(CheckBox.IsCheckedProperty, this.Binding);
            }

            result.SetValue(CheckBox.HorizontalAlignmentProperty, HorizontalAlignment.Center);

            return result;
        }

        #endregion
    }
}
