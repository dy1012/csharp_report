using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace EveReport.Documents {
  [ContentProperty("Items")]
  public class Cell {
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    public Cell() {
      this.Items = new List<Item>();
    }

    /// <summary>
    /// セルのヘッダを取得、設定。
    /// </summary>
    public string Header { get; set; }

    /// <summary>
    /// 描画要素の一覧を取得、設定。
    /// </summary>
    public List<Item> Items { get; set; }

    /// <summary>
    /// 枠線の太さを取得、設定。
    /// </summary>
    public Thickness BorderThickness { get; set; }

    /// <summary>
    /// 枠線のブラシを取得、設定。
    /// </summary>
    public Brush BorderBrush { get; set; }

    /// <summary>
    /// 文字位置を取得、設定。
    /// </summary>
    public TextAlignment TextAlignment { get; set; }

    /// <summary>
    /// 余白を取得、設定。
    /// </summary>
    public Thickness Padding { get; set; }
  }
}
