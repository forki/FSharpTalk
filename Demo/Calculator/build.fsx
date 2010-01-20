#light
// include Fake libs
#I "tools\FAKE"
#r "FakeLib.dll"
open Fake 

// Default target
Target "Default" (fun () ->
  trace "Hello World from FAKE"
)

// start build
run "Default"
    