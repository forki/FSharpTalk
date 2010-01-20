/// Create a histogram of the occurrences of particular unicode characters 
/// using the functions of the Hashtbl module.  
let getCharDict(data:string) = 
  let tab = new System.Collections.Generic.Dictionary<char,int>() 

  for c in data.ToCharArray() do   
    if tab.ContainsKey(c) then 
      let v = tab.[c] 
      tab.[c] <- v+1
    else 
      tab.Add(c,1)
  
  tab
      
let data = "The quick brown fox jumps over the lazy dog"      
for KeyValue(k,v) in getCharDict data do
  printfn "Number of '%c' characters = %d" k v
  
  
/// Create a histogram of the occurrences of particular unicode characters 
/// using the functions of the Map module.
/// Immutable map
let getCharMap(data:string) = 
  data.ToCharArray()
    |> Array.fold
        (fun map c -> 
            match map |> Map.tryFind c with
              | Some v -> map |> Map.add c (v+1)
              | None   -> map |> Map.add c 1)
        Map.empty
            
            
for KeyValue(k,v) in getCharMap data do
  printfn "Number of '%c' characters = %d" k v  