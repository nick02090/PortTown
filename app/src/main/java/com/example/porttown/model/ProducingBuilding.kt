package com.example.porttown.model

import com.example.porttown.model.Building
import com.example.porttown.model.Resource

abstract class ProducingBuilding : Building {
    override fun getUsage(): Building.Usage = Building.Usage.PRODUCTION
    abstract fun produces(): List<Resource.Type>
    abstract fun amountPerSecond(): Long
}