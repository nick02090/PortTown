package com.example.porttown.model.buildings

import com.example.porttown.model.Building
import com.example.porttown.model.Resource

class Storage : Building {
    override fun getType(): Building.Type = Building.Type.STORAGE
    override fun getResourceType(): Resource.Type? = null
}