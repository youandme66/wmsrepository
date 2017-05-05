package com.askey.model;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

import org.apache.http.NameValuePair;

import android.util.Log;

public class CustomHttpURLConnection {
	private static String TAG = "CustomHttpUrlConnection";
	private static HttpURLConnection conn;
	
	public CustomHttpURLConnection() {
	}

	public static String GetFromWebByHttpUrlConnection(String strUrl,
			NameValuePair... nameValuePairs) {
		String result="";
		try {
			URL url = new URL(strUrl);
			
			conn = (HttpURLConnection) url.openConnection();
			conn.setDoInput(true);
			conn.setConnectTimeout(3000);
			conn.setReadTimeout(4000);
			conn.setRequestProperty("accept", "*/*");
//			int resCode=conn.getResponseCode();
			conn.connect();
			InputStream stream=conn.getInputStream();
			InputStreamReader inReader=new InputStreamReader(stream);
			BufferedReader buffer=new BufferedReader(inReader);
			String strLine=null;
			while((strLine=buffer.readLine())!=null)
			{
				result+=strLine;
			}
			inReader.close();
			conn.disconnect();
			return result;
		} catch (MalformedURLException e) {
			// TODO Auto-generated catch block
			Log.e(TAG, "getFromWebByHttpUrlCOnnection:"+e.getMessage());
			e.printStackTrace();
			return null;
		} catch (IOException e) {
			// TODO Auto-generated catch block
			Log.e(TAG, "getFromWebByHttpUrlCOnnection:"+e.getMessage());
			e.printStackTrace();
			return null;
		}
	}

	public static String PostFromWebByHttpURLConnection(String strUrl,
			NameValuePair... nameValuePairs) {
		String result="";
		try {
			URL url = new URL(strUrl);
			conn = (HttpURLConnection) url
					.openConnection();
			// 霈曄蔭�臬隞ttpUrlConnection霂餃嚗�霈斗��萎��眩rue; 
			conn.setDoInput(true);
			// 霈曄蔭�臬�ttpUrlConnection颲嚗�銝箄�銝芣post霂瑟�嚗��啗��曉   
			// http甇�����迨���霈曆蛹true, 暺恕�銝false; 
			conn.setDoOutput(true);
			// 霈曉�霂瑟��瘜蛹"POST"嚗�霈斗GET 
			conn.setRequestMethod("POST");
			//霈曄蔭頞
			conn.setConnectTimeout(3000);
			conn.setReadTimeout(4000);
			// Post 霂瑟�銝雿輻蝻� 
			conn.setUseCaches(false);
			conn.setInstanceFollowRedirects(true);
			// 霈曉�隡���摰寧掩��臬����ava撖寡情   
			// (憒�銝挽甇日★,�其�����撖寡情��敶EB�暺恕���航�蝘掩��航�ava.io.EOFException)  
			conn.setRequestProperty("Content-Type",
					"application/x-www-form-urlencoded");
			// 餈嚗�銝膩蝚��∩葉url.openConnection()�單迨��蝵桀�憿餉��牢onnect銋�摰�嚗�//			urlConn.connect();
			
			InputStream in = conn.getInputStream();
			InputStreamReader inStream=new InputStreamReader(in);
			BufferedReader buffer=new BufferedReader(inStream);
			String strLine=null;
			while((strLine=buffer.readLine())!=null)
			{
				result+=strLine;
			}
			return result;
		} catch (IOException ex) {
			Log.e(TAG,"PostFromWebByHttpURLConnection"+ ex.getMessage());
			ex.printStackTrace();
			return null;
		}
	}

}
