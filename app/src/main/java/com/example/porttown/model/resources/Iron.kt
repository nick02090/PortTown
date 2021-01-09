package com.example.porttown.model.resources

import com.example.porttown.R
import com.example.porttown.model.Resource

class Iron : Resource {
    override fun getName(): String = "Iron"
    override fun getCount(): Long = 0
    override fun getImageResource(): Int = R.drawable.icon_iron
}