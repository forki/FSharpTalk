module Program

open PLINQ
open Sample.TestData

let list = randomList bigint 100000
let sq x =   
  //[1I..(abs x)] |> List.fold (fun acc i -> (abs x) + acc) 0I
  x * x
 
/// With parallel map and fold
let sumOfSquares list =
  list
    |> Seq.map sq
    |> Seq.fold (+) 0I
    
list 
  |> sumOfSquares
  |> printfn "SumOfSquares is: %A"

let ok = System.Console.ReadLine()