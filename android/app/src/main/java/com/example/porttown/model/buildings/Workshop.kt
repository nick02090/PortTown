package com.example.porttown.model.buildings

import com.example.porttown.model.Building
import com.example.porttown.model.Resource

class Workshop : Building {
    override fun getType(): Building.Type = Building.Type.WORKSHOP
    override fun getResourceType(): Resource.Type? = null
}