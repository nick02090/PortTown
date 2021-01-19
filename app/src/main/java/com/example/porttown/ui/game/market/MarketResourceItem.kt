package com.example.porttown.ui.game.market

import android.util.Log
import android.view.View
import android.widget.ImageView
import android.widget.RadioButton
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.porttown.R
import com.example.porttown.model.Resource
import com.mikepenz.fastadapter.FastAdapter
import com.mikepenz.fastadapter.items.AbstractItem
import com.mikepenz.fastadapter.listeners.ClickEventHook
import com.mikepenz.fastadapter.select.SelectExtension

class MarketResourceItem(val resource: Resource.Type) : AbstractItem<MarketResourceItem.ViewHolder>() {

    override val type: Int
        get() = R.id.fastadapter_radiobutton_sample_item_id

    override val layoutRes: Int
        get() = R.layout.item_market_resource

    override fun getViewHolder(v: View): ViewHolder = ViewHolder(v)

    class ViewHolder(var view: View) : FastAdapter.ViewHolder<MarketResourceItem>(view) {
        var radioButton: RadioButton = view.findViewById(R.id.radio_button)
        var resourceImage: ImageView = itemView.findViewById(R.id.resource_image)
        var resourceName: TextView = itemView.findViewById(R.id.resource_name)

        override fun bindView(item: MarketResourceItem, payloads: List<Any>) {
            val resource = item.resource
            resourceName.text = resourceName.resources.getString(resource.getNameResource())
            resourceImage.setImageResource(resource.getImageResource())
        }

        override fun unbindView(item: MarketResourceItem) {
            resourceName.text = null
            resourceImage.setImageDrawable(null)
        }

    }

    override fun bindView(holder: ViewHolder, payloads: List<Any>) {
        holder.radioButton.isChecked = isSelected
        super.bindView(holder, payloads)
    }

    class RadioButtonClickEvent : ClickEventHook<MarketResourceItem>() {
        override fun onClick(
            v: View,
            position: Int,
            fastAdapter: FastAdapter<MarketResourceItem>,
            item: MarketResourceItem
        ) {
            if (!item.isSelected) {
                Log.d("Item", "Clicked")
                val selectExtension: SelectExtension<MarketResourceItem> =
                    fastAdapter.requireExtension()
                val selections = selectExtension.selections
                if (selections.isNotEmpty()) {
                    val selectedPosition = selections.iterator().next()
                    selectExtension.deselect()
                    fastAdapter.notifyItemChanged(selectedPosition)
                }
                selectExtension.select(position)
            }
        }

        override fun onBind(viewHolder: RecyclerView.ViewHolder): View? {
            return if (viewHolder is ViewHolder) {
                viewHolder.radioButton
            } else null
        }
    }

}