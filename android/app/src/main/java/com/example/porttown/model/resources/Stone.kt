package com.example.porttown.model.resources

import com.example.porttown.model.Resource


class Stone(countProvider: (Resource.Type) -> Long) : AbstractResource(countProvider) {
    override fun getImageResource(): Int = getType().getImageResource()
    override fun getNameResource(): Int = getType().getNameResource()
    override fun getType(): Resource.Type = Resource.Type.STONE
    override fun getCount(): Long = countProvider(getType())
}