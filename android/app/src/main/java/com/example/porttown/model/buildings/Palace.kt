package com.example.porttown.model.buildings

import com.example.porttown.model.Building
import com.example.porttown.model.Resource

class Palace : Building {
    override fun getType(): Building.Type = Building.Type.PALACE
    override fun getResourceType(): Resource.Type? = null
}