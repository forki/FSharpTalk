#r "System.Numerics"


//// Factorial sample 
let rec factorialBad x = 
  match x with
    | y when y = 0I -> 1I
    | _ -> x * factorialBad x-1I

/// Where is the problem?
factorialBad 10000I
   
/// Tail recursive version
let factorial x = 
  let rec tailRecursiveFactorial x acc =
    match x with
      | y when y = 0I -> acc
      | _ -> tailRecursiveFactorial (x-1I) (acc*x)           

  tailRecursiveFactorial x 1I
  
factorial 10000I |> printfn "%A"

let rec sum_of_digits n =
  match n with
    | y when y = 0I -> 0I 
    | _ -> n % 10I + sum_of_digits (n / 10I) 
    
let rec fac x =
  match x with
    | y when y = 0I -> 1I
    | _ -> x * fac(x-1I)     
         
let factorialByFold x = List.fold (*) 1I [1I..x]
    
fac 100I
  |> sum_of_digits
  |> printfn "Sum of Digits: %A"
  
  
  