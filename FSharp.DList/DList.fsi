namespace FSharp.DList

[<Sealed>]
type DList<'a> =
    interface System.IEquatable<DList<'a>>

module DList =

    val empty<'a> : DList<'a>

    val fromSeq<'a> : seq<'a> -> DList<'a>

    val toList<'a> : DList<'a> -> 'a list

    val toSeq<'a> : DList<'a> -> seq<'a>

    val foldr<'a, 'b> : ('a -> 'b -> 'b) -> 'b -> DList<'a> -> 'b

    val cons<'a> : 'a -> DList<'a> -> DList<'a>
