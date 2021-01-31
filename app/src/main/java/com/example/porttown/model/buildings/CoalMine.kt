package com.example.porttown.model.buildings

import com.example.porttown.model.Building
import com.example.porttown.model.Resource

class CoalMine : Building{
    override fun getType(): Building.Type = Building.Type.COAL_MINE
    override fun getResourceType(): Resource.Type = Resource.Type.COAL
}