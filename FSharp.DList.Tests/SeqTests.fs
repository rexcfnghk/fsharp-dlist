module FSharp.DList.Tests.SeqTests

open FSharp.DList
open Hedgehog
open Swensen.Unquote
open Xunit

[<Fact>]
let ``Cons an empty list equals to singleton list`` () =
    Property.check <| property {
        let! x = Gen.alphaNum

        List.singleton x =! (Seq.cons x [] |> Seq.toList)
    }

[<Fact>]
let ``Cons an non-empty list equals to calling List.cons`` () =
    Property.check <| property {
        let alphaNumGen = Gen.alphaNum
        let! x = alphaNumGen
        let! xs = Gen.list (Range.constant 1 10) alphaNumGen

        List.cons x xs =! (Seq.cons x xs |> Seq.toList)
    }

