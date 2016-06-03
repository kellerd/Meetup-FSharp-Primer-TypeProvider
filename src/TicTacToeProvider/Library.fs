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
  let inline factorial n = 
      match n with
      | n when n < LanguagePrimitives.GenericZero -> invalidArg "n" "n must be > or = to 0"
      | n -> Seq.reduce (*) [ LanguagePrimitives.GenericOne .. n ]