package com.testrsrpechhulp.ui;

import android.content.Intent;
import android.content.res.Configuration;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.Toolbar;
import android.text.method.LinkMovementMethod;
import android.util.Log;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.testrsrpechhulp.R;


public class MainActivity extends AppCompatActivity {

    private static final String TAG = "MainActivity";

    private Button rsrPechhulp;
    private Button overRsr;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        addToolbar();

        rsrPechhulp = (Button) findViewById(R.id.btn_rsrPechhulp);
        BtnClickRSRPechhulp();

        overRsr = (Button) findViewById(R.id.btn_overRsr);
        BtnClickOverRSR();
    }

    private void addToolbar() {
        Toolbar toolbar = (Toolbar) findViewById(R.id.app_bar_menu);
        setSupportActionBar(toolbar);
        getSupportActionBar().setTitle("RSR Puchhulp");
    }

    private void BtnClickOverRSR() {
        overRsr.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(MainActivity.this, OverRSRActivity.class);
                startActivity(intent);
                finish();
            }
        });
    }

    private void BtnClickRSRPechhulp() {
        rsrPechhulp.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(MainActivity.this, RSRPechhulpActivity.class);
                startActivity(intent);
                finish();
            }
        });
    }

    public void dialog() {
        final AlertDialog alertDialog = new AlertDialog.Builder(this)
                .setTitle(R.string.dialog_title)
                .setMessage(R.string.dialog_message)
                .setPositiveButton(R.string.dialog_action_dismiss, null)
                .create();

        alertDialog.show();
        ((TextView) alertDialog.findViewById(android.R.id.message)).setMovementMethod(LinkMovementMethod.getInstance());
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        if (isTablet()) {
            Log.d(TAG, "onCreateOptionsMenu: is TABLET");
            return false;

        } else {
            Log.d(TAG, "onCreateOptionsMenu: is SMARTPHONE");

            MenuInflater inflater = getMenuInflater();
            inflater.inflate(R.menu.main_menu_toolbar, menu);
            return super.onCreateOptionsMenu(menu);
        }
    }

    public boolean isTablet() {
        return (this.getResources().getConfiguration().screenLayout
                & Configuration.SCREENLAYOUT_SIZE_MASK)
                >= Configuration.SCREENLAYOUT_SIZE_LARGE;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.over_rsr:
                dialog();
                break;
        }
        return super.onOptionsItemSelected(item);
    }
}
