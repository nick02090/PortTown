package com.example.porttown.model

data class Town(
    val id: String,
    val name: String,
    val level: Int,
    val buildings: List<String>,
    val items: List<String>
)