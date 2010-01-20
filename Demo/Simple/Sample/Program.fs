module Program

open PLINQ
open Sample.TestData

let list3 = randomList bigint 100000
let sq x = 
  //[1I..x] |> List.fold (fun acc i -> x + acc) 0I
  x * x
 
/// With parallel map and fold
let sumOfSquares list =
  list
    |> Seq.map sq
    |> Seq.fold (+) 0I
    
printfn "SumOfSquares is: %A" (sumOfSquares list3)    

let ok = System.Console.ReadLine()