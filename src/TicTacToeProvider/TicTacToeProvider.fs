namespace TicTacToeProvider

open Microsoft.FSharp.Core.CompilerServices
open ProviderImplementation.ProvidedTypes

open ``Enterprise-tic-tac-toe``.TicTacToeDomain
open ``Enterprise-tic-tac-toe``.TicTacToeImplementation

type UserAction<'a> = ContinuePlay of 'a | End

[<TypeProvider()>]
type TicTacToeTypeProvider() as this =
  inherit TypeProviderForNamespaces()
  
  // Get the assembly and namespace used to house the provided types
  let asm = System.Reflection.Assembly.GetExecutingAssembly()
  let ns = "TicTacToeGame"

  let rec gameLoop (api, userAction) =
    
    let t = ProvidedTypeDefinition(userAction, Some(typeof<obj>))
    t.IsErased <- true
    // let (ContinuePlay moves) =  userAction
    // for move in moves |> snd  do
    //     t.AddMemberDelayed (fun() -> gameLoop move)

    // t.HideObjectMethods <- true

    // let displayInfo = game.ToString()

    // let docLines = seq {
    //     yield "<summary>"
    //     for line in displayInfo.Split('\n') do
    //         yield sprintf "<para>%s</para>" line
    //     yield "</summary>"
    // }

    // t.AddXmlDoc(System.String.Join("\n", docLines))
    
    // let ctor = ProvidedConstructor([])
    // ctor.InvokeCode <- fun args ->
    //   <@@
    //     displayInfo
    //   @@>
    // t.AddMember ctor

    t

  let rootType = ProvidedTypeDefinition(asm, ns, "Game", Some (typeof<obj>), HideObjectMethods = true)
  do rootType.AddMember (gameLoop (api, ContinuePlay api.newGame))
  do this.AddNamespace(ns, [rootType])
  
[<assembly:TypeProviderAssembly>] 
do()