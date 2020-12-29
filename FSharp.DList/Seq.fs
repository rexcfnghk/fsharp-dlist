module FSharp.DList.Seq

let cons x = (Seq.append << Seq.singleton) x
