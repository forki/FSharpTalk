#light

// using immutable lists

/// The empty list
let listA = [ ]           

/// A list with 3 integers
let listB = [ 1; 2; 3 ]     

/// A list with 3 integers, note :: is the 'cons' operation
let listC = 1 :: [2; 3]    

/// Compute the sum of a list of integers using a recursive function
let rec sumList xs =
  match xs with
  | []    -> 0
  | y::ys -> y + sumList ys

/// Sum of a list
let listD = sumList [1; 2; 3]  

/// The list of integers between 1 and 10 inclusive 
let oneToTen = [1..10]

/// The squares of the first 10 integers
let squaresOfOneToTen = [ for x in 0..10 -> x*x ]  
 
// -----------------------------------------------------------------
// more on lists 
 
let data = [1;2;3;4]
printfn "data = \n%A" data
printfn "List.hd data = %d" (List.hd data)
printfn "List.tl data = \n%A" (List.tl data)
printfn "List.length data = %d" (List.length data)

// pattern matching with lists
let consume data = 
  match data with 
  | 1::rest    -> 
      printfn "matched a 1"
      rest
  | 2::x::rest when x = 3 -> 
      printfn "matched a 2 and 3"
      rest 
  | [4]        -> 
      printfn "matched a 4"
      []
  | _          -> 
    printfn "unexpected!"
    [] 
  
let data2 = consume data 
let data3 = consume data2
let data4 = consume data3

printfn "At end of list? %b" (data4 = [])

// ------------------------------------------------------------------
// pattern matching on lists

let list1 = [2 ; 10; 8; 12; 7; 13; 9; 10; 7; 14; 1; 3; 8; 5 ;6]

/// calculates a sum
let rec sum1 l = 
  match l with
    | x::rest -> x + sum1 rest  // not tail-recursive
    | []      -> 0

printfn "Sum1: %d" (sum1 list1)

/// reveres a list    
let rec rev1 l = 
  match l with
    | x::rest -> rev1 rest @ [x]  // not tail-recursive
    | []      -> []

printfn "Rev1: %A" (rev1 list1)

/// counts the even-valued numbers
let rec countEven1 l = 
  match l with
    | x::rest -> 
       if x % 2 = 0 then 1 + countEven1 rest else countEven1 rest // not tail-recursive
    | []      -> 0
    
printfn "CountEven1: %d" (countEven1 list1)    

/// calculates the sum of the list - tail-recursive
let rec sum2 acc l = 
  match l with
    | x::rest -> sum2 (acc+x) rest
    | []      -> acc

printfn "Sum2: %d" (sum2 0 list1)

/// "Folds a list"
let rec fold f acc l =
  match l with
    | x::rest -> fold f (f x acc) rest
    | []      -> acc

// using fold
let add x y = x + y
let sum3 l = fold add 0 l
printfn "Sum3: %d" (sum3 list1)
    
// Using anonymous lambda expression    
let sum4 l = fold (fun a b -> a + b) 0 l
printfn "Sum4: %d" (sum4 list1)

// (+) <==> (fun a b -> a + b)
let sum5 l = fold (+) 0 l
printfn "Sum5: %d" (sum5 list1)

let rev2 l = fold (fun a b -> b @ [a]) [] l  
printfn "Rev2: %A" (rev2 list1)


/// with built-in fold:
let sum6 l = List.fold (+) 0 l

// ------------------------------------------------------------------
// higher order functions for lists

/// A function that increments its input by 2, as a function definition
let square x = x*x              

// Map a function across a list of values
let squares1 = List.map square [1; 2; 3; 4]
let squares2 = List.map (fun x -> x*x) [1; 2; 3; 4]

// Using Pipelines
// let (|>) x f = f(x)

let squares3 = [1; 2; 3; 4] |> List.map (fun x -> x*x) 
let SumOfSquaresUpTo n = 
  [1..n] 
  |> List.map square 
  |> List.sum
  

let animals = ["Cat";"Dog";"Mouse";"Elephant"]

// List.iter (iterate through a list)
animals |> List.iter (fun a -> printfn "Animal: %s" a)

// functional version of
for a in animals do
  printfn "Animal: %s" a  
  
// List.map (replace each list entry with a function result)
let animals2 =
  animals |> List.map (fun a -> a,a.Length)
  
printfn "%A" animals2
  
// List.filter
let animalsWithShortNames = 
  animals2 |> List.filter (fun (name,l) -> l < 4)
  
printfn "%A" animalsWithShortNames  
