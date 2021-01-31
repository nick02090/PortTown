package com.example.porttown.di

import com.example.porttown.model.User
import com.example.porttown.network.auth.AuthService
import com.example.porttown.network.auth.AuthRepository
import com.example.porttown.session.ResourceSessionController
import com.example.porttown.session.SessionManager
import com.example.porttown.util.Api
import com.example.porttown.viewmodels.AuthViewModel
import kotlinx.coroutines.ExperimentalCoroutinesApi
import okhttp3.OkHttpClient
import okhttp3.logging.HttpLoggingInterceptor
import org.koin.androidx.viewmodel.dsl.viewModel
import org.koin.dsl.module
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.util.concurrent.TimeUnit

@ExperimentalCoroutinesApi
val appModule = module {
    factory { (user: User) -> ResourceSessionController(user) }
    single { SessionManager() }
    viewModel { AuthViewModel(authRepository = get(), sessionManager = get()) }
}

val netModule = module {
    single { provideOkHttpClient() }
    single { provideRetrofit(get()) }
    factory { provideAuthApi(retrofit = get()) }
}

fun provideOkHttpClient(): OkHttpClient {
    return OkHttpClient.Builder()
        .connectTimeout(30, TimeUnit.SECONDS)
        .readTimeout(30, TimeUnit.SECONDS)
        .writeTimeout(30, TimeUnit.SECONDS)
        .addInterceptor(HttpLoggingInterceptor().apply {
            level = HttpLoggingInterceptor.Level.BODY
        })
        .build()
}

fun provideRetrofit(okHttpClient: OkHttpClient): Retrofit {
    return Retrofit.Builder()
        .baseUrl(Api.BASE_URL)
        .client(okHttpClient)
        .addConverterFactory(GsonConverterFactory.create())
        .build()
}

fun provideAuthApi(retrofit: Retrofit): AuthService = retrofit.create(AuthService::class.java)

val repositoryModule = module {
    single { AuthRepository(authService = get()) }
}