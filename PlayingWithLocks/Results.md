| Method                       | Mean      | Error    | StdDev   | Allocated |
|----------------------------- |----------:|---------:|---------:|----------:|
| CreditAndDebitMonitorLedger  |  50.29 ns | 0.458 ns | 0.357 ns |         - |
| CreditAndDebitLockLedger     |  50.99 ns | 0.176 ns | 0.164 ns |         - |
| CreditAndDebitSpinLockLedger | 144.72 ns | 0.300 ns | 0.251 ns |         - |


| Method                          | Mean     | Error    | StdDev   | Allocated |
|-------------------------------- |---------:|---------:|---------:|----------:|
| CreditAndDebitMonitorLedger     | 40.61 ns | 0.400 ns | 0.354 ns |         - |
| CreditAndDebitInterlockedLedger | 14.59 ns | 0.092 ns | 0.082 ns |         - |