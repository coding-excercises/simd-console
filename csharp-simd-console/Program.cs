using System;
using System.Linq;
using System.Numerics;
using System.Globalization;

namespace csharp_simd_console
{
    class Program
    {
        public static int[] ArrayMultiplication(int[] lhs, int[] rhs, bool debug)
        {
            var result = new int[lhs.Length];
            var i = 0;

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            for (i = 0; i < lhs.Length; ++i)
            {
                result[i] = lhs[i] * rhs[i];
            }

            stopwatch.Stop();
            Console.WriteLine("Time taken by normal array addition : {0}", stopwatch.Elapsed);

            if (debug == true)
            {
                for (i = 0; i < lhs.Length; ++i)
                {
                    Console.WriteLine(result[i]);
                }
            }

            return result;
        }

        public static int[] SIMDArrayMultiplication(int[] lhs, int[] rhs, bool debug)
        {

            var simdLength = Vector<int>.Count;
            Console.WriteLine("Vector Count : " + simdLength);
            var result = new int[lhs.Length];
            var i = 0;

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            
            for (; i <= lhs.Length - simdLength; i += simdLength)
            {
                var va = new Vector<int>(lhs, i);
                var vb = new Vector<int>(rhs, i);
                (va * vb).CopyTo(result, i);

                 /* Mopup loop for leftover from vector count.
                 * Vector count is 8. Ex - If array size is 10,
                 * 8 items will be processed in above loop.
                 * Remaining two array items will be 'mopped up'
                 * in the below loop.
                 */   
                for (i=0; i < lhs.Length; ++i)
                {
                    result[i] = lhs[i] * rhs[i];
                }
            }

            stopwatch.Stop();
            Console.WriteLine("Time taken by SIMD array addition : {0}", stopwatch.Elapsed);

            if (debug == true)
            {
                for (i = 0; i < lhs.Length; ++i)
                {
                    Console.WriteLine(result[i]);
                }
            }

            return result;
        }

        public static int[] InitArray(string name, int arraysize, bool debug)
        {
            int Min = 0;
            int Max = 1000;
            Random randNum = new Random();
            int[] testarray = new int[arraysize];

            testarray = Enumerable
                .Repeat(0, arraysize)
                .Select(i => randNum.Next(Min, Max))
                .ToArray();

            if (debug == true)
            {
                Console.WriteLine("Initizing Array: " + name);
                foreach (var item in testarray)
                {
                    Console.WriteLine(item.ToString());
                }

            }

            return testarray;
        }
      
        public static int Main(string[] args)
        {
            if (args.Length < 2)
            {
                System.Console.WriteLine("Please enter a numeric argument and true/false.");
                System.Console.WriteLine("Usage: csharp-simd-console.exe <number> <true/false>");
                return 1;
            }

            Console.WriteLine("Hello SIMD World!");

            var simdsupport = Vector.IsHardwareAccelerated;
            Console.WriteLine("SIMD instructions are supported : " + simdsupport);

            var arraysize = Convert.ToInt32(args[0]);
            bool debug = Convert.ToBoolean(args[1]);

            Console.WriteLine("Input Array Sizes : " + arraysize.ToString("N0", CultureInfo.InvariantCulture)); 

            var simdarrayresult = new int[arraysize];
            var arrayresult = new int[arraysize];

            var arraylhs = InitArray("lhs", arraysize, debug);
            var arrayrhs = InitArray("rhs", arraysize, debug);

            simdarrayresult = SIMDArrayMultiplication(arraylhs, arrayrhs, debug);
            arrayresult = ArrayMultiplication(arraylhs, arrayrhs, debug);

            Console.WriteLine("Output Array Size (using SIMD vectors) : " + simdarrayresult.Length.ToString("N0", CultureInfo.InvariantCulture));
            Console.WriteLine("Output Array Size (without SIMD vectors) : " + arrayresult.Length.ToString("N0", CultureInfo.InvariantCulture));

            return 0;

        }
    }
}
