using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        public class Plan
        {
            string[][] _state;
            int _floor;

            public Plan(string startState)
            {
                var foo = startState.Split(':');
                var floor = int.Parse(foo[0]);

                var floors = foo[1].Split('|');
                _state = floors.Select(f => f.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)).ToArray();
                _floor = floor;
            }

            public string RenderState(string[][] _state, List<string> keep, List<string> move, int _floor, int delta)
            {
                return string.Format("{0}:{1}", _floor + delta, string.Join("|", _state.Select((f, i) =>
                {
                    if (i == _floor) return string.Join(" ", keep);
                    if (i == _floor + delta) return string.Join(" ", f.Concat(move).OrderBy(g => g).ToArray());
                    return string.Join(" ", f);
                })));
            }

            public IEnumerable<string> BuildPlans()
            {
                for (var first = 0; first < _state[_floor].Length; first++)
                {
                    for (var second = first; second < _state[_floor].Length; second++)
                    {
                        var keep = new List<string>(_state[_floor]);
                        var move = new List<string>();

                        move.Add(keep[first]);
                        if (first != second)
                        {
                            move.Add(keep[second]);
                            keep.RemoveAt(second);
                        }
                        keep.RemoveAt(first);

                        if (_floor > 0)
                        {
                            var state = RenderState(_state, keep, move, _floor, -1);
                            if (IsGoodState(state)) yield return state;
                        }
                        if (_floor < 3)
                        {
                            var state = RenderState(_state, keep, move, _floor, 1);
                            if (IsGoodState(state)) yield return state;
                        }
                    }
                }
            }
        }

        public static void Main(string[] args)
        {
            var input = "0:OG OM | CG UG RG PG | CM UM RM PM |";
            Console.WriteLine($"part1: {Solve(input)}");
            var input2 = "0:OG OM EG EM DG DM | CG UG RG PG | CM UM RM PM |";
            Console.WriteLine($"part2: {Solve(input2)}");
        }

        public static int Solve(string state)
        {
            var previousStates = new List<string>();

            var steps = 0;
            var plans = new[] { state };
            while (!plans.Any(f => IsFinished(f)))
            {
                plans = plans.AsParallel().SelectMany(f => new Plan(f).BuildPlans()).AsEnumerable().Distinct().Except(previousStates).ToArray();
                steps++;
                previousStates.AddRange(plans);
            }

            return steps;
        }

        public static bool IsFinished(string state)
        {
            return state.Split(':')[1].Split('|').Take(3).Sum(f => f.Trim().Length) == 0;
        }

        public static bool IsGoodState(string state)
        {
            var floors = state.Split(':')[1].Split('|');
            for (var i = 0; i < floors.Length; i++)
            {
                var parts = floors[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var noGenerators = !parts.Any(f => f[1] == 'G');
                var noParts = parts.Length == 0;
                var chipsNoGenerator = parts.Any(f => f[1] == 'M' && !parts.Any(g => g[0] == f[0] && g[1] == 'G'));

                if (!(noGenerators || noParts || !chipsNoGenerator))
                    return false;
            }
            return true;
        }
    }
}