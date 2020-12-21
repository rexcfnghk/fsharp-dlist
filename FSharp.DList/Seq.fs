module FSharp.DList.Seq

let cons<'a> : 'a -> seq<'a> -> seq<'a> =
    Seq.append << Seq.singleton
