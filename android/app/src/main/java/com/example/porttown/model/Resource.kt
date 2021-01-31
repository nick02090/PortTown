package com.example.porttown.model

import com.example.porttown.R

interface Resource {
    fun getImageResource(): Int
    fun getNameResource(): Int
    fun getType(): Type
    fun getCount(): Long

    enum class Type(private val nameResource: Int, private val imageResource: Int) {
        WOOD(R.string.resource_wood, R.drawable.icon_wood),
        FOOD(R.string.resource_food, R.drawable.icon_food),
        COAL(R.string.resource_coal, R.drawable.icon_coal),
        GOLD(R.string.resource_gold, R.drawable.icon_gold),
        IRON(R.string.resource_iron, R.drawable.icon_iron),
        STONE(R.string.resource_stone, R.drawable.icon_stone);

        fun getNameResource(): Int = nameResource
        fun getImageResource(): Int = imageResource
    }
}

