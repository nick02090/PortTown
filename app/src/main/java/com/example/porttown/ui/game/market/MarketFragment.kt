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
import com.example.porttown.model.resources.Gold
import com.mikepenz.fastadapter.adapters.FastItemAdapter
import com.mikepenz.fastadapter.select.SelectExtension
import com.mikepenz.fastadapter.select.getSelectExtension
import org.koin.core.qualifier._q

class MarketFragment : Fragment() {
    private lateinit var binding: FragmentMarketBinding

    //save our FastAdapter
    private lateinit var fastItemAdapter: FastItemAdapter<MarketResourceItem>
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
        fastItemAdapter = FastItemAdapter()
        selectExtension = fastItemAdapter.getSelectExtension()
        selectExtension.isSelectable = true

        val event = MarketResourceItem.RadioButtonClickEvent()
        fastItemAdapter.onClickListener = { _, _, item, position ->
            Log.d("Selected", "${item.resource}")
            selectExtension.deselect()
            selectExtension.select(position)
            true
        }

        fastItemAdapter.addEventHook(event)

        binding.supply.apply {
            layoutManager = LinearLayoutManager(this.context)
            itemAnimator = DefaultItemAnimator()
            adapter = fastItemAdapter
        }

        fastItemAdapter.add(Resource.Type.values()
            .filter { it != Resource.Type.GOLD }
            .map { MarketResourceItem(it) })
    }


}