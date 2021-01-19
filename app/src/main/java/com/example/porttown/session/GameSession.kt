package com.example.porttown.session

import com.example.porttown.model.Account

class GameSession(var account: Account) {

    companion object {
        lateinit var gameSession: GameSession
        fun getCurrent(): GameSession {
            return gameSession
        }
    }

}