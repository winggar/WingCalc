﻿namespace WingCalculatorShared.Nodes;

internal record OrNode(INode A, INode B) : INode
{
	public double Solve(Scope scope) => A.Solve(scope) != 0 || B.Solve(scope) != 0 ? 1 : 0;
}