package com.example.porttown.model

import com.example.porttown.R

interface Building {

    fun getType(): Type
    fun getUsage() = getType().getUsage()
    fun getNameResource() = getType().getNameResource()

    enum class Type(private val nameResource: Int, private val usage: Usage) {
        FARM(R.string.building_farm, Usage.PRODUCTION),
        WORKSHOP(R.string.building_workshop, Usage.PRODUCTION),
        GOLD_MINE(R.string.building_gold_mine, Usage.PRODUCTION),
        FOOD_SILO(R.string.building_food_silo, Usage.STORAGE),
        STORAGE(R.string.building_storage, Usage.STORAGE),
        QUARRY(R.string.building_quarry, Usage.PRODUCTION),
        COAL_MINE(R.string.building_coal_mine, Usage.PRODUCTION),
        IRON_MINE(R.string.building_iron_mine, Usage.PRODUCTION),
        SAW_MILL(R.string.building_saw_mill, Usage.PRODUCTION),
        PALACE(R.string.building_palace, Usage.PRODUCTION);

        fun getNameResource(): Int = nameResource
        fun getUsage(): Usage = usage
    }

    enum class Usage(private val nameResource: Int) {
        STORAGE(R.string.usage_storage),
        PRODUCTION(R.string.usage_production)
    }

}