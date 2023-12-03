// See https://aka.ms/new-console-template for more information
using AdventOfCode.Day1;
using AdventOfCode.Day2;
using AdventOfCode.Day3;

Day1 day1 = new(@"Input.txt");
Console.WriteLine(day1.Task1());
Console.WriteLine(day1.Task2());

Day2 day2 = new(@"Input.txt");
Console.WriteLine(day2.Task1());
Console.WriteLine(day2.Task2());

Day3 day3 = new(@"Input.txt");
Console.WriteLine(day3.Task1());
Console.WriteLine(day3.Task2());