using System;
using AdventOfCode; // This requires Day1.cs to be in 'namespace AdventOfCode'

namespace AdventOfCode
{
    internal abstract class Program
    {
        private static void Main(string[] args)
        {
            /*
            var day1 = new Day1();
            Console.WriteLine("Day 1 - Part 1: " + day1.Part1());
            Console.WriteLine("Day 1 - Part 2: " + day1.Part2());
            var day2 = new Day2();
            Console.WriteLine("Day 2 - Part 1: " + day2.Part1());
            Console.WriteLine("Day 2 - Part 2: " + day2.Part2());
            var day3 = new Day3();
            Console.WriteLine("Day 3 - Part 1: " +day3.Part1());
            Console.WriteLine("Day 3 - Part 2: " +day3.Part2());
            Console.ReadLine(); 
            */
            var day4 = new Day4();
            Console.WriteLine("Day 4 - Part 4: " + day4.Part1());
            Console.WriteLine("Day 4 - Part 4: " + day4.Part2());
        }
    }
}