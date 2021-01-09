package com.example.porttown.session

class GameSession(var account: Account) {

    companion object {
        lateinit var gameSession: GameSession
        fun getCurrent(): GameSession {
            return gameSession
        }
    }

}