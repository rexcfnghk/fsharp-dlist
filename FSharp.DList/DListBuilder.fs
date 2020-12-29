namespace FSharp.DList

type DListBuilder () =
    member _.Yield x = DList.singleton x

    member _.YieldFrom xs = DList.fromSeq xs

    member _.Combine (xs, ys) = DList.append xs ys

    member _.Delay f = f ()

    member _.For (xs, f) = Seq.foldBack (DList.append << f) xs DList.empty

[<AutoOpen>]
module Builder =
    let dList = DListBuilder ()

