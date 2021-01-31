package com.example.porttown.model.helpers

import com.example.porttown.model.Resource
import com.example.porttown.model.resources.*

object Resources {

    fun createAllWithProvider(countProvider: (Resource.Type) -> Long): List<Resource> {
        return listOf(
            Coal(countProvider),
            Food(countProvider),
            Gold(countProvider),
            Stone(countProvider),
            Wood(countProvider),
            Iron(countProvider)
        )
    }
}