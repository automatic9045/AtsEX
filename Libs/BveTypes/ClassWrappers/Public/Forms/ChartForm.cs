﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BveTypes.ClassWrappers
{
    /// <summary>
    /// 「車両物理量」フォームを表します。
    /// </summary>
    public class ChartForm : ClassWrapperBase
    {
        /// <summary>
        /// オリジナル オブジェクトから <see cref="ChartForm"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="src">ラップするオリジナル オブジェクト。</param>
        protected ChartForm(object src) : base(src)
        {
        }

        /// <summary>
        /// オリジナル オブジェクトからラッパーのインスタンスを生成します。
        /// </summary>
        /// <param name="src">ラップするオリジナル オブジェクト。</param>
        /// <returns>オリジナル オブジェクトをラップした <see cref="ChartForm"/> クラスのインスタンス。</returns>
        [CreateClassWrapperFromSource]
        public static ChartForm FromSource(object src) => src is null ? null : new ChartForm(src);
    }
}
