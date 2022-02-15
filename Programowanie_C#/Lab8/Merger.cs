using System.Collections;

namespace Lab8
{
    /// <summary>
    /// Interfejs łączenia dwóch sekwencji w jedną według jakiejś reguły
    /// </summary>
    public interface IMerger
    {
        /// <summary>
        /// Nazwa metody łączenia sekwencji
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Metoda łącząca sekwencji
        /// </summary>
        /// <param name="IEnumerable1">Pierwsza sekwencji</param>
        /// <param name="IEnumerable2">Druga sekwencji</param>
        /// <returns>Sekwencja będąca wynikiem połączenia pierwszej i drugiej sekwencji</returns>
        IEnumerable Merge(IEnumerable sequence1, IEnumerable sequence2);
    }
    class Add : IMerger
    {
        public string Name => "Add: ";

        public IEnumerable Merge(IEnumerable sequence1, IEnumerable sequence2)
        {
            IEnumerator seq1 = sequence1.GetEnumerator();
            IEnumerator seq2 = sequence2.GetEnumerator();
            while (seq1.MoveNext() && seq2.MoveNext())
                yield return (int)seq1.Current + (int)seq2.Current;
        }
    }
}
