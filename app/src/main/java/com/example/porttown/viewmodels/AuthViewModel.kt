package com.example.porttown.viewmodels

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel

class AuthViewModel : ViewModel() {
    private val _eventLoginClicked = MutableLiveData<Boolean>()
    val eventLoginClicked: LiveData<Boolean> get() = _eventLoginClicked

    private val _eventRegisterClicked = MutableLiveData<Boolean>()
    val eventRegisterClicked: LiveData<Boolean> get() = _eventRegisterClicked

    private val _eventNextClicked = MutableLiveData<Boolean>()
    val eventNextClicked: LiveData<Boolean> get() = _eventNextClicked

    private val _eventLoginFailed = MutableLiveData<Boolean>()
    val eventLoginFailed: LiveData<Boolean> get() = _eventLoginFailed

    fun onLoginClicked() {
        _eventLoginClicked.value = true
    }

    fun onRegisterClicked() {
        _eventRegisterClicked.value = true
    }

    fun onNextClicked() {
        _eventNextClicked.value = true
    }
}