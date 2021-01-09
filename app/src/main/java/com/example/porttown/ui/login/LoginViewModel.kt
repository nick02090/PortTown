package com.example.porttown.ui.login

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel

class LoginViewModel : ViewModel() {
    private val _eventLoginClicked = MutableLiveData<Boolean>()
    val eventLoginClicked: LiveData<Boolean>
        get() = _eventLoginClicked

    private val _eventRegisterClicked = MutableLiveData<Boolean>()
    val eventRegisterClicked: LiveData<Boolean>
        get() = _eventRegisterClicked

    fun onLoginClicked() {
        _eventLoginClicked.value = true
    }

    fun onRegisterClicked() {
        _eventRegisterClicked.value = true
    }
}