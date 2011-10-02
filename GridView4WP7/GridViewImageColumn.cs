// (c) Copyright Alexander G. Bykin, Russia 2011
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

namespace System.Windows.Controls
{
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Represents Image type of Column
    /// </summary>
    public class GridViewImageColumn : GridViewColumnBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets binding for column cell
        /// </summary>
        public Binding Binding { get; set; }

        /// <summary>
        /// Gets or sets Image width
        /// </summary>
        public double ImageWidth { get; set; }

        /// <summary>
        /// Gets or sets Images height
        /// </summary>
        public double ImageHeight { get; set; }

        #endregion

        #region Private Methods

        /// <summary>
        /// Generate content for cell by column binding
        /// </summary>
        /// <returns>Image element</returns>
        internal override FrameworkElement GetGeneratedContent()
        {
            Image result = new Image();

            if (this.Binding != null)
            {
                result.SetBinding(Image.SourceProperty, this.Binding);
            }

            result.SetBinding(Image.WidthProperty, new Binding("ImageWidth") { Source = this });
            result.SetBinding(Image.HeightProperty, new Binding("ImageHeight") { Source = this });

            return result;
        }

        #endregion
    }
}
