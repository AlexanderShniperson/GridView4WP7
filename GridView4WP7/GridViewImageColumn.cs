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

    public class GridViewImageColumn : IGridViewColumn
    {
        #region Public Properties

        public object Header { get; set; }

        public double Width { get; set; }

        public Binding Binding { get; set; }

        public double ImageWidth { get; set; }

        public double ImageHeight { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Generate content for cell by column binding
        /// </summary>
        /// <returns>Image</returns>
        public FrameworkElement GetGeneratedContent()
        {
            Image result = new Image();

            result.Width = this.ImageWidth;

            result.Height = this.ImageHeight;

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

                result.SetBinding(Image.SourceProperty, newBinding);
            }

            return result;
        }

        #endregion
    }
}
