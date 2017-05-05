package com.askey.Adapter;

import org.json.JSONArray;
import org.json.JSONObject;

import android.content.Context;
import android.util.Log;

import com.askey.model.AppData;
import com.askey.model.CustomHttpClient;
import com.askey.model.K_table;

public class LoginAdapter {

	public static String Request(Context context, String user_name,
			String passward) {
		try {
			Log.e("APServerHead", K_table.userquery);
			String strURL = String.format(K_table.userquery, user_name,
					passward).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);

			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}

	public static void Resolve(String Response) {
		try {
			JSONObject json = new JSONObject(Response);
			JSONArray array = json.getJSONArray("result");
			for (int i = 0; i < array.length(); i++) {
				AppData.user_id = array.getJSONObject(i).getString("USER_ID");
			}
		} catch (Exception e) {

		}
	}

}
