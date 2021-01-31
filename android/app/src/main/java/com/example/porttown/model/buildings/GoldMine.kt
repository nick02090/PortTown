package com.example.porttown.model.buildings

import com.example.porttown.model.Building
import com.example.porttown.model.Resource

class GoldMine :Building{
    override fun getType(): Building.Type = Building.Type.GOLD_MINE
    override fun getResourceType(): Resource.Type = Resource.Type.GOLD
}