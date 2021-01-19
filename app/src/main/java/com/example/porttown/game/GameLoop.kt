package com.example.porttown.game

import android.content.Context
import androidx.lifecycle.LifecycleCoroutineScope
import androidx.lifecycle.LifecycleOwner
import kotlinx.coroutines.withContext

/**
 * Used for  incrementing resources
 */
class GameLoop(var coroutineScope: LifecycleCoroutineScope, var onTick: () -> Unit) {


    fun start() {

    }

    fun pause() {

    }

    fun stop() {

    }

    fun attachToTick(block: () -> Unit) {
        block()
    }
}