#r "nuget: Hedgehog"

open Hedgehog

Range.constantBounded ()
|> Gen.int
|> Gen.printSample