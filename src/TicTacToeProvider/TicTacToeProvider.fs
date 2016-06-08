namespace TicTacToeProvider

open Microsoft.FSharp.Core.CompilerServices
open ProviderImplementation.ProvidedTypes

open ``Enterprise-tic-tac-toe``.TicTacToeDomain
open ``Enterprise-tic-tac-toe``.TicTacToeImplementation

type UserAction<'a,'b> = Play of 'a | End of 'b

[<TypeProvider()>]
type TicTacToeTypeProvider() as this =
  inherit TypeProviderForNamespaces()
  
  // Get the assembly and namespace used to house the provided types
  let asm = System.Reflection.Assembly.GetExecutingAssembly()
  let ns = "TicTacToeGame"

  let displayCells cells = 
    let cellToStr cell = 
        match cell.state with
        | Empty -> "-"            
        | Played player ->
            match player with
            | PlayerO -> "O"
            | PlayerX -> "X"

    let printCells cells  = 
        cells
        |> List.map cellToStr
        |> List.reduce (fun s1 s2 -> s1 + "|" + s2) 

    let topCells = 
        cells |> List.filter (fun cell -> snd cell.pos = Top) 
    let centerCells = 
        cells |> List.filter (fun cell -> snd cell.pos = VCenter) 
    let bottomCells = 
        cells |> List.filter (fun cell -> snd cell.pos = Bottom) 

    seq {
        yield printCells topCells
        yield printCells centerCells 
        yield printCells bottomCells 
    }    

  let rec gameLoop api userAction moveName =
    let t = ProvidedTypeDefinition(moveName, Some(typeof<obj>))
    t.HideObjectMethods <- true
    t.IsErased <- true

    let initType state = 
        let displayInfo = System.String.Join("\n", state |> api.getCells |> displayCells)
        let ctor = ProvidedConstructor([])
        ctor.InvokeCode <- fun _ -> <@@ displayInfo @@>
        t.AddMember ctor
        t.AddXmlDoc(displayInfo)

    let addUserActions makeMove state moves = 
        moves 
        |> List.map(fun move -> sprintf "%A" move, Play (makeMove state move))
        |> List.iter(fun (newMoveName, play) -> t.AddMemberDelayed (fun() -> gameLoop api play newMoveName))

    match userAction with 
    | End (state) ->
        initType state
    | Play (state,moveResult) -> 
        initType state
        match moveResult with
        | GameTied -> 
            t.AddMemberDelayed (fun() -> "Game Tied" |> gameLoop api (End state)  )
        | GameWon player -> 
            t.AddMemberDelayed (fun() -> sprintf "Game Won by %A" player |> gameLoop api (End state))
        | PlayerOToMove availableMoves ->
            addUserActions api.playerOMoves  state availableMoves 
        | PlayerXToMove availableMoves ->
            addUserActions api.playerXMoves  state availableMoves 

    t

  let rootType = ProvidedTypeDefinition(asm, ns, "Game", Some (typeof<obj>), HideObjectMethods = true)
  do rootType.AddMember (gameLoop api (Play api.newGame) "Begin")
  do this.AddNamespace(ns, [rootType])
  
[<assembly:TypeProviderAssembly>] 
do()