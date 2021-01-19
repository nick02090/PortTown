package com.example.porttown

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.MenuItem
import android.widget.TextView
import androidx.appcompat.app.ActionBarDrawerToggle
import androidx.core.view.GravityCompat
import androidx.drawerlayout.widget.DrawerLayout
import androidx.navigation.NavController
import androidx.navigation.findNavController
import androidx.navigation.fragment.NavHostFragment
import androidx.navigation.ui.NavigationUI
import androidx.navigation.ui.setupActionBarWithNavController
import androidx.recyclerview.widget.GridLayoutManager
import com.example.porttown.databinding.ActivityMainBinding
import com.example.porttown.adapters.ResourceAdapter
import com.example.porttown.model.helpers.Resources
import com.google.android.material.navigation.NavigationView
import java.lang.IllegalArgumentException

class MainActivity : AppCompatActivity(), NavigationView.OnNavigationItemSelectedListener {
    private lateinit var binding: ActivityMainBinding
    private lateinit var navController: NavController
    private lateinit var drawer: DrawerLayout

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
//        if (savedInstanceState == null) {
//        val intent = Intent(this, AuthActivity::class.java)
//        startActivity(intent)
//        finish()
//        }
        binding = ActivityMainBinding.inflate(layoutInflater)
        drawer = binding.navigationDrawer

        setContentView(binding.root)
        setupNavController()
        setupNavDrawer()
        setupToolbar()
        setupResourcesTrackerView()
    }


    private fun setupNavController() {
        val navHostFragment =
            supportFragmentManager.findFragmentById(R.id.nav_host_fragment) as NavHostFragment
        navController = navHostFragment.navController
    }

    //TODO: fix single item check
    private fun setupNavDrawer() {
        NavigationUI.setupWithNavController(binding.navView, navController)
        binding.navView.setNavigationItemSelectedListener(this)
    }

    private fun setupNavDrawerHeader() {
        val header = binding.navView.getHeaderView(0)
        val headerTownName = header.findViewById<TextView>(R.id.header_username)
        val headerUserName = header.findViewById<TextView>(R.id.header_town_name)

        headerTownName.text = "Rome"
        headerUserName.text = "Marko"
    }

    private fun setupToolbar() {
        setSupportActionBar(binding.materialToolbar)
        setupActionBarWithNavController(navController, drawer)
        supportActionBar?.setDisplayHomeAsUpEnabled(true)
    }

    private fun setupResourcesTrackerView() {
        binding.resourcesView.apply {
            layoutManager = GridLayoutManager(this@MainActivity, 2)
            adapter = ResourceAdapter(Resources.createAllWithProvider { _ -> 0L })
        }
    }

    override fun onNavigationItemSelected(item: MenuItem): Boolean {
        when (item.itemId) {
            R.id.nav_logout -> {
            }
            R.id.nav_town -> navController.navigate(R.id.townFragment)
            R.id.nav_settings -> navController.navigate(R.id.settingsFragment)
            R.id.nav_market -> navController.navigate(R.id.marketFragment)
            else -> throw AssertionError("No item id!")
        }

//        item.isChecked = true
        binding.navigationDrawer.closeDrawer(GravityCompat.START)
        return true
    }

    override fun onSupportNavigateUp(): Boolean {
        return NavigationUI.navigateUp(navController, binding.navigationDrawer)
    }

    override fun onBackPressed() {
        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START)
        } else {
            super.onBackPressed()
        }
    }
}