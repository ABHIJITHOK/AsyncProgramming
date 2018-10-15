using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Application
{
    public class DetachedChildTasks
    {
        public Task parent { get; private set; }
        public Task child { get; private set; }

        public DetachedChildTasks()
        {
            parent = Task.Factory.StartNew(() =>
            {
                DoSomething1();
                child = Task.Factory.StartNew(() =>
                {
                    DoSomething2();                
                });                
            }, TaskCreationOptions.DenyChildAttach);
            parent.Wait();
            Console.WriteLine($"Child Task completed: {child.IsCompletedSuccessfully}. Task Status: {child.Status}");
            Console.WriteLine($"Parent Task completed: {parent.IsCompletedSuccessfully}. Task Status: {parent.Status}");
        }


        public void DoSomething1()
        {
            Console.WriteLine($"Parent Task beginning");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} : Parent Task");
                Thread.SpinWait(5000000);
            }
        }

        public void DoSomething2()
        {
            Console.WriteLine($"Child Task beginning");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} : Child Task");
                Thread.SpinWait(5000000);
            }
        }
    }
}
