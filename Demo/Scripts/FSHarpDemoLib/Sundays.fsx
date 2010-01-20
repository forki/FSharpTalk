open System

/// How many Sundays fell on the first of the month during the twentieth century (1 Jan 1901 to 31 Dec 2000)?
let days startDate endDate = 
  Seq.unfold  
      (fun (actDate:DateTime) -> 
         if actDate <= endDate then Some (actDate, actDate.AddDays(1.0)) else None)
      startDate

days (DateTime(1901,1,1)) (DateTime(2000,12,31))
  |> Seq.filter (fun day -> day.DayOfWeek = DayOfWeek.Sunday && day.Day = 1)
  |> Seq.length
  |> printfn "Days: %d"