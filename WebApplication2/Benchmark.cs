using System.Diagnostics;
using WebApplication2.Interfaces;

namespace WebApplication2
{
    public class Benchmark
    {
        private readonly IEmployee _employeeService;

        public Benchmark(IEmployee employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task RunBenchmark()
        {
            string databaseName = "test_db";
            string pattern = "test_pattern";

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            var employees = await _employeeService.GetEmployeesAsync(databaseName, pattern);
            stopwatch.Stop();

            Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds} ms");

            // Repeat the test multiple times to get an average time
            int iterations = 10;
            long totalTime = 0;
            for (int i = 0; i < iterations; i++)
            {
                stopwatch.Restart();
                employees = await _employeeService.GetEmployeesAsync(databaseName, pattern);
                stopwatch.Stop();
                totalTime += stopwatch.ElapsedMilliseconds;
            }

            Console.WriteLine($"Average time taken over {iterations} iterations: {totalTime / iterations} ms");
        }
    }
}
