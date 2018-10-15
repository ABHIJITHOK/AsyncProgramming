using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class AttachedChildTasks
    {
        public Task parent { get; private set; }
        public Task child { get; private set; }

        public AttachedChildTasks()
        {
            parent = Task.Factory.StartNew(() =>
            {
                DoSomething();
                child = Task.Factory.StartNew(() =>
                {
                    DoSomethingElse();
                }, TaskCreationOptions.AttachedToParent);              
            });
            parent.Wait();
            Console.WriteLine($"Child Task completed: {child.IsCompletedSuccessfully}. Task Status: {child.Status}");
            Console.WriteLine($"Parent Task completed: {parent.IsCompletedSuccessfully}. Task Status: {parent.Status}");
        }

        public void DoSomething()
        {
            Console.WriteLine("Parent Task beginning");
            for (int i = 0; i<10; i++)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} : Parent Task");
                Thread.SpinWait(5000000);
            }
        }

        public void DoSomethingElse()
        {
            Console.WriteLine("Child Task beginning");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} : Child Task");
                Thread.SpinWait(5000000);
            }
        }
    }
}
