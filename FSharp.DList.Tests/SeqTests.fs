module FSharp.DList.Tests.SeqTests

open FSharp.DList
open Hedgehog.Xunit
open Swensen.Unquote

[<Property>]
let ``Cons an empty list equals to singleton list`` (x: char) =
    List.singleton x =! (Seq.cons x [] |> Seq.toList)

[<Property>]
let ``Cons an non-empty list equals to calling List.cons`` (x: char) xs =
    x :: xs =! (Seq.cons x xs |> Seq.toList)

