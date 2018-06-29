using RevisoScheduler;
using System;

namespace RevisoSamples
{
    public class Program
    {
        public static void Main()
        {
            RevisoJobManager jobManager = new RevisoJobManager();
            jobManager.ExecuteAllJobs();
            Console.ReadLine();
        }
    }
}