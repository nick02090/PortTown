package com.example.porttown.di

import com.example.porttown.model.Account
import com.example.porttown.network.auth.AuthApi
import com.example.porttown.session.ResourceSessionController
import com.example.porttown.util.Api
import com.squareup.moshi.Moshi
import com.squareup.moshi.kotlin.reflect.KotlinJsonAdapterFactory
import okhttp3.OkHttpClient
import org.koin.dsl.module
import retrofit2.Retrofit
import retrofit2.converter.moshi.MoshiConverterFactory
import java.util.concurrent.TimeUnit

val appModule = module {
    // predati account nakon log in
    factory { (account: Account) -> ResourceSessionController(account) }

//    Defines a factory ( creates new instance every time)
//    factory { /* nesto */}
}

val netModule = module {
    single { provideOkHttpClient() }
    single { provideRetrofit(get()) }
    factory { provideAuthApi(get()) }

//    single {
//        Moshi.Builder()
//            .addLast(KotlinJsonAdapterFactory())
//            .build()
//    }
//
}

fun provideOkHttpClient(): OkHttpClient {
    return OkHttpClient.Builder()
        .connectTimeout(15, TimeUnit.SECONDS)
        .readTimeout(15, TimeUnit.SECONDS)
        .writeTimeout(15, TimeUnit.SECONDS)
        .build()
}

fun provideRetrofit(okHttpClient: OkHttpClient): Retrofit {
    return Retrofit.Builder()
        .baseUrl(Api.BASE_URL)
        .client(okHttpClient)
        .addConverterFactory(MoshiConverterFactory.create())
        .build()
}

fun provideAuthApi(retrofit: Retrofit): AuthApi = retrofit.create(AuthApi::class.java)