package com.mbds.MaslowAccess;

import android.os.Build;
import android.support.annotation.RequiresApi;


import com.google.gson.Gson;
import com.squareup.okhttp.Request;
import com.squareup.okhttp.RequestBody;
import com.squareup.okhttp.Response;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URI;
import java.net.URISyntaxException;
import java.net.URL;

import com.squareup.okhttp.MediaType;
import com.squareup.okhttp.OkHttpClient;
import com.squareup.okhttp.Request;
import com.squareup.okhttp.RequestBody;
import com.squareup.okhttp.Response;

public class Utility {

    public static  final String BASE_URL = "https://5a7353fcbb89.ngrok.io/";
    public static String get(String urlParam) throws Exception {

        URI uri = new URI(urlParam);
        URL url = uri.toURL();
        try {
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod("GET");
            BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(conn.getInputStream()));
            String inputLine;
            StringBuilder response = new StringBuilder();

            while ((inputLine = bufferedReader.readLine()) != null) {
                response.append(inputLine);
            }

            bufferedReader.close();


            return response.toString();

        }
        catch (IOException e) {
            e.printStackTrace();
            throw new Exception("no object returned");
        }

    }

    /*post */



    public static final MediaType JSON
            = MediaType.parse("application/json; charset=utf-8");

public  static String postApi(String url, String json) throws IOException{


    OkHttpClient client = new OkHttpClient();
    RequestBody body = RequestBody.create(JSON, json);
    Request request = new Request.Builder()
            .url(url)
            .post(body)
            .build();
    Response response = client.newCall(request).execute();
    return response.body().string();
}

    public  static String postApiWithToken(String url, String json, String token) throws IOException{


        OkHttpClient client = new OkHttpClient();
        RequestBody body = RequestBody.create(JSON, json);
        Request request = new Request.Builder()
                .url(url)
                .addHeader("Authorization","Bearer "+token)
                .addHeader("Content-Type","application/json")
                .post(body)
                .build();
        Response response = client.newCall(request).execute();
        return response.body().string();
    }
}
