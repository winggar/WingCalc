﻿namespace WingCalculatorShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal static class Functions
{
	private static readonly Dictionary<string, Func<List<INode>, double>> _functions = new()
	{
		["pow"] = args => Math.Pow(args[0].Solve(), args[1].Solve()),
		["exp"] = args => Math.Exp(args[0].Solve()),
		["sqrt"] = args => Math.Sqrt(args[0].Solve()),
		["cbrt"] = args => Math.Cbrt(args[0].Solve()),

		["ceil"] = args => Math.Ceiling(args[0].Solve()),
		["floor"] = args => Math.Floor(args[0].Solve()),
		["round"] = args => Math.Round(args[0].Solve(), args.Count > 1 ? (int)args[1].Solve() : 0, (MidpointRounding)(args.Count > 2 ? args[2].Solve() : 0)),
		["trunc"] = args => (int)args[0].Solve(),

		["sin"] = args => Math.Sin(args[0].Solve()),
		["cos"] = args => Math.Cos(args[0].Solve()),
		["tan"] = args => Math.Tan(args[0].Solve()),
		["asin"] = args => Math.Asin(args[0].Solve()),
		["acos"] = args => Math.Acos(args[0].Solve()),
		["atan"] = args => args.Count > 1 ? Math.Atan2(args[0].Solve(), args[1].Solve()) : Math.Atan(args[0].Solve()),
		["sinh"] = args => Math.Sinh(args[0].Solve()),
		["cosh"] = args => Math.Cosh(args[0].Solve()),
		["tanh"] = args => Math.Tanh(args[0].Solve()),
		["arcsinh"] = args => Math.Asinh(args[0].Solve()),
		["arccosh"] = args => Math.Acosh(args[0].Solve()),
		["arctanh"] = args => Math.Atanh(args[0].Solve()),

		["abs"] = args => Math.Abs(args[0].Solve()),
		["clamp"] = args => Math.Clamp(args[0].Solve(), args[1].Solve(), args[2].Solve()),
		["sign"] = args => Math.Sign(args[0].Solve()),
		["cpsign"] = args => Math.CopySign(args[0].Solve(), args[1].Solve()),

		["bitinc"] = args => Math.BitIncrement(args[0].Solve()),
		["bitdec"] = args => Math.BitDecrement(args[0].Solve()),

		["max"] = args => args.Select(x => x.Solve()).Max(),
		["min"] = args => args.Select(x => x.Solve()).Min(),
		["sum"] = args => args.Select(x => x.Solve()).Sum(),
		["product"] = args => args.Aggregate((x, y) => new ConstantNode(x.Solve() * y.Solve())).Solve(),
		["mean"] = args => args.Select(x => x.Solve()).Average(),
		["median"] = args =>
		{
			args.Sort();

			return args.Count % 2 == 0
				? args.GetRange(args.Count / 2 - 1, 2).Select(x => x.Solve()).Average()
				: args[args.Count / 2].Solve();
		},
		["mode"] = args => args.Select(x => x.Solve()).GroupBy(v => v).OrderByDescending(g => g.Count()).First().Key,

		["log"] = args => Math.Log(args[0].Solve(), args[1].Solve()),
		["ln"] = args => Math.Log(args[0].Solve()),
	};

	internal static Func<List<INode>, double> Get(string s) => _functions[s];
}
