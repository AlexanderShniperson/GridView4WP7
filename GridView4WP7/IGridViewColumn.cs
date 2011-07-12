/*
  Copyright © Alexander G. Bykin, Russia 2011
  This source is subject to the Microsoft Public License (Ms-PL).
  Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
  All other rights reserved.
*/

namespace GridView4WP7
{
    using System.Windows;

    public interface IGridViewColumn
    {
        object Header { get; set; }
        double Width { get; set; }
        FrameworkElement GetGeneratedContent();
    }
}
