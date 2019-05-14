using System;
using System.Runtime.InteropServices;

namespace TimeToMain
{
    public static class Program
    {
        [DllImport("libnative.so")]
        private static extern void write_marker(string name);

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            write_marker("/function/main");
        }
    }
}
