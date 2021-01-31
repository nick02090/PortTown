package com.example.porttown.model.buildings

import com.example.porttown.model.Building
import com.example.porttown.model.Resource

class IronMine : Building {
    override fun getType(): Building.Type = Building.Type.IRON_MINE
    override fun getResourceType(): Resource.Type = Resource.Type.IRON
}