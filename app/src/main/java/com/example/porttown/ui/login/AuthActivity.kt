package com.example.porttown.ui.login

import android.os.Bundle
import android.view.View
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProvider
import com.example.porttown.databinding.ActivityLoginBinding
import com.example.porttown.viewmodels.AuthViewModel

class AuthActivity : AppCompatActivity() {
    private lateinit var authViewModel: AuthViewModel
    private lateinit var binding: ActivityLoginBinding

    private var isLoginVisible = false
    private var isRegisterVisible = false

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityLoginBinding.inflate(layoutInflater)

        authViewModel = ViewModelProvider(this).get(AuthViewModel::class.java)
        authViewModel.eventLoginClicked.observe(this, Observer {
            onLoginClicked()
        })

        authViewModel.eventRegisterClicked.observe(this, Observer {
            onRegisterClicked()
        })

        authViewModel.eventNextClicked.observe(this, Observer {
            onNextClicked()
        })

        binding.loginViewModel = authViewModel
        binding.lifecycleOwner = this
        setContentView(binding.root)
    }

    private fun onLoginClicked() {
        if (isRegisterVisible) showRegisterForm(false)
        if (isLoginVisible) return
        showLoginForm()
    }

    private fun onRegisterClicked() {
        if (isRegisterVisible) return
        showRegisterForm(true)
    }

    private fun onNextClicked() {
        startLoading()
        binding.loginForm.apply {

        }
    }

    private fun showLoginForm() {
        isLoginVisible = true
        binding.loginForm.root.visibility = View.VISIBLE
    }

    private fun showRegisterForm(visibility: Boolean) {
        isRegisterVisible = visibility
        binding.loginForm.townNameInputLayout.visibility =
            if (visibility) View.VISIBLE else View.GONE
    }

    private fun startLoading() {
        binding.verifyingUser.visibility = View.VISIBLE

    }
}