package com.example.porttown

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.MenuItem
import androidx.appcompat.app.ActionBarDrawerToggle
import androidx.core.view.GravityCompat
import androidx.navigation.NavController
import androidx.navigation.findNavController
import androidx.navigation.fragment.NavHostFragment
import androidx.navigation.ui.NavigationUI
import androidx.navigation.ui.setupActionBarWithNavController
import androidx.recyclerview.widget.GridLayoutManager
import com.example.porttown.model.resources.*
import com.example.porttown.ui.game.resources.ResourceAdapter
import com.google.android.material.navigation.NavigationView
import kotlinx.android.synthetic.main.activity_main.*
import java.lang.IllegalArgumentException

class MainActivity : AppCompatActivity(), NavigationView.OnNavigationItemSelectedListener {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        setSupportActionBar(topAppBar)
        val navHostFragment =
            supportFragmentManager.findFragmentById(R.id.nav_host_fragment) as NavHostFragment
        val navController = navHostFragment.navController

        setupActionBarWithNavController(navController, drawer_layout)
        NavigationUI.setupWithNavController(nav_view, navController)
        nav_view.setNavigationItemSelectedListener(this)

        val drawerToggle =
            ActionBarDrawerToggle(this, drawer_layout, R.string.open_drawer, R.string.close_drawer)
        drawer_layout.addDrawerListener(drawerToggle)
        drawerToggle.syncState()
        supportActionBar?.setDisplayHomeAsUpEnabled(true)


        resources_view.layoutManager = GridLayoutManager(this, 2)
        val adapter = ResourceAdapter(listOf(Food(), Gold(), Iron(), Stone(), Coal(), Wood()))
        resources_view.adapter = adapter
        if (savedInstanceState == null) {
        }
    }

    override fun onNavigationItemSelected(item: MenuItem): Boolean {
        when (item.itemId) {
            R.id.nav_logout -> {
                //remove from session manager etc...
                findNavController(R.id.nav_host_fragment).navigate(R.id.login_fragment)
            }
            R.id.nav_town -> findNavController(R.id.nav_host_fragment).navigate(R.id.town_fragment)
            R.id.nav_market -> Log.d("DRAWER", "market")
            else -> throw IllegalArgumentException("No item id!")
        }
        item.isChecked = true
        drawer_layout.closeDrawer(GravityCompat.START)
//        Log.d("DRAWER", "${item.itemId}:${item.toString()}")
//        Log.d("DRAWE", "login:${R.id.login_fragment}")
        return true
    }

    override fun onOptionsItemSelected(item: MenuItem): Boolean {
        return when (item.itemId) {
            android.R.id.home -> {
                drawer_layout.openDrawer(GravityCompat.START)
                true
            }
            else -> super.onOptionsItemSelected(item)
        }
    }

    /**
     * This fixes IllegalStateException bug, see
     * https://stackoverflow.com/questions/58703451/fragmentcontainerview-as-navhostfragment
     */
    private fun getNavController(): NavController {
        val navHostFragment =
            supportFragmentManager.findFragmentById(R.id.nav_host_fragment) as NavHostFragment
        return navHostFragment.navController
    }
}