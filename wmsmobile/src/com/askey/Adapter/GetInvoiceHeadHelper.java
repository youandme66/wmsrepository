package com.askey.Adapter;

import org.json.JSONArray;
import org.json.JSONObject;

import android.content.Context;
import android.util.Log;

import com.askey.model.AppData;
import com.askey.model.CustomHttpClient;
import com.askey.model.K_table;

public class GetInvoiceHeadHelper {
	public static String Request(Context context, String type,String orgcode) {
		try {
			String strURL = String.format(K_table.GetInvoicHeadQuery, type,orgcode).replace(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}
	/*
	public void GetInvoice(Context context, String type,String orgcode) {
		try {
			String strURL = String.format(K_table.GetInvoicHeadQuery, type,orgcode);
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			Resolve(strData);
		} catch (Exception e) {
			Log.d("tag", e.getMessage());
		}
	}
	*/	
	public static void Resolve(String Response) {
		try {
			JSONObject json = new JSONObject(Response);
			JSONArray array = json.getJSONArray("result");
			//
			AppData.ginvoice = array.getJSONObject(0).getString("HEAD");// 字段必須是大寫
		} catch (Exception e) {
			Log.d("tag", e.getMessage());
		}
		
	}
}
