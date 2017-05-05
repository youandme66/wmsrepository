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
import com.askey.model.Tquerystock;
import com.askey.wms.R;

public class QueryStockAdapter extends BaseAdapter {
	private Context mContext;

	public QueryStockAdapter(Context context) {
		mContext = context;
	}

	public int getCount() {
		return AppData.mquerystock.size();
	}

	public Object getItem(int position) {
		return null;
	}

	public long getItemId(int position) {
		return 0;
	}
	
	public static String Request(Context context, String pn,String dc, String subinv,String orgid) {
		// private static final CommonLog log = LogFactory.createLog();
		try {
			String strURL = String.format(K_table.querystock, pn,dc, subinv,orgid).replaceAll(" ","%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}	
	
	public static void Resolve(String Response) {
		try {
			AppData.mquerystock.clear();
			JSONObject json = new JSONObject(Response);
			JSONArray array = json.getJSONArray("result");
			Tquerystock item = null;
			item = new Tquerystock();
			//增加表頭
			item.Setitemname("料號");
			item.Setsubinv("庫別");
			item.Setframe("料架");
			item.Setdc("D/C");
			item.setOnhandqty_dc("D/C庫存");
			item.Setonhandqty("庫存");
			item.Setsimqty("模擬量");
			item.Setpickedqty("備料量");
			item.Setreturnflag("退回標誌");
			item.Setreinspectflag("複驗標誌");
            //item.Setlastreinspecttime("ReinspectTime");
			//item.Setcreatetime("CreateTime");
			//
			AppData.mquerystock.add(item);
			//
			for (int i = 0; i < array.length(); i++) {	
				item = new Tquerystock();
				//字段必須是大寫
				item.Setitemname(array.getJSONObject(i).getString("ITEM_NAME"));				
				item.Setsubinv(array.getJSONObject(i).getString("SUBINVENTORY_NAME"));
				item.Setframe(array.getJSONObject(i).getString("FRAME_NAME"));
				item.Setdc(array.getJSONObject(i).getString("DATECODE"));	
				item.setOnhandqty_dc(array.getJSONObject(i).getString("ONHANDQTY_DC"));	
				item.Setonhandqty(array.getJSONObject(i).getString("ONHANDQTY"));
				item.Setsimqty(array.getJSONObject(i).getString("SIMULATED_QTY"));
				item.Setpickedqty(array.getJSONObject(i).getString("PICKED_QTY"));
				item.Setreturnflag(array.getJSONObject(i).getString("RETURN_FLAG"));
				item.Setreinspectflag(array.getJSONObject(i).getString("LAST_REINSPECT_STATUS"));
				//item.Setlastreinspecttime(array.getJSONObject(i).getString("LAST_REINSPECT_TIME"));
				//
				AppData.mquerystock.add(item);
			}
		} catch (Exception e) {
			//
		}
	}

	public View getView(final int position, View convertView, ViewGroup parent) {
		ViewHolder holder = null;
		//
		if (convertView == null) {
			convertView = LayoutInflater.from(mContext).inflate(
					R.layout.querystockitem, null);
			//
			holder = new ViewHolder();
			holder.mitem_name = (TextView) convertView
					.findViewById(R.id.querystock_pn);
			holder.msubinventory_name = (TextView) convertView
					.findViewById(R.id.querystock_subinv);
			holder.mframe = (TextView) convertView
					.findViewById(R.id.querystock_frame);
			holder.mdatecode = (TextView) convertView
					.findViewById(R.id.querystock_dc);
			holder.monhandqty_dc = (TextView) convertView
					.findViewById(R.id.querystock_onhandqty_dc);
			holder.monhand_qty = (TextView) convertView
					.findViewById(R.id.querystock_onhandqty);
			holder.msimulated_qty = (TextView) convertView
					.findViewById(R.id.querystock_simqty);
			holder.mpickedqty = (TextView) convertView
					.findViewById(R.id.querystock_pickedqty);
			holder.mreturnflag = (TextView) convertView
					.findViewById(R.id.querystock_returnflag);
			holder.mreinspectflag = (TextView) convertView
					.findViewById(R.id.querystock_reinspectstatus);
			//holder.mlastreinspecttime = (TextView) convertView
			//		.findViewById(R.id.querystock_lastreinspecttime);
			//holder.mcreatetime = (TextView) convertView
			//		.findViewById(R.id.querystock_createtime);
			//
			convertView.setTag(holder);
		} else {
			holder = (ViewHolder) convertView.getTag();
		}
		//				
		holder.mitem_name.setWidth((int)(AppData.screenwidth*AppData.colwidth.citemname));
		holder.msubinventory_name.setWidth((int)(AppData.screenwidth*AppData.colwidth.csubinv));
		holder.mframe.setWidth((int)(AppData.screenwidth*AppData.colwidth.cframe));
		holder.mdatecode.setWidth((int)(AppData.screenwidth*AppData.colwidth.cdc));
		holder.monhandqty_dc.setWidth((int)(AppData.screenwidth*AppData.colwidth.cqty));
		holder.monhand_qty.setWidth((int)(AppData.screenwidth*AppData.colwidth.cqty));
		holder.msimulated_qty.setWidth((int)(AppData.screenwidth*AppData.colwidth.cqty));
		holder.mpickedqty.setWidth((int)(AppData.screenwidth*AppData.colwidth.cqty));
		holder.mreturnflag.setWidth((int)(AppData.screenwidth*AppData.colwidth.creturnflag));
		holder.mreinspectflag.setWidth((int)(AppData.screenwidth*AppData.colwidth.creinspectflag));	
			
		//
		holder.mitem_name.setText(AppData.mquerystock.get(position)
				.Getitemname());
		holder.msubinventory_name.setText(AppData.mquerystock.get(position)
				.Getsubinv());
		holder.msubinventory_name.setText(AppData.mquerystock.get(
				position).Getsubinv());
		holder.mframe.setText(AppData.mquerystock.get(position)
				.Getframe());
		holder.mdatecode.setText(AppData.mquerystock.get(
				position).Getdc());
		holder.monhandqty_dc.setText(AppData.mquerystock.get(position)
				.getOnhandqty_dc());
		holder.monhand_qty.setText(AppData.mquerystock.get(position)
				.Getonhandqty());
		holder.msimulated_qty.setText(AppData.mquerystock.get(position)
				.Getsimqty());
		holder.mpickedqty.setText(AppData.mquerystock.get(position)
				.Getpickedqty());
		holder.mreturnflag.setText(AppData.mquerystock.get(position)
				.Getreturnflag());
		holder.mreinspectflag.setText(AppData.mquerystock.get(position)
				.Getreinspectflag());
		//holder.mlastreinspecttime.setText(AppData.mquerystock.get(position)
		//		.Getlastreinspecttime());
		//holder.mcreatetime.setText(AppData.mquerystock.get(position)
		//		.Getcreatetime());	
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

	class ViewHolder {
		TextView mitem_name;
		TextView msubinventory_name;
		TextView mframe;
		TextView mdatecode;
		TextView monhandqty_dc;
		TextView monhand_qty;
		TextView msimulated_qty;
		TextView mpickedqty;
		TextView mreturnflag;
		TextView mreinspectflag;
		//TextView mlastreinspecttime;
		//TextView mcreatetime;

	}
}

