``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-8700 CPU 3.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
  [Host]        : .NET Framework 4.8 (4.8.4300.0), X64 LegacyJIT DEBUG
  .NET 4.8      : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  .NET Core 3.1 : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  .NET Core 5.0 : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT


```
|           Method |           Job |       Runtime |  Size |          Mean |       Error |      StdDev | Ratio | RatioSD |
|----------------- |-------------- |-------------- |------ |--------------:|------------:|------------:|------:|--------:|
|        **DListCons** |      **.NET 4.8** |      **.NET 4.8** |  **1000** |     **41.116 ns** |   **0.3221 ns** |   **0.3013 ns** |  **1.00** |    **0.00** |
|        DListCons | .NET Core 3.1 | .NET Core 3.1 |  1000 |     44.062 ns |   0.2262 ns |   0.2115 ns |  1.07 |    0.01 |
|        DListCons | .NET Core 5.0 | .NET Core 5.0 |  1000 |     43.497 ns |   0.2686 ns |   0.2512 ns |  1.06 |    0.01 |
|                  |               |               |       |               |             |             |       |         |
| FSharpxDListCons |      .NET 4.8 |      .NET 4.8 |  1000 |      9.933 ns |   0.0368 ns |   0.0327 ns |  1.00 |    0.00 |
| FSharpxDListCons | .NET Core 3.1 | .NET Core 3.1 |  1000 |     11.127 ns |   0.0481 ns |   0.0450 ns |  1.12 |    0.01 |
| FSharpxDListCons | .NET Core 5.0 | .NET Core 5.0 |  1000 |     11.899 ns |   0.0661 ns |   0.0618 ns |  1.20 |    0.01 |
|                  |               |               |       |               |             |             |       |         |
|         ListCons |      .NET 4.8 |      .NET 4.8 |  1000 |      2.859 ns |   0.0292 ns |   0.0273 ns |  1.00 |    0.00 |
|         ListCons | .NET Core 3.1 | .NET Core 3.1 |  1000 |      3.270 ns |   0.0394 ns |   0.0329 ns |  1.14 |    0.02 |
|         ListCons | .NET Core 5.0 | .NET Core 5.0 |  1000 |      4.118 ns |   0.0349 ns |   0.0326 ns |  1.44 |    0.02 |
|                  |               |               |       |               |             |             |       |         |
|         ListSnoc |      .NET 4.8 |      .NET 4.8 |  1000 |  5,471.756 ns |  15.6913 ns |  14.6776 ns |  1.00 |    0.00 |
|         ListSnoc | .NET Core 3.1 | .NET Core 3.1 |  1000 |  5,836.186 ns |  20.8179 ns |  16.2533 ns |  1.07 |    0.00 |
|         ListSnoc | .NET Core 5.0 | .NET Core 5.0 |  1000 |  5,814.922 ns |  13.5122 ns |  11.9782 ns |  1.06 |    0.00 |
|                  |               |               |       |               |             |             |       |         |
|        DListSnoc |      .NET 4.8 |      .NET 4.8 |  1000 |     43.348 ns |   0.1763 ns |   0.1649 ns |  1.00 |    0.00 |
|        DListSnoc | .NET Core 3.1 | .NET Core 3.1 |  1000 |     48.105 ns |   0.4642 ns |   0.4342 ns |  1.11 |    0.01 |
|        DListSnoc | .NET Core 5.0 | .NET Core 5.0 |  1000 |     49.594 ns |   0.3526 ns |   0.2944 ns |  1.14 |    0.01 |
|                  |               |               |       |               |             |             |       |         |
| FSharpxDListSnoc |      .NET 4.8 |      .NET 4.8 |  1000 |     20.643 ns |   0.0899 ns |   0.0841 ns |  1.00 |    0.00 |
| FSharpxDListSnoc | .NET Core 3.1 | .NET Core 3.1 |  1000 |     20.874 ns |   0.0765 ns |   0.0639 ns |  1.01 |    0.01 |
| FSharpxDListSnoc | .NET Core 5.0 | .NET Core 5.0 |  1000 |     20.777 ns |   0.1421 ns |   0.1329 ns |  1.01 |    0.01 |
|                  |               |               |       |               |             |             |       |         |
|        **DListCons** |      **.NET 4.8** |      **.NET 4.8** | **10000** |     **40.975 ns** |   **0.1265 ns** |   **0.1057 ns** |  **1.00** |    **0.00** |
|        DListCons | .NET Core 3.1 | .NET Core 3.1 | 10000 |     44.008 ns |   0.1691 ns |   0.1582 ns |  1.07 |    0.01 |
|        DListCons | .NET Core 5.0 | .NET Core 5.0 | 10000 |     43.761 ns |   0.2004 ns |   0.1776 ns |  1.07 |    0.01 |
|                  |               |               |       |               |             |             |       |         |
| FSharpxDListCons |      .NET 4.8 |      .NET 4.8 | 10000 |     10.132 ns |   0.0516 ns |   0.0457 ns |  1.00 |    0.00 |
| FSharpxDListCons | .NET Core 3.1 | .NET Core 3.1 | 10000 |     11.190 ns |   0.0349 ns |   0.0292 ns |  1.10 |    0.01 |
| FSharpxDListCons | .NET Core 5.0 | .NET Core 5.0 | 10000 |     11.745 ns |   0.0537 ns |   0.0502 ns |  1.16 |    0.01 |
|                  |               |               |       |               |             |             |       |         |
|         ListCons |      .NET 4.8 |      .NET 4.8 | 10000 |      2.836 ns |   0.0238 ns |   0.0223 ns |  1.00 |    0.00 |
|         ListCons | .NET Core 3.1 | .NET Core 3.1 | 10000 |      3.239 ns |   0.0210 ns |   0.0197 ns |  1.14 |    0.01 |
|         ListCons | .NET Core 5.0 | .NET Core 5.0 | 10000 |      4.121 ns |   0.0293 ns |   0.0274 ns |  1.45 |    0.02 |
|                  |               |               |       |               |             |             |       |         |
|         ListSnoc |      .NET 4.8 |      .NET 4.8 | 10000 | 80,038.930 ns | 487.0256 ns | 455.5640 ns |  1.00 |    0.00 |
|         ListSnoc | .NET Core 3.1 | .NET Core 3.1 | 10000 | 82,734.634 ns | 337.3372 ns | 315.5455 ns |  1.03 |    0.01 |
|         ListSnoc | .NET Core 5.0 | .NET Core 5.0 | 10000 | 76,950.883 ns | 370.7376 ns | 328.6493 ns |  0.96 |    0.01 |
|                  |               |               |       |               |             |             |       |         |
|        DListSnoc |      .NET 4.8 |      .NET 4.8 | 10000 |     43.225 ns |   0.2220 ns |   0.2077 ns |  1.00 |    0.00 |
|        DListSnoc | .NET Core 3.1 | .NET Core 3.1 | 10000 |     48.235 ns |   0.3274 ns |   0.3063 ns |  1.12 |    0.01 |
|        DListSnoc | .NET Core 5.0 | .NET Core 5.0 | 10000 |     50.533 ns |   0.2572 ns |   0.2406 ns |  1.17 |    0.01 |
|                  |               |               |       |               |             |             |       |         |
| FSharpxDListSnoc |      .NET 4.8 |      .NET 4.8 | 10000 |     20.483 ns |   0.0704 ns |   0.0624 ns |  1.00 |    0.00 |
| FSharpxDListSnoc | .NET Core 3.1 | .NET Core 3.1 | 10000 |     21.232 ns |   0.0775 ns |   0.0687 ns |  1.04 |    0.00 |
| FSharpxDListSnoc | .NET Core 5.0 | .NET Core 5.0 | 10000 |     20.615 ns |   0.1095 ns |   0.1024 ns |  1.01 |    0.01 |
