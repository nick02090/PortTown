package com.example.porttown.ui.game.resources

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.porttown.R
import com.example.porttown.model.Resource

class ResourceAdapter(private val resources: List<Resource>) :
    RecyclerView.Adapter<ResourceAdapter.ResourceViewHolder>() {

    class ResourceViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        var resourceImage: ImageView = itemView.findViewById(R.id.resource_image)
        var resourceName: TextView = itemView.findViewById(R.id.resource_name)
        var resourceCount: TextView = itemView.findViewById(R.id.resource_count)

        //listeners?
        init {

        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ResourceViewHolder {
        val view = LayoutInflater.from(parent.context)
            .inflate(R.layout.fragment_single_resource, parent, false)

        return ResourceViewHolder(view)
    }

    override fun onBindViewHolder(holder: ResourceViewHolder, position: Int) {
        val resource = resources[position]
        holder.resourceName.text = resource.getName()
        holder.resourceCount.text = resource.getCount().toString()
        holder.resourceImage.setImageResource(resource.getImageResource())
    }

    override fun getItemCount(): Int = resources.size
}