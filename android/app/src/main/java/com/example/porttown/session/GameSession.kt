package com.example.porttown.session

import com.example.porttown.model.User

class GameSession(var user: User) {

    companion object {
        lateinit var gameSession: GameSession
        fun getCurrent(): GameSession {
            return gameSession
        }
    }

}