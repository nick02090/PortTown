package com.example.porttown.viewmodels

import androidx.lifecycle.*
import com.example.porttown.model.Account
import com.example.porttown.network.auth.AuthRepository
import com.example.porttown.network.auth.AuthResource
import com.example.porttown.session.SessionManager
import kotlinx.coroutines.*

@ExperimentalCoroutinesApi
class AuthViewModel constructor(
    private val authRepository: AuthRepository,
    private val sessionManager: SessionManager
) : ViewModel() {

    val observeAuthState: LiveData<AuthResource<Account>> get() = sessionManager.getAccount()

    fun registerAccount(username: String, password: String, email: String, townName: String) {
        viewModelScope.launch {
            val source = authRepository.registerAccount(username, password, email, townName)
            sessionManager.registerAccount(source.asLiveData())
        }
    }

    companion object {
        val TAG = AuthViewModel::class.java.simpleName
    }
}