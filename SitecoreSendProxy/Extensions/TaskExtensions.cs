using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SitecoreSendProxy.Extensions
{
    public static class TaskExtensions
    {
        public static TaskAwaiter<T[]> GetAwaiter<T>(this IEnumerable<Task<T>> tasks)
        {
            return Task.WhenAll(tasks).GetAwaiter();
        }
    }
}