package com.example.porttown.model.resources

import com.example.porttown.model.Resource

abstract class AbstractResource(var countProvider: (Resource.Type) -> Long) : Resource {
    override fun getImageResource(): Int = getType().getImageResource()
    override fun getNameResource(): Int = getType().getNameResource()
}