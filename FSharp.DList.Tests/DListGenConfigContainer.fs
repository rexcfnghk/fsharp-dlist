namespace FSharp.DList.Tests

open FSharp.DList
open Hedgehog

type DListGenConfigContainer<'a> =
    static member __ =
        let dListGen = Gen.map DList.fromSeq <| Gen.seq (Range.exponentialBounded ()) GenX.auto<'a>
        GenX.defaults |> AutoGenConfig.addGenerator dListGen

