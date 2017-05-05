package com.askey.Adapter;

import org.json.JSONArray;
import org.json.JSONObject;

import android.content.Context;

import com.askey.model.AppData;
import com.askey.model.CustomHttpClient;
import com.askey.model.K_table;
import com.askey.model.Tchooseorg;

public class ChooseorgHelper {
	public String Request(Context context, String userid) {
		// private static final CommonLog log = LogFactory.createLog();
		try {
			String strURL = String.format(K_table.chooseorg, userid).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}
	
	
	public void Resolve(String Response) {
		try {
			AppData.mchooseorg.clear();
			JSONObject json = new JSONObject(Response);
			JSONArray array = json.getJSONArray("result");
			Tchooseorg item = null;
			item = new Tchooseorg();
			//增加表頭
			item.Setorgdesc("OrgCode");
			item.Setorg("Org");
			//
			AppData.mchooseorg.add(item);
			//
			for (int i = 0; i < array.length(); i++) {	
				item = new Tchooseorg();
				//
				String orgdesc=array.getJSONObject(i).getString("ORGDESC");//字段必須是大寫
				item.Setorgdesc(orgdesc);
				//
				String org=array.getJSONObject(i).getString("ORG");//字段必須是大寫
				item.Setorg(org);	
				//
				AppData.mchooseorg.add(item);
			}
		} catch (Exception e) {
			//
		}
	}
}
