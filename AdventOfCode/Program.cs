// See https://aka.ms/new-console-template for more information


using AdventOfCode;


int day = int.Parse(args.Length == 0 ? Console.ReadLine() : args[0]);
string inputFile = args.Length > 1 ? args[1] : "Input.txt";

var DaySolution = DayFactory.Create(day, inputFile);
Console.WriteLine(DaySolution.Task1());
Console.WriteLine(DaySolution.Task2());