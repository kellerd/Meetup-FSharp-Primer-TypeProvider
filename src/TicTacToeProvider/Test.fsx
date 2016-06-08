#r "bin/Release/TicTacToeProvider.dll"
type game = TicTacToeGame.Game.Begin.``PlayerXPos (HCenter, Bottom)``.``PlayerOPos (HCenter, Top)``.``PlayerXPos (Left, VCenter)``.``PlayerOPos (HCenter, VCenter)``.``PlayerXPos (Left, Bottom)``.``PlayerOPos (Left, Top)``.``PlayerXPos (Right, Bottom)``.``Game Won by PlayerX``
printfn "%A" (game())
