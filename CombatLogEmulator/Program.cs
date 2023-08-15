
using System.Diagnostics;
using System.Globalization;

Console.WriteLine("Log to run: ");
string sourcePath = Console.ReadLine()!;
var reader = new StreamReader(sourcePath);


Console.WriteLine("Absolute path to target file"); ;
string targetPath = Console.ReadLine()!;
var fs = new FileStream(targetPath, FileMode.Open);
var writer = new StreamWriter(fs);

var firstLine = reader.ReadLine()!;
var splitLine = firstLine.Split("  ");
var startDateString = splitLine[0];

var bogusYear = 2018;
var format = "yyyy/M/d HH:mm:ss.fff";

// since I wanted to use datetime helper methods, I just added a bogus year, since the log files doesn't have year.
var startTime = DateTime.ParseExact($"{bogusYear}/{startDateString}", format, CultureInfo.InvariantCulture);

var stopWatch = new Stopwatch();
stopWatch.Start();

string? currentLine = null;
while (true) {
    var includetime = startTime.AddMilliseconds(stopWatch.ElapsedMilliseconds);
    
    currentLine ??= reader.ReadLine()!;

    if(currentLine == null) {
        Console.WriteLine("Done");
        break;
    }
    var timeString = currentLine.Split("  ")[0];
    var lineTime = DateTime.ParseExact($"{bogusYear}/{timeString}", format, CultureInfo.InvariantCulture);

    if (lineTime < includetime) {
        Console.WriteLine(currentLine);
        writer.WriteLine(currentLine);
        currentLine = null;
    }
}