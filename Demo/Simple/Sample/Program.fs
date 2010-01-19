open PLINQ
open Sample.TestData

let list3 = randomList bigint 100000
let sq x = x * x
let fac x = List.fold (*) 1I [1I..x]
 
/// With parallel map and fold
let sumOfSquares list =
  list
    |> PSeq.adapt
    |> PSeq.map fac
    |> PSeq.fold (+) 0I
    
printfn "SumOfSquares is: %A" (sumOfSquares list3)    

let ok = System.Console.ReadLine()