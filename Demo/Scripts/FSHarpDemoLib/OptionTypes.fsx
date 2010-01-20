open System

// use option types for "nullable" functions

let openingHours day = 
  match day with 
  | DayOfWeek.Monday 
  | DayOfWeek.Tuesday 
  | DayOfWeek.Thursday 
  | DayOfWeek.Friday    -> Some(9,17)
  | DayOfWeek.Wednesday -> Some(9,19) // extended hours on Wednesday
  | _ -> None 

// openingHours: DateTime -> (int * int) option

let today = DateTime.Now.DayOfWeek

match openingHours today with 
  | None -> printfn "The shop's not open today"
  | Some(s,f) -> printfn "The shop's open today from %02d:00-%d:00" s f
