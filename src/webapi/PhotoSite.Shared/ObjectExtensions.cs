using System;
using System.Collections.Generic;

namespace PhotoSite.Shared
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Exec action for target
        /// </summary>
        /// <typeparam name="T">Object's type</typeparam>
        /// <param name="target">Target</param>
        /// <param name="action">Action for target</param>
        /// <returns>Target</returns>
        public static T Apply<T>(this T target, Action<T> action)
        {
            action?.Invoke(target);
            return target;
        }

        /// <summary>
        /// Exec function for object
        /// </summary>
        /// <typeparam name="TSource">Type of source object</typeparam>
        /// <typeparam name="TResult">Type of result object</typeparam>
        /// <param name="source">Source object</param>
        /// <param name="convertFunc">Function</param>
        /// <returns>Result object</returns>
        public static TResult Convert<TSource, TResult>(this TSource source, Func<TSource, TResult> convertFunc) =>
            convertFunc.Invoke(source);

        /// <summary>
        /// Exec action for each element
        /// </summary>
        /// <typeparam name="T">Object's type</typeparam>
        /// <param name="target">Target</param>
        /// <param name="action">Action for target</param>
        public static void ForEach<T>(this IEnumerable<T> target, Action<T> action)
        {
            if (action is null)
                throw new ArgumentNullException(nameof(action));
            foreach(var item in target)
                action.Invoke(item);
        }

        /// <summary>
        /// Exec action for each element
        /// </summary>
        /// <typeparam name="T">Object's type</typeparam>
        /// <param name="target">Target</param>
        /// <param name="action">Action for target (second parameter is index of element)</param>
        public static void ForEach<T>(this IEnumerable<T> target, Action<T, int> action)
        {
            if (action is null)
                throw new ArgumentNullException(nameof(action));
            var index = 0;
            foreach (var item in target)
                action.Invoke(item, index++);
        }
    }
}