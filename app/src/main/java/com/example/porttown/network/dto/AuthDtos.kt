package com.example.porttown.network.dto

import com.example.porttown.model.Town
import com.squareup.moshi.Json

sealed class AuthDtos {

    data class VerifyUser(
        @field:Json(name = "uid") val uid: String,
        @field:Json(name = "name") val name: String,
        @field:Json(name = "town") val town: Town
    )
}