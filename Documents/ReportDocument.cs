using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace EveReport.Documents {
  public class ReportDocument : FlowDocument {
    public ReportDocument() {
      this.ColumnWidth = double.PositiveInfinity;
    }

    /// <summary>
    /// RowTemplate 依存関係プロパティを識別。
    /// </summary>
    public static readonly DependencyProperty RowTemplateProperty = DependencyProperty.Register("RowTemplate", typeof(Template), typeof(ReportDocument), new PropertyMetadata(null, OnRowTemplatePropertyChanged));

    /// <summary>
    /// HeaderTemplate 依存関係プロパティを識別。
    /// </summary>
    public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof(Template), typeof(ReportDocument), new PropertyMetadata(null, OnHeaderTemplatePropertyChanged));

    /// <summary>
    /// FooterTemplate 依存関係プロパティを識別。
    /// </summary>
    public static readonly DependencyProperty FooterTemplateProperty = DependencyProperty.Register("FooterTemplate", typeof(Template), typeof(ReportDocument), new PropertyMetadata(null, OnFooterTemplatePropertyChanged));

    /// <summary>
    /// Source 依存関係プロパティを識別。
    /// </summary>
    public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(IEnumerable), typeof(ReportDocument), new PropertyMetadata(null, OnSourcePropertyChanged));

    /// <summary>
    /// 行のテンプレートを取得、設定。
    /// </summary>
    public Template RowTemplate {
      get { return this.GetValue(RowTemplateProperty) as Template; }
      set { this.SetValue(RowTemplateProperty, value); }
    }

    /// <summary>
    /// ヘッダのテンプレートを取得、設定。
    /// </summary>
    public Template HeaderTemplate {
      get { return this.GetValue(HeaderTemplateProperty) as Template; }
      set { this.SetValue(HeaderTemplateProperty, value); }
    }

    /// <summary>
    /// フッダのテンプレートを取得、設定。
    /// </summary>
    public Template FooterTemplate {
      get { return this.GetValue(FooterTemplateProperty) as Template; }
      set { this.SetValue(FooterTemplateProperty, value); }
    }

    /// <summary>
    /// ソースを取得、設定。
    /// </summary>
    public IEnumerable Source {
      get { return this.GetValue(SourceProperty) as IEnumerable; }
      set { this.SetValue(SourceProperty, value); }
    }
    
    #region Dependency property callbacks

    /// <summary>
    /// 
    /// </summary>
    /// <param name="d"></param>
    /// <param name="e"></param>
    static void OnSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      ((ReportDocument)d).Draw();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="d"></param>
    /// <param name="e"></param>
    static void OnRowTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      ((ReportDocument)d).Draw();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="d"></param>
    /// <param name="e"></param>
    static void OnHeaderTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="d"></param>
    /// <param name="e"></param>
    static void OnFooterTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
    }

    #endregion

    /// <summary>
    /// 描画を行う。
    /// </summary>
    void Draw() {
      var headerTemplate = this.HeaderTemplate;
      var footerTemplate = this.FooterTemplate;
      var template = this.RowTemplate;
      var source = this.Source;

      if (template == null || source == null)
        return;

      this.Blocks.Clear();

      var table = new Table();
      table.CellSpacing = 0;

      var rowGroup = new TableRowGroup();
      rowGroup.Rows.Add(this.CreateHeader(template));

      foreach (var item in source) {
        var row = this.CreateRow(template);
        row.DataContext = item;
        rowGroup.Rows.Add(row);
      }
      
      if (headerTemplate != null) {
        var headerGroup = new TableRowGroup();
        headerGroup.Rows.Add(this.CreateRow(headerTemplate));
        table.RowGroups.Add(headerGroup);
      }

      table.RowGroups.Add(rowGroup);

      if (footerTemplate != null) {
        var footerGroup = new TableRowGroup();
        footerGroup.Rows.Add(this.CreateRow(footerTemplate));
        table.RowGroups.Add(footerGroup);
      }

      this.Blocks.Add(table);
    }

    /// <summary>
    /// 見出し行を作成。
    /// </summary>
    /// <param name="template"></param>
    /// <returns></returns>
    TableRow CreateHeader(Template template) {
      var result = new TableRow();
      foreach (var cell in template.Cells) {
        var addCell = this.CreateCell(new Cell() {
          Items = new List<Item>() {
            new TextItem() {  Text = cell.Header }
          }
        });
        addCell.BorderThickness = new Thickness(0, 0, 0, 2);
        addCell.BorderBrush = cell.BorderBrush;
        addCell.Padding = cell.Padding;
        result.Cells.Add(addCell);
      }

      return result;
    }

    /// <summary>
    /// テーブルの行を作成する。
    /// </summary>
    /// <param name="template"></param>
    /// <returns></returns>
    TableRow CreateRow(Template template) {
      var result = new TableRow();
      foreach (var cell in template.Cells) {
        var addCell = this.CreateCell(cell);
        addCell.BorderThickness = cell.BorderThickness;
        addCell.BorderBrush = cell.BorderBrush;
        addCell.TextAlignment = cell.TextAlignment;
        addCell.Padding = cell.Padding;
        result.Cells.Add(addCell);
      }

      return result;
    }

    /// <summary>
    /// テーブルの列を作成する。
    /// </summary>
    /// <param name="cell"></param>
    /// <returns></returns>
    TableCell CreateCell(Cell cell) {
      var result = new TableCell();
      foreach (var item in cell.Items) {
        var addBlock = item.Create();
        addBlock.FontSize = item.FontSize <= 0 ? addBlock.FontSize : item.FontSize;
        addBlock.FontWeight = item.FontWeight;
        result.Blocks.Add(addBlock);
      }

      return result;
    }
  }
}
