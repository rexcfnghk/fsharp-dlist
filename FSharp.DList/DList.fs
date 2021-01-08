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
        Seq.fold (fun acc x -> acc * 7 + Unchecked.hash x) 13 this

    interface IEquatable<DList<'a>> with
        member this.Equals other =
            let left, right = Seq.toArray this, Seq.toArray other
            if Array.length left <> Array.length right
            then false
            else Array.forall2 Unchecked.equals left right

    interface IComparable<DList<'a>> with
        member this.CompareTo other =
            let left, right = Seq.toArray this, Seq.toArray other

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

    let toArray (xs: DList<_>) = Seq.toArray xs

    let toList (xs: DList<_>) = Seq.toList xs

    let foldr f z (xs: DList<_>) = Seq.foldBack f xs z

    let foldl f z (xs: DList<_>) = Seq.fold f z xs

    let fromSeq xs = DList (Seq.append xs)

    let cons x (xs: DList<_>) = fromSeq <| Seq.cons x xs

    let singleton x = flip cons empty x

    let append (DList xs) (DList ys) = DList (xs << ys)

    let snoc xs x = append xs (singleton x)

    let map f xs = foldr (cons << f) empty xs

    let filter predicate xs =
        foldr (fun x xs -> if predicate x then cons x xs else xs) empty xs

    let iter f (xs: DList<_>) = Seq.iter f xs

    let length (xs: DList<_>) = Seq.length xs

    let isEmpty (xs: DList<_>) = Seq.isEmpty xs

    let collect f xs = foldr (append << f) empty xs

    let concat xs = Seq.fold append empty xs
