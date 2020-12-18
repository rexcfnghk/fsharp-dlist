module FSharp.DList.Seq

let cons x xs =
    seq {
        yield x
        yield! xs
    }
