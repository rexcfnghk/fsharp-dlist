module FSharp.DList.Tests.DListTests

open FSharp.DList
open Hedgehog
open Swensen.Unquote
open Xunit

[<Fact>]
let ``Empty Dlists are considered equal`` () =
    let x = DList.empty
    let y = DList.empty

    x =! y

[<Fact>]
let ``Dlists with same content are considered equal`` () =
    Property.check <| property {
        let! chars =
            Gen.alphaNum
            |> Gen.seq (Range.constant 10 100)

        let (left, right) = (DList.fromSeq chars, DList.fromSeq chars)

        left =! right
    }

[<Fact>]
let ``Dlists are not equal when lengths are not equal`` () =
    Property.check <| property {
        let! leftChars =
            Gen.alphaNum
            |> Gen.seq (Range.constant 1 10)

        let! rightChars =
            Gen.alphaNum
            |> Gen.seq (Range.constant 11 100)

        let (left, right) = (DList.fromSeq leftChars, DList.fromSeq rightChars)

        left <>! right
    }

[<Fact>]
let ``DLists can roundtrip`` () =
    Property.check <| property {
        let! sut =
            Gen.alphaNum
            |> Gen.seq (Range.constant 10 100)
            |> Gen.map DList.fromSeq

        DList.fromSeq (DList.toSeq sut) =! sut
    }

[<Fact>]
let ``DLists are comparable`` () =
    Property.check <| property {
        let! left =
            Gen.int (Range.constant 10 100)
            |> Gen.seq (Range.constant 1 100)
            |> Gen.map DList.fromSeq

        let right = DList.map ((+) 1) left

        right >! left
    }

[<Fact>]
let ``Snoc returns expected list with new element concatenated at the end`` () =
    Property.check <| property {
        let intGen = Gen.int (Range.constant 10 100)

        let! dlist =
            intGen
            |> Gen.seq (Range.constant 1 100)
            |> Gen.map DList.fromSeq

        let! x = intGen

        DList.toList dlist @ [x] =! DList.toList (DList.snoc dlist x)
    }

[<Fact>]
let ``Singleton returns expected DList with expected single element`` () =
    Property.check <| property {
        let! char = Gen.alphaNum

        let sut = DList.singleton char

        sut =! DList.fromSeq [char]
    }
