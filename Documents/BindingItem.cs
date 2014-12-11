using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Documents;

namespace EveReport.Documents {
  public class BindingItem : Item {
    /// <summary>
    /// バインディングを行うソースを取得、設定。
    /// </summary>
    public BindingBase Source { get; set; }

    /// <summary>
    /// 描画要素の作成を行う。
    /// </summary>
    /// <returns></returns>
    public override Block Create() {
      var run = new Run();
      run.SetBinding(Run.TextProperty, this.Source);

      return new Paragraph(run);
    }
  }
}
