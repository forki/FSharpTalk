open System.Windows.Forms

let form = new Form(Visible=true, TopMost=true) 

let rec clicked n =
    async {
      let! firstClick = Async.AwaitEvent form.Click
      form.Text <- sprintf "I got clicked %d-times" n // returning to UI Thread!!!
      if n < 5 then
        return! clicked (n+1)}

clicked 1 |> Async.StartImmediate