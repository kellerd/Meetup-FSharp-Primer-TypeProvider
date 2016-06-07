namespace TicTacToeProvider

open Microsoft.FSharp.Core.CompilerServices
open ProviderImplementation.ProvidedTypes

open ``Enterprise-tic-tac-toe``.TicTacToeDomain
open ``Enterprise-tic-tac-toe``.TicTacToeImplementation

type UserAction<'a> = Play of 'a

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

  let rec gameLoop moveName api userAction =

    let addUserActions (t:ProvidedTypeDefinition) player makeMove state moves= 
        moves 
        |> List.map(fun move -> sprintf "%A - %A" player move, Play (makeMove state move))
        |> List.iter(fun (newMoveName, play) -> t.AddMemberDelayed (fun() -> gameLoop newMoveName api play))


    let t = ProvidedTypeDefinition(moveName, Some(typeof<obj>))
    t.IsErased <- true
    match userAction with 
    | Play (state,moveResult) -> 
            let displayInfo = state 
                                |> api.getCells 
                                |> displayCells 

            let ctor = ProvidedConstructor([])
            ctor.InvokeCode <- fun args ->
              <@@
                displayInfo
              @@>
            t.AddMember ctor

            let makeMethod name inv docs = 
                let m = ProvidedMethod(name,[],inv.GetType())
                m.IsStaticMethod <- true
                m.InvokeCode <- fun _ -> <@@ inv @@>
                docs |> Option.iter (fun d -> m.AddXmlDoc(d))
                m    

            t.AddXmlDoc(System.String.Join("\n", displayInfo))
            match moveResult with
            | GameTied -> 
                let tied = ProvidedTypeDefinition("GameTied", Some(typeof<obj>))
                tied.AddXmlDoc(System.String.Join("\n", displayInfo))
                tied.AddMember ctor
                tied.IsErased <- true
                t.AddMember(tied)
            | GameWon player -> 
                let won = ProvidedTypeDefinition("GameWon", Some(typeof<obj>))
                won.AddXmlDoc(System.String.Join("\n", displayInfo))
                won.AddMember ctor
                won.IsErased <- true
                t.AddMember(won)
            | PlayerOToMove availableMoves ->
                addUserActions t PlayerO api.playerOMoves  state availableMoves 
            | PlayerXToMove availableMoves ->
                addUserActions t PlayerX api.playerXMoves  state availableMoves 


    t.HideObjectMethods <- true

    t

  let rootType = ProvidedTypeDefinition(asm, ns, "Game", Some (typeof<obj>), HideObjectMethods = true)
  do rootType.AddMember (gameLoop "Begin" api (Play api.newGame))
  do this.AddNamespace(ns, [rootType])
  
[<assembly:TypeProviderAssembly>] 
do()