#r "System.Numerics"
#load "TestData.fs"  
open Sample.TestData 

/// Via mutable state
let sum1 list = 
  let mutable r = 0
  for e in list do
    r <- r + e
  r

printfn "Sum1 is: %A" (sum1 list2) 

/// Without mutable state
let rec sum2 list = 
  match list with
  | x::rest -> x + sum2 rest  // not tail-recursive
  | []      -> 0

printfn "Sum2 is: %A" (sum2 list2)

/// Without mutable state
/// but tail-recursive
let rec sum3 acc list = 
  match list with
  | x::rest -> sum3 (x + acc) rest
  | []      -> acc
  
printfn "Sum3 is: %A" (sum3 0 list2)  

/// Using fold
let rec fold f acc list = 
  match list with
  | x::rest -> fold f (f x acc) rest
  | []      -> acc

let sum4 list = fold (fun a b -> a + b) 0 list  
let sum list = fold (+) 0 list
printfn "Sum is: %A" (sum list2)  

/// Sum of Squares
let sq x = x * x
let sumOfSquares1 list = fold (fun x acc -> sq x + acc) 0 list
printfn "SumOfSquares1 is: %A" (sumOfSquares1 list2)

/// With map and fold
let sumOfSquares2 list =
  list
    |> List.map sq
    |> List.sum
    
printfn "SumOfSquares2 is: %A" (sumOfSquares2 list2)    