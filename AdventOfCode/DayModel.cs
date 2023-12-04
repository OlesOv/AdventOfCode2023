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
}
