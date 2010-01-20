// while loop with referenced cell
let count = ref 0  // mutable = evil
while !count < 10 do 
  printfn "Counting, skipping by 2, count = %d..." !count
  count := !count + 2
printfn "Done counting!"  

// while loop with mutable data 
let mutable count2 = 0  // mutable = evil
while count2 < 10 do 
  printfn "Counting, skipping by 2, count = %d..." count2
  count2 <- count2 + 2
printfn "Done counting!"
