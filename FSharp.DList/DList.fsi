namespace FSharp.DList

[<Sealed>]
type DList<'a> =
    interface System.IEquatable<DList<'a>>
    interface System.IComparable<DList<'a>>
    interface System.IComparable

module DList =

    val empty<'a> : DList<'a>

    val fromSeq<'a> : seq<'a> -> DList<'a>

    val toList<'a> : DList<'a> -> 'a list

    val toSeq<'a> : DList<'a> -> seq<'a>

    val foldr<'a, 'b> : ('a -> 'b -> 'b) -> 'b -> DList<'a> -> 'b

    val cons<'a> : 'a -> DList<'a> -> DList<'a>

    val map<'a, 'b> : ('a -> 'b) -> DList<'a> -> DList<'b>
