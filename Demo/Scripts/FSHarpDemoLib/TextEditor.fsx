// FSharp sample - text editor 
open System
open System.IO  
open System.Windows.Forms

let form = new Form(Width= 400, Height = 300, Visible = true, Text = "F# Editor Sample", Menu = new MainMenu())

// menu bar and menus 
let mFile = form.Menu.MenuItems.Add("&File")
let mHelp = form.Menu.MenuItems.Add("&Help")

// menu items 
let miOpen  = new MenuItem("&Open...")
let miSave  = new MenuItem("&Save As...")
let miQuit  = new MenuItem("&Quit")
let miAbout = new MenuItem("&About...")

mFile.MenuItems.Add(miOpen)
mFile.MenuItems.Add(miSave)
mFile.MenuItems.Add(miQuit)
mHelp.MenuItems.Add(miAbout)

// RichTextBox - text area 
let textB = new RichTextBox(Dock=DockStyle.Fill)
form.Controls.Add(textB)

// filename state 
let mutable filename = ""
let SetFilename f  = filename <- f; form.Text <- "Editor - " ^ f

// initial state   
SetFilename "scratch.txt"
textB.Text <- "type text here..."

// ReadFile dialog 
let ReadFile () =
    let d = new OpenFileDialog() 
    d.Filter <- "text files *.txt|*.txt|All files *.*|*.*";
    d.FilterIndex <- 2;
    if d.ShowDialog() = DialogResult.OK then
        let str  = new StreamReader(d.FileName)
        let text = str.ReadToEnd ()
        Some (d.FileName,text)
    else
        None

// SaveFile dialog 
let SaveFile file text =
    let d = new SaveFileDialog() 
    d.Filter <- "text files *.txt|*.txt|All files *.*|*.*";
    d.FilterIndex <- 2;
    d.FileName <- file;
    d.AddExtension <- false;  
    d.OverwritePrompt <- true;
    if d.ShowDialog() = DialogResult.OK then
        let sw = new StreamWriter(d.FileName)
        sw.Write(text:string); // type constrain text to resolve sw.Write(_) method overload 
        sw.Close();
        Some d.FileName
    else
        None

// ops 
let opLoadText _ = 
    match ReadFile () with
    | Some (file,text) -> SetFilename file; textB.Text <- text
    | None -> ()

let opSaveText _ = 
    match SaveFile filename textB.Text with
    | Some file -> SetFilename file
    | None -> ()

let opAbout _ = 
    MessageBox.Show("Hello Winforms F# Sample!","About Sample") |> ignore

let opExitForm _ = form.Close ()

// callbacks 
miOpen.Click.Add(opLoadText)
miSave.Click.Add(opSaveText)
miQuit.Click.Add(opExitForm)
miAbout.Click.Add(opAbout)
