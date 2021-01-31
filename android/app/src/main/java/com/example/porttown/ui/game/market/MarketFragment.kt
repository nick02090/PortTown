package com.example.porttown.ui.game.market

import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.recyclerview.widget.DefaultItemAnimator
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.porttown.databinding.FragmentMarketBinding
import com.example.porttown.model.Resource
import com.mikepenz.fastadapter.adapters.FastItemAdapter
import com.mikepenz.fastadapter.select.SelectExtension
import com.mikepenz.fastadapter.select.getSelectExtension

class MarketFragment : Fragment() {
    private lateinit var binding: FragmentMarketBinding

    //save our FastAdapter
    private lateinit var sellItemAdapter: FastItemAdapter<MarketResourceItem>
    private lateinit var selectExtension: SelectExtension<MarketResourceItem>

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?
    ): View {
        binding = FragmentMarketBinding.inflate(inflater, container, false)

        binding.briefResourceView.briefResourceImage.setImageResource(Resource.Type.GOLD.getImageResource())
        binding.briefResourceView.briefResourceCount.text = "0"

        setupResourcePicker()
        return binding.root
    }

    private fun setupResourcePicker() {
        sellItemAdapter = FastItemAdapter()
        selectExtension = sellItemAdapter.getSelectExtension()
        selectExtension.isSelectable = true

        val event = MarketResourceItem.RadioButtonClickEvent()
        sellItemAdapter.onClickListener = { _, _, item, position ->
            Log.d("Selected", "${item.resource}")
            selectExtension.deselect()
            selectExtension.select(position)
            true
        }

        sellItemAdapter.addEventHook(event)

        binding.supply.apply {
            layoutManager = LinearLayoutManager(this.context)
            itemAnimator = DefaultItemAnimator()
            adapter = sellItemAdapter
        }

        sellItemAdapter.add(Resource.Type.values()
            .filter { it != Resource.Type.GOLD }
            .map { MarketResourceItem(it) })
    }


}