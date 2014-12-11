using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;

namespace EveReport.Documents {
  public abstract class Item {
    /// <summary>
    /// 文字のサイズを取得、設定。
    /// </summary>
    public int FontSize { get; set; }

    /// <summary>
    /// 文字の太さを取得、設定。
    /// </summary>
    public FontWeight FontWeight { get; set; }

    /// <summary>
    /// 描画要素の作成を行う。
    /// </summary>
    /// <returns></returns>
    public abstract Block Create();
  }
}
