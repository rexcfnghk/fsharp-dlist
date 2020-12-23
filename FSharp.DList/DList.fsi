namespace FSharp.DList

[<Sealed>]
type DList<'a> =
    interface System.Collections.Generic.IEnumerable<'a>
    interface System.Collections.IEnumerable
    interface System.IEquatable<DList<'a>>
    interface System.IComparable<DList<'a>>
    interface System.IComparable

module DList =

    val empty<'a> : DList<'a>

    val singleton<'a> : 'a -> DList<'a>

    val fromSeq<'a> : seq<'a> -> DList<'a>

    val toArray<'a> : DList<'a> -> 'a[]

    val toList<'a> : DList<'a> -> 'a list

    val toSeq<'a> : DList<'a> -> seq<'a>

    val foldr<'a, 'b> : ('a -> 'b -> 'b) -> 'b -> DList<'a> -> 'b

    val cons<'a> : 'a -> DList<'a> -> DList<'a>

    val snoc<'a> : DList<'a> -> 'a -> DList<'a>

    val map<'a, 'b> : ('a -> 'b) -> DList<'a> -> DList<'b>

    val append : DList<'a> -> DList<'a> -> DList<'a>
