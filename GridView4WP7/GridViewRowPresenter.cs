// (c) Copyright Alexander G. Bykin, Russia 2011
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

namespace System.Windows.Controls
{
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Represents Row presenter control that contains Cell controls
    /// </summary>
    public sealed class GridViewRowPresenter : Control
    {
        #region Constants

        private const string ROWPRESENTER_ROOTELEMENT = "RootElement";

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether selected state of row
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        #endregion

        #region Private properties

        /// <summary>
        /// Represents a RootElement of control
        /// </summary>
        private Grid rootElement;

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            "IsSelected",
            typeof(bool),
            typeof(GridViewRowPresenter),
            new PropertyMetadata(false));

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the GridViewRowPresenter class.
        /// </summary>
        public GridViewRowPresenter()
        {
            DefaultStyleKey = typeof(GridViewRowPresenter);
        }

        /// <summary>
        /// Indicates when control is loaded
        /// </summary>
        public override void OnApplyTemplate()
        {
            if (this.rootElement == null)
            {
                this.rootElement = this.GetTemplateChild(ROWPRESENTER_ROOTELEMENT) as Grid;
                this.rootElement.MouseLeftButtonDown += this.OnRootElementMouseLeftButtonDown;
                this.GenerateFields();
            }

            base.OnApplyTemplate();
        }

        /// <summary>
        /// Indicates Row clicked by user
        /// </summary>
        /// <param name="sender">GridViewRowPresenter control</param>
        /// <param name="e">MouseButtonEventArgs arguments</param>
        private void OnRootElementMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GridView gridView = TreeHelper.FindVisualParent<GridView>(this);

            if (gridView == null)
            {
                return;
            }

            gridView.SelectedItem = this.DataContext;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Generate cell fields by columns
        /// </summary>
        private void GenerateFields()
        {
            // Get top parent control that have Columns, because we cant bind it from XAML DataTemplate
            GridView gridView = TreeHelper.FindVisualParent<GridView>(this);

            if (gridView == null)
            {
                return;
            }

            if (gridView.Columns == null || gridView.Columns.Count == 0)
            {
                return;
            }

            this.Margin = new Thickness(0, gridView.RowSpacing, 0, 0);

            for (int i = 0; i < gridView.Columns.Count; i++)
            {
                double columnWidth = gridView.Columns[i].Width;

                this.rootElement.ColumnDefinitions.Add(new ColumnDefinition());

                GridViewCell lastCell = new GridViewCell() { Content = gridView.Columns[i].GetGeneratedContent(), Margin = new Thickness(gridView.CellSpacing, 0, 0, 0) };

                if (lastCell != null)
                {
                    if (columnWidth > 0)
                    {
                        this.rootElement.ColumnDefinitions[i].SetValue(ColumnDefinition.WidthProperty, new GridLength(columnWidth));
                        //// lastCell.Width = columnWidth;
                        gridView.ResizeColumn(i, columnWidth);
                    }

                    lastCell.SetValue(Grid.ColumnProperty, this.rootElement.ColumnDefinitions.Count - 1);
                    this.rootElement.Children.Add(lastCell);

                    lastCell.SetBinding(GridViewCell.IsSelectedProperty, new Binding("IsSelected") { Source = this });
                }
            }
        }

        #endregion
    }
}
