# Multiplayer-Unity-Game

A tech demo made using multiple services combined within Unity. The project is two player co-op puzzle game which requires player co-opeartion. The services used were - 

## Gamesparks 

The project integrates a Gamesparks backend using the Gamesparks SDK. It allows user registration and authentication, save and loading player data, post player
score to a leaderboard and rewarding players with items from a loot table pulled from Gamesparks.

## AWS Lambda

The project connect to AWS Lambda to manipulate some player data and return the data to the request. The connection is established through a [web socket library](https://github.com/endel/NativeWebSocket)
which allows for a consistent connection. The application sends a request to AWS through the API gateway route and triggers the function to increment the player score.

## Photon Multiplayer Framework

The project has integrated Photon networking with lobby functionality and server replication. The project can host and join lobbies for both players to join. The application also has 
server replication through Photon's observable components functionality which allows for specific targeting of data and proccess that need to be communicated to other players.
