namespace FSharp.DList

open System

[<CustomEquality; CustomComparison>]
type DList<'a> =
    | DList of (seq<'a> -> seq<'a>)

    static member ToSeq (DList xs) =
        xs Seq.empty

    override this.ToString () =
        sprintf "%A" <| DList.ToSeq<'a> this

    override this.Equals other =
        match other with
        | :? DList<'a> as y ->
            (this :> IEquatable<DList<'a>>).Equals y
        | _ -> false

    override this.GetHashCode () =
        let seq = DList.ToSeq<'a> this
        Seq.fold (fun acc x -> acc * 7 + Unchecked.hash x) 0 seq

    interface IEquatable<DList<'a>> with
        member this.Equals other =
            let (left, right) = (DList.ToSeq<'a> this, DList.ToSeq<'a> other)
            Seq.forall2 (Unchecked.equals) left right

    interface IComparable<DList<'a>> with
        member this.CompareTo other =
            let (leftSeq, rightSeq) = (DList.ToSeq<'a> this, DList.ToSeq<'a> other)
            Seq.fold2 (fun acc x y -> if acc <> 0 then acc else Unchecked.compare x y) 0 leftSeq rightSeq

    interface IComparable with
        member this.CompareTo other =
            match other with
            | :? DList<'a> as y ->
                (this :> IComparable<DList<'a>>).CompareTo y
            | _ -> invalidArg (nameof other) <| sprintf "Object must be of type %s" (nameof DList)

[<RequireQualifiedAccess>]
module DList =
    let empty<'a> : DList<'a> = DList id

    let toSeq (DList xs) = xs Seq.empty

    let toList xs = (Seq.toList << toSeq) xs

    let foldr f z xs = Seq.foldBack f (toList xs) z

    let fromSeq xs = (DList << Seq.append) xs

    let cons x xs = fromSeq <| Seq.cons x (toSeq xs)

    let map f x = (foldr (cons << f) empty) x
