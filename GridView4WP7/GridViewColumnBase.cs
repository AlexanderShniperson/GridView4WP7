// (c) Copyright Alexander G. Bykin, Russia 2011
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

namespace System.Windows.Controls
{
    /// <summary>
    /// Represents a Base class for GridView columns
    /// </summary>
    public abstract class GridViewColumnBase : DependencyObject
    {
        /// <summary>
        /// Gets or sets header of column
        /// </summary>
        public object Header { get; set; }

        /// <summary>
        /// Gets or sets width of column
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Generates content for cell of column
        /// </summary>
        /// <returns>FrameworkElement defined at implemented column</returns>
        internal abstract FrameworkElement GetGeneratedContent();
    }
}
