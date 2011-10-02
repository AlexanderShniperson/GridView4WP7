// (c) Copyright Alexander G. Bykin, Russia 2011
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

namespace System.Windows.Controls
{
    using System.Windows;

    /// <summary>
    /// GridViewCell content control that represents data of cell.
    /// </summary>
    [TemplateVisualState(Name = VisualStates.SelectedState, GroupName = VisualStates.GroupSelectionStates)]
    [TemplateVisualState(Name = VisualStates.UnselectedState, GroupName = VisualStates.GroupSelectionStates)]
    public sealed class GridViewCell : ContentControl
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether selected state of cell
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the GridViewCell class.
        /// </summary>
        public GridViewCell()
        {
            DefaultStyleKey = typeof(GridViewCell);
        }

        /// <summary>
        /// Indicates when control is loaded
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.ChangeVisualState(true);
        }

        #endregion

        #region Private Methods

        private static void OnIsSelectedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GridViewCell control = d as GridViewCell;

            if (control == null)
            {
                return;
            }

            control.ChangeVisualState(true);
        }

        /// <summary>
        /// Change visual state of control
        /// </summary>
        /// <param name="useTransitions">Indicates to use a VisualTransition to transition between states</param>
        private void ChangeVisualState(bool useTransitions)
        {
            VisualStateManager.GoToState(this, this.IsSelected ? VisualStates.SelectedState : VisualStates.UnselectedState, useTransitions);
        }

        #endregion
    }
}
