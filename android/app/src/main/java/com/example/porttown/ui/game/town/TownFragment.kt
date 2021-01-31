package com.example.porttown.ui.game.town

import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.recyclerview.widget.DefaultItemAnimator
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.porttown.databinding.FragmentTownBinding
import com.example.porttown.model.helpers.Buildings
import com.mikepenz.fastadapter.adapters.FastItemAdapter

private const val TAG = "TownFragment"

class TownFragment : Fragment() {
    private lateinit var binding: FragmentTownBinding
    private lateinit var buildingAdapter: FastItemAdapter<TownBuildingItem>
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?
    ): View {
        binding = FragmentTownBinding.inflate(inflater, container, false)


        setupBuildingPicker()

        return binding.root
    }

    private fun setupBuildingPicker() {
        buildingAdapter = FastItemAdapter()
        buildingAdapter.onClickListener = { _, _, item, position ->
            Log.d(TAG, "Clicked ${item.building}")
            true
        }

        binding.buildingList.apply {
            layoutManager = LinearLayoutManager(this.context)
            itemAnimator = DefaultItemAnimator()
            adapter = buildingAdapter
        }

        buildingAdapter.add(Buildings.createAllBuildings().map { TownBuildingItem(it) })
    }
}