package com.inmotion.in_motion_android.util

import android.content.Context
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import kotlin.math.abs

class FocusedItemFinder(
    private val context: Context,
    private val layoutManager: LinearLayoutManager,
    private val callback: (Int) -> Unit,
    private val controlState: Int = RecyclerView.SCROLL_STATE_IDLE
) :
    RecyclerView.OnScrollListener() {
    override fun onScrollStateChanged(recyclerView: RecyclerView, newState: Int) {
        if (controlState == ALL_STATES || newState == controlState) {
            val firstVisible = layoutManager.findFirstVisibleItemPosition()
            val lastVisible = layoutManager.findLastVisibleItemPosition()
            val itemsCount = lastVisible - firstVisible + 1
            val screenCenter: Int = context.resources.displayMetrics.widthPixels / 2
            var minCenterOffset = Int.MAX_VALUE
            var middleItemIndex = 0
            for (index in 0 until itemsCount) {
                val listItem = layoutManager.getChildAt(index) ?: return
                val topOffset = listItem.top
                val bottomOffset = listItem.bottom
                val centerOffset =
                    abs(topOffset - screenCenter) + abs(bottomOffset - screenCenter)
                if (minCenterOffset > centerOffset) {
                    minCenterOffset = centerOffset
                    middleItemIndex = index + firstVisible
                }
            }
            callback(middleItemIndex)
        }
    }

    companion object {
        const val ALL_STATES = 10
    }
}
