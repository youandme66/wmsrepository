package com.askey.Adapter;

import org.json.JSONArray;
import org.json.JSONObject;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.EditText;
import android.widget.TextView;

import com.askey.model.AppData;
import com.askey.model.CommonUtil;
import com.askey.model.CustomHttpClient;
import com.askey.model.K_table;
import com.askey.model.Tissuequery;
import com.askey.wms.R;

public class IssueAdapter extends BaseAdapter {
	private Context mContext;
	private EditText medbarcode;

	public IssueAdapter(Context context,EditText edbarcode) {
		mContext = context;
		medbarcode= edbarcode;
	}

	public int getCount() {
		return AppData.missuequery.size();
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
			String strURL = String.format(K_table.issuequery, invoice_no).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}

	public static String CommitIssueTrans(Context context, String org_id,
			String invoice_no, String wo, String wip_entity_id,
			String operation_seq_num, String reason_name, String item_name,
			String item_id, String required_qty, String balance_qty,
			String sub_name, String locator, String frame, String dc,
			String qty, String pu, String reason_id, String dept_code,
			String transaction_type_id, String suffocate_code, String remark,
			String model_name, String customer_code, String demand_id,
			String batch_line_id, String issue_line_id, String create_by) {
		try {
			String strURL = String.format(K_table.issuetrans, org_id,
					invoice_no, wo, wip_entity_id, operation_seq_num,
					reason_name, item_name, item_id, required_qty, balance_qty,
					sub_name, locator, frame, dc, qty, pu, reason_id,
					dept_code, transaction_type_id, suffocate_code, remark,
					model_name, customer_code, demand_id, batch_line_id,
					issue_line_id, create_by).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}

	public static void Resolve(String Response) {
		try {
			AppData.missuequery.clear();
			JSONObject json = new JSONObject(Response);
			JSONArray array = json.getJSONArray("result");
			Tissuequery item = null;
			item = new Tissuequery();
			
			// 增加表頭			
			item.Setorg_id("ORG");
			item.Setinvoice_no("單據號");
			item.setTranstype("領料類別");
			item.Setitem_name("料號");
			item.Setrequired_qty("需求量");
			item.Setbalance_qty("剩餘量");
			item.Setsub_name("庫別");
			item.Setlocator("Locator");
			item.Setwo_no("工單");
			item.Setoperation_seq_num("製程");
			item.Setreason_name("原因碼");
            //
			AppData.missuequery.add(item);

			for (int i = 0; i < array.length(); i++) {
				item = new Tissuequery();
				//
				item.Setorg_id(array.getJSONObject(i).getString("ORG_ID"));
				item.Setinvoice_no(array.getJSONObject(i).getString(
						"INVOICE_NO"));
				item.setTranstype(array.getJSONObject(i).getString("TRANSTYPE"));
				item.Setwo_no(array.getJSONObject(i).getString("WO_NO"));
				item.Setwo_key(array.getJSONObject(i).getString(
						"WIP_ENTITY_ID"));
				item.Setoperation_seq_num(array.getJSONObject(i).getString(
						"OPERATION_SEQ_NUM"));
				item.Setreason_name(array.getJSONObject(i).getString(
						"REASON_NAME"));
				item.Setitem_name(array.getJSONObject(i).getString("ITEM_NAME"));
				item.Setitem_id(array.getJSONObject(i).getString("ITEM_ID"));				
				item.Setrequired_qty(array.getJSONObject(i).getString("REQUIRED_QTY"));				
				item.Setbalance_qty(array.getJSONObject(i).getString("BALANCE_QTY"));
				item.Setsub_name(array.getJSONObject(i).getString(
						"SUBINVENTORY_NAME"));
				item.Setlocator(array.getJSONObject(i)
						.getString("LOCATOR_NAME"));
				item.Setpu(array.getJSONObject(i).getString("PU"));
				item.Setreason_id(array.getJSONObject(i).getString("REASON_ID"));
				item.Setdept_code(array.getJSONObject(i).getString("DEPT_CODE"));
				item.Settransaction_type_id(array.getJSONObject(i).getString(
						"TRANSACTION_TYPE_ID"));
				item.Setsuffocate_code(array.getJSONObject(i).getString(
						"SUFFOCATE_CODE"));
				item.Setremark(array.getJSONObject(i).getString("REMARK"));
				item.Setmodel_name(array.getJSONObject(i).getString(
						"MODEL_NAME"));
				item.Setcustomer_code(array.getJSONObject(i).getString(
						"CUSTOMER_CODE"));
				item.Setdemand_id(array.getJSONObject(i).getString("DEMAND_ID"));
				item.Setbatch_line_id(array.getJSONObject(i).getString(
						"BATCH_LINE_ID"));
				item.Setissue_line_id(array.getJSONObject(i).getString(
						"ISSUE_LINE_ID"));
				item.setAframe(array.getJSONObject(i).getString("AFRAME"));
				//
				AppData.missuequery.add(item);
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
					R.layout.issuequeryitem, null);
			holder = new ViewHolder();
			holder.morg_id = (TextView) convertView
					.findViewById(R.id.issuequery_org);
			holder.minvoice_no = (TextView) convertView
					.findViewById(R.id.issuequery_invoice_no);
			holder.mtranstype = (TextView) convertView.findViewById(R.id.issuequery_transactiontype);
			holder.mwo = (TextView) convertView
					.findViewById(R.id.issuequery_wo);
			holder.moperation_seq_num = (TextView) convertView
					.findViewById(R.id.issuequery_route);
			holder.mreason_name = (TextView) convertView
					.findViewById(R.id.issuequery_reason_name);
			holder.mitem_name = (TextView) convertView
					.findViewById(R.id.issuequery_pn);
			holder.mrequired_qty = (TextView) convertView
					.findViewById(R.id.issuequery_require);
			holder.mbalance_qty = (TextView) convertView
					.findViewById(R.id.issuequery_balance);
			holder.msub_name = (TextView) convertView
					.findViewById(R.id.issuequery_sub);
			holder.mlocator = (TextView) convertView
					.findViewById(R.id.issuequery_loc);
			holder.mpu = (TextView) convertView
					.findViewById(R.id.issuequery_pu);
			holder.mreason_id = (TextView) convertView
					.findViewById(R.id.issuequery_reason_id);
			holder.mdept_code = (TextView) convertView
					.findViewById(R.id.issuequery_dept_code);
			holder.mtransaction_type_id = (TextView) convertView
					.findViewById(R.id.issuequery_transaction_type_id);
			holder.msuffocate_code = (TextView) convertView
					.findViewById(R.id.issuequery_suffocate_code);
			holder.mremark = (TextView) convertView
					.findViewById(R.id.issuequery_remark);
			holder.mmodel_name = (TextView) convertView
					.findViewById(R.id.issuequery_model_name);
			holder.mcustomer_code = (TextView) convertView
					.findViewById(R.id.issuequery_customer_code);
			holder.mdemand_id = (TextView) convertView
					.findViewById(R.id.issuequery_demand_id);
			holder.mbatch_line_id = (TextView) convertView
					.findViewById(R.id.issuequery_batch_line_id);
			holder.missue_line_id = (TextView) convertView
					.findViewById(R.id.issuequery_issue_line_id);
			//
			convertView.setTag(holder);
		} else {
			holder = (ViewHolder) convertView.getTag();
		}
		//
		
		holder.morg_id.setWidth((int) (AppData.screenwidth * AppData.colwidth.corgid));
		holder.minvoice_no.setWidth((int) (AppData.screenwidth * AppData.colwidth.cinvoice));
		holder.mtranstype.setWidth((int) (AppData.screenwidth * AppData.colwidth.ctranstype));
		holder.mitem_name.setWidth((int) (AppData.screenwidth * AppData.colwidth.citemname));
		holder.mrequired_qty.setWidth((int) (AppData.screenwidth * AppData.colwidth.cqty));
		holder.mbalance_qty.setWidth((int) (AppData.screenwidth * AppData.colwidth.cqty));
		holder.msub_name.setWidth((int) (AppData.screenwidth * AppData.colwidth.csubinv));
		holder.mlocator.setWidth((int) (AppData.screenwidth * AppData.colwidth.clocator));
		holder.mwo.setWidth((int) (AppData.screenwidth * AppData.colwidth.cwo));
		holder.moperation_seq_num.setWidth((int) (AppData.screenwidth * AppData.colwidth.copseq));
		holder.mreason_name.setWidth((int) (AppData.screenwidth * AppData.colwidth.creasonname));				
		//
		holder.morg_id.setText(AppData.missuequery.get(position).Getorg_id());
		holder.minvoice_no.setText(AppData.missuequery.get(position)
				.Getinvoice_no());
		//transaction type
		holder.mtranstype.setText(AppData.missuequery.get(position)
				.getTranstype());
		holder.mwo.setText(AppData.missuequery.get(position).Getwo_no());
		holder.moperation_seq_num.setText(AppData.missuequery.get(position)
				.Getoperation_seq_num());
		holder.mreason_name.setText(AppData.missuequery.get(position)
				.Getreason_name());
		holder.mitem_name.setText(AppData.missuequery.get(position)
				.Getitem_name());
		holder.mrequired_qty.setText(AppData.missuequery.get(position)
				.Getrequired_qty());
		holder.mbalance_qty.setText(AppData.missuequery.get(position)
				.Getbalance_qty());
		holder.msub_name.setText(AppData.missuequery.get(position)
				.Getsub_name());
		holder.mlocator.setText(AppData.missuequery.get(position).Getlocator());
		holder.mpu.setText(AppData.missuequery.get(position).Getpu());
		holder.mreason_id.setText(AppData.missuequery.get(position)
				.Getreason_id());
		holder.mdept_code.setText(AppData.missuequery.get(position)
				.Getdept_code());
	    //transaction type id
		holder.mtransaction_type_id.setText(AppData.missuequery.get(position)
				.Gettransaction_type_id());
		//
		holder.msuffocate_code.setText(AppData.missuequery.get(position)
				.Getsuffocate_code());
		holder.mremark.setText(AppData.missuequery.get(position).Getremark());
		holder.mmodel_name.setText(AppData.missuequery.get(position)
				.Getmodel_name());
		holder.mcustomer_code.setText(AppData.missuequery.get(position)
				.Getcustomer_code());
		holder.mdemand_id.setText(AppData.missuequery.get(position)
				.Getdemand_id());
		holder.mbatch_line_id.setText(AppData.missuequery.get(position)
				.Getbatch_line_id());
		holder.missue_line_id.setText(AppData.missuequery.get(position)
				.Getissue_line_id());
		//		
		convertView.setOnClickListener(new OnClickListener() {   
    			//
			    final Intent intent = new Intent();			
			    public void onClick(View v) {
			    final String pn=AppData.missuequery.get(position).Getitem_name();
			    final String qty=AppData.missuequery.get(position).Getbalance_qty();
			    if(!pn.substring(0, 1).equals("6") && !pn.substring(0, 1).equals("7"))
			    {
					CommonUtil.WMSShowmsg(mContext, String.format("料號[%s]不是半成品料號", pn));
					return;
			    }
			    //
				final EditText edview = new EditText(mContext);
				edview.setText(qty);
				new AlertDialog.Builder(mContext).setTitle("請輸入數量")
				.setIcon(android.R.drawable.ic_dialog_info).setView(edview)
				.setNegativeButton("取消", null)
				.setPositiveButton("确定", new DialogInterface.OnClickListener() {
					@Override
					public void onClick(DialogInterface dialog, int which) {
						//ed.setText(edview.getText().toString());
						//傳入料號的輸入框,給該框按四合一的格式賦值後獲取焦點,按掃描鍵跳轉到料架框,下同
						String sinput=edview.getText().toString();
						if(!CommonUtil.isNumeric(sinput))
						{
							CommonUtil.WMSShowmsg(mContext, "數量必須爲數字型");
							return;
						}
						String barcode_4in1=pn +"#"+sinput+"##";
						medbarcode.setText(barcode_4in1);
						medbarcode.requestFocus();
					}
					//
				}).show();
				
				/*
				final EditText inputed = new EditText(mContext);
		        AlertDialog.Builder builder = new AlertDialog.Builder(mContext);
		        builder.setTitle("请输入批号").setIcon(android.R.drawable.ic_dialog_info).setView(inputed)
		                .setNegativeButton("取消", null);
		        builder.setPositiveButton("确定", new DialogInterface.OnClickListener() {
		            public void onClick(DialogInterface dialog, int which) {
		              AppData.gbatchno= inputed.getText().toString().trim();
		              //
		              intent.putExtra("val","");//可以帶值回去
		              ((Activity) mContext).setResult(Activity.RESULT_OK, intent);
				      ((Activity) mContext).finish();
		              //
		             }
		        });
		        builder.show();
				*/
				
				/*
				if (position >= 1) {
					String pn = AppData.missuequery.get(position)
							.Getitem_name();
					String qty = AppData.missuequery.get(position)
							.Getbalance_qty();
					String pn_head = pn.substring(0, 1);
					if (pn_head.equals("6") || pn_head.equals("7")) {
						CommonUtil.Inputbox_ret(mContext, "請輸入數量", qty);
					}
				}
				*/
				
				/*
				Intent intent = new Intent(mContext, issue_detail.class);
				if (position >= 1) {
					intent.putExtra("org_id", AppData.missuequery.get(position)
							.Getorg_id());
					intent.putExtra("invoice_no",
							AppData.missuequery.get(position).Getinvoice_no());
					intent.putExtra("wo", AppData.missuequery.get(position)
							.Getwo_no());
					intent.putExtra("wip_entity_id",
							AppData.missuequery.get(position)
									.Getwo_key());
					intent.putExtra("operation_seq_num", AppData.missuequery
							.get(position).Getoperation_seq_num());
					intent.putExtra("reason_name",
							AppData.missuequery.get(position).Getreason_name());
					intent.putExtra("item_name",
							AppData.missuequery.get(position).Getitem_name());
					intent.putExtra("item_id", AppData.missuequery
							.get(position).Getitem_id());
					intent.putExtra("required_qty",
							AppData.missuequery.get(position).Getrequired_qty());
					intent.putExtra("balance_qty",
							AppData.missuequery.get(position).Getbalance_qty());
					intent.putExtra("sub_name",
							AppData.missuequery.get(position).Getsub_name());
					intent.putExtra("locator", AppData.missuequery
							.get(position).Getlocator());
					intent.putExtra("pu", AppData.missuequery.get(position)
							.Getpu());
					intent.putExtra("reason_id",
							AppData.missuequery.get(position).Getreason_id());
					intent.putExtra("dept_code",
							AppData.missuequery.get(position).Getdept_code());
					intent.putExtra("transaction_type_id", AppData.missuequery
							.get(position).Gettransaction_type_id());
					intent.putExtra("suffocate_code",
							AppData.missuequery.get(position)
									.Getsuffocate_code());
					intent.putExtra("remark", AppData.missuequery.get(position)
							.Getremark());
					intent.putExtra("model_name",
							AppData.missuequery.get(position).Getmodel_name());
					intent.putExtra("customer_code",
							AppData.missuequery.get(position)
									.Getcustomer_code());
					intent.putExtra("demand_id",
							AppData.missuequery.get(position).Getdemand_id());
					intent.putExtra("batch_line_id",
							AppData.missuequery.get(position)
									.Getbatch_line_id());
					intent.putExtra("issue_line_id",
							AppData.missuequery.get(position)
									.Getissue_line_id());

					//
					mContext.startActivity(intent);
				}
				*/
			}
		});
		
		
		//
		//int[] colors = { Color.WHITE, Color.rgb(219, 238, 244) };// RGB颜色
		//convertView.setBackgroundColor(colors[position % 2]);// 每隔item之间颜色不同
		//
		return convertView;
	}

	class ViewHolder {
		TextView morg_id;
		TextView minvoice_no;
		TextView mtranstype;
		TextView mwo;
		TextView moperation_seq_num;
		TextView mreason_name;
		TextView mitem_name;
		TextView mrequired_qty;
		TextView mbalance_qty;
		TextView msub_name;
		TextView mlocator;
		TextView mpu;
		TextView mreason_id;
		TextView mdept_code;
		TextView mtransaction_type_id;
		TextView msuffocate_code;
		TextView mremark;
		TextView mmodel_name;
		TextView mcustomer_code;
		TextView mdemand_id;
		TextView mbatch_line_id;
		TextView missue_line_id;
	}
}
