open System

/// A Pythagorean triplet is a set of three natural numbers, a < b < c, for which,
/// a² + b² = c²
/// 
/// For example, 3² + 4² = 9 + 16 = 25 = 5².
/// 
/// There exists exactly one Pythagorean triplet for which a + b + c = 1000.
/// Find the product abc.

let sq x = x * x
let triplets n = 
  seq 
    {for i in 1..n do
      for j in i+1..n do
        let k = sq i + sq j |> double |> sqrt
        if k = Math.Round(k) then
          yield i,j,int k}
        
triplets 1000 
  |> Seq.filter (fun (a,b,c) -> a + b + c = 1000) 
  |> printfn "%A"
  
 
open System

let max = 40 
let fibArray = Array.create max 1

let fibonacci a =
    for i = 2 to (a-1) do
        fibArray.[i] <- fibArray.[i-1] + fibArray.[i-2];

let startTime = System.DateTime.Now 

fibonacci max    

fibArray |> Array.iteri (fun i x -> printfn "Fibonacci Number(%d) = %d" (i+1) x)

let endTime = System.DateTime.Now
let duration = endTime.Subtract(startTime)

printfn "%d" duration.Milliseconds

Console.ReadLine()
  