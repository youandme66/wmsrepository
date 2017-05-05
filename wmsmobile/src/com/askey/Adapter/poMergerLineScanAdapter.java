package com.askey.Adapter;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;

import com.askey.model.CustomHttpClient;
import com.askey.model.K_table;

public class poMergerLineScanAdapter extends BaseAdapter {

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
	public static String commitdata(Context context,String prefix,
			String deliverno_suffix, String pn, String lotno, String datecode, String boxno,
			String vendorcode, String qty, String userid,String orgcode ) {
		// private static final CommonLog log = LogFactory.createLog();
		try {
			String strURL = String.format(K_table.poMergerLineScanCommitdata, 
					prefix,deliverno_suffix,pn,lotno,datecode,boxno,vendorcode,
					qty,userid,orgcode).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}

}
