module FSharp.DList.Tests

open Hedgehog

type DListGenConfigContainer =
    static member _ =
        GenX.defaults |> AutoGenConfig.addGenerator

