/*
  Copyright © Alexander G. Bykin, Russia 2011
  This source is subject to the Microsoft Public License (Ms-PL).
  Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
  All other rights reserved.
*/

namespace GridView4WP7
{
    using System.Windows.Controls;
    using System.Windows;

    public sealed class GridViewCell : ContentControl
    {
        #region Public Properties

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        #endregion
        
        #region Dependency Properties

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            "IsSelected",
            typeof(bool),
            typeof(GridViewCell),
            new PropertyMetadata(false, OnIsSelectedPropertyChanged));

        #endregion

        #region Constructors

        public GridViewCell()
        {
            DefaultStyleKey = typeof(GridViewCell);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ChangeVisualState(true);
        }

        #endregion

        #region Private Methods

        private static void OnIsSelectedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GridViewCell control = d as GridViewCell;

            if (control == null)
                return;

            control.ChangeVisualState(true);
        }

        private void ChangeVisualState(bool useTransitions)
        {
            VisualStateManager.GoToState(this, IsSelected ? "SelectedState" : "UnselectedState", useTransitions);
        }

        #endregion

    }
}
