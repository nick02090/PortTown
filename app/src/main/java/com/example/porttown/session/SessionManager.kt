package com.example.porttown.session

import android.util.Log
import androidx.lifecycle.LiveData
import androidx.lifecycle.MediatorLiveData
import androidx.lifecycle.Observer
import com.example.porttown.model.Account
import com.example.porttown.network.auth.AuthResource

class SessionManager {
    private var cachedAccount = MediatorLiveData<AuthResource<Account>>()

    fun registerAccount(source: LiveData<AuthResource<Account>>) {
        if (cachedAccount.value != null) {
            cachedAccount.apply {
                addSource(source, Observer { authResource ->
                    this.value = authResource
                })
            }
        }
    }

    fun logOut() {
        Log.d(TAG, "Logging out...")
        cachedAccount.value = AuthResource.logout()
    }

    fun getAccount(): LiveData<AuthResource<Account>> {
        return cachedAccount
    }

    fun getNickname(): String? {
        return cachedAccount.value?.data?.user?.nickname
    }

    fun getTownName(): String? {
        return cachedAccount.value?.data?.town?.name
    }
    companion object {
        val TAG = SessionManager::class.java.simpleName
    }
}