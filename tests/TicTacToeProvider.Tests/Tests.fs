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
  let result = Library.tryFactorial 0
  result |> should equal (Some 1)
  
[<Test>]
let ``Factorial of 5 returns 120`` () = 
  let result = Library.tryFactorial 5
  result |> should equal (Some 120)
  
[<Test>]
let ``Factorial of -1 is none`` () = 
  Library.tryFactorial -1 |> should equal None


// // Use LanguagePrimitives.GenericZero and LanguagePrimitives.GenericOne in tryFactorial to enable any type that has a Zero, One

// let inline tryFactorial n = 
//   match n with
//     | n when n < LanguagePrimitives.GenericZero -> None
//     | n when n = LanguagePrimitives.GenericZero -> Some LanguagePrimitives.GenericOne
//     | n -> [ LanguagePrimitives.GenericOne .. n ] |> Seq.reduce (*) |> Some

// [<Test>]
// let ``Factorial of bigint 120 returns 6.689502913 E+198`` () = 
//  let result = tryFactorial 120I
//  result |> should equal (Some 6689502913449127057588118054090372586752746333138029810295671352301633557244962989366874165271984981308157637893214090552534408589408121859898481114389650005964960521256960000000000000000000000000000I)

// //tryFactorial bacon?  
// type Bacon = Uncooked | Crispy | Chewy | Little of Bacon | Lots of Bacon 
//   with static member get_Zero() = Uncooked
//        static member get_One() = Chewy
//        static member (*) (x,y) = Lots x
//        static member (+) (x,y) = Little x
  
// let result = tryFactorial Chewy
// let result2 = tryFactorial 120I


