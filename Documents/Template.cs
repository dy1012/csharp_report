using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace EveReport.Documents {
  [ContentProperty("Cells")]
  public class Template {
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    public Template() {
      this.Cells = new List<Cell>();
    }

    /// <summary>
    /// セルの一覧を取得、設定。
    /// </summary>
    public List<Cell> Cells { get; set; }
  }
}
