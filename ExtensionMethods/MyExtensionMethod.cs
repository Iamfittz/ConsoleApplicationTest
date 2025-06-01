using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationTest.ExtensionMethods {
    public static class MyExtensionMethod {
        public static IEnumerable<T> MyWhereCustom<T>(this IEnumerable<T> collection, Func<T, bool> predicate) {
            foreach (var item in collection) {
                if (predicate.Invoke(item)) {
                    yield return item;
                }
            }
        }
        public static IEnumerable<T> MySelectCustom<T, U>(this IEnumerable<U> collection, Func<U, T> func) {
            foreach (var item in collection) {
                yield return func.Invoke(item);
            }
        }
    }
}
