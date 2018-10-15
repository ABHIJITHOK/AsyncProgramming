using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;

namespace Application
{
    public class TaskCancellationExample
    {
        public Task task { get; private set; }
        public ConcurrentBag<Task> tasks = new ConcurrentBag<Task>();

        public TaskCancellationExample()
        {
            var cts = new CancellationTokenSource();
            var ct = cts.Token;

            task = Task.Factory.StartNew(action: () =>
            {
                ct.ThrowIfCancellationRequested();
                DoSomething3(ct);
            },cancellationToken: ct);

            tasks.Add(task);

            cts.Cancel();
            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException ex1)
            {
                Console.WriteLine($"Exception caught: ");
                foreach (var innerException in ex1.InnerExceptions)
                {
                    Console.WriteLine($"{innerException.Message}");
                }
            }
            finally
            {
                cts.Dispose();
            }

            if (task.Exception==null)
            {
                Console.WriteLine($"Task cancelled successfully: {task.Exception}");
            }
            Console.WriteLine($"Task status: {task.Status}");

        }

        public void DoSomething3(CancellationToken ct)
        {
            Console.WriteLine($"Task beginning");
            for (int i = 0; i < 100; i++)
            {
                ct.ThrowIfCancellationRequested();
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} : Task : i = {i}");
                Thread.SpinWait(5000000);
            }
        }
    }
}
