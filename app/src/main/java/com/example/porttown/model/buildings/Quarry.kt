package com.example.porttown.model.buildings

import com.example.porttown.model.Building
import com.example.porttown.model.Resource

class Quarry : Building {
    override fun getType(): Building.Type = Building.Type.QUARRY
    override fun getResourceType(): Resource.Type = Resource.Type.STONE
}