package com.askey.Adapter;

import org.json.JSONArray;
import org.json.JSONObject;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.askey.model.AppData;
import com.askey.model.CustomHttpClient;
import com.askey.model.K_table;
import com.askey.model.Treturnquery;
import com.askey.wms.R;

public class ReturnAdapter extends BaseAdapter {
	private Context mContext;

	public ReturnAdapter(Context context) {
		mContext = context;
	}

	public int getCount() {
		return AppData.mreturnquery.size();
	}

	public Object getItem(int position) {
		return null;
	}

	public long getItemId(int position) {
		return 0;
	}

	public static String Request(Context context, String invoice_no) {
		// private static final CommonLog log = LogFactory.createLog();
		try {
			String strURL = String.format(K_table.returnquery, invoice_no).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}

	public static String CommitReturnTrans(Context context, String org_id,
			String invoice_no, String wo_no, String wo_key, String route,
			String reason_code, String item_name, String item_id,
			String required_qty, String balance_qty, String sub_name,
			String locator, String frame, String dc, String qty, String pu,
			String reason_id, String dept_code, String remark,
			String batch_line_id, String create_by, String transaction_type_id,
			String return_line_id, String suffocate_code) {
		try {
			String strURL = String.format(K_table.returntrans, org_id,
					invoice_no, wo_no, wo_key, route, reason_code, item_name,
					item_id, required_qty, balance_qty, sub_name, locator,
					frame, dc, qty, pu, reason_id, dept_code, remark,
					batch_line_id, create_by, transaction_type_id,
					return_line_id, suffocate_code).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}

	public static void Resolve(String Response) {
		try {
			AppData.mreturnquery.clear();
			JSONObject json = new JSONObject(Response);
			JSONArray array = json.getJSONArray("result");
			Treturnquery item = null;
			item = new Treturnquery();
			// �W�[���Y
//			item.Setorg_id("ORG");
//			item.Setinvoice_no("INVOICE");
//			item.Setitem_name("PN");
//			item.Setrequired_qty("REQUIRE");
//			item.Setbalance_qty("BALANCE");
//			item.Setsub_name("SUB");
//			item.Setlocator("LOC");
			
			item.Setorg_id("ORG");
			item.Setinvoice_no("Invoice");
			item.Setitem_name("Item");
			item.Setrequired_qty("RequiredQty");
			item.Setbalance_qty("BalanceQty");
			item.Setsub_name("Sub");
			item.Setlocator("Locator");
			item.Settransaction_type("TransType");
			item.Setdept_code("DeptCode");
            //
			AppData.mreturnquery.add(item);

			for (int i = 0; i < array.length(); i++) {
				item = new Treturnquery();
                //
				item.Setorg_id(array.getJSONObject(i).getString("ORG_ID"));
				item.Setinvoice_no(array.getJSONObject(i).getString(
						"INVOICE_NO"));
				item.Setwo_no(array.getJSONObject(i).getString("RETURN_WO_NO"));
				item.Setwo_key(array.getJSONObject(i).getString("WO_KEY"));
				item.Setroute(array.getJSONObject(i).getString("ROUTE_CODE"));
				item.Setreason_code(array.getJSONObject(i).getString(
						"REASON_NAME"));
				item.Setitem_name(array.getJSONObject(i).getString("ITEM_NAME"));
				item.Setitem_id(array.getJSONObject(i).getString("ITEM_ID"));
				
				String require_qty = array.getJSONObject(i).getString("REQUIRED_QTY");
				item.Setrequired_qty(require_qty);
				
				String balance_qty = array.getJSONObject(i).getString("BALANCE_QTY");
				item.Setbalance_qty(balance_qty);
				
//				item.Setrequired_qty(array.getJSONObject(i).getString(
//						"REQUIRED_QTY"));
//				item.Setbalance_qty(array.getJSONObject(i).getString(
//						"BALANCE_QTY"));
				item.Setsub_name(array.getJSONObject(i).getString(
						"SUBINVENTORY_NAME"));
				item.Setlocator(array.getJSONObject(i)
						.getString("LOCATOR_NAME"));
				item.Setpu(array.getJSONObject(i).getString("PU"));
				item.Setreason_id(array.getJSONObject(i).getString("REASON_ID"));
				item.Setdept_code(array.getJSONObject(i).getString("DEPT_CODE"));
				item.Setremark(array.getJSONObject(i).getString("REMARK"));
				item.Setbatch_line_id(array.getJSONObject(i).getString(
						"BATCH_LINE_ID"));
				item.Settransaction_type(array.getJSONObject(i).getString(
						"TRANSACTION_TYPE"));
				item.setTransaction_type_id(array.getJSONObject(i).getString(
						"TRANSACTION_TYPE_ID"));
				item.Setreturn_line_id(array.getJSONObject(i).getString(
						"RETURN_LINE_ID"));
				item.Setsuffocate_code(array.getJSONObject(i).getString(
						"SUFFOCATE_CODE"));
				item.setLastframe(array.getJSONObject(i).getString("LASTFRAME"));
				//
				AppData.mreturnquery.add(item);
			}
		} catch (Exception e) {

		}

	}

	public View getView(final int position, View convertView, ViewGroup parent) {
		ViewHolder holder = null;
		//
		if (convertView == null) {
			convertView = LayoutInflater.from(mContext).inflate(
					R.layout.returnqueryitem, null);
			holder = new ViewHolder();
			holder.morg_id = (TextView) convertView
					.findViewById(R.id.returnquery_org);
			holder.minvoice_no = (TextView) convertView
					.findViewById(R.id.returnquery_invoice_no);
			holder.mwo_no = (TextView) convertView
					.findViewById(R.id.returnquery_wo_no);
			holder.mwo_key = (TextView) convertView
					.findViewById(R.id.returnquery_wo_key);
			holder.mroute = (TextView) convertView
					.findViewById(R.id.returnquery_route);
			holder.mreason_code = (TextView) convertView
					.findViewById(R.id.returnquery_reason_code);
			holder.mitem_name = (TextView) convertView
					.findViewById(R.id.returnquery_pn);
			holder.mitem_id = (TextView) convertView
					.findViewById(R.id.returnquery_item_id);
			holder.mrequired_qty = (TextView) convertView
					.findViewById(R.id.returnquery_require);
			holder.mbalance_qty = (TextView) convertView
					.findViewById(R.id.returnquery_balance);
			holder.msub_name = (TextView) convertView
					.findViewById(R.id.returnquery_sub);
			holder.mlocator = (TextView) convertView
					.findViewById(R.id.returnquery_loc);
			holder.mpu = (TextView) convertView
					.findViewById(R.id.returnquery_pu);
			holder.mreason_id = (TextView) convertView
					.findViewById(R.id.returnquery_reason_id);
			holder.mdept_code = (TextView) convertView
					.findViewById(R.id.returnquery_dept_code);
			holder.mremark = (TextView) convertView
					.findViewById(R.id.returnquery_remark);
			holder.mbatch_line_id = (TextView) convertView
					.findViewById(R.id.returnquery_batch_line_id);
			holder.mtransaction_type = (TextView) convertView
					.findViewById(R.id.returnquery_transaction_type);
			holder.mreturn_line_id = (TextView) convertView
					.findViewById(R.id.returnquery_return_line_id);
			holder.msuffocate_code = (TextView) convertView
					.findViewById(R.id.returnquery_suffocate_code);
			//
			convertView.setTag(holder);
		} else {
			holder = (ViewHolder) convertView.getTag();
		}
		//
		holder.morg_id.setWidth((int)(AppData.screenwidth*AppData.colwidth.corgid));
		holder.minvoice_no.setWidth((int)(AppData.screenwidth*AppData.colwidth.cinvoice));
		holder.mitem_name.setWidth((int)(AppData.screenwidth*AppData.colwidth.citemname));
		holder.mrequired_qty.setWidth((int)(AppData.screenwidth*AppData.colwidth.cqty));
		holder.mbalance_qty.setWidth((int)(AppData.screenwidth*AppData.colwidth.cqty));
		holder.msub_name.setWidth((int)(AppData.screenwidth*AppData.colwidth.csubinv));
		holder.mlocator.setWidth((int)(AppData.screenwidth*AppData.colwidth.clocator));
		holder.mtransaction_type.setWidth((int)(AppData.screenwidth*AppData.colwidth.ctranstype));
		holder.mdept_code.setWidth((int)(AppData.screenwidth*AppData.colwidth.cdept));		
		//
		holder.morg_id.setText(AppData.mreturnquery.get(position).Getorg_id());
		holder.minvoice_no.setText(AppData.mreturnquery.get(position)
				.Getinvoice_no());
		holder.mwo_no.setText(AppData.mreturnquery.get(position).Getwo_no());
		holder.mwo_key.setText(AppData.mreturnquery.get(position).Getwo_key());
		holder.mroute.setText(AppData.mreturnquery.get(position).Getroute());
		holder.mreason_code.setText(AppData.mreturnquery.get(position)
				.Getreason_code());
		holder.mitem_name.setText(AppData.mreturnquery.get(position)
				.Getitem_name());
		holder.mitem_id
				.setText(AppData.mreturnquery.get(position).Getitem_id());
		holder.mrequired_qty.setText(AppData.mreturnquery.get(position)
				.Getrequired_qty());
		holder.mbalance_qty.setText(AppData.mreturnquery.get(position)
				.Getbalance_qty());
		holder.msub_name.setText(AppData.mreturnquery.get(position)
				.Getsub_name());
		holder.mlocator
				.setText(AppData.mreturnquery.get(position).Getlocator());
		holder.mpu.setText(AppData.mreturnquery.get(position).Getpu());
		holder.mreason_id.setText(AppData.mreturnquery.get(position)
				.Getreason_id());
		holder.mdept_code.setText(AppData.mreturnquery.get(position)
				.Getdept_code());
		holder.mremark.setText(AppData.mreturnquery.get(position).Getremark());
		holder.mbatch_line_id.setText(AppData.mreturnquery.get(position)
				.Getbatch_line_id());
		//
		holder.mtransaction_type.setText(AppData.mreturnquery.get(position)
				.Gettransaction_type());
		//
		holder.mreturn_line_id.setText(AppData.mreturnquery.get(position)
				.Getreturn_line_id());
		holder.msuffocate_code.setText(AppData.mreturnquery.get(position)
				.Getsuffocate_code());

		//
		/*
		convertView.setOnClickListener(new OnClickListener() {
			public void onClick(View v) {
				Intent intent = new Intent(mContext, Return_detail.class);
				if (position >= 1) {
					intent.putExtra("org_id", AppData.mreturnquery
							.get(position).Getorg_id());
					intent.putExtra("invoice_no",
							AppData.mreturnquery.get(position).Getinvoice_no());
					intent.putExtra("wo_no", AppData.mreturnquery.get(position)
							.Getwo_no());
					intent.putExtra("wo_key", AppData.mreturnquery
							.get(position).Getwo_key());
					intent.putExtra("route", AppData.mreturnquery.get(position)
							.Getroute());
					intent.putExtra("reason_code",
							AppData.mreturnquery.get(position).Getreason_code());
					intent.putExtra("item_name",
							AppData.mreturnquery.get(position).Getitem_name());
					intent.putExtra("item_id",
							AppData.mreturnquery.get(position).Getitem_id());
					intent.putExtra("required_qty",
							AppData.mreturnquery.get(position)
									.Getrequired_qty());
					intent.putExtra("balance_qty",
							AppData.mreturnquery.get(position).Getbalance_qty());
					intent.putExtra("sub_name",
							AppData.mreturnquery.get(position).Getsub_name());
					intent.putExtra("locator",
							AppData.mreturnquery.get(position).Getlocator());
					intent.putExtra("pu", AppData.mreturnquery.get(position)
							.Getpu());
					intent.putExtra("reason_id",
							AppData.mreturnquery.get(position).Getreason_id());
					intent.putExtra("dept_code",
							AppData.mreturnquery.get(position).Getdept_code());
					intent.putExtra("remark", AppData.mreturnquery
							.get(position).Getremark());
					intent.putExtra("batch_line_id",
							AppData.mreturnquery.get(position)
									.Getbatch_line_id());
					//
					intent.putExtra("transaction_type", AppData.mreturnquery
							.get(position).Gettransaction_type());
					intent.putExtra("transaction_type_id", AppData.mreturnquery
							.get(position).getTransaction_type_id());
					//
					intent.putExtra("return_line_id",
							AppData.mreturnquery.get(position)
									.Getreturn_line_id());
					intent.putExtra("suffocate_code",
							AppData.mreturnquery.get(position)
									.Getsuffocate_code());
					//
					mContext.startActivity(intent);
				}
			}
		});
        */
        
		return convertView;
	}

	class ViewHolder {
		TextView morg_id;
		TextView minvoice_no;
		TextView mwo_no;
		TextView mwo_key;
		TextView mroute;
		TextView mreason_code;
		TextView mitem_name;
		TextView mitem_id;
		TextView mrequired_qty;
		TextView mbalance_qty;
		TextView msub_name;
		TextView mlocator;
		TextView mpu;
		TextView mreason_id;
		TextView mdept_code;
		TextView mremark;
		TextView mbatch_line_id;
		TextView mtransaction_type;
		TextView mtransaction_type_id;
		TextView mreturn_line_id;
		TextView msuffocate_code;
	}
}
