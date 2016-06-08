namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("TicTacToeProvider")>]
[<assembly: AssemblyProductAttribute("TicTacToeProvider")>]
[<assembly: AssemblyDescriptionAttribute("Type provider for TicTacToe")>]
[<assembly: AssemblyVersionAttribute("1.0")>]
[<assembly: AssemblyFileVersionAttribute("1.0")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "1.0"
    let [<Literal>] InformationalVersion = "1.0"
