// See https://aka.ms/new-console-template for more information

using strictly_come_coding;
using System.Diagnostics;

var stopWatch = new Stopwatch();
stopWatch.Start();

var test1 = new TestParsing();
test1.solution("measurements.txt");

//B: Run stuff you want timed
stopWatch.Stop();


// Get the elapsed time as a TimeSpan value.
TimeSpan ts = stopWatch.Elapsed;

// Format and display the TimeSpan value.
string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
    ts.Hours, ts.Minutes, ts.Seconds,
    ts.Milliseconds / 10);
Console.WriteLine("RunTime " + elapsedTime);



