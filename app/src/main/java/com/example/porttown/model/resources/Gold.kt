package com.example.porttown.model.resources

import com.example.porttown.R
import com.example.porttown.model.Resource

class Gold : Resource {
    override fun getName(): String = "Gold"
    override fun getCount(): Long = 0
    override fun getImageResource(): Int = R.drawable.icon_gold
}