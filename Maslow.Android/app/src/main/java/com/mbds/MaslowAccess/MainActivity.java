package com.mbds.MaslowAccess;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

public class MainActivity extends AppCompatActivity  {

    Button  btnScanBarcode;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        initViews();
        startActivity(new Intent(MainActivity.this, ScannedBarcodeActivity.class));
    }

    private void initViews() {

        btnScanBarcode = findViewById(R.id.btnScanBarcode);
      //  btnScanBarcode.setOnClickListener(this);
    }

 /*   @Override
    public void onClick(View v) {

        switch (v.getId()) {

            case R.id.btnScanBarcode:
                startActivity(new Intent(MainActivity.this, ScannedBarcodeActivity.class));
                break;
        }

    }

  */
}
