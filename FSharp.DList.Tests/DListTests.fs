module FSharp.DList.Tests.DListTests

open System
open FSharp.DList
open Xunit
open Hedgehog.Xunit
open Swensen.Unquote

[<Fact>]
let ``Empty Dlists are considered equal`` () =
    let x = DList.empty
    let y = DList.empty

    x =! y

[<Property>]
let ``Dlists with same content are considered equal`` (chars: seq<char>) =
    let (left, right) = (DList.fromSeq chars, DList.fromSeq chars)

    left =! right

[<Property>]
let ``Dlists are not equal when lengths are not equal`` (leftChars: seq<char>) (rightChars: seq<char>) =
    let (left, right) = (DList.fromSeq leftChars, DList.fromSeq rightChars)

    left <>! right

[<Property(typeof<DListGenConfigContainer<char>>)>]
let ``DLists can roundtrip`` (sut: DList<char>) =
    DList.fromSeq (DList.toSeq sut) =! sut

[<Property>]
let ``DLists are comparable`` (left: DList<int>) =
    let right = DList.map ((+) 1) left

    right >! left

[<Property>]
let ``Snoc returns expected list with new element concatenated at the end`` (dlist: DList<int>) x =
    DList.toList dlist @ [x] =! DList.toList (DList.snoc dlist x)

[<Property>]
let ``Cons returns expected list with new element concatenated at the start`` (dlist: DList<int>) x =
    x :: DList.toList dlist =! DList.toList (DList.cons x dlist)

[<Property>]
let ``Singleton returns expected DList with expected single element`` (char: char) =
    let sut = DList.singleton char

    sut =! DList.fromSeq [char]

[<Property>]
let ``Map id returns original DList`` (dlist: DList<int>) =
    DList.map id dlist =! dlist

[<Property>]
let ``Length returns expected number of elements in DList`` (seq: seq<int>) =
    DList.length (DList.fromSeq seq) =! Seq.length seq

[<Fact>]
let ``IsEmpty returns true when DList is empty`` () =
    test <@ DList.isEmpty DList.empty @>

[<Property>]
let ``IsEmpty returns false when DList has at least one element`` (sut: DList<int>) =
    test <@ not (DList.isEmpty sut) @>

[<Property>]
let ``Iter calls f expected number of times`` (sut: DList<int>) =
    let mutable i = 0
    let incr _ = i <- i + 1

    DList.iter incr sut

    i =! DList.length sut

[<Fact>]
let ``Equals returns false when other is not DList`` () =
    let sut = DList.empty
    let other = obj ()

    sut.Equals other =! false

[<Property>]
let ``GetHashCode returns equal values when instances are equal`` (sut: DList<int>) =
    let other = sut

    hash sut =! hash other

[<Property>]
let ``Non-generic CompareTo throws when other is not DList`` (sut: DList<int>) =
    let other = obj ()

    raises<ArgumentException> <@ (sut :> IComparable).CompareTo other @>

[<Property>]
let ``CompareTo returns larger than 0 when left DList has more elements than right`` (sut: DList<int>) =
    compare sut DList.empty >! 0

[<Property>]
let ``CompareTo returns smaller than 0 when left DList has less elements than right`` (sut: DList<int>) =
    compare DList.empty sut <! 0

[<Property>]
let ``ToString returns expected representation`` (sut: DList<int>) =
    sprintf $"%A{sut}" =! sprintf $"%A{DList.toSeq sut}"

[<Property>]
let ``DList builder returns equivalent DList using fromSeq`` (x: int) xs =
    let sut = dList { yield x; yield! xs }

    DList.fromSeq (Seq.cons x xs) =! sut

[<Property>]
let ``DList builder for returns equivalent DList using fromSeq`` (x: int) xs =
    DList.fromSeq (Seq.cons x xs) =! dList { yield x; for i in xs -> i }

[<Property>]
let ``Combine obeys monoid laws`` (sut: DList<int>) =
    DList.append DList.empty sut =! DList.append sut DList.empty
    dList { (); yield! sut } =! dList { yield! sut; () }

[<Fact>]
let ``DList builder zero returns empty DList`` () =
    let sut = dList {()}

    sut =! DList.empty

[<Property>]
let ``Foldl returns same result as foldr when operator is commutative`` (sut: DList<int>) =
    DList.foldr (+) 0 sut =! DList.foldl (+) 0 sut

[<Property>]
let ``ToArray returns same sequence as toList`` (sut: DList<int>) =
    DList.toList sut =! List.ofArray (DList.toArray sut)

[<Property>]
let ``Collect returns original DList when f returns singleton`` (sut: DList<int>) =
    sut =! DList.collect DList.singleton sut

[<Property>]
let ``Concat returns same result as append given two DLists`` (xs: DList<int>) ys =
    DList.concat [xs; ys] =! DList.append xs ys

[<Fact>]
let ``Concat empty sequence returns empty DList`` () =
    DList.concat Seq.empty =! DList.empty

[<Property>]
let ``Filter returns original DList when predicate always returns true`` (sut: DList<int>) =
    let predicate _ = true

    DList.filter predicate sut =! sut

[<Property>]
let ``Filter returns empty DList when predicate always returns false`` (sut: DList<int>) =
    let predicate _ = false

    DList.filter predicate sut =! DList.empty

[<Property>]
let ``Non-generic GetEnumerator returns expected seq`` (seq: seq<int>) =
    let sut = DList.fromSeq seq :> Collections.IEnumerable

    Seq.toList (Seq.cast<int> sut) =! Seq.toList seq
