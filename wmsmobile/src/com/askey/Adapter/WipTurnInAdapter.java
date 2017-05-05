package com.askey.Adapter;

import org.json.JSONArray;
import org.json.JSONObject;

import com.askey.activity.wip_turnin_op_head;
import com.askey.activity.wip_turnin_op_detail;
import com.askey.model.AppData;
import com.askey.model.CustomHttpClient;
import com.askey.model.K_table;
import com.askey.model.Twipturninquery;
import com.askey.wms.R;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.View.OnClickListener;
import android.widget.BaseAdapter;
import android.widget.TextView;

public class WipTurnInAdapter extends BaseAdapter {

	private Context mContext;
	
	public WipTurnInAdapter(Context context) {
		mContext = context;
	}

	public int getCount() {
		return AppData.mwipturninquery.size();//
	}

	public Object getItem(int position) {
		// TODO 自動產生的方法 Stub
		return null;
	}

	public long getItemId(int position) {
		// TODO 自動產生的方法 Stub
		return 0;
	}

	public static String Request(Context context, String rcvno, String org_id) {
		// private static final CommonLog log = LogFactory.createLog();
		try {
			String strURL = String
					.format(K_table.wipturninquery, rcvno, org_id).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "";
			// log.e("IOException");
		}
	}

	public static String CommitWipTurnIn(Context context,
			String unique_id, String invoice_no, String wo_no,
			String region_name, String part_no, String required_qty,
			String balance_qty,
			String subinventory_name, String locator_name, String version,
			String inputqty, String frame_name, String user_id, String org_id) {
		try {
			String strURL = String.format(K_table.wipturnincommit, 
					unique_id, invoice_no, wo_no, region_name, part_no,
					required_qty, balance_qty,
					subinventory_name, locator_name, 
					version, inputqty, frame_name,
					user_id, org_id);
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return e.getMessage().toString();
		}
	}

	public static void Resolve(String Response) {
		try {
			AppData.mwipturninquery.clear();
			JSONObject json = new JSONObject(Response);
			JSONArray array = json.getJSONArray("result");
			Twipturninquery item = null;
			item = new Twipturninquery();
			// 增加表頭
			item.setUnique_id("序號");
			item.setInvoice_no("單號");
			item.setWo_no("工單");
			item.setRegion_name("區域");
			item.setPart_no("料號");
			item.setRequired_qty("申請量");
			item.setBalance_qty("剩餘量");
			item.setSubinventory_name("庫別");
			item.setLocator_name("Locator");
			item.setVersion("版本");
			item.setFrame_name("Frame");
			//
			AppData.mwipturninquery.add(item);
			//
			for (int i = 0; i < array.length(); i++) {
				item = new Twipturninquery();
				// 字段必須是大寫
				item.setUnique_id(array.getJSONObject(i).getString("UNIQUE_ID"));
				item.setInvoice_no(array.getJSONObject(i).getString(
						"INVOICE_NO"));
				item.setWo_no(array.getJSONObject(i).getString("WO_NO"));
				item.setRegion_name(array.getJSONObject(i).getString(
						"REGION_NAME"));
				item.setPart_no(array.getJSONObject(i).getString("PART_NO"));
				item.setRequired_qty(array.getJSONObject(i).getString(
						"REQUIRED_QTY"));
				item.setBalance_qty(array.getJSONObject(i).getString(
						"BALANCE_QTY"));
				item.setSubinventory_name(array.getJSONObject(i).getString(
						"SUBINVENTORY_NAME"));
				item.setLocator_name(array.getJSONObject(i).getString(
						"LOCATOR_NAME"));
				item.setVersion(array.getJSONObject(i).getString("VERSION"));
				item.setFrame_name(array.getJSONObject(i).getString(
						"LASTFRAME"));
				//
				AppData.mwipturninquery.add(item);
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
					R.layout.wip_turnin_op_item, null);
			//
			holder = new ViewHolder();
			holder.munique_id = (TextView) convertView
					.findViewById(R.id.wipturnin_query_unique_id);
			holder.minvoice_no = (TextView) convertView
					.findViewById(R.id.wipturnin_query_invoice_no);
			holder.mwo_no = (TextView) convertView
					.findViewById(R.id.wipturnin_query_wo_no);
			holder.mregion_name = (TextView) convertView
					.findViewById(R.id.wipturnin_query_region_name);
			holder.mpart_no = (TextView) convertView
					.findViewById(R.id.wipturnin_query_part_no);
			holder.mrequired_qty = (TextView) convertView
					.findViewById(R.id.wipturnin_query_required_qty);
			holder.mbalance_qty = (TextView) convertView
					.findViewById(R.id.wipturnin_query_balance_qty);
			holder.msubinventory_name = (TextView) convertView
					.findViewById(R.id.wipturnin_query_subinventory_name);
			holder.mlocator_name = (TextView) convertView
					.findViewById(R.id.wipturnin_query_locator_name);
			holder.mversion = (TextView) convertView
					.findViewById(R.id.wipturnin_query_version);
			holder.mframe_name = (TextView) convertView
					.findViewById(R.id.wipturnin_query_frame_name);
			//
			convertView.setTag(holder);
		} else {
			holder = (ViewHolder) convertView.getTag();
		}
		//
		holder.munique_id.setWidth((int) (AppData.screenwidth * AppData.colwidth.cuniqueid));
		holder.minvoice_no.setWidth((int) (AppData.screenwidth * AppData.colwidth.cinvoice));
		holder.mwo_no.setWidth((int) (AppData.screenwidth * AppData.colwidth.cwo));
		holder.mregion_name.setWidth((int) (AppData.screenwidth * AppData.colwidth.cregionname));
		holder.mpart_no.setWidth((int) (AppData.screenwidth * AppData.colwidth.citemname));
		holder.mrequired_qty.setWidth((int) (AppData.screenwidth * AppData.colwidth.cqty));
		holder.mbalance_qty.setWidth((int) (AppData.screenwidth * AppData.colwidth.cqty));
		holder.msubinventory_name.setWidth((int) (AppData.screenwidth * AppData.colwidth.csubinv));
		holder.mlocator_name.setWidth((int) (AppData.screenwidth * AppData.colwidth.clocator));
		holder.mversion.setWidth((int) (AppData.screenwidth * AppData.colwidth.cversion));
		holder.mframe_name.setWidth((int) (AppData.screenwidth * AppData.colwidth.cframe));
		
		
		//holder.mflag.setText(AppData.mwipturninquery.get(position).getFlag());
		holder.munique_id.setText(AppData.mwipturninquery.get(position)
				.getUnique_id());
		holder.minvoice_no.setText(AppData.mwipturninquery.get(position)
				.getInvoice_no());
		holder.mwo_no.setText(AppData.mwipturninquery.get(position).getWo_no());
		holder.mregion_name.setText(AppData.mwipturninquery.get(position)
				.getRegion_name());
		holder.mpart_no.setText(AppData.mwipturninquery.get(position)
				.getPart_no());
		holder.mrequired_qty.setText(AppData.mwipturninquery.get(position)
				.getRequired_qty());
		holder.mbalance_qty.setText(AppData.mwipturninquery.get(position)
				.getBalance_qty());
		holder.msubinventory_name.setText(AppData.mwipturninquery.get(position)
				.getSubinventory_name());
		holder.mlocator_name.setText(AppData.mwipturninquery.get(position)
				.getLocator_name());
		holder.mversion.setText(AppData.mwipturninquery.get(position)
				.getVersion());
		holder.mframe_name.setText(AppData.mwipturninquery.get(position)
				.getFrame_name());

		//
		
		convertView.setOnClickListener(new OnClickListener() {
			public void onClick(View v) {
				Intent intent = new Intent(mContext,
						wip_turnin_op_detail.class);// 创建Intent对象
				//
				if (position >= 1) {
					intent.putExtra("unique_id",
							AppData.mwipturninquery.get(position)
									.getUnique_id());
					intent.putExtra("invoice_no",
							AppData.mwipturninquery.get(position)
									.getInvoice_no());
					intent.putExtra("wo_no",
							AppData.mwipturninquery.get(position).getWo_no());
					intent.putExtra("region_name",
							AppData.mwipturninquery.get(position)
									.getRegion_name());
					intent.putExtra("part_no",
							AppData.mwipturninquery.get(position).getPart_no());
					intent.putExtra("required_qty", AppData.mwipturninquery
							.get(position).getRequired_qty());
					intent.putExtra("balance_qty",
							AppData.mwipturninquery.get(position)
									.getBalance_qty());
					intent.putExtra("subinventory_name",
							AppData.mwipturninquery.get(position)
									.getSubinventory_name());
					intent.putExtra("locator_name", AppData.mwipturninquery
							.get(position).getLocator_name());
					intent.putExtra("version",
							AppData.mwipturninquery.get(position).getVersion());
					intent.putExtra("frame_name",
							AppData.mwipturninquery.get(position)
									.getFrame_name());

					//
					//mContext.startActivity(intent);// 执行Intent操作
					((wip_turnin_op_head)mContext).startActivityForResult(intent,0);
				}
			}			
		});
		return convertView;
	}

	class ViewHolder {
		TextView munique_id;
		TextView minvoice_no;
		TextView mwo_no;
		TextView mregion_name;
		TextView mpart_no;
		TextView mrequired_qty;
		TextView mbalance_qty;
		TextView msubinventory_name;
		TextView mlocator_name;
		TextView mversion;
		TextView mframe_name;
	}
}
