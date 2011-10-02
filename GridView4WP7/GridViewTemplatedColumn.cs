// (c) Copyright Alexander G. Bykin, Russia 2011
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

namespace System.Windows.Controls
{
    using System.Windows;

    /// <summary>
    /// Represents Templated type of Column
    /// </summary>
    public class GridViewTemplatedColumn : GridViewColumnBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets Template of column
        /// </summary>
        public DataTemplate Template { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Generate content for cell by column binding
        /// </summary>
        /// <returns>ContentPresenter control</returns>
        internal override FrameworkElement GetGeneratedContent()
        {
            return (this.Template == null) ? null : (FrameworkElement)this.Template.LoadContent();
        }

        #endregion
    }
}
