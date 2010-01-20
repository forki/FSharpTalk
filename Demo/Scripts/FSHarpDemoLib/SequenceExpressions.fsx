// This sample demonstrates a simple generative sequence expressions to specify lists
let data1 = [ for x in 0..20 -> x, x * x ]

// This uses a nested loop
let data2 = [ for x in 0..5 do
                for y in 0..5 do
                  yield x, y, x * y ]

// This uses a filter
let data3 = [ for x in 0..5 do
                for y in 0..5 do
                  if x > y then 
                    yield (x, y, x * y) ]

// This uses an internal let-binding
let data4 = [ for x in 0..5 do
                for y in 0..5 do
                  if x > y then 
                    let z = x * y 
                    yield (x, y, z) ]

printfn "data1 = \n%A\n\n" data1
printfn "data2 = \n%A\n\n" data2
printfn "data3 = \n%A\n\n" data3
printfn "data4 = \n%A\n\n" data4


// Sequence expressions for arrays
let array1 = [| for x in 0..20 -> x, x * x |]
    
printfn "array1 = \n%A" array1

// Sequence expressions for lazy sequences
 
let seq1 = seq {for x in 0..20 -> x, x * x }
    
printfn "seq1 = \n%A" seq1
 