using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return $@"{Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\.."))}\{this.GetType().Name}\{inputFileName}";
        }
    }
}
