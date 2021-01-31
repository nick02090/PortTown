package com.example.porttown.ui.login

import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.Toast
import androidx.appcompat.app.AlertDialog
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.Observer
import com.example.porttown.ui.main.MainActivity
import com.example.porttown.databinding.ActivityLoginBinding
import com.example.porttown.network.auth.AuthResource.AuthStatus
import com.example.porttown.viewmodels.AuthViewModel
import kotlinx.coroutines.ExperimentalCoroutinesApi
import org.koin.androidx.viewmodel.ext.android.viewModel

@ExperimentalCoroutinesApi
class AuthActivity : AppCompatActivity() {
    private val authViewModel: AuthViewModel by viewModel()
    private lateinit var binding: ActivityLoginBinding

    private var isLoginVisible = false
    private var isRegisterVisible = false

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityLoginBinding.inflate(layoutInflater)

        authViewModel.observeAuthState.observe(this, Observer {
            when (it.status) {
                AuthStatus.LOADING -> {
                    showProgressBar(true)
                    Log.d(TAG, "LOADING")
                }
                AuthStatus.NOT_AUTHENTICATED -> {
                    Toast.makeText(this, "NOT_AUTHENTICATED", Toast.LENGTH_SHORT).show()
                }

                AuthStatus.AUTHENTICATED -> {
                    showProgressBar(false)
                    Log.d(TAG, "AUTHENTICATED")
                    onLoginSuccess()
                }
                AuthStatus.ERROR -> {
                    showProgressBar(false)
                    Log.d(TAG, "ERROR")
                    Toast.makeText(this, "ERROR", Toast.LENGTH_SHORT).show()
                }
            }
        })

        binding.authViewModel = authViewModel
        binding.lifecycleOwner = this
        setContentView(binding.root)
    }

    private fun onLoginSuccess() {
        val intent = Intent(this, MainActivity::class.java)
        startActivity(intent)
        finish()
    }

    fun onLoginClicked(view: View) {
        if (isRegisterVisible) showRegisterForm(false)
        if (isLoginVisible) return
        showLoginForm()
    }

    fun onRegisterClicked(view: View) {
        if (isRegisterVisible) return
        if (!isLoginVisible) showLoginForm()
        showRegisterForm(true)
    }

    fun onNextClicked(view: View) {
        if (isRegisterVisible) {
            binding.loginForm.apply {
                authViewModel.registerAccount(
                    usernameInputText.text.toString(),
                    passwordInputText.text.toString(),
                    emailInputText.text.toString(),
                    townNameInputText.text.toString()
                )
            }
        }
    }

    private fun showLoginForm() {
        isLoginVisible = true
        binding.loginForm.root.visibility = View.VISIBLE
    }

    private fun showRegisterForm(visibility: Boolean) {
        isRegisterVisible = visibility
        binding.loginForm.apply {
            val viewVisibility = if (visibility) View.VISIBLE else View.GONE
            townNameInputLayout.visibility = viewVisibility
            emailInputLayout.visibility = viewVisibility
        }
    }

    private fun showProgressBar(visibility: Boolean) {
        binding.verifyingUser.visibility = if (visibility) View.VISIBLE else View.GONE
    }

    private fun showEmailAlreadyInUseError() {
        AlertDialog.Builder(this)
            .setMessage("Email already in use!")
            .setPositiveButton(android.R.string.ok, null)
            .show()
    }

    companion object {
        val TAG = AuthActivity::class.java.simpleName
    }
}