module FSharp.DList.Utilities

let flip f y x = f x y

let curry f x y = f (x, y)
