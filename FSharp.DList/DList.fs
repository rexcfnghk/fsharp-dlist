namespace FSharp.DList

open System

[<CustomEquality; NoComparison>]
type DList<'a> =
    | DList of (seq<'a> -> seq<'a>)

    static member toSeq (DList xs) =
        xs Seq.empty

    override this.Equals other =
        match other with
        | :? DList<'a> as y ->
            (this :> IEquatable<DList<'a>>).Equals y
        | _ -> false

    override this.GetHashCode () =
        hash (DList.toSeq<'a> this)

    interface IEquatable<DList<'a>> with
        member this.Equals other =
            let (left, right) = (DList.toSeq<'a> this, DList.toSeq<'a> other)
            Seq.forall2 (Unchecked.equals) left right

module DList =
    let empty<'a> : DList<'a> = DList id

    let toSeq (DList xs) = xs Seq.empty

    let toList xs = (Seq.toList << toSeq) xs

    let foldr f z xs = Seq.foldBack f (toList xs) z

    let fromSeq xs = (DList << Seq.append) xs

    let cons x xs = fromSeq <| Seq.cons x (toSeq xs)
