module TicTacToeProvider.Tests

open TicTacToeProvider
open NUnit.Framework
open FsUnit

[<Test>]
let ``hello returns 42`` () =
  let result = Library.hello 42
  result |> should equal 42

[<Test>]
let ``Factorial of 0 returns 1`` () = 
  let result = Library.factorial 1
  result |> should equal 1
 
[<Test>]
let ``Factorial of bigint 120 returns 6.689502913 E+198`` () = 
  let result = Library.factorial 120I
  result |> should equal 6689502913449127057588118054090372586752746333138029810295671352301633557244962989366874165271984981308157637893214090552534408589408121859898481114389650005964960521256960000000000000000000000000000I
  

let wrap f x = fun () -> f x |> ignore
[<Test>]
let ``Factorial of -1 throws exception`` () = 
  wrap (Library.factorial) 1 |> should (throwWithMessage "n must be > or = to 0\r\nParameter name: n") typeof<System.ArgumentException>
