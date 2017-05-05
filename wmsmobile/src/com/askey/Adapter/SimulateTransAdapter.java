package com.askey.Adapter;

import com.askey.model.CustomHttpClient;
import com.askey.model.K_table;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;

public class SimulateTransAdapter extends BaseAdapter {
	private Context mContext;

	public SimulateTransAdapter(Context context) {
		mContext = context;
	}

	@Override
	public int getCount() {
		// TODO 自動產生的方法 Stub
		return 0;
	}

	@Override
	public Object getItem(int position) {
		// TODO 自動產生的方法 Stub
		return null;
	}

	@Override
	public long getItemId(int position) {
		// TODO 自動產生的方法 Stub
		return 0;
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		// TODO 自動產生的方法 Stub
		return null;
	}
	
	public static String SimulateTrans(Context context,String invoice_no,String org_id,String user_id){
		try {
			String strURL = String.format(K_table.SimulateTrans,invoice_no,org_id,user_id).
					replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
		
	}

}
