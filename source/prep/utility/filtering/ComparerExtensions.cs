using System;
using System.Collections.Generic;

namespace prep.utility.filtering
{
  public static class ComparerExtensions
  {
    public static IComparer<TItemToSort> then_by<TItemToSort, TPropertyTypeToSortOn>(this IComparer<TItemToSort> comparer,PropertyAccessor<TItemToSort,TPropertyTypeToSortOn> accessor) where TPropertyTypeToSortOn : IComparable<TPropertyTypeToSortOn>
    {
      return new ThenComparer<TItemToSort, TPropertyTypeToSortOn>(comparer, accessor);
    }
  }

  public class ThenComparer<TTypeToSort, TPropertyTypeToSortOn> : IComparer<TTypeToSort>
    where TPropertyTypeToSortOn : IComparable<TPropertyTypeToSortOn>
  {
    PropertyAccessor<TTypeToSort, TPropertyTypeToSortOn> accessor;
    private IComparer<TTypeToSort> _previous;

    public ThenComparer(IComparer<TTypeToSort> previous, PropertyAccessor<TTypeToSort, TPropertyTypeToSortOn> accessor)
    {
      _previous = previous;
      this.accessor = accessor;
    }

    public int Compare(TTypeToSort x, TTypeToSort y)
    {
      return _previous.Compare(x, y) == 0 ? accessor(x).CompareTo(accessor(y)) : _previous.Compare(x, y);
    }
  }
}