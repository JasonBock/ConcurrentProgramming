﻿using BenchmarkDotNet.Attributes;

namespace PlayingWithLocks;

[MemoryDiagnoser]
public class InterlockedPerformance
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private IntegerFieldMonitorLedger monitorLedger;
	private IntegerFieldInterlockedLedger interlockedLedger;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

	[GlobalSetup]
	public void GlobalSetup()
	{
		this.monitorLedger = new IntegerFieldMonitorLedger(100);
		this.interlockedLedger = new IntegerFieldInterlockedLedger(100);
	}

	[Benchmark]
	public int CreditAndDebitMonitorLedger()
	{
		this.monitorLedger.Credit(200);
		this.monitorLedger.Debit(50);
		return this.monitorLedger.Value;
	}

	[Benchmark]
	public int CreditAndDebitInterlockedLedger()
	{
		this.interlockedLedger.Credit(200);
		this.interlockedLedger.Debit(50);
		return this.interlockedLedger.Value;
	}
}