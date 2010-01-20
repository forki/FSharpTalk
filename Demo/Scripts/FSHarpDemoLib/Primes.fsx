#load "TimeHelper.fs"
open Demo
open System
open Microsoft.FSharp.Math

/// returns all primes up to n 
/// Sieve of Eratosthenes
let primes (n:bigint) =    
  let max = bigint.ToDouble n |> Math.Sqrt
  let rec filterPrimes L =
    match L with
      | x::L -> 
        if bigint.ToDouble x <= max then
          x :: filterPrimes (List.filter (fun num -> num % x <> 0I) L)
        else
          x::L
      | [] -> []
      
  [2I] @ [3I..2I..n] 
    |> filterPrimes
  
start "Primes"
primes 2000000I
  |> Seq.sum
  |> solved