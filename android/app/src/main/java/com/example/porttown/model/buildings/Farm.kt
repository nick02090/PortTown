package com.example.porttown.model.buildings

import com.example.porttown.model.Building
import com.example.porttown.model.Resource

class Farm : Building{
    override fun getType(): Building.Type = Building.Type.FARM
    override fun getResourceType(): Resource.Type = Resource.Type.FOOD
}