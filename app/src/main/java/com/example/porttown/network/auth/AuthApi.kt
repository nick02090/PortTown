package com.example.porttown.network.auth

import android.accounts.Account
import com.example.porttown.network.dto.AuthDtos
import retrofit2.Call
import retrofit2.http.GET
import retrofit2.http.Header
import retrofit2.http.POST
import retrofit2.http.Url

interface AuthApi {

    @GET
    suspend fun getAccount(
        @Url name: String,
        @Header("Authorization") password: String
    ): Call<AuthDtos.VerifyUser>

    //not using any encryption from server side
    @POST("/register/account")
    suspend fun createAccount(
        account: Account
    ): Call<AuthDtos.VerifyUser>
}