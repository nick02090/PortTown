package com.example.porttown.model.helpers

import com.example.porttown.model.Building
import com.example.porttown.model.buildings.*

object Buildings {
    fun createAllBuildings(): List<Building> {
        return listOf(
            CoalMine(),
            GoldMine(),
            IronMine(),
            Quarry(),
            SawMill(),
            Farm(),
            FoodSilo(),
            Storage(),
            Workshop(),
            Palace()
        )
    }
}