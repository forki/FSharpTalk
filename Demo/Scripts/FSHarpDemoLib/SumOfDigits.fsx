open Microsoft.FSharp.Math

let rec sum_of_digits n =
  match n with
    | y when y = 0I -> 0I 
    | _ -> n % 10I + sum_of_digits (n / 10I) 
    
/// What is the sum of the digits of the number 2^1000?
bigint.Pow(2I,1000I) 
  |> sum_of_digits 
  |> printfn "Quersumme von 2^1000 = %A"

// -----------------------------------------------
// Find the sum of the digits in the number 100!"
bigint.Factorial(100I) 
  |> sum_of_digits 
  |> printfn "Quersumme von 100! = %A"