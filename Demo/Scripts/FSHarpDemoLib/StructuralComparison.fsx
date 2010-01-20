// F# supports structural comparison and equality of values with the same type.  
// This sample shows the results of some simple structural comparison operations.
let show a b = 
    printfn "%A < %A: %b" a b (a < b)
    printfn "%A = %A: %b" a b (a = b)
    printfn "%A > %A: %b" a b (a > b)
    printfn ""
    
show 1 2
show 2 2
show "1" "2"
show "abb" "abc" 
show "aBc" "ABB" // case-sensitive
show None (Some 1)
show None None
show (Some 0) (Some 1)
show (Some 1) (Some 1)
show [1;2;3] [1;2;2]
show [] [1;2;2]


// F# supports structural hashing on values. 
// Typically only F# record/union structured terms are traversed, though this can be customized on a per-type basis. 
// This sample shows the results of some simple structural hashing operations.

let showHash a = printfn "hash(%A) : %d" a (hash a) 

showHash 1
showHash 2
showHash "1"
showHash "2"
showHash "abb" 
showHash "aBc" // case-sensitive
showHash None
showHash (Some 1)
showHash (Some 0)
showHash [1;2;3]
showHash [1;2;3;4;5;6;7;8]
showHash [1;2;3;4;5;6;7;8;9;10;11]
showHash [1;2;3;4;5;6;7;8;9;10;11;12;13;14;15]