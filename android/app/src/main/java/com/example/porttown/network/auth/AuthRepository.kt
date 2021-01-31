package com.example.porttown.network.auth

import android.util.Log
import com.example.porttown.model.Account
import com.example.porttown.model.User
import com.example.porttown.network.dto.AuthDtos
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.ExperimentalCoroutinesApi
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.conflate
import kotlinx.coroutines.flow.flow
import kotlinx.coroutines.flow.flowOn

@ExperimentalCoroutinesApi
class AuthRepository(private val authService: AuthService) {

    suspend fun registerAccount(
        username: String,
        password: String,
        email: String,
        townName: String
    ) =
        flow<AuthResource<Account>> {
            emit(AuthResource.loading(null))
            try {
                val response = authService.createAccount(
                    townName,
                    AuthDtos.UserDto(username, password, email)
                )
                if (response.isSuccessful) {
                    Log.d(TAG, "response.isSuccessful")
                    val account = response.body()?.let {
                        Account(User(it.uid, it.name, it.email, it.token), it.townDto.toTown())
                    }
                    emit(AuthResource.authenticated(account))
                } else {
                    Log.d(TAG, "not successful")
                    throw IllegalStateException("Failed to verify account.")
                }
            } catch (t: Throwable) {
                Log.d(TAG, "Caught error")
                emit(AuthResource.error(null, t.message!!))
            }
        }.flowOn(Dispatchers.IO).conflate()

    companion object {
        private val TAG = AuthRepository::class.java.simpleName
    }
}