namespace FSharp.DList.Benchmarks

open System
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Jobs
open FSharp.DList

[<SimpleJob(RuntimeMoniker.Net48, baseline = true)>]
[<SimpleJob(RuntimeMoniker.NetCoreApp31)>]
[<SimpleJob(RuntimeMoniker.NetCoreApp50)>]
type DListVsList () =
    let mutable dList = DList.empty
    let mutable list = List.empty
    let mutable fSharpxDList = FSharpx.Collections.DList.empty

    [<DefaultValue; Params(1_000, 10_000)>]
    val mutable public Size : int

    [<GlobalSetup>]
    member this.Setup () =
        let array = Array.zeroCreate this.Size
        (Random ()).NextBytes array

        let ints = Array.map int array

        dList <- DList.fromSeq ints
        list <- List.ofArray ints
        fSharpxDList <- FSharpx.Collections.DList.ofSeq ints


    [<Benchmark>]
    member this.DListCons () = DList.cons this.Size dList

    [<Benchmark>]
    member this.FSharpxDListCons () = FSharpx.Collections.DList.cons this.Size fSharpxDList

    [<Benchmark>]
    member this.ListCons () = this.Size :: list

    [<Benchmark>]
    member this.ListSnoc () = list @ [this.Size]

    [<Benchmark>]
    member this.DListSnoc () = DList.snoc dList this.Size

    [<Benchmark>]
    member this.FSharpxDListSnoc () =
        FSharpx.Collections.DList.append fSharpxDList (FSharpx.Collections.DList.singleton this.Size)
