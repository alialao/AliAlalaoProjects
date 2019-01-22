package com.testrsrpechhulp.ui;

import android.Manifest;
import android.app.Dialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.pm.PackageManager;
import android.content.res.Configuration;
import android.location.Address;
import android.location.Geocoder;
import android.location.Location;
import android.location.LocationManager;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.Uri;
import android.os.Handler;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GoogleApiAvailability;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.location.FusedLocationProviderClient;
import com.google.android.gms.location.LocationListener;
import com.google.android.gms.location.LocationRequest;
import com.google.android.gms.location.LocationServices;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.BitmapDescriptorFactory;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.testrsrpechhulp.R;
import com.testrsrpechhulp.models.MyBroadcastReceiver;
import com.testrsrpechhulp.models.UserLocationInfo;

import java.io.IOException;
import java.util.List;
import java.util.Locale;

import static com.testrsrpechhulp.Constants.CONNECTION_BROADCAST_ACTION;
import static com.testrsrpechhulp.Constants.ERROR_DIALOG_REQUEST;
import static com.testrsrpechhulp.Constants.PERMISSIONS_REQUEST_ACCESS_FINE_LOCATION;
import static com.testrsrpechhulp.Constants.PERMISSIONS_REQUEST_CALL;
import static com.testrsrpechhulp.Constants.PERMISSIONS_REQUEST_ENABLE_GPS;
import static com.testrsrpechhulp.Constants.PERMISSIONS_REQUEST_ENABLE_WIFI;

public class RSRPechhulpActivity extends AppCompatActivity implements OnMapReadyCallback, GoogleApiClient.ConnectionCallbacks, GoogleApiClient.OnConnectionFailedListener, LocationListener {

    private static final String TAG = "RSRPechhulpActivity";
    private Handler mHandler = new Handler();

    //widgets
    private Button btnCall;
    private Button btnCallNoew;
    private TextView btnCancel;
    private View dialogCall;

    //vars
    private boolean mLocationPermissionGranted = false;
    private boolean mCallPhonePermissionGranted = false;

    private MyBroadcastReceiver broadcastReceiver = new MyBroadcastReceiver();

    private GoogleMap mMap;
    private GoogleApiClient googleApiClient;
    private Marker mMarker;
    private int moveCameraCounter = 0;
    private FusedLocationProviderClient mFusedLocationProviderClient;
    private LocationRequest locationRequest;
    private UserLocationInfo mUserLocationInfo;

    private final static long UPDATE_INTERVAL = 10 * 1000;  /* 10 secs */
    private final static long FASTEST_INTERVAL = 5000; /* 5 sec */
    private static final float DEFAULT_ZOOM = 15.9F;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_rsrpechhulp);

        addToolbar();
        init();
    }

    @Override
    protected void onStart() {
        super.onStart();

        IntentFilter filter = new IntentFilter();
        filter.addAction(ConnectivityManager.CONNECTIVITY_ACTION);
        filter.addAction(LocationManager.GPS_PROVIDER);

        registerReceiver(broadcastReceiver, filter);
        Log.d(TAG, "onStart: broadcastReceiver have been registered");
    }

    private void init() {
        Log.d(TAG, "init: called");

        googleApiClient = new GoogleApiClient.Builder(this)
                .addApi(LocationServices.API)
                .addConnectionCallbacks(this)
                .addOnConnectionFailedListener(this)
                .build();

        btnCall = (Button) findViewById(R.id.btnCallRSR);
        btnCallNoew = (Button) findViewById(R.id.btn_dialog_call_rsr);
        btnCancel = (TextView) findViewById(R.id.btn_cancel_call_dialog);
        dialogCall = findViewById(R.id.dialog_call_rsr);

        btnCall.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Log.d(TAG, "onClick: btn call");
                btnCall.setVisibility(View.GONE);

                dialogCall.setVisibility(View.VISIBLE);
            }
        });

        btnCancel.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Log.d(TAG, "onClick: btn Cancel");
                btnCall.setVisibility(View.VISIBLE);
                dialogCall.setVisibility(View.GONE);
            }
        });

        if (isTablet()) {
            dialogCall.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    if (dialogCall.getVisibility() == View.VISIBLE) {
                        dialogCall.setVisibility(View.GONE);
                        btnCall.setVisibility(View.VISIBLE);
                    }
                }
            });
        }

        btnCallNoew.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Log.d(TAG, "onClick: btn call now");
                makeCall();
            }
        });
    }

    //region map
    private void initMap() {
        Log.d(TAG, "initMap: initializing map");
        SupportMapFragment mapFragment = (SupportMapFragment) getSupportFragmentManager()
                .findFragmentById(R.id.map);
        mapFragment.getMapAsync(RSRPechhulpActivity.this);
    }

    @Override
    public void onMapReady(GoogleMap googleMap) {
        Log.d(TAG, "onMapReady: Map is ready");
        mMap = googleMap;
        mMap.getUiSettings().setMapToolbarEnabled(false);

        //initializeMap();
    }

    public void initializeMap() {
        Log.d(TAG, "initializeMap: called");
        if (checkServices()) {
            if (mLocationPermissionGranted) {
                Log.d(TAG, "initializeMap: **************************************************");
                if (!googleApiClient.isConnected()) {
                    googleApiClient.connect();
                }

                locationRequest = new LocationRequest();
                locationRequest.setInterval(UPDATE_INTERVAL);
                locationRequest.setFastestInterval(FASTEST_INTERVAL);
                locationRequest.setPriority(LocationRequest.PRIORITY_HIGH_ACCURACY);
                moveCameraCounter = 0;
                //getDeviceLocation();
                requistLocationUpdate();
                initMap();

            } else {
                getLocationPermission();
            }
        }
    }

    private void moveCamera(LatLng latLng, float zoom) {
        if (latLng != null && moveCameraCounter < 1) {
            Log.d(TAG, "moveCamera: moving the camera to: Lat: " + latLng.latitude + ", Lng: " + latLng.longitude);
            mMap.moveCamera(CameraUpdateFactory.newLatLngZoom(latLng, zoom));
            moveCameraCounter++;
        }
    }

    private void addMarkerSnippet(LatLng latLng, UserLocationInfo userLocationInfo) {
        Log.d(TAG, "addSnippet: called");
        mMap.clear();
        mMap.setInfoWindowAdapter(new CustomInfoWindowAdapter(RSRPechhulpActivity.this));

        if (userLocationInfo != null) {
            try {
                String snippet = userLocationInfo.getStreetName() + " " +
                        userLocationInfo.getHouseNumber() + ", " +
                        userLocationInfo.getPostalCode() + "\n" +
                        userLocationInfo.getCity() + ", " +
                        userLocationInfo.getCountry() +
                        "\n\n" + "Onthoud deze locatie voor het" + "\n" + "telefoongesprek.";

                MarkerOptions markerOptions = new MarkerOptions()
                        .position(latLng)
                        .icon(BitmapDescriptorFactory.fromResource(R.drawable.map_marker))
                        .title("Uw Location:")
                        .snippet(snippet);
                mMarker = mMap.addMarker(markerOptions);
                mMarker.showInfoWindow();
            } catch (NullPointerException e) {
                Log.e(TAG, "addSnippet: NullPointerException: " + e.getMessage());
            }
        }
    }

    //endregion

    //region device location
    private void getDeviceLocation() {
        Log.d(TAG, "getDeviceLocation: getting the device current location");

        mFusedLocationProviderClient = LocationServices.getFusedLocationProviderClient(this);
        try {
            Task location = mFusedLocationProviderClient.getLastLocation();
            location.addOnCompleteListener(new OnCompleteListener() {
                @Override
                public void onComplete(@NonNull Task task) {
                    if (task.isSuccessful()) {
                        Log.d(TAG, "onComplete: found location");

                        Location currentLocation = (Location) task.getResult();
                        try {
                            Log.d(TAG, "onComplete: found location: Lat: " +
                                    currentLocation.getLatitude() + ", Log: " + currentLocation.getLongitude());
                            moveCamera(new LatLng(currentLocation.getLatitude(), currentLocation.getLongitude()), DEFAULT_ZOOM);
                            getLocationInfo(new LatLng(currentLocation.getLatitude(), currentLocation.getLongitude()));
                        } catch (NullPointerException e) {
                            Log.e(TAG, "onComplete: NullPointerException" + e.getMessage());
                        }
                    }
                }
            });
        } catch (SecurityException e) {
            Log.e(TAG, "getDeviceLocation: SecurityException: " + e.getMessage());
        }
    }

    @Override
    public void onConnected(@Nullable Bundle bundle) {
        initializeMap();
    }

    private void requistLocationUpdate() {
        Log.d(TAG, "requistLocationUpdate: called");
        if (googleApiClient != null && googleApiClient.isConnected()) {
            if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) ==
                    PackageManager.PERMISSION_GRANTED &&
                    ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) ==
                            PackageManager.PERMISSION_GRANTED) {
                Log.d(TAG, "requistLocationUpdate: requestLocationUpdates called");
                LocationServices.FusedLocationApi.requestLocationUpdates(googleApiClient, locationRequest, this);
            }
        }
    }

    @Override
    public void onLocationChanged(Location location) {
        Log.d(TAG, "onLocationChanged: ");
        try {
            Log.d(TAG, "onLocationChanged: found location: Lat: " +
                    location.getLatitude() + ", Log: " + location.getLongitude());
            moveCamera(new LatLng(location.getLatitude(), location.getLongitude()), DEFAULT_ZOOM);
            getLocationInfo(new LatLng(location.getLatitude(), location.getLongitude()));
        } catch (NullPointerException e) {
            Log.e(TAG, "onLocationChanged: NullPointerException" + e.getMessage());
        }
    }

    private void removeLocationUpdate() {
        Log.d(TAG, "removeLocationUpdate: ");
        if (googleApiClient != null && googleApiClient.isConnected()) {
            LocationServices.FusedLocationApi.removeLocationUpdates(googleApiClient, this);
        }
    }

    @Override
    public void onConnectionSuspended(int i) {
        Log.d(TAG, "onConnectionSuspended: ");
        //initializeMap();
    }

    @Override
    public void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
        Log.d(TAG, "onConnectionFailed: ");
        //initializeMap();
    }

    private void getLocationInfo(LatLng latLng) {
        Log.d(TAG, "getLocationInfo: Called");
        Geocoder geocoder = new Geocoder(getApplicationContext(), Locale.getDefault());

        try {
            List<Address> addressList = geocoder.getFromLocation(latLng.latitude,
                    latLng.longitude, 1);

            if (addressList != null && addressList.size() > 0) {
                Log.d("Address: ", addressList.get(0).toString());

                mUserLocationInfo = new UserLocationInfo();
                mUserLocationInfo.setStreetName(addressList.get(0).getThoroughfare());
                mUserLocationInfo.setHouseNumber(addressList.get(0).getFeatureName());
                mUserLocationInfo.setPostalCode(addressList.get(0).getPostalCode());
                mUserLocationInfo.setCity(addressList.get(0).getLocality());
                mUserLocationInfo.setCountry(addressList.get(0).getCountryCode());
                mUserLocationInfo.setLatitude(latLng.latitude);
                mUserLocationInfo.setLongitude(latLng.longitude);

                Log.d(TAG, "getLocationInfo: LocationInfo: " + mUserLocationInfo.toString());

                addMarkerSnippet(latLng, mUserLocationInfo);
            }
        } catch (NullPointerException e) {
            Log.d(TAG, "getLocationInfo: NullPointerException: " + e.getMessage());
        } catch (IOException e) {
            Log.d(TAG, "getLocationInfo: IOException: " + e.getMessage());
        }
    }
    //endregion

    //region Services
    //Determine whether or not if the device able to use google services
    public boolean isServicesOK() {
        Log.d(TAG, "isServicesOK: checking google services version");

        int available = GoogleApiAvailability.getInstance().isGooglePlayServicesAvailable(RSRPechhulpActivity.this);

        if (available == ConnectionResult.SUCCESS) {
            //everything is fine and the user can make map requests
            Log.d(TAG, "isServicesOK: Google Play Services is working");
            return true;
        } else if (GoogleApiAvailability.getInstance().isUserResolvableError(available)) {
            //an error occurred but we can resolve it
            Log.d(TAG, "isServicesOK: an error occurred but we can fix it");
            Dialog dialog = GoogleApiAvailability.getInstance().getErrorDialog(RSRPechhulpActivity.this, available, ERROR_DIALOG_REQUEST);
            dialog.show();
        } else {
            Toast.makeText(this, "You can't make map requests", Toast.LENGTH_SHORT).show();
        }
        return false;
    }

    //region network Service
    //Determine whether or not the application has Network enabled
    private boolean isNetworkEnabled() {
        Log.d(TAG, "isNetworkEnabled: ");
        final ConnectivityManager connectivityManager =
                (ConnectivityManager) getSystemService(Context.CONNECTIVITY_SERVICE);

        NetworkInfo activeNetwork = connectivityManager.getActiveNetworkInfo();
        if (activeNetwork != null &&
                activeNetwork.isConnectedOrConnecting()) {
            Log.d(TAG, "isNetworkEnabled: Network is on");
            return true;
        } else {
            Log.d(TAG, "isNetworkEnabled: Network is off");
            buildAlertMessageNoNetwork();
            return false;
        }
    }

    private void buildAlertMessageNoNetwork() {
        Log.d(TAG, "buildAlertMessageNoNetwork: ");
        final AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle("Network is uitgeschakeld");
        builder.setMessage("Uw netwerk is uitgeschakeld.\n" +
                "Schakel het alstublieft in om door te gaan.")
                .setCancelable(false)
                .setPositiveButton("AANZETTEN", new DialogInterface.OnClickListener() {
                    public void onClick(@SuppressWarnings("unused") final DialogInterface dialog,
                                        @SuppressWarnings("unused") final int id) {
                        Intent enableWifiIntent = new Intent(android.provider.Settings.ACTION_WIFI_SETTINGS);
                        startActivityForResult(enableWifiIntent, PERMISSIONS_REQUEST_ENABLE_WIFI);
                    }
                })
                .setNegativeButton("ANNULEREN", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(final DialogInterface dialog, final int which) {
                        backToMenu();
                    }
                });

        final AlertDialog alert = builder.create();
        alert.show();
    }
    //endregion network Service

    //region GPS
    //Determine whether or not the application has GPS enabled
    public boolean isMapsEnabled() {
        Log.d(TAG, "isMapsEnabled: ");
        final LocationManager manager = (LocationManager) getSystemService(Context.LOCATION_SERVICE);

        if (!manager.isProviderEnabled(LocationManager.GPS_PROVIDER)) {
            Log.d(TAG, "isMapsEnabled: GPS is off");
            buildAlertMessageNoGps();
            return false;
        }
        Log.d(TAG, "isMapsEnabled: GPS is on");
        return true;
    }

    private void buildAlertMessageNoGps() {
        Log.d(TAG, "buildAlertMessageNoGps: ");
        final AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle("GPS uitgeschakeld");
        builder.setMessage("Uw GPS is uitgeschakeld. \n" +
                "Schakel het alstublieft in om door te gaan.")
                .setCancelable(false)
                .setPositiveButton("AANZETTEN", new DialogInterface.OnClickListener() {
                    public void onClick(@SuppressWarnings("unused") final DialogInterface dialog,
                                        @SuppressWarnings("unused") final int id) {
                        Intent enableGpsIntent = new Intent(android.provider.Settings.ACTION_LOCATION_SOURCE_SETTINGS);
                        startActivityForResult(enableGpsIntent, PERMISSIONS_REQUEST_ENABLE_GPS);
                    }
                })
                .setNegativeButton("ANNULEREN", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(final DialogInterface dialog, final int which) {
                        backToMenu();
                    }
                });

        final AlertDialog alert = builder.create();
        alert.show();
    }
    //endregion GPS

    private boolean checkServices() {
        if (isServicesOK()) {
            Log.d(TAG, "checkServices: Google services is Ok");
            if (isNetworkEnabled()) {
                Log.d(TAG, "checkServices: Network is enabled");
                if (isMapsEnabled()) {
                    Log.d(TAG, "checkServices: GPS is enabled");
                    return true;
                }
                Log.d(TAG, "checkServices: GPS is disabled");
                return false;
            }
            Log.d(TAG, "checkServices: Network is disabled");
            return false;
        }
        Log.d(TAG, "checkServices: Google services is not valid");
        return false;
    }
    //endregion Services

    //region Permission
    //Ask for location permission
    private void getLocationPermission() {
        Log.d(TAG, "getLocationPermission: Getting location permission");
        //check Permission
        if (ContextCompat.checkSelfPermission(this,
                android.Manifest.permission.ACCESS_FINE_LOCATION) == PackageManager.PERMISSION_GRANTED) {
            // Permission has already been granted
            Log.d(TAG, "getLocationPermission: Location Permission is granted");
            mLocationPermissionGranted = true;
            initializeMap();
        } else {
            // Permission is not granted
            Log.d(TAG, "getLocationPermission: Location Permission is not granted: Ask for permission");
            ActivityCompat.requestPermissions(this,
                    new String[]{Manifest.permission.ACCESS_FINE_LOCATION},
                    PERMISSIONS_REQUEST_ACCESS_FINE_LOCATION);
        }
    }

    //Ask for call phone permission
    private void getCallPhonePermission() {
        Log.d(TAG, "getCallPhonePermission: Getting call phone permission");
        //check Permission
        if (ContextCompat.checkSelfPermission(this,
                Manifest.permission.CALL_PHONE) == PackageManager.PERMISSION_GRANTED) {
            Log.d(TAG, "getCallPhonePermission: Call Phone Permission is granted");
            mCallPhonePermissionGranted = true;
            makeCall();
        } else {
            Log.d(TAG, "getCallPhonePermission: Call Phone Permission is not granted");
            ActivityCompat.requestPermissions(this,
                    new String[]{Manifest.permission.CALL_PHONE}, PERMISSIONS_REQUEST_CALL);
        }
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String permissions[], @NonNull int[] grantResults) {
        Log.d(TAG, "onRequestPermissionsResult: called.");
        mLocationPermissionGranted = false;
        mCallPhonePermissionGranted = false;
        switch (requestCode) {
            case PERMISSIONS_REQUEST_ACCESS_FINE_LOCATION: {
                if (grantResults.length > 0 && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                    if ((ContextCompat.checkSelfPermission(RSRPechhulpActivity.this,
                            Manifest.permission.ACCESS_FINE_LOCATION) == PackageManager.PERMISSION_GRANTED)) {
                        Log.d(TAG, "onRequestPermissionsResult: ACCESS_FINE_LOCATION: permission granted");
                        mLocationPermissionGranted = true;
                        //initialize map
                        initializeMap();
                    }
                } else {
                    Log.d(TAG, "onRequestPermissionsResult: ACCESS_FINE_LOCATION: Permission denied");
                    backToMenu();
                }
            }
            break;
            case PERMISSIONS_REQUEST_CALL: {
                if (grantResults.length > 0 && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                    if (ContextCompat.checkSelfPermission(RSRPechhulpActivity.this,
                            Manifest.permission.CALL_PHONE) == PackageManager.PERMISSION_GRANTED) {
                        Log.d(TAG, "onRequestPermissionsResult: REQUEST_CALL: permission granted");
                        mCallPhonePermissionGranted = true;
                        makeCall();
                    } else {
                        Log.d(TAG, "onRequestPermissionsResult: REQUEST_CALL: Permission denied");
                        return;
                    }
                }
            }
            break;
        }
    }
    //endregion

    //region phone call
    private void makeCall() {
        String number = getString(R.string.rsr_phonenumber);
        if (ContextCompat.checkSelfPermission(RSRPechhulpActivity.this,
                Manifest.permission.CALL_PHONE) != PackageManager.PERMISSION_GRANTED) {
            getCallPhonePermission();
        } else {
            String dial = "tel:" + number;
            startActivity(new Intent(Intent.ACTION_CALL, Uri.parse(dial)));
        }
    }
    //endregion

    //Check if the devise is a Tablet
    private boolean isTablet() {
        return (this.getResources().getConfiguration().screenLayout
                & Configuration.SCREENLAYOUT_SIZE_MASK)
                >= Configuration.SCREENLAYOUT_SIZE_LARGE;
    }

    //back to the MainActivity
    private void backToMenu() {
        Intent backToMenuIntent = new Intent(this, MainActivity.class);
        startActivity(backToMenuIntent);
        finish();
    }

    @Override
    protected void onResume() {
        super.onResume();
        Log.d(TAG, "onResume: ----------------------------------------------------------");
        initializeMap();
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        Log.d(TAG, "onActivityResult: called.");
        switch (requestCode) {
            case PERMISSIONS_REQUEST_ENABLE_GPS: {
                initializeMap();
            }
            break;
            case PERMISSIONS_REQUEST_CALL: {
                if (mCallPhonePermissionGranted) {
                    makeCall();
                } else {
                    getCallPhonePermission();
                }
            }
            break;
            case CONNECTION_BROADCAST_ACTION: {
                initializeMap();
            }
            break;
        }
    }

    private void addToolbar() {
        Toolbar toolbar = (Toolbar) findViewById(R.id.app_bar_menu);
        setSupportActionBar(toolbar);
        toolbar.setNavigationIcon(R.drawable.menu_arrow);
        toolbar.setNavigationOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(RSRPechhulpActivity.this, MainActivity.class);
                startActivity(intent);
                finish();
            }
        });

        getSupportActionBar().setTitle("RSR Pechhulp");
    }

    @Override
    public void onBackPressed() {
        super.onBackPressed();
        backToMenu();
    }

    @Override
    protected void onStop() {
        super.onStop();
        Log.d(TAG, "onStop: ");

        unregisterReceiver(broadcastReceiver);
        Log.d(TAG, "onStart: broadcastReceiver have been unregistered");

        if (googleApiClient.isConnected()) {
            googleApiClient.disconnect();
        }
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        Log.d(TAG, "onDestroy: ");
    }

    @Override
    protected void onPause() {
        super.onPause();
        Log.d(TAG, "onPause: ");
        removeLocationUpdate();
        if (googleApiClient.isConnected()) {
            googleApiClient.disconnect();
        }
    }
}