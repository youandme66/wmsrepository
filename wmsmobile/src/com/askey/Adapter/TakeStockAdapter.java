package com.askey.Adapter;

import org.json.JSONArray;
import org.json.JSONObject;

import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.askey.model.AppData;
import com.askey.model.CustomHttpClient;
import com.askey.model.K_table;
import com.askey.model.Ttakestockquery;
import com.askey.wms.R;

public class TakeStockAdapter extends BaseAdapter {
	private Context mContext;

	public TakeStockAdapter(Context context) {
		mContext = context;
	}

	public int getCount() {
		return AppData.mtakestockdetail.size();
	}

	public Object getItem(int position) {
		return null;
	}

	public long getItemId(int position) {
		return 0;
	}

	public static String Request_getallframe(Context context, String orgcode,
			String subinv, String pn) {
		// private static final CommonLog log = LogFactory.createLog();
		try {
			String strURL = String.format(K_table.getallframe, orgcode, subinv,
					pn).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}

	public static void Resolve_getallframe(String Response) {
		try {
			AppData.getallframe = "";
			JSONObject json = new JSONObject(Response);
			JSONArray array = json.getJSONArray("result");
			//
			if (array.length() > 0) {
				for (int i = 0; i < array.length(); i++) {
					AppData.getallframe = AppData.getallframe
							+ array.getJSONObject(i).getString("FRAME_NAME")
							+ ",";// 字段必須是大寫
				}
				AppData.getallframe = AppData.getallframe.substring(0,
						AppData.getallframe.length() - 1);
			} else {
				AppData.getallframe = "";
			}
		} catch (Exception e) {
			Log.d("tag", e.getMessage());
			AppData.getallframe = e.getMessage();
		}
	}

	public static void Resolve_getAframe(String Response) {
		try {
			AppData.getAframe = "";
			JSONObject json = new JSONObject(Response);
			JSONArray array = json.getJSONArray("result");
			//
			if (array.length() > 0) {
				AppData.getAframe = array.getJSONObject(0).getString(
						"FRAME_NAME");// 字段必須是大寫
			} else {
				AppData.getAframe = "";
			}
		} catch (Exception e) {
			Log.d("tag", e.getMessage());
			AppData.getAframe = e.getMessage();
		}
	}

	public static String Request_getonhandqty(Context context, String orgid,
			String subinv, String pn) {
		// private static final CommonLog log = LogFactory.createLog();
		try {
			String strURL = String.format(K_table.getonhandqty, orgid, subinv,
					pn).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}

	public static void Resolve_getonhandqty(String Response) {
		try {
			JSONObject json = new JSONObject(Response);
			JSONArray array = json.getJSONArray("result");
			//
			if (array.length() > 0) {
				AppData.getonhandqty = array.getJSONObject(0).getString(
						"ONHANDQTY");// 字段必須是大寫
			} else {
				AppData.getonhandqty = "0";
			}
		} catch (Exception e) {
			Log.d("tag", e.getMessage());
			AppData.getonhandqty = e.getMessage();
		}
	}

	public static String Request_inserttakestockdata(Context context,
			String orgid, String pn, String subinv,String onhandqty,String scanedqty,String create_by) {
		try {
			String strURL = String.format(K_table.inserttakestockdata, orgid,
					pn, subinv,onhandqty,scanedqty, create_by).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}
	
	public static String Request_querypickingupqty(Context context, String orgid,
			String pn,String subinv) {
		try {
			String strURL = String.format(K_table.GetPickingupQty, orgid,
					pn,subinv).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}

	public static String Request_querytakestock(Context context, String orgid,
			String subinv, String pn, String querytype,String totalqty) {
		// private static final CommonLog log = LogFactory.createLog();
		try {
			String strURL = String.format(K_table.gettakestockdetail, orgid,
					subinv, pn, querytype,totalqty).replaceAll(" ", "%20");
			String strData = CustomHttpClient.getFromWebByHttpClient(context,
					strURL);
			return strData;
		} catch (Exception e) {
			return "{\"result\":\"" + e.getMessage().toString() + "\"}";
		}
	}
	
	public static void Resolve_querytakestock(String Response) {
		try {
			AppData.mtakestockdetail.clear();
			JSONObject json = new JSONObject(Response);
			JSONArray array = json.getJSONArray("result");// 把jason結果解析成數組
			Ttakestockquery item = new Ttakestockquery();
			// 增加表頭
			item.setINVOICE_NO("INVOICE");
			item.setTXN_FLAG("FLAG");// IN/OUT
			item.setTXN_QTY("TXNQTY");
			item.setTXN_TIME("TIME");
			item.setONHANDQTY("ONHANDQTY");
			item.setUSER_NAME("USER");
			item.setTXN_TYPE("TXNDESC");// transaction id對應的描述
			//
			AppData.mtakestockdetail.add(item);
			//
			for (int i = 0; i < array.length(); i++) {
				item = new Ttakestockquery();
				// 字段必須是大寫
				item.setINVOICE_NO(array.getJSONObject(i).getString(
						"INVOICE_NO"));
				item.setTXN_FLAG(array.getJSONObject(i).getString("TXN_FLAG"));
				item.setTXN_QTY(array.getJSONObject(i).getString("TXN_QTY"));
				item.setTXN_TIME(array.getJSONObject(i).getString("TXN_TIME"));
				item.setONHANDQTY(array.getJSONObject(i).getString("ONHANDQTY"));
				item.setUSER_NAME(array.getJSONObject(i).getString("USER_NAME"));
				item.setTXN_TYPE(array.getJSONObject(i).getString("TXN_TYPE"));
				//
				AppData.mtakestockdetail.add(item);
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
					R.layout.takestockitem, null);
			//
			holder = new ViewHolder();
			holder.mINVOICE_NO = (TextView) convertView
					.findViewById(R.id.takestock_INVOICENO);
			holder.mTXN_FLAG = (TextView) convertView
					.findViewById(R.id.takestock_TXN_FLAG);
			holder.mTXN_QTY = (TextView) convertView
					.findViewById(R.id.takestock_TXN_QTY);
			holder.mTXN_TIME = (TextView) convertView
					.findViewById(R.id.takestock_TXN_TIME);
			holder.mONHANDQTY = (TextView) convertView
					.findViewById(R.id.takestock_ONHANDQTY);
			holder.mUSER_NAME = (TextView) convertView
					.findViewById(R.id.takestock_USER_NAME);
			holder.mTXN_TYPE = (TextView) convertView
					.findViewById(R.id.takestock_TXN_TYPE);
			//
			convertView.setTag(holder);
		} else {
			holder = (ViewHolder) convertView.getTag();
		}
		//
		holder.mINVOICE_NO
				.setWidth((int) (AppData.screenwidth * 0.35));
		holder.mTXN_TIME
				.setWidth((int) (AppData.screenwidth * AppData.colwidth.ctime));
		holder.mTXN_FLAG
				.setWidth((int) (AppData.screenwidth * AppData.colwidth.ctxnflag));
		holder.mTXN_QTY
				.setWidth((int) (AppData.screenwidth * AppData.colwidth.cqty));
		holder.mONHANDQTY
				.setWidth((int) (AppData.screenwidth * AppData.colwidth.cqty));
		holder.mUSER_NAME
				.setWidth((int) (AppData.screenwidth * AppData.colwidth.cusername));
		holder.mTXN_TYPE
				.setWidth((int) (AppData.screenwidth * AppData.colwidth.ctxntype));
		//
		holder.mINVOICE_NO.setText(AppData.mtakestockdetail.get(position)
				.getINVOICE_NO());
		holder.mTXN_FLAG.setText(AppData.mtakestockdetail.get(position)
				.getTXN_FLAG());
		holder.mTXN_QTY.setText(AppData.mtakestockdetail.get(position)
				.getTXN_QTY());
		holder.mTXN_TIME.setText(AppData.mtakestockdetail.get(position)
				.getTXN_TIME());
		holder.mTXN_TYPE.setText(AppData.mtakestockdetail.get(position)
				.getTXN_TYPE());
		holder.mONHANDQTY.setText(AppData.mtakestockdetail.get(position)
				.getONHANDQTY());
		holder.mUSER_NAME.setText(AppData.mtakestockdetail.get(position)
				.getUSER_NAME());
		//
		convertView.setOnClickListener(new OnClickListener() {
			public void onClick(View v) {
			}
		});
		//
		// int[] colors = { Color.WHITE, Color.rgb(219, 238, 244) };// RGB颜色
		// convertView.setBackgroundColor(colors[position % 2]);// 每隔item之间颜色不同
		//
		return convertView;
	}

	class ViewHolder {
		TextView mINVOICE_NO;
		TextView mTXN_FLAG;
		TextView mTXN_QTY;
		TextView mTXN_TIME;
		TextView mONHANDQTY;
		TextView monhand_qty;
		TextView mUSER_NAME;
		TextView mTXN_TYPE;

	}
}
