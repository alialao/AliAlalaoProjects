package com.testrsrpechhulp.models;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.location.LocationManager;
import android.net.ConnectivityManager;
import android.util.Log;

import com.testrsrpechhulp.ui.RSRPechhulpActivity;

import static com.testrsrpechhulp.Constants.CONNECTION_BROADCAST_ACTION;

public class MyBroadcastReceiver extends BroadcastReceiver {

    private static final String TAG = "MyBroadcastReceiver";

    @Override
    public void onReceive(Context context, Intent intent) {
        Log.d(TAG, "onReceive: MyBroadcastReceiver called");
        LocationManager locationManager = (LocationManager) context.getSystemService(context.LOCATION_SERVICE);
        Intent broadcast = new Intent(context, RSRPechhulpActivity.class);
        if (ConnectivityManager.CONNECTIVITY_ACTION.equals(intent.getAction())) {
            Log.d(TAG, "onReceive: Network connection has been changed");

            broadcast.setAction(String.valueOf(CONNECTION_BROADCAST_ACTION));
            intent.putExtra("Network", "CONNECTIVITY");
            context.sendBroadcast(broadcast);
        }

        if (locationManager.isProviderEnabled(LocationManager.GPS_PROVIDER)) {
            Log.d(TAG, "onReceive: GPS provider state has been changed");

            broadcast.setAction(String.valueOf(CONNECTION_BROADCAST_ACTION));
            intent.putExtra("GPS", "GPSPROVIDER");
            context.sendBroadcast(broadcast);
        }
    }
}
