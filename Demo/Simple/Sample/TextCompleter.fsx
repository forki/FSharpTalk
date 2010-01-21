#r @"..\lib\Rx\System.Reactive.dll"
#r @"..\lib\Rx\System.CoreEx.dll"
#r @"..\lib\Rx\System.Threading.dll"
#r @"System.Core.dll"
#r @"WindowsBase.dll"

#load "Observable.fs"
open RxExtensions

open System.Windows.Forms

// setup UI
let form = new Form(Visible=true, TopMost=true) 
let textBox1 = 
  new TextBox(
    Location = new System.Drawing.Point(12, 12),
    Size = new System.Drawing.Size(260, 20))
form.Controls.Add textBox1

let textBox2 = 
  new TextBox(
    Location = new System.Drawing.Point(13, 39),
    Size = new System.Drawing.Size(259, 211),
    Multiline = true)
form.Controls.Add textBox2

/// create a list with "common" words
let data =
  ["hell"; "Hello"; "Halle"; "Bonn"; "Bonjour"; "Steffen"; "Bad Homburg"; "Hamburg"]


let findWords prefix =  
    if prefix = "" then Seq.empty else
    let prefix' = prefix.ToUpper()
    data |> Seq.filter (fun word -> word.ToUpper().StartsWith prefix')


// create observable
let wordsFound =
  textBox1.TextChanged
    |> Observable.fromEvent
    |> Observable.map (fun _ -> findWords textBox1.Text)


// create observers
let observer1 =
  wordsFound
    |> Observable.subscribe (fun words ->
         textBox2.Text <- Seq.fold (sprintf "%s%s\r\n") "" words)


let observer2 =
  wordsFound 
    |> Observable.filter (fun words -> Seq.length words = 1)     
    |> Observable.throttle (System.TimeSpan.FromSeconds 1.5)
    |> Observable.map (fun words -> Seq.head words)
    |> Observable.subscribe (fun word -> 
         printfn "Text completed to %s" word
         textBox1.Text <- word) 
