package com.example.porttown.model.buildings

import com.example.porttown.model.Building
import com.example.porttown.model.Resource

class SawMill : Building {
    override fun getType(): Building.Type = Building.Type.SAW_MILL
    override fun getResourceType(): Resource.Type = Resource.Type.WOOD
}