# Gorilla Server Stats Documentation
## Showcase

https://github.com/user-attachments/assets/03bb2c4c-f77b-4026-877b-08ccdf66fa57

THIS IS A OLD VIDEO WILL UPDATE THIS SOON

## Overview
The "Gorilla Server Stats" mod provides server statistics in the Gorilla Tag game. These stats include details like the lobby code, the number of players, the master client's nickname, the total number of players across all rooms, and the count of tags in the current room.

## Dependencies

- MODDED GORILLA TAG

## Installation

To install this mod, place it in the appropriate `BepInEx` plugins folder for Gorilla Tag.

## Features

### 1. Displaying Server Statistics

Once in a server room, a sign in the Forest location will display the following information:

Screen (1)
- Lobby Code
- Lobby Count
- Number of Players in the Room
- Master Client's Nickname
- Total Number of Players across all rooms
- Number of Tags made by the user in the current room

### 2. Tracking Tags

The mod keeps track of the number of tags made by the user during their time in a room.

## Usage

### Initialization

On game initialization, the mod locates the sign in the Forest location and prepares it for updating the stats. If the sign isn't found, appropriate errors are logged.

### Displaying Stats

The mod constantly updates the sign with the current server statistics. If the player joins a new room or leaves a room, the mod updates the sign accordingly. If the player tags another player in an Infection game mode, the mod increments the tag count for the player.
