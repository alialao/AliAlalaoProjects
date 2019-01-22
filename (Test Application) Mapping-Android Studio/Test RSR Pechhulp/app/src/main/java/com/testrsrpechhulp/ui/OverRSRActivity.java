package com.testrsrpechhulp.ui;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.Toolbar;
import android.text.method.LinkMovementMethod;
import android.view.View;
import android.widget.TextView;

import com.testrsrpechhulp.R;

public class OverRSRActivity extends AppCompatActivity {

    private static final String TAG = "OverRSRActivity";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_over_rsr);

        toolbar();

        TextView textViewRSRLink = (TextView) findViewById(R.id.textViewOverRSR);
        textViewRSRLink.setMovementMethod(LinkMovementMethod.getInstance());
    }

    private void toolbar(){
        Toolbar toolbar = (Toolbar) findViewById(R.id.app_bar_menu);
        setSupportActionBar(toolbar);
        toolbar.setNavigationIcon(R.drawable.menu_arrow);
        toolbar.setNavigationOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(OverRSRActivity.this, MainActivity.class);
                startActivity(intent);
                finish();
            }
        });
        getSupportActionBar().setTitle("Over RSR");
    }

    public void backToMenu() {
        Intent backToMenuIntent = new Intent(this, MainActivity.class);
        startActivity(backToMenuIntent);
        finish();
    }

    public void onBackPressed() {
        super.onBackPressed();
        backToMenu();
    }

}
