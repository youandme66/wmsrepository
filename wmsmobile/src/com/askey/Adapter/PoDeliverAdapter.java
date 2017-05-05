package com.askey.Adapter;

import org.json.JSONArray;
import org.json.JSONObject;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.askey.activity.poDeliver_Detail;
import com.askey.model.AppData;
import com.askey.model.CustomHttpClient;
import com.askey.model.K_table;
import com.askey.model.Tporcvnoquery;
import com.askey.wms.R;

public class PoDeliverAdapter extends BaseAdapter {
	private Context mContext;

	public PoDeliverAdapter(Context context) {
		mContext = context;
	}

	public int getCount() {
		return AppData.mporcvnoquery.size();
	}

	public Object getItem(int position) {
		return null;
	}

	public long getItemId(int position) {
		return 0;
	}

	public static String Request(Context context, String rcvno, String orgid) {
		// private static final CommonLog log = LogFactory.createLog();
		try {
			String strURL = String.format(K_table.poqueryrcvno, rcvno, orgid).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}

	public static String CommitPODeliver(Context context, String rcvno,
			String po_no, String orgid, String orgcode, String item_name,
			String datecode, String locator, String temp_region, String frame,
			String temp_qty, String rcv_qty, String delivered_qty,
			String iqc_flag, String po_header_id, String po_line_id,
			String line_location_id, String distribution_id, String invoice,
			String special_no, String special_wo_no, String user_id,
			String suffocate_code) {
		try {
			String strURL = String.format(K_table.podelivertrans, rcvno, po_no,
					orgid, orgcode, item_name, datecode, locator, temp_region,
					frame, temp_qty, rcv_qty, delivered_qty, iqc_flag,
					po_header_id, po_line_id, line_location_id,
					distribution_id, invoice, special_no, special_wo_no,
					user_id, suffocate_code).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}

	public static void Resolve(String Response) {
		try {
			AppData.mporcvnoquery.clear();
			JSONObject json = new JSONObject(Response);
			JSONArray array = json.getJSONArray("result");
			Tporcvnoquery item = null;
			item = new Tporcvnoquery();
			// 增加表頭
			item.Setorgcode("ORG");
			item.Setpono("PO");
			item.Setpoline("Line");
			item.Setpn("PN");
			item.Setiqcflag("IQCFlag");
			item.Setdc("D/C");
			item.Setsubinv("庫別");
			item.Setframe("料架");
			item.Setrcvqty("暫收量");
			item.Setdeliverqty("剩餘量 ");
			item.Setlocator("Locator");
			//
			AppData.mporcvnoquery.add(item);
			//
			for (int i = 0; i < array.length(); i++) {
				item = new Tporcvnoquery();
				// 字段必須是大寫
				item.Setorgcode(array.getJSONObject(i).getString("ORG_CODE"));
				item.Setpono(array.getJSONObject(i).getString("PO_NO"));
				item.Setpoline(array.getJSONObject(i).getString("LINE_NUM"));
				item.Setpn(array.getJSONObject(i).getString("ITEM_NAME"));
				item.Setiqcflag(array.getJSONObject(i).getString(
						"INSPECT_REQUIRED_FLAG"));
				item.Setdc(array.getJSONObject(i).getString("DATECODE"));
				item.Setsubinv(array.getJSONObject(i).getString(
						"SUBINVENTORY_NAME"));
				item.Setframe(array.getJSONObject(i).getString("FRAME_NAME"));
				item.Setrcvqty(array.getJSONObject(i).getString("TEMP_QTY"));
				item.Setdeliverqty(array.getJSONObject(i).getString(
						"BALANCE_QTY"));
				//
				item.Setrcvno(array.getJSONObject(i).getString("RECEIPT_NO"));
				item.Setorgid(array.getJSONObject(i).getString("ORG_ID"));
				item.Setlocator(array.getJSONObject(i)
						.getString("LOCATOR_NAME"));
				item.Settemp_region(array.getJSONObject(i).getString(
						"TEMP_REGION"));
				item.Setrcv_qty(array.getJSONObject(i).getString(
						"RCV_QTY"));
				item.Setpo_header_id(array.getJSONObject(i).getString(
						"PO_HEADER_ID"));
				item.Setpo_line_id(array.getJSONObject(i).getString(
						"PO_LINE_ID"));
				item.Setline_location_id(array.getJSONObject(i).getString(
						"PO_LINE_LOCATION_ID"));
				item.Setdistribution_id(array.getJSONObject(i).getString(
						"PO_DISTRIBUTION_ID"));
				item.Setinvoice(array.getJSONObject(i).getString("INVOICE"));
				item.Setspecial_no(array.getJSONObject(i).getString(
						"SPECIAL_NO"));
				item.Setspecial_wo_no(array.getJSONObject(i).getString(
						"SPECIAL_WO_NO"));
				item.Setsuffocate_code(array.getJSONObject(i).getString(
						"SUFFOCATE_CODE"));
				item.setLastframe(array.getJSONObject(i).getString(
						"LASTFRAME"));
				//
				AppData.mporcvnoquery.add(item);
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
					R.layout.poqueryrcvnoitem, null);
			//
			holder = new ViewHolder();
			holder.morgcode = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_orgcode);
			holder.mpono = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_po);
			holder.mpoline = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_poline);
			holder.mpn = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_pn);
			holder.miqcflag = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_iqcflag);
			holder.mdc = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_dc);
			holder.msubinv = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_subinv);
			holder.mframe = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_frame);
			holder.mrcvqty = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_rcvqty);
			holder.mdeliverqty = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_deliverqty);
			holder.mrcvno = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_rcvno);
			holder.morgid = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_orgid);
			holder.mlocator = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_locator);
			holder.mtemp_region = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_temp_region);
			holder.mrcv_qty = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_rcv_qty);
			holder.mpo_header_id = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_po_header_id);
			holder.mpo_line_id = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_po_line_id);
			holder.mline_location_id = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_line_location_id);
			holder.mdistribution_id = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_distribution_id);
			holder.minvoice = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_invoice);
			holder.mspecial_no = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_special_no);
			holder.mspecial_wo_no = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_special_wo_no);
			holder.msuffocate_code = (TextView) convertView
					.findViewById(R.id.poqueryrvnno_suffocate_code);
			//
			convertView.setTag(holder);
		} else {
			holder = (ViewHolder) convertView.getTag();
		}
		//
		holder.morgcode.setWidth((int) (AppData.screenwidth*AppData.colwidth.corgcode));
		holder.mpono.setWidth((int) (AppData.screenwidth*AppData.colwidth.cpo));
		holder.mpoline.setWidth((int) (AppData.screenwidth*AppData.colwidth.cpoline));
		holder.mpn.setWidth((int) (AppData.screenwidth*AppData.colwidth.citemname));
		holder.miqcflag.setWidth((int) (AppData.screenwidth*AppData.colwidth.ciqcflag));
		holder.mdc.setWidth((int) (AppData.screenwidth*AppData.colwidth.cdc));
		holder.msubinv.setWidth((int) (AppData.screenwidth*AppData.colwidth.csubinv));
		holder.mframe.setWidth((int) (AppData.screenwidth*AppData.colwidth.cframe));
		holder.mrcvqty.setWidth((int) (AppData.screenwidth*AppData.colwidth.cqty));
		holder.mdeliverqty.setWidth((int) (AppData.screenwidth*AppData.colwidth.cqty));
		holder.mlocator.setWidth((int) (AppData.screenwidth*AppData.colwidth.clocator));		
		
		//
		holder.morgcode.setText(AppData.mporcvnoquery.get(position)
				.Getorgcode());
		holder.mpono.setText(AppData.mporcvnoquery.get(position).Getpono());
		holder.mpoline.setText(AppData.mporcvnoquery.get(position).Getpoline());
		holder.mpn.setText(AppData.mporcvnoquery.get(position).Getpn());
		holder.miqcflag.setText(AppData.mporcvnoquery.get(position)
				.Getiqcflag());
		holder.mdc.setText(AppData.mporcvnoquery.get(position).Getdc());
		holder.msubinv.setText(AppData.mporcvnoquery.get(position).Getsubinv());
		holder.mframe.setText(AppData.mporcvnoquery.get(position).Getframe());
		holder.mrcvqty.setText(AppData.mporcvnoquery.get(position).Getrcvqty());//暫收量
		holder.mdeliverqty.setText(AppData.mporcvnoquery.get(position)
				.Getdeliverqty());
		holder.mrcvno.setText(AppData.mporcvnoquery.get(position).Getrcvno());
		holder.morgid.setText(AppData.mporcvnoquery.get(position).Getorgid());
		holder.mlocator.setText(AppData.mporcvnoquery.get(position)
				.Getlocator());
		holder.mtemp_region.setText(AppData.mporcvnoquery.get(position)
				.Gettemp_region());
		holder.mrcv_qty.setText(AppData.mporcvnoquery.get(position)
				.Getrcv_qty());
		holder.mpo_header_id.setText(AppData.mporcvnoquery.get(position)
				.Getpo_header_id());
		holder.mpo_line_id.setText(AppData.mporcvnoquery.get(position)
				.Getpo_line_id());
		holder.mline_location_id.setText(AppData.mporcvnoquery.get(position)
				.Getline_location_id());
		holder.mdistribution_id.setText(AppData.mporcvnoquery.get(position)
				.Getdistribution_id());
		holder.minvoice.setText(AppData.mporcvnoquery.get(position)
				.Getinvoice());
		holder.mspecial_no.setText(AppData.mporcvnoquery.get(position)
				.Getspecial_no());
		holder.mspecial_wo_no.setText(AppData.mporcvnoquery.get(position)
				.Getspecial_wo_no());
		holder.msuffocate_code.setText(AppData.mporcvnoquery.get(position)
				.Getsuffocate_code());

		//
		convertView.setOnClickListener(new OnClickListener() {
			public void onClick(View v) {
				Intent intent = new Intent(mContext, poDeliver_Detail.class);// 创建Intent对象
				//
				if (position >= 1) {
					intent.putExtra("orgcode",
							AppData.mporcvnoquery.get(position).Getorgcode());
					intent.putExtra("pono", AppData.mporcvnoquery.get(position)
							.Getpono());
					intent.putExtra("poline",
							AppData.mporcvnoquery.get(position).Getpoline());
					intent.putExtra("pn", AppData.mporcvnoquery.get(position)
							.Getpn());
					intent.putExtra("iqcflag",
							AppData.mporcvnoquery.get(position).Getiqcflag());
					intent.putExtra("dc", AppData.mporcvnoquery.get(position)
							.Getdc());
					intent.putExtra("subinv",
							AppData.mporcvnoquery.get(position).Getsubinv());
					intent.putExtra("frame", AppData.mporcvnoquery
							.get(position).Getframe());
					intent.putExtra("rcvqty",
							AppData.mporcvnoquery.get(position).Getrcvqty());
					intent.putExtra("deliverqty",
							AppData.mporcvnoquery.get(position).Getdeliverqty());
					intent.putExtra("rcvno", AppData.mporcvnoquery
							.get(position).Getrcvno());
					intent.putExtra("orgid", AppData.mporcvnoquery
							.get(position).Getorgid());
					intent.putExtra("locator",
							AppData.mporcvnoquery.get(position).Getlocator());
					intent.putExtra("temp_region",
							AppData.mporcvnoquery.get(position)
									.Gettemp_region());
					intent.putExtra("delivered_qty",
							AppData.mporcvnoquery.get(position)
									.Getrcv_qty());
					intent.putExtra("po_header_id",
							AppData.mporcvnoquery.get(position)
									.Getpo_header_id());
					intent.putExtra("po_line_id",
							AppData.mporcvnoquery.get(position).Getpo_line_id());
					intent.putExtra("line_location_id", AppData.mporcvnoquery
							.get(position).Getline_location_id());
					intent.putExtra("distribution_id", AppData.mporcvnoquery
							.get(position).Getdistribution_id());
					intent.putExtra("invoice",
							AppData.mporcvnoquery.get(position).Getinvoice());
					intent.putExtra("special_no",
							AppData.mporcvnoquery.get(position).Getspecial_no());
					intent.putExtra("special_wo_no",
							AppData.mporcvnoquery.get(position)
									.Getspecial_wo_no());
					intent.putExtra("suffocate_code", AppData.mporcvnoquery
							.get(position).Getsuffocate_code());
					intent.putExtra("lastframe", AppData.mporcvnoquery
							.get(position).getLastframe());
					//
					mContext.startActivity(intent);//执行Intent操作
				}
			}
		});
		//
		//int[] colors = { Color.WHITE, Color.rgb(219, 238, 244) };// RGB颜色
		//convertView.setBackgroundColor(colors[position % 2]);// 每隔item之间颜色不同
		//
		return convertView;
	}

	class ViewHolder {
		TextView morgcode;
		TextView mpono;
		TextView mpoline;
		TextView mpn;
		TextView miqcflag;
		TextView mdc;
		TextView msubinv;
		TextView mframe;
		TextView mrcvqty;
		TextView mdeliverqty;
		TextView mrcvno;
		TextView morgid;
		TextView mlocator;
		TextView mtemp_region;
		TextView mrcv_qty;
		TextView mpo_header_id;
		TextView mpo_line_id;
		TextView mline_location_id;
		TextView mdistribution_id;
		TextView minvoice;
		TextView mspecial_no;
		TextView mspecial_wo_no;
		TextView msuffocate_code;
	}
}
