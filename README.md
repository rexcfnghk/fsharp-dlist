# FSharp.DLIst

Toy implementation of [Difference List](https://wiki.haskell.org/Difference_list).

## Usage

```fsharp
open FSharp.DList

let dList1 = DList.singleton 1
let dList2 = DList.fromSeq [1..3]
let dList3 = dList { 1; 2; 3 }
let dList4 = dList { yield 1; yield 2; yield 3 }
let dList5 = dList { yield! [1..3] }
let dList6 = dList { for i in [1..5] -> i }
```

## Coverage

[![Coverage Status](https://coveralls.io/repos/github/rexcfnghk/fsharp-dlist/badge.svg)](https://coveralls.io/github/rexcfnghk/fsharp-dlist)