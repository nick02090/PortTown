package com.example.porttown.ui.login

import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.view.WindowManager
import androidx.databinding.DataBindingUtil
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProvider
import com.example.porttown.R
import com.example.porttown.databinding.FragmentLoginBinding

class LoginFragment : Fragment() {
    private lateinit var loginViewModel: LoginViewModel
    private lateinit var binding: FragmentLoginBinding

    override fun onCreate(savedInstanceState: Bundle?) {
//        activity?.window!!.setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_VISIBLE);
        super.onCreate(savedInstanceState)
    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = DataBindingUtil.inflate(inflater, R.layout.fragment_login, container, false)
        loginViewModel = ViewModelProvider(this).get(LoginViewModel::class.java)

        loginViewModel.eventLoginClicked.observe(viewLifecycleOwner, Observer {
            onLoginClicked()
        })

        loginViewModel.eventRegisterClicked.observe(viewLifecycleOwner, Observer {
            onRegisterClicked()
        })

        binding.loginViewModel = loginViewModel
        binding.lifecycleOwner = this
        return binding.root
    }

    private fun onLoginClicked() {
        println("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")
        Log.d(tag, "login")
    }

    private fun onRegisterClicked() {
        Log.d("xd", "register")
    }
}