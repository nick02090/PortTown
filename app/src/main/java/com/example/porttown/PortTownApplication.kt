package com.example.porttown

import android.app.Application
import com.example.porttown.di.appModule
import com.example.porttown.di.netModule
import org.koin.android.ext.koin.androidContext
import org.koin.core.context.startKoin

class PortTownApplication : Application() {

    override fun onCreate() {
        super.onCreate()

        startKoin {
            androidContext(this@PortTownApplication)

            modules(
                listOf(
                    appModule,
                    netModule
                )
            )
        }
    }
}