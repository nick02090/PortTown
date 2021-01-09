package com.example.porttown.model.resources

import android.graphics.drawable.Drawable
import com.example.porttown.R
import com.example.porttown.model.Resource

class Coal : Resource {
    override fun getName(): String = "Coal"
    override fun getCount(): Long = 0
    override fun getImageResource(): Int = R.drawable.icon_coal
}