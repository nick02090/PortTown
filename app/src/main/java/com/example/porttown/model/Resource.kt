package com.example.porttown.model

import android.graphics.drawable.Drawable
import android.widget.ImageView

interface Resource {
    fun getName(): String
    fun getCount(): Long
    fun getImageResource(): Int
}