package com.example.porttown.ui.game.town

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.example.porttown.databinding.FragmentTownBinding

class TownFragment : Fragment() {
    private lateinit var binding: FragmentTownBinding

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?
    ): View {
        binding = FragmentTownBinding.inflate(inflater, container, false)



        return binding.root
    }
}