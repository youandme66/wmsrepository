package com.askey.Adapter;

import org.json.JSONArray;
import org.json.JSONObject;

import android.content.Context;

import com.askey.model.AppData;
import com.askey.model.CustomHttpClient;
import com.askey.model.K_table;
import com.askey.model.Tchangedcframedetail;

public class ChangedcframeHelper {
	public String Request(Context context, String pn, String subinv,
			String orgid) {
		// private static final CommonLog log = LogFactory.createLog();
		try {
			String strURL = String.format(K_table.GetIOMTLURL, pn, subinv,
					orgid).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}

	public String Updatedcframe(Context context, String unqid, String subinv,
			String onhandqty, String simqty, String dc, String frame) {
		// private static final CommonLog log = LogFactory.createLog();
		try {
			String strURL = String.format(K_table.updatedcframe, unqid, subinv,
					onhandqty, simqty, dc, frame).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			//
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}

	public void Resolve(String Response) {
		try {
			AppData.mchangedcframedetail.clear();
			JSONObject json = new JSONObject(Response);
			JSONArray array = json.getJSONArray("result");
			Tchangedcframedetail item = null;
			item = new Tchangedcframedetail();
			//增加表頭
			item.Setunique_id("ID");
			item.Setitem_name("料號");
			item.Setsubinventory_name("庫別");
			item.Setonhand_qty("庫存");
			item.Setsimulated_qty("模擬量");
			item.Setdatecode("D/C");
			item.Setframe("料架");
			//
			AppData.mchangedcframedetail.add(item);
			//
			for (int i = 0; i < array.length(); i++) {
				item = new Tchangedcframedetail();
				//
				String unique_id = array.getJSONObject(i)
						.getString("UNIQUE_ID");// 字段必須是大寫
				item.Setunique_id(unique_id);
				//
				String item_name = array.getJSONObject(i)
						.getString("ITEM_NAME");// 字段必須是大寫
				item.Setitem_name(item_name);
				//
				String subinv = array.getJSONObject(i).getString(
						"SUBINVENTORY_NAME");
				item.Setsubinventory_name(subinv);
				//
				String onhand_qty = array.getJSONObject(i).getString(
						"ONHAND_QTY");
				for (int j = onhand_qty.length(); j <= 3; j++) {
					onhand_qty = onhand_qty + "  ";
				}
				item.Setonhand_qty(onhand_qty);
				//
				String SIMULATED_QTY = array.getJSONObject(i).getString(
						"SIMULATED_QTY");
				for (int j = SIMULATED_QTY.length(); j <= 3; j++) {
					SIMULATED_QTY = SIMULATED_QTY + "  ";
				}
				item.Setsimulated_qty(SIMULATED_QTY);
				//
				String DATECODE = array.getJSONObject(i).getString("DATECODE");
				item.Setdatecode(DATECODE);
				//
				String Frame = array.getJSONObject(i).getString("FRAME_NAME");
				item.Setframe(Frame);
				//
				AppData.mchangedcframedetail.add(item);
			}
		} catch (Exception e) {
			//
		}
	}
}
