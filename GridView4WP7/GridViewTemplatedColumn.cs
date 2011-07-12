/*
  Copyright © Alexander G. Bykin, Russia 2011
  This source is subject to the Microsoft Public License (Ms-PL).
  Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
  All other rights reserved.
*/

namespace GridView4WP7
{
    using System.Windows;

    public class GridViewTemplatedColumn : IGridViewColumn
    {
        #region Public Properties

        public object Header { get; set; }

        public double Width { get; set; }

        public DataTemplate Template { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Generate content for cell by column binding
        /// </summary>
        /// <returns>ContentPresenter</returns>
        public FrameworkElement GetGeneratedContent()
        {
            //ContentPresenter result = new ContentPresenter() { DataContext = dataContext };

            //result.SetValue(ContentPresenter.ContentTemplateProperty, this.Template);

            //if (this.Template != null)
            //    result.Content = this.Template.LoadContent();

            return (this.Template == null) ? null : (FrameworkElement)this.Template.LoadContent();
        }

        #endregion
    }
}
