using System;

namespace AdventOfCode
{
    internal abstract class DayModel
    {
        protected readonly string[] _input;
        public DayModel(string inputFileName) 
        {
            _input = File.ReadAllLines(getInputFilePath(inputFileName));
        }
        private string getInputFilePath(string inputFileName)
        {
            return Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @$"..\..\..\{this.GetType().Name}\{inputFileName}"));
        }
        public abstract long Task1();
        public abstract long Task2();
    }
    internal static class DayFactory
    {
        public static DayModel Create(int day, string inputFileName)
        {
            var type = GetDayType(day);
            if (type == null) throw new InvalidOperationException($"Day {day} solution not found");

            return (DayModel)Activator.CreateInstance(type, inputFileName)!;
        }

        private static Type? GetDayType(int day)
        {
            var typeName = $"{nameof(AdventOfCode)}.Day{day}";
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Select(ass => ass.GetType(typeName))
                .FirstOrDefault(t => t != null);
        }
    }
}
