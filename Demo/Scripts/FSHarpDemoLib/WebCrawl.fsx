//---------------------------------------------------------------------------
// Part I. Web.

open System.Net
open System
open System.IO

// ok, now fetch a page.  Create the web request,
// wait for the response, read off the response.
let req = System.Net.WebRequest.Create("http://www.yahoo.com")
let stream = req.GetResponse().GetResponseStream()
let reader = new IO.StreamReader(stream)
let html = reader.ReadToEnd()

// Ok, let's look at the HTML
html

// If the above failed, you may need to set up your web proxy to your system default
// System.Net.WebRequest.DefaultWebProxy <- System.Net.WebRequest.GetSystemWebProxy()


/// Fetch the contents of a web page
let http(url: string) = 
    let req = System.Net.WebRequest.Create(url) 
    let resp = req.GetResponse()
    let stream = resp.GetResponseStream() 
    let reader = new IO.StreamReader(stream) 
    let html = reader.ReadToEnd()
    resp.Close()
    html

let live   = http("http://www.live.com")
let google = http("http://www.google.com")
let bbc    = http("http://news.bbc.co.uk")
let msft   = http("http://www.microsoft.com")
let nytRSS = http("http://feeds.nytimes.com/nyt/rss/HomePage")

nytRSS

// ----------------------------
// Windows Forms

open System.Windows.Forms
open System.Drawing

let form = new Form(Visible = true, Text = "A Simple F# Form", 
                    TopMost = true, Size = Size(600,600))

let textB = new RichTextBox(Dock = DockStyle.Fill, Text = "F# Programming is Fun!",
                            Font = new Font("Lucida Console",16.0f,FontStyle.Bold),
                            ForeColor = Color.DarkBlue)


form.Controls.Add(textB)

// onClick-Handler via anonymous lambda
textB.Click.Add(fun evArgs -> MessageBox.Show("Click!") |> ignore)


// ----------------------------
// Dumping data to a form

let Show(x) = printfn "%A" x
let Clear() = textB.Clear()
let Append(x) = textB.AppendText(x + "\n")

// OK, let's pipeline some data to the output...

(1,2,3) |> Show

[ for i in 0 .. 100 -> (i,i*i) ] |> Show

// ----------------------------
// Scan RSS for news titles

open System.Xml
open System.Collections
open System.Collections.Generic

let xdoc = new XmlDocument()

nytRSS |> Show

xdoc.LoadXml(nytRSS)

// Query the loaded XML
[ for n in xdoc.SelectNodes("//title") -> n.InnerText ] |> Show


// ----------------------------
// Search for URLs in HTML

bbc |> Show

open System.Text.RegularExpressions

let linkPat = "href=\s*\"[^\"h]*(http://[^&\"]*)\""

let bbcLinks = Regex.Matches(bbc,linkPat)
bbcLinks |> Show

// Fetch out the matched strings
seq { for m in bbcLinks -> m.Groups.Item(1).Value } |> Show

// Put that in a function
let GetLinks (txt:string) =  
   seq { for m in Regex.Matches(txt,linkPat) -> m.Groups.Item(1).Value }

// Collect the links
let CollectLinks url = GetLinks (try http(url) with _ -> "")

CollectLinks "http://www.navision-blog.de" |> Show
CollectLinks "http://news.google.de" |> Show

// ----------------------------
// Crawling (Synchronous)

let limit = 10


let rec Crawl (visited: Set<string>) url =
 
    // show the URL...
    Append (url + "\n")

    // DoEvents() needed for a synchronous crawl when running in the GUI thread
    Application.DoEvents(); 

    // are we done?
    if visited.Count >= limit or visited.Contains(url) 
    then visited
    
    else 
        // get the results and crawl...
        let links = CollectLinks url 
        let visited = visited.Add(url)
        Seq.fold Crawl visited links
    
    
Clear()

Crawl Set.empty "http://news.google.com"

// ---------------------------------------------

form.Close()

// ---------------------------------------------
// Part II. Random web walk 

// Random numbers

let rand = new System.Random()

/// selects a random element in the sequence
/// if the sequence is empty the default is used
let throwDice xs dflt =  
  let xs = Array.of_seq xs 
  let n = Array.length xs 
  if n=0 then dflt else xs.[rand.Next n]

// Web browser control inside a form
open System
open System.Windows.Forms

let wb = new WebBrowser(Dock = DockStyle.Fill, AllowNavigation = true)

let webForm = new Form(Visible = true, Size = Size(400,300), TopMost = true)
webForm.Controls.Add(wb)

webForm.Text <- "MyWebForm"
// Point it at pages and get the text

wb.Navigate("http://news.bbc.co.uk")
let text = wb.DocumentText

// Regular expressions  

open System.Text.RegularExpressions
let rx   = Regex("http://news.bbc.co.uk/[a-z0-9_.-/]+stm")

webForm.Size <- Size(800,600)
// Regular expression to filter urls 

let urlsOfDocument (doc : HtmlDocument) =   
   seq { for elt in doc.Links do
         let url = elt.GetAttribute("href")
         if rx.IsMatch(url)
         then yield url }

let randomLink doc =
  let url  = throwDice (urlsOfDocument doc) "http://news.bbc.co.uk" 
  Show (sprintf "JUMP: %s\n" url);
  url

// Click on a timer event

let randomClick () = wb.Navigate(randomLink(wb.Document))
let timer = new Timer(Interval = 3000)
timer.Tick.Add(fun _ -> randomClick ())
timer.Start()

// Enough!

timer.Stop()

webForm.Close()
