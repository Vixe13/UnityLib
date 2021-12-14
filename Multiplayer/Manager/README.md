# Player Manager

## Role
This script is there to create a local multiplayer in the game.

This script manages the entry of players into the game from a prefab entered in the PlayerInputManager.

For this, as soon as the game starts, it will disable all the scripts that need the player’s prefab to work.
Once all the players have joined the game, it reactivates all these scripts.


## **!! Warning !!**
Pour être sûr que le sripts s'éxecute en premier, il est préférable de mettre le script en premier dans le Script Execution order.
(Méthode temporaire car non optimisé)

![ScriptExecutionOrder](./Documentation~/Img/ScriptExecutionOrder.jpg "ScriptExecutionOrder")


## ***Requirements***
- Input System asset
- PlayerInputManager components in script's object.
- Player input component in a player prefab








---
## Cas pratique






















