using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuHelper
{
    public static class ProcessUtils
    {
        public static async Task<T?> ReTry<T>(this Func<Task<T>> func, int reTryCount = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (reTryCount < 0)
            {
                throw new ArgumentOutOfRangeException("reTryCount");
            }

            Exception lastException = null;
            for (int i = 0; i <= reTryCount; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                try
                {
                    return await func();
                }
                catch (Exception ex)
                {
                    lastException = ex;
                }
            }

            throw new Exception("Multiple retries failed", lastException);
        }

        public static async Task ReTry(this Func<Task> func, int reTryCount = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (reTryCount < 0)
            {
                throw new ArgumentOutOfRangeException("reTryCount");
            }

            Exception lastException = null;
            for (int i = 0; i <= reTryCount; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                try
                {
                    await func();
                    return;
                }
                catch (Exception ex)
                {
                    lastException = ex;
                }
            }

            throw new Exception("Multiple retries failed", lastException);
        }
    }
}
