/*
  Copyright © Alexander G. Bykin, Russia 2011
  This source is subject to the Microsoft Public License (Ms-PL).
  Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
  All other rights reserved.
*/

namespace GridView4WP7
{
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using GridView4WP7.Helpers;
    using System;
    using System.Windows.Data;

    public class GridView : Control
    {
        #region Constants

        private const string GRIDVIEW_ROOTELEMENT = "RootElement";

        private const string GRIDVIEW_ITEMSELEMENT = "ItemsElement";

        private const string GRIDVIEW_HEADERSELEMENT = "HeadersElement";

        #endregion

        #region Public Properties

        /// <summary>
        /// Indicates spacing between cells
        /// </summary>
        public double CellSpacing
        {
            get { return (double)GetValue(CellSpacingProperty); }
            set { SetValue(CellSpacingProperty, value); }
        }

        /// <summary>
        /// Indicates spacing between rows
        /// </summary>
        public double RowSpacing
        {
            get { return (double)GetValue(RowSpacingProperty); }
            set { SetValue(RowSpacingProperty, value); }
        }

        /// <summary>
        /// Indicates items for show at the GridView
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Indicates columns that must be show at GridView
        /// </summary>
        public ObservableCollection<IGridViewColumn> Columns
        {
            get { return (ObservableCollection<IGridViewColumn>)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        /// <summary>
        /// Indicates Selected item in GridView
        /// </summary>
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// Indicates control where show all items
        /// </summary>
        private ItemsControl itemsElement;

        /// <summary>
        /// Indicates control where show column headers
        /// </summary>
        private Grid headersElement;

        /// <summary>
        /// Indicated root element of the control
        /// </summary>
        private Grid rootElement;

        /// <summary>
        /// Indicates that all controls is initialized for working
        /// </summary>
        private bool isControlsInitialized;

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty RowSpacingProperty = DependencyProperty.Register(
            "RowSpacing",
            typeof(double),
            typeof(GridView),
            new PropertyMetadata(2d));

        public static readonly DependencyProperty CellSpacingProperty = DependencyProperty.Register(
            "CellSpacing",
            typeof(double),
            typeof(GridView),
            new PropertyMetadata(2d));

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource",
            typeof(IEnumerable),
            typeof(GridView),
            new PropertyMetadata(OnItemsSourcePropertyChanged));

        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register(
            "Columns",
            typeof(ObservableCollection<IGridViewColumn>),
            typeof(GridView),
            new PropertyMetadata(new ObservableCollection<IGridViewColumn>()));

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(object),
            typeof(GridView),
            new PropertyMetadata(null, OnSelectedItemPropertyChanged));

        #endregion

        #region Events

        public event EventHandler<EventArgs> SelectedItemChanged;

        #endregion

        #region Constructors

        public GridView()
        {
            DefaultStyleKey = typeof(GridView);
            this.isControlsInitialized = false;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.InitializeControls();

            this.CreateHeaders();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnItemsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnSelectedItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GridView control = d as GridView;

            if (control == null || !control.isControlsInitialized)
                return;

            if (e.NewValue == null)
                return;

            GridViewRowPresenter newSelectionRow = TreeHelper.FindChild<GridViewRowPresenter>(control.itemsElement.ItemContainerGenerator.ContainerFromItem(e.NewValue));

            if (newSelectionRow == null)
                return;

            if (e.OldValue == null)
            {
                newSelectionRow.IsSelected = true;
            }
            else if (e.OldValue.GetType() == e.NewValue.GetType())
            {
                if (e.NewValue == e.OldValue)
                    return;

                GridViewRowPresenter selectedRow = TreeHelper.FindChild<GridViewRowPresenter>(control.itemsElement.ItemContainerGenerator.ContainerFromItem(e.OldValue));

                newSelectionRow.IsSelected = true;

                if (selectedRow == null)
                    return;

                selectedRow.IsSelected = false;
            }

            if (control.SelectedItemChanged != null)
            {
                control.SelectedItemChanged(control, EventArgs.Empty);
            }

        }

        /// <summary>
        /// Initialize controls
        /// </summary>
        private void InitializeControls()
        {
            if (this.isControlsInitialized)
                return;

            bool fail = false;

            if (this.rootElement == null)
            {
                this.rootElement = this.GetTemplateChild(GRIDVIEW_ROOTELEMENT) as Grid;
            }

            if (this.headersElement == null)
            {
                this.headersElement = this.GetTemplateChild(GRIDVIEW_HEADERSELEMENT) as Grid;
            }

            if (this.itemsElement == null)
            {
                this.itemsElement = this.GetTemplateChild(GRIDVIEW_ITEMSELEMENT) as ItemsControl;
            }

            if (this.rootElement == null || this.headersElement == null || this.itemsElement == null)
                fail = true;
            else
            {
                this.LayoutUpdated -= OnGridViewLayoutUpdated;
                this.LayoutUpdated += OnGridViewLayoutUpdated;
            }

            this.isControlsInitialized = !fail;
        }

        /// <summary>
        /// Indicates GridView loaded or updated
        /// </summary>
        /// <param name="sender">GridView</param>
        /// <param name="e">EventArgs</param>
        private void OnGridViewLayoutUpdated(object sender, System.EventArgs e)
        {
            if (this.isControlsInitialized)
            {
                BindingExpression be = this.itemsElement.GetBindingExpression(ItemsControl.ItemsSourceProperty);

                if (be == null || be.DataItem == null)
                {
                    this.itemsElement.SetBinding(ItemsControl.ItemsSourceProperty, new System.Windows.Data.Binding("ItemsSource") { Source = this });
                    this.UpdateColumnsSize();
                }
            }
        }

        /// <summary>
        /// Create headers by columns
        /// </summary>
        private void CreateHeaders()
        {
            if (this.Columns == null)
                return;

            int columnCount = this.Columns.Count;

            if (columnCount == 0)
                return;

            this.RemoveHeaders();

            for (int i = 0; i < columnCount; i++)
            {
                double columnWidth = this.Columns[i].Width;

                GridViewHeader headerElement = new GridViewHeader() { Content = this.Columns[i].Header, Margin = new Thickness(this.CellSpacing, 0, 0, 0) };

                this.headersElement.ColumnDefinitions.Add(new ColumnDefinition());
                int lastColumnIndex = this.headersElement.ColumnDefinitions.Count - 1;
                ColumnDefinition lastColumn = this.headersElement.ColumnDefinitions[lastColumnIndex];

                if (columnWidth > 0)
                {
                    lastColumn.SetValue(ColumnDefinition.WidthProperty, new GridLength(columnWidth));
                    headerElement.SetValue(GridViewHeader.WidthProperty, columnWidth);
                }

                headerElement.SetValue(Grid.ColumnProperty, lastColumnIndex);
                this.headersElement.Children.Add(headerElement);
            }
        }

        /// <summary>
        /// Remove existing headers of columns
        /// </summary>
        private void RemoveHeaders()
        {
            this.headersElement.Children.Clear();
            this.headersElement.ColumnDefinitions.Clear();
        }


        #endregion

        #region Public Methods

        /// <summary>
        /// Update column width
        /// </summary>
        public void ResizeColumn(int columnIndex, double width)
        {
            //int itemsAmount = this.itemsElement.Items.Count - 1;
            int headerColumnAmount = this.headersElement.ColumnDefinitions.Count - 1;

            if (columnIndex > headerColumnAmount || width == 0)
                return;

            try
            {
                this.headersElement.ColumnDefinitions[columnIndex].SetValue(ColumnDefinition.WidthProperty, new GridLength(width));
                this.headersElement.Children[columnIndex].SetValue(GridViewHeader.WidthProperty, width);

                /*for (int i = 0; i <= itemsAmount; i++)
                {
                    DependencyObject itemTopContainer = this.itemsElement.ItemContainerGenerator.ContainerFromIndex(i);
                    Grid itemContainer = Helpers.TreeHelper.FindChild<Grid>(itemTopContainer);

                    for (int j = 0; j < itemContainer.ColumnDefinitions.Count; j++)
                    {
                        if (j > headerColumnAmount)
                            break;

                        double itemContainerColumnWidth = itemContainer.ColumnDefinitions[j].ActualWidth;

                        if (this.headersElement.ColumnDefinitions[j].Width.Value < itemContainerColumnWidth)
                            this.headersElement.ColumnDefinitions[j].SetValue(ColumnDefinition.WidthProperty, new GridLength(itemContainerColumnWidth));
                    }
                }*/
            }
            catch { }
        }

        /// <summary>
        /// Update columns width by column cell content
        /// </summary>
        public void UpdateColumnsSize()
        {
            int itemsAmount = this.itemsElement.Items.Count - 1;
            int headerColumnAmount = this.headersElement.ColumnDefinitions.Count - 1;

            try
            {
                for (int i = 0; i <= itemsAmount; i++)
                {
                    DependencyObject itemTopContainer = this.itemsElement.ItemContainerGenerator.ContainerFromIndex(i);

                    if (itemTopContainer == null)
                        continue;

                    Grid itemContainer = Helpers.TreeHelper.FindChild<Grid>(itemTopContainer);

                    if (itemContainer == null)
                        continue;

                    for (int j = 0; j < itemContainer.ColumnDefinitions.Count; j++)
                    {
                        if (j > headerColumnAmount)
                            break;

                        double itemContainerColumnWidth = itemContainer.ColumnDefinitions[j].ActualWidth;

                        if (this.headersElement.ColumnDefinitions[j].Width.Value < itemContainerColumnWidth)
                            this.headersElement.ColumnDefinitions[j].SetValue(ColumnDefinition.WidthProperty, new GridLength(itemContainerColumnWidth));
                    }
                }
            }
            catch { }
        }

        #endregion
    }
}
