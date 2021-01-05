namespace FSharp.DList

[<Sealed>]
type DList<'a> =
    interface System.Collections.Generic.IEnumerable<'a>
    interface System.Collections.IEnumerable
    interface System.IEquatable<DList<'a>>
    interface System.IComparable<DList<'a>>
    interface System.IComparable

module DList =

    val empty : DList<'a>

    val singleton : 'a -> DList<'a>

    val fromSeq : seq<'a> -> DList<'a>

    val toArray : DList<'a> -> 'a[]

    val toList : DList<'a> -> 'a list

    val toSeq : DList<'a> -> seq<'a>

    val foldr : ('a -> 'b -> 'b) -> 'b -> DList<'a> -> 'b

    val foldl : ('b -> 'a -> 'b) -> 'b -> DList<'a> -> 'b

    val cons : 'a -> DList<'a> -> DList<'a>

    val snoc : DList<'a> -> 'a -> DList<'a>

    val map : ('a -> 'b) -> DList<'a> -> DList<'b>

    val collect : ('a -> DList<'b>) -> DList<'a> -> DList<'b>

    val iter : ('a -> unit) -> DList<'a> -> unit

    val append : DList<'a> -> DList<'a> -> DList<'a>

    val concat : seq<DList<'a>> -> DList<'a>

    val length : DList<'a> -> int

    val isEmpty : DList<'a> -> bool
