using System;
using System.Collections;
using System.Collections.Generic;

namespace WJ.Infrastructure.Office
{
    public class WorksheetCollection : ICollection<Worksheet>
    {
        IList<Worksheet> worksheets = new List<Worksheet>();

        #region ICollection<Worksheet> 成员

        public void Add(Worksheet item)
        {
            worksheets.Add(item);
        }

        public void Clear()
        {
            worksheets.Clear();
        }

        public bool Contains(Worksheet item)
        {
            return worksheets.Contains(item);
        }

        public void CopyTo(Worksheet[] array, int arrayIndex)
        {
            worksheets.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return worksheets.Count; }
        }

        public bool IsReadOnly
        {
            get { return worksheets.IsReadOnly; }
        }

        public bool Remove(Worksheet item)
        {
            return worksheets.Remove(item);
        }

        #endregion

        #region IEnumerable<Worksheet> 成员

        public IEnumerator<Worksheet> GetEnumerator()
        {
            return worksheets.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return worksheets.GetEnumerator();
        }

        #endregion
    }
}
