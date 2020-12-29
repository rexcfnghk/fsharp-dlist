namespace FSharp.DList

open System
open System.Collections
open System.Collections.Generic
open FSharp.DList

[<CustomEquality; CustomComparison>]
[<StructuredFormatDisplay("{AsString}")>]
type DList<'a> =
    | DList of (seq<'a> -> seq<'a>)

    static member UnDList (DList xs) = xs

    static member ToSeq xs = DList.UnDList<'a> xs Seq.empty

    member this.AsString = this.ToString ()

    override this.ToString () =
        sprintf $"%A{DList.ToSeq this}"

    override this.Equals other =
        match other with
        | :? DList<'a> as y ->
            (this :> IEquatable<DList<'a>>).Equals y
        | _ -> false

    override this.GetHashCode () =
        let seq = DList.ToSeq this
        Seq.fold (fun acc x -> acc * 7 + Unchecked.hash x) 13 seq

    interface IEquatable<DList<'a>> with
        member this.Equals other =
            let (left, right) =
                (Seq.toArray <| DList.ToSeq this,
                 Seq.toArray <| DList.ToSeq other)
            if Array.length left <> Array.length right
            then false
            else Array.forall2 (Unchecked.equals) left right

    interface IComparable<DList<'a>> with
        member this.CompareTo other =
            let left, right =
                (Seq.toArray <| DList.ToSeq this,
                 Seq.toArray <| DList.ToSeq other)

            let leftLength, rightLength =
                Array.length left, Array.length right

            if leftLength <> rightLength
            then compare leftLength rightLength
            else
                let folder acc x y =
                    if acc <> 0
                    then acc
                    else Unchecked.compare x y
                Array.fold2 folder 0 left right

    interface IComparable with
        member this.CompareTo other =
            match other with
            | :? DList<'a> as y ->
                (this :> IComparable<DList<'a>>).CompareTo y
            | _ ->
                invalidArg (nameof other) <| sprintf $"Object must be of type %s{nameof DList}"

    interface IEnumerable<'a> with
        member this.GetEnumerator () = (DList.ToSeq this).GetEnumerator ()

    interface IEnumerable with
        member this.GetEnumerator () =
            (this :> IEnumerable<'a>).GetEnumerator () :> IEnumerator

[<RequireQualifiedAccess>]
module DList =
    open Utilities

    let empty = DList id

    let toSeq (DList xs) = xs Seq.empty

    let toArray xs = (Seq.toArray << toSeq) xs

    let toList xs = (Seq.toList << toSeq) xs

    let foldr f z xs = Seq.foldBack f (toSeq xs) z

    let fromSeq xs = (DList << Seq.append) xs

    let cons x xs = fromSeq <| Seq.cons x (toSeq xs)

    let singleton x = (flip cons empty) x

    let append (DList xs) (DList ys) = DList (xs << ys)

    let snoc xs x = append xs (singleton x)

    let map f x = (foldr (cons << f) empty) x

    let iter f x = (Seq.iter f << toSeq) x

    let length xs = (Seq.length << toSeq) xs

    let isEmpty x = (Seq.isEmpty << toSeq) x
