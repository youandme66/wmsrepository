package com.askey.Adapter;

import org.json.JSONArray;
import org.json.JSONObject;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.EditText;
import android.widget.TextView;

import com.askey.model.AppData;
import com.askey.model.CustomHttpClient;
import com.askey.model.K_table;
import com.askey.model.Tsimlutebasedata;
import com.askey.wms.R;

public class SimbasedataAdapter_merger extends BaseAdapter {
	private Context mContext;
	private TextView mtvsubname;
	private EditText medtbarcode;

	public SimbasedataAdapter_merger(Context context) {
		mContext = context;		
	}
	
	public SimbasedataAdapter_merger(Context context,TextView tvsubname,EditText edtbarcode) {
		mContext = context;	
		mtvsubname=tvsubname;
		medtbarcode=edtbarcode;
	}

	public int getCount() {
		return AppData.msimlutebasedata.size();
	}

	public Object getItem(int position) {
		return null;
	}

	public long getItemId(int position) {
		return 0;
	}

	public static String Request(Context context, String simid) {
		// private static final CommonLog log = LogFactory.createLog();
		try {
			String strURL = String.format(K_table.getsimbasedata, AppData.orgid,simid).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}

	public static void Resolve(String Response) {
		try {
			AppData.msimlutebasedata.clear();
			JSONObject json = new JSONObject(Response);
			JSONArray array = json.getJSONArray("result");
			Tsimlutebasedata item = null;
			item = new Tsimlutebasedata();
			//item.setWip_Entity_Name("WO");
			//item.setOPERATION_SEQ_NUM("OPSeq");
			//item.setSubinventory_Name("SUB");
            //
			//AppData.msimlutebasedata.add(item);

			for (int i = 0; i < array.length(); i++) {
				item = new Tsimlutebasedata();
                //
				item.setWip_Entity_Name(array.getJSONObject(i).getString("WIP_ENTITY_NAME"));
				item.setOPERATION_SEQ_NUM(array.getJSONObject(i).getString(
						"OPERATION_SEQ_NUM"));
				item.setSubinventory_Name(array.getJSONObject(i).getString("SUBINVENTORY_NAME"));
				//
				AppData.msimlutebasedata.add(item);
			}
		} catch (Exception e) {

		}

	}

	public View getView(final int position, View convertView, ViewGroup parent) {
		ViewHolder holder = null;
		//
		if (convertView == null) {
			convertView = LayoutInflater.from(mContext).inflate(
					R.layout.simbasedataitem, null);
			//
			holder = new ViewHolder();
			holder.mSubinventory_Name = (TextView) convertView
					.findViewById(R.id.simbasedata_subname);
			
			//
			convertView.setTag(holder);
		} else {
			holder = (ViewHolder) convertView.getTag();
		}
		//
		holder.mSubinventory_Name
				.setWidth((int) (AppData.screenwidth * 0.4));
		//
		holder.mSubinventory_Name.setText(AppData.msimlutebasedata.get(position)
				.getSubinventory_Name());
		//
		convertView.setOnClickListener(new OnClickListener() {
			public void onClick(View v) {
				if (position >= 0) {
					//String sim_wo=AppData.msimlutebasedata.get(position).getWip_Entity_Name();
					//String sim_opseq=AppData.msimlutebasedata.get(position).getOPERATION_SEQ_NUM();
					String sim_subname=AppData.msimlutebasedata.get(position).getSubinventory_Name();
					//
					mtvsubname.setText(sim_subname);
					//
					medtbarcode.requestFocus();
				}
			}
		});
		//
		return convertView;
	}

	class ViewHolder {		
		TextView mSubinventory_Name;
	}
}
