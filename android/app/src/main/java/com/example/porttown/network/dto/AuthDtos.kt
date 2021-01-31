package com.example.porttown.network.dto

import com.example.porttown.model.Town
import com.google.gson.annotations.SerializedName

sealed class AuthDtos {

    data class VerifyUser(
        @SerializedName("Id") val uid: String,
        @SerializedName("Username") val name: String,
        @SerializedName("Email") val email: String,
        @SerializedName("Town") val townDto: TownDto,
        @SerializedName("Token") val token: String
    )

    /**
     * Ne treba productions i storages buildings...
     */
    data class TownDto(
        @SerializedName("Id") val id: String,
        @SerializedName("Name") val name: String,
        @SerializedName("Level") val level: Int,
        @SerializedName("Buildings") val buildings: List<String>,
        @SerializedName("ProductionBuildings") val productions: List<String>,
        @SerializedName("Storages") val storages: List<String>,
        @SerializedName("Items") val items: List<String>,
    ) {
        fun toTown(): Town {
            return Town(id, name, level, buildings, items)
        }
    }

    class UserCheckAvailability(
        val isAvailable: Boolean
    )

    data class UserDto(
        @SerializedName("Username") var username: String,
        @SerializedName("Password") var password: String,
        @SerializedName("Email") var email: String
    )
}