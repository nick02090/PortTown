package com.example.porttown.model.resources

import com.example.porttown.model.Resource


class Gold(countProvider: (Resource.Type) -> Long) : AbstractResource(countProvider) {
    override fun getImageResource(): Int = getType().getImageResource()
    override fun getNameResource(): Int = getType().getNameResource()
    override fun getType(): Resource.Type = Resource.Type.GOLD
    override fun getCount(): Long = countProvider(getType())
}