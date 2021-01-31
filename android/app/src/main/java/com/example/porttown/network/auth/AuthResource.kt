package com.example.porttown.network.auth

class AuthResource<T>(
    val status: AuthStatus,
    val data: T?,
    val message: String?
) {

    companion object {
        fun <T> authenticated(data: T?): AuthResource<T> =
            AuthResource(AuthStatus.AUTHENTICATED, data, null)

        fun <T> loading(data: T?): AuthResource<T> =
            AuthResource(AuthStatus.LOADING, data, null)

        fun <T> error(data: T?, message: String): AuthResource<T> =
            AuthResource(AuthStatus.ERROR, data, message)

        fun <T> logout(): AuthResource<T> =
            AuthResource(AuthStatus.NOT_AUTHENTICATED, null, null)
    }

    enum class AuthStatus { AUTHENTICATED, ERROR, LOADING, NOT_AUTHENTICATED }
}