package com.example.porttown.network.auth

import com.example.porttown.network.dto.AuthDtos
import retrofit2.Response
import retrofit2.http.*

interface AuthService {

    @POST("/user/")
    suspend fun postUser(
        @Url name: String,
        @Header("Authorization") password: String
    ): Response<AuthDtos.VerifyUser>

    @POST("user/check-availability")
    suspend fun checkAvailability(
        @Body email: Map<String, String>
    ): Response<Boolean>

    @Headers("Content-Type: application/json")
    @POST("user/register/{townName}")
    suspend fun createAccount(
        @Path(value = "townName") townName: String,
        @Body userDto: AuthDtos.UserDto
    ): Response<AuthDtos.VerifyUser>
}