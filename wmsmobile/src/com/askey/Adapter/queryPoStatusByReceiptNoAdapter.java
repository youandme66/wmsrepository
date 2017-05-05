package com.askey.Adapter;

import org.json.JSONArray;
import org.json.JSONObject;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.askey.model.AppData;
import com.askey.model.CustomHttpClient;
import com.askey.model.K_table;
import com.askey.model.Tpostatusquery;
import com.askey.wms.R;

public class queryPoStatusByReceiptNoAdapter extends BaseAdapter {
	private Context mContext;

	public queryPoStatusByReceiptNoAdapter(Context context) {
		mContext = context;
	}

	public int getCount() {
		return AppData.mpostatusquery.size();
	}

	public Object getItem(int position) {
		return null;
	}

	public long getItemId(int position) {
		return 0;
	}
	
	public static String Request(Context context, String ReceiptNo,String orgid) {
		// private static final CommonLog log = LogFactory.createLog();
		try {
			String strURL = String.format(K_table.queryPoStatusByReceiptNo, ReceiptNo,orgid).replaceAll(" ","%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}	
	
	public static void Resolve(String Response) {
		try {
			AppData.mpostatusquery.clear();
			JSONObject json = new JSONObject(Response);
			JSONArray array = json.getJSONArray("result");
			Tpostatusquery item = new Tpostatusquery();
			//增加表頭
			item.setRECEIPT_NUM("單號");
			item.setLEVEL("Level");
			item.setTRANSACTION_TYPE("交易類型");
			item.setQUANTITY("數量");
			item.setUPDATE_DATE("時間");
			//
			AppData.mpostatusquery.add(item);
			//
			for (int i = 0; i < array.length(); i++) {	
				item = new Tpostatusquery();
				//字段必須是大寫
				item.setRECEIPT_NUM(array.getJSONObject(i).getString("RECEIPT_NUM"));				
				item.setLEVEL(array.getJSONObject(i).getString("LEVEL"));
				item.setTRANSACTION_TYPE(array.getJSONObject(i).getString("TRANSACTION_TYPE"));
				item.setQUANTITY(array.getJSONObject(i).getString("QUANTITY"));	
				item.setUPDATE_DATE(array.getJSONObject(i).getString("UPDATE_DATE"));	
				//
				AppData.mpostatusquery.add(item);
			}
		} catch (Exception e) {
			//
		}
	}

	class ViewHolder {
		TextView RECEIPT_NUM;
		TextView LEVEL;
		TextView TRANSACTION_TYPE;
		TextView QUANTITY;
		TextView UPDATE_DATE;
	}

	public View getView(final int position, View convertView, ViewGroup parent) {
		ViewHolder holder = null;
		//
		if (convertView == null) {
			convertView = LayoutInflater.from(mContext).inflate(
					R.layout.postatusbyreceiptnoitem, null);
			//
			holder = new ViewHolder();
			holder.RECEIPT_NUM = (TextView) convertView
					.findViewById(R.id.tvRECEIPT_NUM);
			holder.LEVEL = (TextView) convertView
					.findViewById(R.id.tvLEVEL);
			holder.TRANSACTION_TYPE = (TextView) convertView
					.findViewById(R.id.tvTRANSACTION_TYPE);
			holder.QUANTITY = (TextView) convertView
					.findViewById(R.id.tvQUANTITY);
			holder.UPDATE_DATE = (TextView) convertView
					.findViewById(R.id.tvUpdate_Date);
			//
			convertView.setTag(holder);
		} else {
			holder = (ViewHolder) convertView.getTag();
		}
		//				
		holder.RECEIPT_NUM.setWidth((int)(AppData.screenwidth*AppData.colwidth.cinvoice));
		holder.LEVEL.setWidth((int)(AppData.screenwidth*0.15));
		holder.TRANSACTION_TYPE.setWidth((int)(AppData.screenwidth*0.25));
		holder.QUANTITY.setWidth((int)(AppData.screenwidth*AppData.colwidth.cqty));
		holder.UPDATE_DATE.setWidth((int)(AppData.screenwidth*AppData.colwidth.ctime));
		//
		holder.RECEIPT_NUM.setText(AppData.mpostatusquery.get(position)
				.getRECEIPT_NUM());
		holder.LEVEL.setText(AppData.mpostatusquery.get(position)
				.getLEVEL());
		holder.TRANSACTION_TYPE.setText(AppData.mpostatusquery.get(
				position).getTRANSACTION_TYPE());
		holder.QUANTITY.setText(AppData.mpostatusquery.get(position)
				.getQUANTITY());
		holder.UPDATE_DATE.setText(AppData.mpostatusquery.get(
				position).getUPDATE_DATE());
		//
		convertView.setOnClickListener(new OnClickListener() {
			public void onClick(View v) {
			}
		});
		//
		//int[] colors = { Color.WHITE, Color.rgb(219, 238, 244) };// RGB颜色
		//convertView.setBackgroundColor(colors[position % 2]);// 每隔item之间颜色不同
		//
		return convertView;
	}

}

