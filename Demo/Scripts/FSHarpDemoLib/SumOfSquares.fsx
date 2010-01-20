/// The sum of the squares of the first ten natural numbers is,
///     1² + 2² + ... + 10² = 385
/// The square of the sum of the first ten natural numbers is,
///     (1 + 2 + ... + 10)² = 55² = 3025
/// What is the sum of the squares of the first 1000 natural numbers?
/// What is the square of the sum of the 1000 natural numbers?

let sum_of_squares n = 
  let sq x = x * x
  [1I..n]
    |> List.map sq 
    |> List.sum
        
let square_of_sum n =
  let sq x = x * x
  [1I..n] 
    |> List.fold (+) 0I // same as List.sum
    |> sq
  
printfn "Sum of Squares: %A" (sum_of_squares 1000I)
printfn "Square of sum: %A" (square_of_sum 1000I)