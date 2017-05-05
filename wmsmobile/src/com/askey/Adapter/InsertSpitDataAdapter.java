package com.askey.Adapter;


import org.json.JSONArray;
import org.json.JSONObject;

import android.content.Context;

import com.askey.model.AppData;
import com.askey.model.CustomHttpClient;
import com.askey.model.K_table;

public class InsertSpitDataAdapter {
	

	public static String DoInsertSpitData(Context context, String PN, String LotNo, String DC, 
			String VC,String BoxID_old,String BoxID_1,String BoxID_2,
			String qty1, String qty2, String userID,String empno) {
		try {
			String strURL = String.format(K_table.InsertSpitData, PN, LotNo, DC, VC,
					BoxID_old,BoxID_1,BoxID_2,qty1, qty2, userID,empno).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			//
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}
	
	public static String CheckEMP_request(Context context, String empno){
		try {
			String strURL = String.format(K_table.CheckEmpno, empno).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}
	
	public static String GetVNByVC_request(Context context, String VC){
		try {
			String strURL = String.format(K_table.GetVNByVC, VC).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}
	
	//{"result":[{"VENDOR_NAME":""}]}
	public static String GetVNByVC_Resolve(String Response) {
		try {
			String VN="";
			JSONObject json = new JSONObject(Response);
			JSONArray array = json.getJSONArray("result");
			for (int i = 0; i < array.length(); i++) {
				//AppData.user_id = array.getJSONObject(i).getString("USER_ID");
				VN = array.getJSONObject(i).getString("VENDOR_NAME").toString();
			}			
			return VN;
		} catch (Exception e) {
			return "";
		}
	}
	//
	public static String QueryDescByPN_request(Context context, String pn){
		try {
			String strURL = String.format(K_table.QueryDescByPN, pn).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}
	public static String QueryDescByPN_Resolve(String Response) {
		try {
			String VN="";
			JSONObject json = new JSONObject(Response);
			JSONArray array = json.getJSONArray("result");
			VN = array.getJSONObject(0).getString("PART_DESC").toString();
			return VN;
		} catch (Exception e) {
			return "";
		}
	}
	//
	public static String Get6in1ID_request(Context context){
		try {
			String strURL = String.format(K_table.Get6in1ID).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}
	public static String Get6in1ID_Resolve(String Response) {
		try {
			String SEQ="";
			JSONObject json = new JSONObject(Response);
			JSONArray array = json.getJSONArray("result");
			SEQ = array.getJSONObject(0).getString("SEQ").toString();
			return SEQ;
		} catch (Exception e) {
			return "";
		}
	}
}
