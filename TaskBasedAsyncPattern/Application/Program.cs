using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Application
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Parent task waits for the child tasks attached to the parent taks to finish.");
            AttachedChildTasks attachedChildTasks = new AttachedChildTasks();

            Console.WriteLine("\n\nParent task does not wait for the child tasks attached to the parent taks to finish.");
            DetachedChildTasks detachedChildTasks = new DetachedChildTasks();

            Console.WriteLine("\n\nTask cancellation using Cancellation Token passing");
            TaskCancellationExample taskCancellationExample = new TaskCancellationExample();
        }


    }
}
