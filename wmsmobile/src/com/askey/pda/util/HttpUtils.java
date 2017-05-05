package com.askey.pda.util;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

import android.content.Context;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;



/**
 * Created by gym on 2016/10/25.
 */

public class HttpUtils  {


    public static boolean isNetworkConnected(Context context) {
        if (context != null) {
            ConnectivityManager mConnectivityManager = (ConnectivityManager) context
                    .getSystemService(Context.CONNECTIVITY_SERVICE);
            NetworkInfo mNetworkInfo = mConnectivityManager.getActiveNetworkInfo();
            if (mNetworkInfo != null) {
                return mNetworkInfo.isAvailable();
            }
        }
        return false;
    }
    
	public static String getMethodConnectNet(String requestUrl) {
		// TODO Auto-generated method stub
		String readerline, str = null;
		
		URL url;
		try {
			url = new URL(requestUrl);
	
	        HttpURLConnection urlConnection=(HttpURLConnection) url.openConnection();
	        urlConnection.setConnectTimeout(5000);
	        urlConnection.setReadTimeout(5000);
	        urlConnection.setRequestMethod("GET");
	        urlConnection.connect();
	        //int code=urlConnection.getResponseCode();
	 
	        InputStream inputStream=urlConnection.getInputStream();
	        BufferedReader reader=new BufferedReader(new InputStreamReader(inputStream));
	        StringBuffer buffer=new StringBuffer();
	        while ((readerline=reader.readLine())!=null) {
	            buffer.append(readerline);
	         }
	        str = buffer.toString();
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return str;

	}
}


