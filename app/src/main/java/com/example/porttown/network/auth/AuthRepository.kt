package com.example.porttown.network.auth

class AuthRepository(private val authApi: AuthApi) {
    suspend fun getAccount(name: String, password: String) = authApi.getAccount(name, password)
}