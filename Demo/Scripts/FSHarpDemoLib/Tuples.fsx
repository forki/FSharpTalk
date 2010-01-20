/// Function that prints information about the city
let printCity cityInfo =
  printfn "Population of %s is %d." (fst cityInfo) (snd cityInfo)

// Inferred type of the function
// printCity : string * int -> unit 
  
// Create tuples representing Prague and Seattle
let prague  = ("Prague", 1188126)
// brackets are optional
let seattle = "Seattle", 594210

// print cities
printCity prague
printCity seattle

// another city (New York has a half inhabitant?!)
//let newyork = "New York", 7180000.5
//// Inferred type of newyork: string * float
//printCity newyork

// pattern matching
let printCity2 city =
  match city with
    // Pattern that matches only Seattle
    | ("Seattle", n) -> 
       printCity city 
       printf "MS Headquarter"
    | _ -> printCity city   
    
    
/// Swap the order of two integers
let swap (a, b) = b, a    

// ---------------------------------------------------
/// Returns the div and modulo of an integer division
let (/%) a b = a / b, a % b

let res = 10 /% 3
printfn "10 /%% 3 = %A" res