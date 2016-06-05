namespace TicTacToeProvider

/// Documentation for my library
///
/// ## Example
///
///     let h = Library.hello 1
///     printfn "%d" h
///
module Library = 
  
  /// Returns 42
  ///
  /// ## Parameters
  ///  - `num` - whatever
  let hello num = 42
  let inline tryFactorial n = 
    match n with
      | n when n < LanguagePrimitives.GenericZero -> None
      | n when n = LanguagePrimitives.GenericZero -> Some LanguagePrimitives.GenericOne
      | n -> [ LanguagePrimitives.GenericOne .. n ] |> Seq.reduce (*) |> Some
  let facTimes5 = tryFactorial 0 |> Option.map ((*) 5) |> Option.iter (printfn "%A") 