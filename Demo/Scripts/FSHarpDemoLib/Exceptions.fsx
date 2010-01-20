open System

// How to raise exceptions in F#

/// Raise standard exceptions via failwith
let simpleException() = failwith "Here's how to raise a simple 'Failure' exception"

simpleException()

/// shows a simple try catch block
let simpleTryWith error =
  try 
    printfn "About to raise a simple 'Failure' exception..."
    failwith error
  with 
    Failure msg -> 
        printfn "Caught a simple 'Failure' exception, msg = '%s'" msg
        
simpleTryWith "Baaam!"

open System.Collections.Generic

/// Match specific KeyNotFoundException
let catchSpecificException() = 
  try 
    printfn "About to raise an exception..."
    match DateTime.Now.DayOfWeek with 
      | DayOfWeek.Monday -> raise (new KeyNotFoundException())
      | DayOfWeek.Friday -> raise (new ArgumentException("Thank good it's Friday."))
      | _                -> failwith "it's not Monday or Friday"
  with 
    | :? KeyNotFoundException -> 
        printfn "Caught a 'Not_found' exception, it must be Monday"
    | :? ArgumentException as e -> printfn "Error: %s" e.Message       
    | Failure msg -> printfn "Caught a 'Failure' exception: %s" msg
        
catchSpecificException()        