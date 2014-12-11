using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;

namespace EveReport.Documents {
  public class TextItem : Item {
    /// <summary>
    /// 表示する文字列を取得、設定。
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// 描画要素の作成を行う。
    /// </summary>
    /// <returns></returns>
    public override Block Create() {
      return new Paragraph(new Run(this.Text));
    }
  }
}
