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
import com.askey.model.Tpickedupdetail;
import com.askey.wms.R;

public class PickupedDetailAdapter extends BaseAdapter {
	private Context mContext;

	public PickupedDetailAdapter(Context context) {
		mContext = context;
	}

	public int getCount() {
		return AppData.mpickedupdetail.size();
	}

	public Object getItem(int position) {
		return null;
	}

	public long getItemId(int position) {
		return 0;
	}

	public static String Request(Context context, String simid, String itemname) {
		// private static final CommonLog log = LogFactory.createLog();
		try {
			String strURL = String.format(K_table.getpickedupdetail, simid,
					itemname).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}

	public static void Resolve(String Response) {
		try {
			AppData.mpickedupdetail.clear();
			JSONObject json = new JSONObject(Response);
			JSONArray array = json.getJSONArray("result");
			Tpickedupdetail item = new Tpickedupdetail();
			//
			item.setItem_name("Item");
			item.setDatecode("D/C");
			item.setPickup_qty("Pickedqty");
			item.setPick_times("PickedTimes");
			item.setCreate_time("Time");
			//
			AppData.mpickedupdetail.add(item);

			for (int i = 0; i < array.length(); i++) {
				item = new Tpickedupdetail();
				//
				item.setItem_name(array.getJSONObject(i).getString("ITEM_NAME"));
				item.setDatecode(array.getJSONObject(i).getString("DATECODE"));
				item.setPickup_qty(array.getJSONObject(i).getString(
						"PICKUP_QTY"));
				item.setPick_times(array.getJSONObject(i).getString(
						"PICK_TIMES"));
				item.setCreate_time(array.getJSONObject(i).getString(
						"CREATE_TIME"));
				//
				AppData.mpickedupdetail.add(item);
			}
		} catch (Exception e) {

		}

	}

	public View getView(final int position, View convertView, ViewGroup parent) {
		ViewHolder holder = null;
		//
		if (convertView == null) {
			convertView = LayoutInflater.from(mContext).inflate(
					R.layout.pickedupitem, null);
			//
			holder = new ViewHolder();
			holder.mitem_name = (TextView) convertView
					.findViewById(R.id.pickedupdetail_itemname);
			holder.mdatecode = (TextView) convertView
					.findViewById(R.id.pickedupdetail_dc);
			holder.mpickup_qty = (TextView) convertView
					.findViewById(R.id.pickedupdetail_pickedupqty);
			holder.mPick_Times = (TextView) convertView
					.findViewById(R.id.pickedupdetail_pickeduptimes);
			holder.mcreate_time = (TextView) convertView
					.findViewById(R.id.pickedupdetail_pickedupdate);
			//
			convertView.setTag(holder);
		} else {
			holder = (ViewHolder) convertView.getTag();
		}
		//
		holder.mitem_name
				.setWidth((int) (AppData.screenwidth * 0.25));
		holder.mdatecode
				.setWidth((int) (AppData.screenwidth * 0.13));
		holder.mpickup_qty
				.setWidth((int) (AppData.screenwidth * 0.15));
		holder.mPick_Times
				.setWidth((int) (AppData.screenwidth * 0.15));
		holder.mcreate_time
				.setWidth((int) (AppData.screenwidth * 0.4));
		//
		holder.mitem_name.setText(AppData.mpickedupdetail.get(position)
				.getItem_name());
		holder.mdatecode.setText(AppData.mpickedupdetail
				.get(position).getDatecode());
		holder.mpickup_qty.setText(AppData.mpickedupdetail
				.get(position).getPickup_qty());
		holder.mPick_Times.setText(AppData.mpickedupdetail
				.get(position).getPick_times());
		holder.mcreate_time.setText(AppData.mpickedupdetail
				.get(position).getCreate_time());
		//
		convertView.setOnClickListener(new OnClickListener() {
			public void onClick(View v) {
			}
		});
		//
		return convertView;
	}

	class ViewHolder {
		TextView mitem_name;
		TextView mdatecode;
		TextView mpickup_qty;
		TextView mPick_Times;
		TextView mcreate_time;
	}
}
