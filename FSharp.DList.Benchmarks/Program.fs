open System.Reflection
open BenchmarkDotNet.Running

[<EntryPoint>]
let main argv =
    Assembly.GetExecutingAssembly ()
    |> BenchmarkSwitcher.FromAssembly
    |> fun s -> s.Run argv
    |> ignore

    0
