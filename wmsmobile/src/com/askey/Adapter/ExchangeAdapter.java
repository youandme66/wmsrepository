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
import com.askey.model.Texchangequery;
import com.askey.wms.R;

public class ExchangeAdapter extends BaseAdapter {
	private Context mContext;

	public ExchangeAdapter(Context context) {
		mContext = context;
	}

	public int getCount() {
		return AppData.mexchangequery.size();
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
			String strURL = String.format(K_table.exchangequery, invoice_no)
					.replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}

	public static String CommitExchangeTrans(Context context,
			String exchange_header_id, String exchange_line_id,
			String invoice_no, String item_name, String item_id,
			String required_qty, String balance_qty, String out_sub_name,
			String out_locator_name, String out_org_id, String in_sub_name,
			String in_locator_name, String in_org_id, String in_frame_name,
			String out_frame_name, String model_name, String duty_dept_code,
			String scrap_type, String model_type, String reason_code,
			String wo_no, String scrap_line_id, String remark,
			String customer_code, String demand_id, String suffocate_code,
			String transaction_type_id, String charge_no, String create_by,
			String qty, String dc) {
		try {
			String strURL = String.format(K_table.exchangetrans,
					exchange_header_id, exchange_line_id, invoice_no,
					item_name, item_id, required_qty, balance_qty,
					out_sub_name, out_locator_name, out_org_id, in_sub_name,
					in_locator_name, in_org_id, in_frame_name, out_frame_name,
					model_name, duty_dept_code, scrap_type, model_type,
					reason_code, wo_no, scrap_line_id, remark, customer_code,
					demand_id, suffocate_code, transaction_type_id, charge_no,
					create_by, qty, dc).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}

	public static String CommitExchangeTrans_Out(Context context,
			String exchange_header_id, 
			String exchange_line_id,
			String invoice_no, 
			String item_name, 
			String item_id,
			String required_qty, 
			String balance_qty, 
			String in_sub_name, 
			String in_org_id, 
			String out_sub_name,
			String out_org_id,
			String out_locator_name,
			String out_frame_name,
			String model_name, 
			String duty_dept_code, 
			String scrap_type,
			String model_type, 
			String reason_code, 
			String wo_no,
			String scrap_line_id, 
			String remark, 
			String customer_code,
			String demand_id, 
			String suffocate_code,
			String transaction_type_id, 
			String charge_no, 
			String create_by,
			String qty, 
			String dc) {
		try {
			String strURL = String.format(K_table.exchangetrans_out,
					exchange_header_id, exchange_line_id, invoice_no,
					item_name, item_id, required_qty, balance_qty,
					out_sub_name,in_sub_name,in_org_id,  out_locator_name, out_org_id, 
					out_frame_name,
					model_name, duty_dept_code, scrap_type, model_type,
					reason_code, wo_no, scrap_line_id, remark, customer_code,
					demand_id, suffocate_code, transaction_type_id, charge_no,
					create_by, qty, dc).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}
	
	public static void Resolve(String Response) {
		try {
			AppData.mexchangequery.clear();
			JSONObject json = new JSONObject(Response);
			JSONArray array = json.getJSONArray("result");
			Texchangequery item = new Texchangequery();
			// 增加表頭
			item.Setinvoice_no("單據號");
			item.Setitem_name("料號");
			item.Setrequired_qty("需求量");
			item.Setbalance_qty("剩餘量");
			item.Setout_org_id("OUT_ORG");
			item.Setout_sub_name("OUT_SUB");
			item.Setout_locator_name("OUT_LOC");
			item.Setin_org_id("IN_ORG");
			item.Setin_sub_name("IN_SUB");
			item.Setin_locator_name("IN_LOC");
			//
			AppData.mexchangequery.add(item);

			for (int i = 0; i < array.length(); i++) {
				item = new Texchangequery();

				item.Setexchange_header_id(array.getJSONObject(i).getString(
						"EXCHANGE_HEADER_ID"));
				item.Setexchange_line_id(array.getJSONObject(i).getString(
						"EXCHANGE_LINE_ID"));
				item.Setinvoice_no(array.getJSONObject(i).getString(
						"INVOICE_NO"));
				item.Setitem_name(array.getJSONObject(i).getString("ITEM_NAME"));
				item.Setitem_id(array.getJSONObject(i).getString("ITEM_ID"));
				item.Setrequired_qty(array.getJSONObject(i).getString(
						"REQUIRED_QTY"));
				item.Setbalance_qty(array.getJSONObject(i).getString(
						"BALANCE_QTY"));
				item.Setout_sub_name(array.getJSONObject(i).getString(
						"OUT_SUB_NAME"));
				item.Setout_locator_name(array.getJSONObject(i).getString(
						"OUT_LOCATOR_NAME"));
				item.Setout_org_id(array.getJSONObject(i).getString(
						"OUT_ORG_ID"));
				item.Setin_sub_name(array.getJSONObject(i).getString(
						"IN_SUB_NAME"));
				item.Setin_locator_name(array.getJSONObject(i).getString(
						"IN_LOCATOR_NAME"));
				item.Setin_org_id(array.getJSONObject(i).getString("IN_ORG_ID"));
				item.Setmodel_name(array.getJSONObject(i).getString(
						"MODEL_NAME"));
				item.Setduty_dept_code(array.getJSONObject(i).getString(
						"DUTY_DEPT_CODE"));
				item.Setscrap_type(array.getJSONObject(i).getString(
						"SCRAP_TYPE"));
				item.Setmodel_type(array.getJSONObject(i).getString(
						"MODEL_TYPE"));
				item.Setreason_code(array.getJSONObject(i).getString(
						"REASON_CODE"));
				item.Setwo_no(array.getJSONObject(i).getString("WO_NO"));
				item.Setscrap_line_id(array.getJSONObject(i).getString(
						"SCRAP_LINE_ID"));
				item.Setremark(array.getJSONObject(i).getString("REMARK"));
				item.Setcustomer_code(array.getJSONObject(i).getString(
						"CUSTOMER_CODE"));
				item.Setdemand_id(array.getJSONObject(i).getString("DEMAND_ID"));
				item.Setsuffocate_code(array.getJSONObject(i).getString(
						"SUFFOCATE_CODE"));
				item.Settransaction_type_id(array.getJSONObject(i).getString(
						"TRANSACTION_TYPE_ID"));
				item.Setcharge_no(array.getJSONObject(i).getString("CHARGE_NO"));
				item.setOutneedframe(array.getJSONObject(i).getString(
						"OUTNEEDFRAME"));
				item.setInneedframe(array.getJSONObject(i).getString(
						"INNEEDFRAME"));
				item.setAframe(array.getJSONObject(i).getString(
						"AFRAME"));
				//
				AppData.mexchangequery.add(item);
			}
		} catch (Exception e) {

		}

	}

	public View getView(final int position, View convertView, ViewGroup parent) {
		ViewHolder holder = null;
		//
		if (convertView == null) {
			convertView = LayoutInflater.from(mContext).inflate(
					R.layout.exchangequeryitem, null);
			holder = new ViewHolder();

			holder.mexchange_header_id = (TextView) convertView
					.findViewById(R.id.exchangequery_exchange_header_id);
			holder.mexchange_line_id = (TextView) convertView
					.findViewById(R.id.exchangequery_exchange_line_id);
			holder.minvoice_no = (TextView) convertView
					.findViewById(R.id.exchangequery_invoice_no);
			holder.mitem_name = (TextView) convertView
					.findViewById(R.id.exchangequery_item_name);
			holder.mitem_id = (TextView) convertView
					.findViewById(R.id.exchangequery_item_id);
			holder.mrequired_qty = (TextView) convertView
					.findViewById(R.id.exchangequery_required_qty);
			holder.mbalance_qty = (TextView) convertView
					.findViewById(R.id.exchangequery_balance_qty);
			holder.mout_sub_name = (TextView) convertView
					.findViewById(R.id.exchangequery_out_sub_name);
			holder.mout_locator_name = (TextView) convertView
					.findViewById(R.id.exchangequery_out_locator_name);
			holder.mout_org_id = (TextView) convertView
					.findViewById(R.id.exchangequery_out_org_id);
			holder.min_sub_name = (TextView) convertView
					.findViewById(R.id.exchangequery_in_sub_name);
			holder.min_locator_name = (TextView) convertView
					.findViewById(R.id.exchangequery_in_locator_name);
			holder.min_org_id = (TextView) convertView
					.findViewById(R.id.exchangequery_in_org_id);
			holder.mmodel_name = (TextView) convertView
					.findViewById(R.id.exchangequery_model_name);
			holder.mduty_dept_code = (TextView) convertView
					.findViewById(R.id.exchangequery_duty_dept_code);
			holder.mscrap_type = (TextView) convertView
					.findViewById(R.id.exchangequery_scrap_type);
			holder.mmodel_type = (TextView) convertView
					.findViewById(R.id.exchangequery_model_type);
			holder.mreason_code = (TextView) convertView
					.findViewById(R.id.exchangequery_reason_code);
			holder.mwo_no = (TextView) convertView
					.findViewById(R.id.exchangequery_wo_no);
			holder.mscrap_line_id = (TextView) convertView
					.findViewById(R.id.exchangequery_scrap_line_id);
			holder.mremark = (TextView) convertView
					.findViewById(R.id.exchangequery_remark);
			holder.mcustomer_code = (TextView) convertView
					.findViewById(R.id.exchangequery_customer_code);
			holder.mdemand_id = (TextView) convertView
					.findViewById(R.id.exchangequery_demand_id);
			holder.msuffocate_code = (TextView) convertView
					.findViewById(R.id.exchangequery_suffocate_code);
			holder.mtransaction_type_id = (TextView) convertView
					.findViewById(R.id.exchangequery_transaction_type_id);
			holder.mcharge_no = (TextView) convertView
					.findViewById(R.id.exchangequery_charge_no);
			//
			convertView.setTag(holder);
		} else {
			holder = (ViewHolder) convertView.getTag();
		}
		//
		holder.minvoice_no
				.setWidth((int) (AppData.screenwidth * AppData.colwidth.cinvoice));
		holder.mitem_name
				.setWidth((int) (AppData.screenwidth * AppData.colwidth.citemname));
		holder.mrequired_qty
				.setWidth((int) (AppData.screenwidth * AppData.colwidth.cqty));
		holder.mbalance_qty
				.setWidth((int) (AppData.screenwidth * AppData.colwidth.cqty));
		holder.mout_org_id
				.setWidth((int) (AppData.screenwidth * AppData.colwidth.corgid));
		holder.mout_sub_name
				.setWidth((int) (AppData.screenwidth * AppData.colwidth.csubinv));
		holder.mout_locator_name
				.setWidth((int) (AppData.screenwidth * AppData.colwidth.clocator));
		holder.min_org_id
				.setWidth((int) (AppData.screenwidth * AppData.colwidth.corgid));
		holder.min_sub_name
				.setWidth((int) (AppData.screenwidth * AppData.colwidth.csubinv));
		holder.min_locator_name
				.setWidth((int) (AppData.screenwidth * AppData.colwidth.clocator));
		//

		holder.mexchange_header_id.setText(AppData.mexchangequery.get(position)
				.Getexchange_header_id());
		holder.mexchange_line_id.setText(AppData.mexchangequery.get(position)
				.Getexchange_line_id());
		holder.minvoice_no.setText(AppData.mexchangequery.get(position)
				.Getinvoice_no());
		holder.mitem_name.setText(AppData.mexchangequery.get(position)
				.Getitem_name());
		holder.mitem_id.setText(AppData.mexchangequery.get(position)
				.Getitem_id());
		holder.mrequired_qty.setText(AppData.mexchangequery.get(position)
				.Getrequired_qty());
		holder.mbalance_qty.setText(AppData.mexchangequery.get(position)
				.Getbalance_qty());
		holder.mout_sub_name.setText(AppData.mexchangequery.get(position)
				.Getout_sub_name());
		holder.mout_locator_name.setText(AppData.mexchangequery.get(position)
				.Getout_locator_name());
		holder.mout_org_id.setText(AppData.mexchangequery.get(position)
				.Getout_org_id());
		holder.min_sub_name.setText(AppData.mexchangequery.get(position)
				.Getin_sub_name());
		holder.min_locator_name.setText(AppData.mexchangequery.get(position)
				.Getin_locator_name());
		holder.min_org_id.setText(AppData.mexchangequery.get(position)
				.Getin_org_id());
		holder.mmodel_name.setText(AppData.mexchangequery.get(position)
				.Getmodel_name());
		holder.mduty_dept_code.setText(AppData.mexchangequery.get(position)
				.Getduty_dept_code());
		holder.mscrap_type.setText(AppData.mexchangequery.get(position)
				.Getscrap_type());
		holder.mmodel_type.setText(AppData.mexchangequery.get(position)
				.Getmodel_type());
		holder.mreason_code.setText(AppData.mexchangequery.get(position)
				.Getreason_code());
		holder.mwo_no.setText(AppData.mexchangequery.get(position).Getwo_no());
		holder.mscrap_line_id.setText(AppData.mexchangequery.get(position)
				.Getscrap_line_id());
		holder.mremark
				.setText(AppData.mexchangequery.get(position).Getremark());
		holder.mcustomer_code.setText(AppData.mexchangequery.get(position)
				.Getcustomer_code());
		holder.mdemand_id.setText(AppData.mexchangequery.get(position)
				.Getdemand_id());
		holder.msuffocate_code.setText(AppData.mexchangequery.get(position)
				.Getsuffocate_code());
		holder.mtransaction_type_id.setText(AppData.mexchangequery
				.get(position).Gettransaction_type_id());
		holder.mcharge_no.setText(AppData.mexchangequery.get(position)
				.Getcharge_no());
		//
		/*
		 * convertView.setOnClickListener(new OnClickListener() { public void
		 * onClick(View v) { Intent intent = new Intent(mContext,
		 * exchange_detail.class); if (position >= 1) {
		 * intent.putExtra("exchange_header_id",
		 * AppData.mexchangequery.get(position) .Getexchange_header_id());
		 * intent.putExtra("exchange_line_id", AppData.mexchangequery
		 * .get(position).Getexchange_line_id()); intent.putExtra("invoice_no",
		 * AppData.mexchangequery.get(position) .Getinvoice_no());
		 * intent.putExtra("item_name",
		 * AppData.mexchangequery.get(position).Getitem_name());
		 * intent.putExtra("item_id",
		 * AppData.mexchangequery.get(position).Getitem_id());
		 * intent.putExtra("required_qty", AppData.mexchangequery.get(position)
		 * .Getrequired_qty()); intent.putExtra("balance_qty",
		 * AppData.mexchangequery.get(position) .Getbalance_qty());
		 * intent.putExtra("out_sub_name", AppData.mexchangequery.get(position)
		 * .Getout_sub_name()); intent.putExtra("out_locator_name",
		 * AppData.mexchangequery .get(position).Getout_locator_name());
		 * intent.putExtra("out_org_id", AppData.mexchangequery.get(position)
		 * .Getout_org_id()); intent.putExtra("in_sub_name",
		 * AppData.mexchangequery.get(position) .Getin_sub_name());
		 * intent.putExtra("in_locator_name", AppData.mexchangequery
		 * .get(position).Getin_locator_name()); intent.putExtra("in_org_id",
		 * AppData.mexchangequery.get(position).Getin_org_id());
		 * intent.putExtra("model_name", AppData.mexchangequery.get(position)
		 * .Getmodel_name()); intent.putExtra("duty_dept_code",
		 * AppData.mexchangequery .get(position).Getduty_dept_code());
		 * intent.putExtra("scrap_type", AppData.mexchangequery.get(position)
		 * .Getscrap_type()); intent.putExtra("model_type",
		 * AppData.mexchangequery.get(position) .Getmodel_type());
		 * intent.putExtra("reason_code", AppData.mexchangequery.get(position)
		 * .Getreason_code()); intent.putExtra("wo_no",
		 * AppData.mexchangequery.get(position).Getwo_no());
		 * intent.putExtra("scrap_line_id", AppData.mexchangequery
		 * .get(position).Getscrap_line_id()); intent.putExtra("remark",
		 * AppData.mexchangequery.get(position).Getremark());
		 * intent.putExtra("customer_code", AppData.mexchangequery
		 * .get(position).Getcustomer_code()); intent.putExtra("demand_id",
		 * AppData.mexchangequery.get(position).Getdemand_id());
		 * intent.putExtra("suffocate_code", AppData.mexchangequery
		 * .get(position).Getsuffocate_code());
		 * intent.putExtra("transaction_type_id",
		 * AppData.mexchangequery.get(position) .Gettransaction_type_id());
		 * intent.putExtra("charge_no",
		 * AppData.mexchangequery.get(position).Getcharge_no()); //
		 * mContext.startActivity(intent); } } });
		 */
		//
		// int[] colors = { Color.WHITE, Color.rgb(219, 238, 244) };// RGB颜色
		// convertView.setBackgroundColor(colors[position % 2]);// 每隔item之间颜色不同
		//
		return convertView;
	}

	class ViewHolder {
		TextView mexchange_header_id;
		TextView mexchange_line_id;
		TextView minvoice_no;
		TextView mitem_name;
		TextView mitem_id;
		TextView mrequired_qty;
		TextView mbalance_qty;
		TextView mout_sub_name;
		TextView mout_locator_name;
		TextView mout_org_id;
		TextView min_sub_name;
		TextView min_locator_name;
		TextView min_org_id;
		TextView mmodel_name;
		TextView mduty_dept_code;
		TextView mscrap_type;
		TextView mmodel_type;
		TextView mreason_code;
		TextView mwo_no;
		TextView mscrap_line_id;
		TextView mremark;
		TextView mcustomer_code;
		TextView mdemand_id;
		TextView msuffocate_code;
		TextView mtransaction_type_id;
		TextView mcharge_no;
	}
}
