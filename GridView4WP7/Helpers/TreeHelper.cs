// (c) Copyright Alexander G. Bykin, Russia 2011
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

namespace System.Windows.Controls
{
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Implementation of this class founded at Internet, don't remember who author, but thank you :)
    /// </summary>
    public class TreeHelper
    {
        /// <summary>
        /// Find visual parent of child element
        /// </summary>
        /// <typeparam name="T">Target type of element</typeparam>
        /// <param name="element">Child element</param>
        /// <returns>UIElement of parent element or null if not found</returns>
        public static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;

            while (parent != null)
            {
                T correctlyTyped = parent as T;

                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }

                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }

            return null;
        }

        /// <summary>
        /// Find visual child at parent element
        /// </summary>
        /// <typeparam name="T">Target type of element</typeparam>
        /// <param name="parent">Parent element</param>
        /// <returns>UIElement of child element or null if not found</returns>
        public static T FindChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null)
            {
                return null;
            }

            T childElement = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                T childType = child as T;

                if (childType == null)
                {
                    childElement = FindChild<T>(child);

                    if (childElement != null)
                    {
                        break;
                    }
                }
                else
                {
                    childElement = (T)child;
                    break;
                }
            }

            return childElement;
        }
    }
}
