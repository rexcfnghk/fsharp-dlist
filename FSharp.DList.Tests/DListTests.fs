module FSharp.DList.Tests.DListTests

open System
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
let ``Cons returns expected list with new element concatenated at the start`` () =
    Property.check <| property {
        let intGen = Gen.int (Range.constant 10 100)

        let! dlist =
            intGen
            |> Gen.seq (Range.constant 1 100)
            |> Gen.map DList.fromSeq

        let! x = intGen

        x :: DList.toList dlist =! DList.toList (DList.cons x dlist)
    }

[<Fact>]
let ``Singleton returns expected DList with expected single element`` () =
    Property.check <| property {
        let! char = Gen.alphaNum

        let sut = DList.singleton char

        sut =! DList.fromSeq [char]
    }

[<Fact>]
let ``Map id returns original DList`` () =
    Property.check <| property {
        let intGen = Gen.int (Range.constant 10 100)

        let! dlist =
            intGen
            |> Gen.seq (Range.constant 1 100)
            |> Gen.map DList.fromSeq

        DList.map id dlist =! dlist
    }

[<Fact>]
let ``Length returns expected number of elements in DList`` () =
    Property.check <| property {
        let intGen = Gen.int (Range.constant 10 100)

        let seqGen =
            intGen
            |> Gen.seq (Range.constant 1 100)

        let! seq = seqGen

        DList.length (DList.fromSeq seq) =! Seq.length seq
    }

[<Fact>]
let ``IsEmpty returns true when DList is empty`` () =
    test <@ DList.isEmpty DList.empty @>

[<Fact>]
let ``IsEmpty returns false when DList has at least one element`` () =
    Property.check <| property {
        let intGen = Gen.int (Range.constant 10 100)

        let! sut =
            intGen
            |> Gen.seq (Range.constant 1 100)
            |> Gen.map DList.fromSeq

        test <@ not (DList.isEmpty sut) @>
    }

[<Fact>]
let ``Iter calls f expected number of times`` () =
    Property.check <| property {
        let intGen = Gen.int (Range.constant 10 100)
        let mutable i = 0
        let incr _ = i <- i + 1

        let! sut =
            intGen
            |> Gen.seq (Range.constant 1 100)
            |> Gen.map DList.fromSeq

        DList.iter incr sut

        i =! DList.length sut
    }

[<Fact>]
let ``Equals returns false when other is not DList`` () =
    let sut = DList.empty
    let other = obj ()

    sut.Equals other =! false

[<Fact>]
let ``GetHashCode returns equal values when instances are equal`` () =
    Property.check <| property {
        let intGen = Gen.int (Range.constant 10 100)

        let! sut =
            intGen
            |> Gen.seq (Range.constant 1 100)
            |> Gen.map DList.fromSeq

        let other = sut

        hash sut =! hash other
    }

[<Fact>]
let ``Non-generic CompareTo throws when other is not DList`` () =
    let sut : DList<obj> = DList.empty
    let other = obj ()

    raises<ArgumentException> <@ (sut :> IComparable).CompareTo other @>

[<Fact>]
let ``CompareTo returns larger than 0 when left DList has more elements than right`` () =
    Property.check <| property {
        let! sut =
            Range.constant 10 100
            |> Gen.int
            |> Gen.map DList.singleton

        compare sut DList.empty >! 0
    }

[<Fact>]
let ``CompareTo returns smaller than 0 when left DList has less elements than right`` () =
    Property.check <| property {
        let! sut =
            Range.constant 10 100
            |> Gen.int
            |> Gen.map DList.singleton

        compare DList.empty sut <! 0
    }

[<Fact>]
let ``ToString returns expected representation`` () =
    Property.check <| property {
        let! seq =
            Gen.int (Range.constant 10 100)
            |> Gen.seq (Range.constant 1 100)

        let sut = DList.fromSeq seq

        sprintf $"%A{sut}" =! sprintf $"%A{DList.toSeq sut}"
    }

[<Fact>]
let ``DList builder returns equivalent DList using fromSeq`` () =
    Property.check <| property {
        let intGen = Gen.int (Range.constant 10 100)

        let! x = intGen
        let! xs =
            intGen
            |> Gen.seq (Range.constant 1 100)

        let sut = dList { yield x; yield! xs }

        DList.fromSeq (Seq.cons x xs) =! sut
    }

[<Fact>]
let ``DList builder for returns equivalent DList using fromSeq`` () =
    Property.check <| property {
        let intGen = Gen.int (Range.constant 10 100)

        let! x = intGen
        let! xs =
            intGen
            |> Gen.seq (Range.constant 1 100)

        DList.fromSeq (Seq.cons x xs) =! dList { yield x; for i in xs -> i }
    }

[<Fact>]
let ``Combine obeys monoid laws`` () =
    Property.check <| property {
        let! sut =
            Gen.int (Range.constant 10 100)
            |> Gen.seq (Range.constant 1 100)
            |> Gen.map DList.fromSeq

        DList.append DList.empty sut =! DList.append sut DList.empty
        dList { (); yield! sut } =! dList { yield! sut; () }
    }

[<Fact>]
let ``DList builder zero returns empty DList`` () =
    let sut = dList {()}

    sut =! DList.empty

[<Fact>]
let ``Foldl returns same result as foldr when operator is commutative`` () =
    Property.check <| property {
        let! sut =
            Gen.int (Range.constant 10 100)
            |> Gen.seq (Range.constant 1 100)
            |> Gen.map DList.fromSeq

        DList.foldr (+) 0 sut =! DList.foldl (+) 0 sut
    }

[<Fact>]
let ``ToArray returns same sequence as toList`` () =
    Property.check <| property {
        let! sut =
            Gen.int (Range.constant 10 100)
            |> Gen.seq (Range.constant 1 100)
            |> Gen.map DList.fromSeq

        DList.toList sut =! List.ofArray (DList.toArray sut)
    }

[<Fact>]
let ``Collect returns original DList when f returns singleton`` () =
    Property.check <| property {
        let! sut =
            Gen.int (Range.constant 10 100)
            |> Gen.seq (Range.constant 1 100)
            |> Gen.map DList.fromSeq

        sut =! DList.collect DList.singleton sut
    }

[<Fact>]
let ``Concat returns same result as append given two DLists`` () =
    Property.check <| property {
        let dListGen =
            Gen.int (Range.constant 10 100)
            |> Gen.seq (Range.constant 1 100)
            |> Gen.map DList.fromSeq

        let! xs = dListGen
        let! ys = dListGen

        DList.concat [xs; ys] =! DList.append xs ys
    }

[<Fact>]
let ``Concat empty sequence returns empty DList`` () =
    DList.concat Seq.empty =! DList.empty

[<Fact>]
let ``Filter returns original DList when predicate always returns true`` () =
    Property.check <| property {
        let! sut =
            Gen.int (Range.constant 10 100)
            |> Gen.seq (Range.constant 1 100)
            |> Gen.map DList.fromSeq
        let predicate _ = true

        DList.filter predicate sut =! sut
    }

[<Fact>]
let ``Filter returns empty DList when predicate always returns false`` () =
    Property.check <| property {
        let! sut =
            Gen.int (Range.constant 10 100)
            |> Gen.seq (Range.constant 1 100)
            |> Gen.map DList.fromSeq
        let predicate _ = false

        DList.filter predicate sut =! DList.empty
    }

[<Fact>]
let ``Non-generic GetEnumerator returns expected seq`` () =
    Property.check <| property {
        let! seq =
            Gen.int (Range.constant 10 100)
            |> Gen.seq (Range.constant 1 100)
            
        let sut = DList.fromSeq seq

        (sut :> Collections.IEnumerable) =! (seq :> Collections.IEnumerable)
    }    
