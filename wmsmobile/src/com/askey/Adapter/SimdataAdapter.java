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
import com.askey.model.Tsimulatedata;
import com.askey.wms.R;

public class SimdataAdapter extends BaseAdapter {
	private Context mContext;

	public SimdataAdapter(Context context) {
		mContext = context;
	}

	public int getCount() {
		return AppData.msimlutedata.size();//
	}

	public Object getItem(int position) {
		return null;
	}

	public long getItemId(int position) {
		return 0;
	}

	public static String Request(Context context, String simid,String querytype,String opseq,
			String subname,String itemname) {
		// private static final CommonLog log = LogFactory.createLog();
		try {
			String strURL = String.format(K_table.getsimdata,AppData.orgid, simid,querytype,opseq,subname,itemname)
					.replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}

	public static void Resolve(String Response) {
		try {
			AppData.msimlutedata.clear();
			JSONObject json = new JSONObject(Response);
			JSONArray array = json.getJSONArray("result");
			Tsimulatedata item = new Tsimulatedata();
			item.setWIP_ENTITY_NAME("WO");
			item.setSUBINVENTORY_NAME("SUB");
			item.setITEM_NAME("Item");
			item.setOPERATION_SEQ_NUM("Seq");
			item.setDATECODE("D/C");
			item.setFRAME_NAME("Frame");
			item.setREQUIREMENT_QTY("REQQTY");
			item.setSIMULATED_QTY("SimQTY");
			item.setPICKUP_QTY("PICKUPQTY");
			//
			AppData.msimlutedata.add(item);

			for (int i = 0; i < array.length(); i++) {
				item = new Tsimulatedata();
				//
				item.setWIP_ENTITY_NAME(array.getJSONObject(i).getString(
						"WIP_ENTITY_NAME"));
				item.setSUBINVENTORY_NAME(array.getJSONObject(i).getString(
						"SUBINVENTORY_NAME"));
				item.setITEM_NAME(array.getJSONObject(i).getString("ITEM_NAME"));
				item.setOPERATION_SEQ_NUM(array.getJSONObject(i).getString(
						"OPERATION_SEQ_NUM"));
				item.setDATECODE(array.getJSONObject(i)
						.getString("DATECODE"));
				item.setFRAME_NAME(array.getJSONObject(i).getString(
						"FRAME_NAME"));
				item.setREQUIREMENT_QTY(array.getJSONObject(i).getString(
						"REQUIREMENT_QTY"));
				item.setSIMULATED_QTY(array.getJSONObject(i)
						.getString("SIMULATED_QTY"));
				item.setPICKUP_QTY(array.getJSONObject(i).getString("PICKUP_QTY"));
				//
				AppData.msimlutedata.add(item);
			}
		} catch (Exception e) {

		}

	}

	public View getView(final int position, View convertView, ViewGroup parent) {
		ViewHolder holder = null;
		//
		if (convertView == null) {
			convertView = LayoutInflater.from(mContext).inflate(
					R.layout.simdataitem, null);
			//
			holder = new ViewHolder();
			holder.mWIP_ENTITY_NAME = (TextView) convertView
					.findViewById(R.id.simdata_wo);
			holder.mSUBINVENTORY_NAME = (TextView) convertView
					.findViewById(R.id.simdata_subname);
			holder.mITEM_NAME = (TextView) convertView
					.findViewById(R.id.simdata_itemname);			
			holder.mOPERATION_SEQ_NUM = (TextView) convertView
					.findViewById(R.id.simdata_opseq);
			holder.mDATECODE = (TextView) convertView
					.findViewById(R.id.simdata_dc);
			holder.mFRAME_NAME = (TextView) convertView
					.findViewById(R.id.simdata_frame);
			holder.mREQUIREMENT_QTY=(TextView) convertView.findViewById(R.id.simdata_reqqty);
			holder.mSIMULATED_QTY=(TextView) convertView.findViewById(R.id.simdata_simqty);
			holder.mPICKUP_QTY=(TextView) convertView.findViewById(R.id.simdata_pickupqty);
			//
			convertView.setTag(holder);
		} else {
			holder = (ViewHolder) convertView.getTag();
		}
		//
		holder.mWIP_ENTITY_NAME.setWidth((int) (AppData.screenwidth * AppData.colwidth.cwo));
		holder.mSUBINVENTORY_NAME.setWidth((int) (AppData.screenwidth * AppData.colwidth.csubinv));
		holder.mITEM_NAME.setWidth((int) (AppData.screenwidth * AppData.colwidth.citemname));
		holder.mOPERATION_SEQ_NUM.setWidth((int) (AppData.screenwidth * AppData.colwidth.copseq));
		holder.mDATECODE.setWidth((int) (AppData.screenwidth * AppData.colwidth.cdc));
		holder.mFRAME_NAME.setWidth((int) (AppData.screenwidth * AppData.colwidth.cframe));
		holder.mREQUIREMENT_QTY.setWidth((int) (AppData.screenwidth * AppData.colwidth.cqty));
		holder.mSIMULATED_QTY.setWidth((int) (AppData.screenwidth * AppData.colwidth.cqty));
		holder.mPICKUP_QTY.setWidth((int) (AppData.screenwidth * AppData.colwidth.cqty));
		
		//
		holder.mWIP_ENTITY_NAME.setText(AppData.msimlutedata.get(position)
				.getWIP_ENTITY_NAME());
		holder.mSUBINVENTORY_NAME.setText(AppData.msimlutedata
				.get(position).getSUBINVENTORY_NAME());
		holder.mITEM_NAME.setText(AppData.msimlutedata
				.get(position).getITEM_NAME());
		holder.mOPERATION_SEQ_NUM.setText(AppData.msimlutedata
				.get(position).getOPERATION_SEQ_NUM());		
		holder.mDATECODE.setText(AppData.msimlutedata
				.get(position).getDATECODE());
		holder.mFRAME_NAME.setText(AppData.msimlutedata
				.get(position).getFRAME_NAME());
		holder.mREQUIREMENT_QTY.setText(AppData.msimlutedata
				.get(position).getREQUIREMENT_QTY());
		holder.mSIMULATED_QTY.setText(AppData.msimlutedata
				.get(position).getSIMULATED_QTY());
		holder.mPICKUP_QTY.setText(AppData.msimlutedata
				.get(position).getPICKUP_QTY());
		//
		convertView.setOnClickListener(new OnClickListener() {
			public void onClick(View v) {
				
			}
		});
		//
		return convertView;
	}

	class ViewHolder {
		TextView mWIP_ENTITY_NAME	;
		TextView mSUBINVENTORY_NAME;
		TextView mITEM_NAME;
		TextView mOPERATION_SEQ_NUM;
		TextView mDATECODE;
		TextView mFRAME_NAME;
		TextView mREQUIREMENT_QTY;
		TextView mSIMULATED_QTY;
		TextView mPICKUP_QTY;
	}
}
