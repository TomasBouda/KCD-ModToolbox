using System;
using System.Collections.Generic;
using System.Linq;

namespace TomLabs.KCDModToolbox.Core.Extensions.Collections
{
	public static class LinkedListExtensions
	{
		/// <summary>
		/// Returns next or first element from given <see cref="LinkedList{T}"/> that satisfies a specified condition
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="current"></param>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public static LinkedListNode<T> NextOrFirst<T>(this LinkedListNode<T> current, Func<T, bool> predicate)
		{
			var next = current.Next;

			return next == null
				? current.List.FirstOrDefault(predicate)
				: predicate(next.Value)
					? next
					: next.NextOrFirst(predicate) == current
						? null
						: next.NextOrFirst(predicate);
		}

		/// <summary>
		/// Returns next or first element from given <see cref="LinkedList{T}"/>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="current"></param>
		/// <returns></returns>
		public static LinkedListNode<T> NextOrFirst<T>(this LinkedListNode<T> current)
		{
			return current.Next ?? current.List.First;
		}

		/// <summary>
		/// Returns previous or last element from given <see cref="LinkedList{T}"/>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="current"></param>
		/// <returns></returns>
		public static LinkedListNode<T> PreviousOrLast<T>(this LinkedListNode<T> current)
		{
			return current.Previous ?? current.List.Last;
		}

		/// <summary>
		/// Returns previous or last element from given <see cref="LinkedList{T}"/> that satisfies a specified condition
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="current"></param>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public static LinkedListNode<T> PreviousOrLast<T>(this LinkedListNode<T> current, Func<T, bool> predicate)
		{
			return current.Previous ?? current.List.LastOrDefault(predicate);
		}

		/// <summary>
		/// Returns the first element in a sequence that satisfies a specified condition or default.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public static LinkedListNode<T> FirstOrDefault<T>(this LinkedList<T> list, Func<T, bool> predicate)
		{
			return list.Any(predicate) ? list.Find(list.First(predicate)) : default(LinkedListNode<T>);
		}

		/// <summary>
		///  Returns the last element in a sequence that satisfies a specified condition or default.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public static LinkedListNode<T> LastOrDefault<T>(this LinkedList<T> list, Func<T, bool> predicate)
		{
			return list.Any(predicate) ? list.Find(list.Last(predicate)) : default(LinkedListNode<T>);
		}
	}
}