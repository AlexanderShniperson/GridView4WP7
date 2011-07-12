/*
  Copyright © Alexander G. Bykin, Russia 2011
  This source is subject to the Microsoft Public License (Ms-PL).
  Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
  All other rights reserved.
*/

namespace GridView4WP7
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class GridViewTextColumn : IGridViewColumn
    {
        #region Public Properties

        /// <summary>
        /// Indicates binding for column cell
        /// </summary>
        public Binding Binding { get; set; }

        /// <summary>
        /// Indicates column header
        /// </summary>
        public object Header { get; set; }

        /// <summary>
        /// Indicates column width
        /// </summary>
        public double Width { get; set; }

        #endregion

        #region Constructors

        public GridViewTextColumn()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Generate content for cell by column binding
        /// </summary>
        /// <returns>TextBlock</returns>
        public FrameworkElement GetGeneratedContent()
        {
            TextBlock result = new TextBlock();

            if (this.Binding != null)
            {
                Binding newBinding = new Binding()
                {
                    Converter = this.Binding.Converter,
                    ConverterCulture = this.Binding.ConverterCulture,
                    ConverterParameter = this.Binding.ConverterParameter,
                    Mode = this.Binding.Mode,
                    NotifyOnValidationError = this.Binding.NotifyOnValidationError,
                    Path = this.Binding.Path,
                    Source = this.Binding.Source,
                    ValidatesOnExceptions = this.Binding.ValidatesOnExceptions
                };

                result.SetBinding(TextBlock.TextProperty, newBinding);
            }

            return result;
        }

        #endregion
    }
}
