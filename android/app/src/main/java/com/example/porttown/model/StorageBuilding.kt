package com.example.porttown.model

import com.example.porttown.model.Building
import com.example.porttown.model.Resource

abstract class StorageBuilding : Building {
    abstract fun stores(): List<Resource.Type>
    abstract fun storageSize(): Long
}