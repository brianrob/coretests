using System;
using System.Threading.Tasks;

using EventPipe;

namespace eventpipe_onoff
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start the allocator thread.
            Task t = new Task(new Action(Allocator));
            t.Start();

            int iteration = 1;
            while(true)
            {
                string outputFile = string.Format("/home/brianrob/src/tests/managed/eventpipe_onoff/file-{0}.netperf", iteration);
                TraceConfiguration config = CreateConfiguration(outputFile);

                Console.WriteLine("Iteration {0}:", iteration++);
                Console.WriteLine("\tStart: Enable tracing.");
                TraceControl.Enable(config);
                Console.WriteLine("\tEnd: Enable tracing.\n");

                Console.WriteLine("\tStart: Allocation.");
                // Allocate for 1000000 iterations.
                for(int i=0; i<1000000; i++)
                {
                    GC.KeepAlive(new object());
                }
                Console.WriteLine("\tEnd: Allocation.\n");

                Console.WriteLine("\tStart: Disable tracing.");
                TraceControl.Disable();
                Console.WriteLine("\tEnd: Disable tracing.\n");

                System.IO.File.Delete(outputFile);
            }
        }

        private static void Allocator()
        {
            while(true)
            {
                GC.KeepAlive(new object());
            }
        }

        private static TraceConfiguration CreateConfiguration(string outputFile)
        {
            // Setup the configuration values.
            uint circularBufferMB = 1024; // 1 GB
            uint level = 5; // Verbose

            // Create a new instance of EventPipeConfiguration.
            TraceConfiguration config = new TraceConfiguration(outputFile, circularBufferMB);
            // Setup the provider values.
            // Public provider.
            string providerName = "Microsoft-Windows-DotNETRuntime";
            UInt64 keywords = 0x4c14fccbd;

            // Enable the provider.
            config.EnableProvider(providerName, keywords, level);

            // Private provider.
            providerName = "Microsoft-Windows-DotNETRuntimePrivate";
            keywords = 0x4002000b;

            // Enable the provider.
            config.EnableProvider(providerName, keywords, level);

            // Sample profiler.
            providerName = "3c530d44-97ae-513a-1e6d-783e8f8e03a9";
            keywords = 0x0;

            // Enable the provider.
            config.EnableProvider(providerName, keywords, level);

            return config;
        }
    }
}
