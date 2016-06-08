# TicTacToeProvider

This project is a demo to showcase a silly type provider for playing a game of TicTacToe in your IDE

To build, run:

    > build.cmd // on windows    
    $ ./build.sh  // on unix

Then reference TicTacToeProvider.dll from another project or script

    #r "bin/Release/TicTacToeProvider.dll"
    type game = TicTacToeGame.Game.Begin.``PlayerXPos (HCenter, Bottom)``...
    printfn "%A" (game())

Documentation: http://kellerd.github.io/TicTacToeProvider

## Maintainer(s)

- [@kellerd](https://github.com/kellerd)
