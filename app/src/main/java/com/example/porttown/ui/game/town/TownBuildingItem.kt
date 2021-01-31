package com.example.porttown.ui.game.town

import android.view.View
import android.widget.ImageView
import android.widget.TextView
import com.example.porttown.R
import com.example.porttown.model.Building
import com.mikepenz.fastadapter.FastAdapter
import com.mikepenz.fastadapter.items.AbstractItem
import java.util.*

class TownBuildingItem(val building: Building) : AbstractItem<TownBuildingItem.ViewHolder>() {

    override val layoutRes: Int
        get() = R.layout.item_building

    override val type: Int
        get() = R.id.building_item_id

    override fun getViewHolder(v: View): ViewHolder = ViewHolder(v)
    class ViewHolder(var view: View) : FastAdapter.ViewHolder<TownBuildingItem>(view) {
        var buildingName: TextView = view.findViewById(R.id.building_name)
        var buildingUsage: TextView = view.findViewById(R.id.building_type)
        var buildingLevelText: TextView = view.findViewById(R.id.building_level)
        var buildingLevel: TextView = view.findViewById(R.id.building_level)
        var buildingResource: ImageView = view.findViewById(R.id.resource_image)

        override fun bindView(item: TownBuildingItem, payloads: List<Any>) {
            val building = item.building
            val resources = buildingName.resources
            buildingName.text = resources.getString(building.getNameResource())
            buildingUsage.text = resources.getString(building.getUsage().getNameResource())
            buildingLevel.text = (Random().nextInt(8) + 1).toString()
            buildingResource.setImageResource(
                if (building.getResourceType() == null) {
                    android.R.color.transparent
                } else {
                    building.getResourceType()!!.getImageResource()
                }
            )
        }

        override fun unbindView(item: TownBuildingItem) {
            buildingName.text = null
            buildingUsage.text = null
            buildingLevel.text = null
            buildingResource.setImageDrawable(null)
        }

    }

}