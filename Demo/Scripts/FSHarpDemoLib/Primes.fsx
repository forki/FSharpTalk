#r "System.Numerics"
#load "TimeHelper.fs"
open Demo
open System
open Microsoft.FSharp.Math
open System.Numerics

/// returns all primes up to n 
/// Sieve of Eratosthenes
let primes (n:BigInteger) =    
  let max = double n |> Math.Sqrt
  let rec filterPrimes L =
    match L with
      | x::L -> 
        if double x <= max then
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