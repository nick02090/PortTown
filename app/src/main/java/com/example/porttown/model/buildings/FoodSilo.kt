package com.example.porttown.model.buildings

import com.example.porttown.model.Building
import com.example.porttown.model.Resource

class FoodSilo : Building {
    override fun getType(): Building.Type = Building.Type.FOOD_SILO
    override fun getResourceType(): Resource.Type? = null
}