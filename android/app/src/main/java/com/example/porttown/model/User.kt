package com.example.porttown.model

data class User(
    val id: String,
    val nickname: String,
    val email: String,
    val authToken: String
)