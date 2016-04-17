using System;
using System.Collections;
using System.Collections.Generic;

namespace WJ.Infrastructure.Office
{
    /// <summary>
    /// Excel样式集
    /// </summary>
    public class ExcelStyleCollection : ICollection<ExcelStyle>
    {
        public ExcelStyleCollection()
        {
            styles.Add(new ExcelStyle());
        }
        IList<ExcelStyle> styles = new List<ExcelStyle>();

        #region ICollection<ExcelStyle> 成员

        public void Add(ExcelStyle item)
        {
            styles.Add(item);
        }

        public void Clear()
        {
            styles.Clear();
        }

        public bool Contains(ExcelStyle item)
        {
            return styles.Contains(item);
        }

        public void CopyTo(ExcelStyle[] array, int arrayIndex)
        {
            styles.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return styles.Count; }
        }

        public bool IsReadOnly
        {
            get { return styles.IsReadOnly; }
        }

        public bool Remove(ExcelStyle item)
        {
            return styles.Remove(item);
        }

        #endregion

        #region IEnumerable<ExcelStyle> 成员

        public IEnumerator<ExcelStyle> GetEnumerator()
        {
            return styles.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return styles.GetEnumerator();
        }

        #endregion
    }
}
