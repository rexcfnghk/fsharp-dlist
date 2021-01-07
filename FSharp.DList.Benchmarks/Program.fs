open System.Reflection
open BenchmarkDotNet.Running

[<EntryPoint>]
let main argv =
    ignore <| BenchmarkSwitcher.FromAssembly(Assembly.GetExecutingAssembly ()).Run argv
    0
