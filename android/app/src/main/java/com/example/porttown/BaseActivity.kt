package com.example.porttown

import android.content.Intent
import android.os.Bundle
import android.os.PersistableBundle
import android.util.Log
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.Observer
import com.example.porttown.network.auth.AuthResource
import com.example.porttown.session.SessionManager
import com.example.porttown.ui.login.AuthActivity
import org.koin.android.ext.android.inject

abstract class BaseActivity : AppCompatActivity() {
    protected val sessionManager by inject<SessionManager>()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        subscribeToSession()
        //check if user was persisted on disk...
        //sessionManager.logOut()
    }

    private fun subscribeToSession() {
        Log.d("Base", "subscribe")
        sessionManager.getAccount().observe(this, Observer { authResource ->
            when (authResource.status) {
                AuthResource.AuthStatus.NOT_AUTHENTICATED -> {
                    startAuthActivity()
                }
            }
        })
    }

    private fun startAuthActivity() {
        val intent = Intent(this, AuthActivity::class.java)
        startActivity(intent)
        finish()
    }
}