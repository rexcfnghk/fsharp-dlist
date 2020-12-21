module FSharp.DList.List

open Utilities

let cons<'a> : 'a -> 'a list -> 'a list = curry List.Cons
