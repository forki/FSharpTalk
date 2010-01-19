#r @"..\lib\Rx\System.Reactive.dll"
#r @"..\lib\Rx\System.CoreEx.dll"
#r @"..\lib\Rx\System.Threading.dll"
#r @"System.Core.dll"
#r @"WindowsBase.dll"

#load "Observable.fs"
open RxExtensions

open System.Windows.Forms

let form = new Form(Visible=true, TopMost=true) 

/// Creates two observables
///  - left is triggered when the left mouse button is down and the mouse is in the area
///  - right is triggered when the right mouse button is down and the mouse is in the area
let left,right =  
  form.MouseDown
    |> Observable.fromEvent
    |> Observable.mergeWith (form.MouseMove |> Observable.fromEvent)
    |> Observable.filter (fun args ->  
          args.Button = MouseButtons.Left || 
          args.Button = MouseButtons.Right)
    |> Observable.map (fun args -> args.X, args.Y, args.Button)
    |> Observable.filter (fun (x,y,b) -> y < 100)
    |> Observable.partition (fun (_,_,button) -> 
         button = MouseButtons.Left)
    
let leftSubscription =
  left 
    |> Observable.subscribe 
         (fun (x,y,_) -> printfn "Left (%d,%d)" x y) 

let rightSubscription = // returns IDisposable
  right
    |> Observable.subscribeComplete
         (fun (x,y,_) -> printfn "Right (%d,%d)" x y)
         (fun error -> printfn "Error: %s" error.Message)
         (fun ()    -> printfn "Ready.")

// unsubscribe
leftSubscription.Dispose()  // -= doesn't work with lamdas!
rightSubscription.Dispose() 

