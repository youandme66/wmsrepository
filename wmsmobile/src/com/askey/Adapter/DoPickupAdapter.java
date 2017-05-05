package com.askey.Adapter;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;

import com.askey.model.AppData;
import com.askey.model.CustomHttpClient;
import com.askey.model.K_table;

public class DoPickupAdapter extends BaseAdapter {
	private Context mContext;

	public DoPickupAdapter(Context context) {
		mContext = context;
	}

	public int getCount() {
		return AppData.msimlutedata.size();
	}

	public Object getItem(int position) {
		return null;
	}

	public long getItemId(int position) {
		return 0;
	}

	public static String Request(Context context, String simid, String opseq,
			String subname, String itemname, String qty, String dc,String reelid,String vendorcode) 
	{
		// private static final CommonLog log = LogFactory.createLog();
		try {
			String strURL = String.format(K_table.dopickup, AppData.orgid,
					simid, opseq, subname, itemname, qty, dc,reelid,vendorcode, AppData.user_id)
					.replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}

	public View getView(final int position, View convertView, ViewGroup parent) {
		return convertView;
	}

}
