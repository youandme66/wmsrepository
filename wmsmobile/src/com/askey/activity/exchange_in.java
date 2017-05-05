package com.askey.activity;

import org.json.JSONObject;

import android.content.DialogInterface;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.ListView;

import com.askey.Adapter.Exchange_in_Adapter;
import com.askey.Adapter.GetInvoiceHeadHelper;
import com.askey.model.AppData;
import com.askey.model.BaseActivity;
import com.askey.model.CommonUtil;
import com.askey.model.K_table;
import com.askey.model.Texchangequery;
import com.askey.wms.R;

public class exchange_in extends BaseActivity implements OnClickListener {
	Texchangequery founditem = new Texchangequery();
	private String inneedframe = "";
	private String pn_5in1;
	private ImageView ivRefresh;
	private ListView lvDisplay;
	private ImageView ivGohome;
	private EditText edtexchange_invoicenosearch, edtbarcode, edtframein, edtlog;
	private Exchange_in_Adapter mAdapter;

	protected void onCreate(Bundle savedInstanceState) {
		if (AppData.orgid.equals("")) {
			CommonUtil.WMSToast(exchange_in.this, "請先選擇ORG");
			finish();
		}
		super.onCreate(savedInstanceState);
		setContentView(R.layout.exchange_in);

		ivGohome = (ImageView) this.findViewById(R.id.exchange_in_gohome);
		ivGohome.setOnClickListener(this);
		ivRefresh = (ImageView) this.findViewById(R.id.exchange_in_refresh);
		ivRefresh.setOnClickListener(this);
		lvDisplay = (ListView) findViewById(R.id.exchange_in_display);
		edtexchange_invoicenosearch = (EditText) findViewById(R.id.exchange_in_invoicenosearch);
		edtbarcode = (EditText) findViewById(R.id.exchange_in_barcode);
		edtframein = (EditText) findViewById(R.id.exchange_in_frame_in);
		edtlog = (EditText) findViewById(R.id.exchange_in_log);
		//
		mAdapter = new Exchange_in_Adapter(this);
		//
//		if (!AppData.orgid.equals("")) {
//			GetInvoicHead();
//		}
		//
		edtexchange_invoicenosearch
		.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
			@Override
			public void onFocusChange(View v, boolean hasFocus) {
				if (hasFocus) {
					BaseActivity.mCommReception = edtexchange_invoicenosearch;
					btnOpenFlag = true;
					Open("/dev/ttyHS1", 9600);
				} else {
					btnOpenFlag = false;
					Close();
				}
			}

		});
		edtbarcode
				.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
					@Override
					public void onFocusChange(View v, boolean hasFocus) {
						if (hasFocus) {
							BaseActivity.mCommReception = edtbarcode;
							btnOpenFlag = true;
							Open("/dev/ttyHS1", 9600);
						} else {
							btnOpenFlag = false;
							Close();
						}
					}

				});
		//
		edtframein
				.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
					@Override
					public void onFocusChange(View v, boolean hasFocus) {
						if (hasFocus) {
							BaseActivity.mCommReception = edtframein;
							btnOpenFlag = true;
							Open("/dev/ttyHS1", 9600);
						} else {
							btnOpenFlag = false;
							Close();
						}
					}

				});
		registerReadgoodReceiver();
	}

	public void trimandupperedt() {
		CommonUtil.trimandupper(edtexchange_invoicenosearch);
		CommonUtil.trimandupper(edtbarcode);
		CommonUtil.trimandupper(edtframein);
	}

	public boolean checkinput() {
		trimandupperedt();
		//
		if(edtbarcode.getText().toString().indexOf("#")<=0){
			pn_5in1=edtbarcode.getText().toString();
			return true;
		}
		//
		pn_5in1 = "";
		//
		String barcode = edtbarcode.getText().toString();
		if (!CommonUtil.Split5in1(exchange_in.this, barcode)) {
			return false;
		}
		//
		pn_5in1=AppData.pn_5in1;
		edtbarcode.setText(pn_5in1);
		//
		return true;
	}

	@Override
	public boolean dispatchKeyEvent(KeyEvent event) {
		if (event.getAction() == KeyEvent.ACTION_DOWN) {
			Log.d("BarcodeActivity",
					"BarcodeActivity keycode = " + event.getKeyCode());
			//
			edtlog.setText("");
			//
			switch (event.getKeyCode()) {
			// case KeyEvent.KEYCODE_BARCODE_SCAN:
			case 223:
				if (btnOpenFlag == false)
					return true;
				if (btnScanFlag == false) {
					btnScanFlag = true;
					Scan(false);
				}
				return true;
			}
		} else if (event.getAction() == KeyEvent.ACTION_UP) {
			switch (event.getKeyCode()) {
			// case KeyEvent.KEYCODE_BARCODE_SCAN:
			case 223:
				if (btnOpenFlag == false)
					return true;
				btnScanFlag = false;
				StopScan();
				//
				trimandupperedt();
				//
				if (edtbarcode.hasFocus()) {
					if (edtbarcode.getText().toString().equals("")) {
						break;
					}
					//
					inneedframe="";
					//
					if (!checkinput()) {
						CommonUtil.errorsound();
						break;
					}
					//
					boolean isfound = false;
					for (int i = 1; i <= AppData.mexchangequery.size() - 1; i++) {
						Texchangequery item = new Texchangequery();
						//
						item = ((Texchangequery) AppData.mexchangequery.get(i));
						if (item.Getitem_name().equals(pn_5in1)) {
							isfound = true;
							founditem = item;
							//
							break;
						}
					}
					if (!isfound) {
						founditem = null;
						CommonUtil.WMSToast(exchange_in.this, "未找到料號[" + pn_5in1
								+ "]");
						CommonUtil.errorsound();
						break;
					}
					inneedframe = founditem.getInneedframe();
					//
					if (inneedframe.equals("N")){
						if (!checkinput()) {
							CommonUtil.errorsound();
							break;
						}
						exchangetrans();
						break;
					}
					else{
						edtframein.requestFocus();
						break;
					}
				}
				//
				if (edtframein.hasFocus()) {
					if (inneedframe.equals("Y")) {
						if (!checkinput()) {
							CommonUtil.errorsound();
							break;
						}
						exchangetrans();
						break;
					} else if (inneedframe.equals("N")) {
						CommonUtil.WMSToast(exchange_in.this, "不需要刷入調入料架");
						CommonUtil.errorsound();
						break;
					} else {
						CommonUtil.WMSToast(exchange_in.this, "無法判斷是否需要刷入調入料架");
						CommonUtil.errorsound();
						break;
					}
				}
			}
			//
			return true;
		}
		//
		return super.dispatchKeyEvent(event);
	}

	public void cleardone() {
		edtbarcode.setText("");
		edtframein.setText("");
		//
		edtbarcode.requestFocus();
	}

	public void onClick(View v) {
		CommonUtil.trimandupper(edtexchange_invoicenosearch);
		//
		switch (v.getId()) {
		case R.id.exchange_in_gohome: {
			finish();
			break;
		}
		case R.id.exchange_in_refresh: {
			ExchangeQuery();
			edtbarcode.requestFocus();
			break;
		}
		}
	}

	public void ExchangeQuery() {
		new ExchangeQueryAsyncTask().execute();
	}

	class ExchangeQueryAsyncTask extends AsyncTask<Void, Void, Boolean> {
		@Override
		protected void onPreExecute() {
			super.onPreExecute();
			showProgressDialog("", "加載中...",
					new DialogInterface.OnCancelListener() {
						public void onCancel(DialogInterface dialog) {
						}
					});
		}

		@Override
		protected void onPostExecute(Boolean result) {
			super.onPostExecute(result);
			mAlertDialog.dismiss();
			//
			lvDisplay.setAdapter(mAdapter);// 顯示資料
			CommonUtil.feedbackqueryres(exchange_in.this, result);

		}

		@Override
		protected Boolean doInBackground(Void... params) {
			try {
				String strResponse = Exchange_in_Adapter.Request_in(exchange_in.this,
						edtexchange_invoicenosearch.getText().toString());
				Exchange_in_Adapter.Resolve(strResponse);
				return true;
			} catch (Exception e) {
				return false;
			}

		}
	}

	//
	// get invoice head
	public void GetInvoicHead() {
		new GetInvoicHeadAsyncTask().execute();
	}

	// 参数1：传入doInBackground的参数类型
	// 参数2：传入onProgressUpdate的参数类型
	// 参数3：传入onPostExecute的参数类型，也是doInBackground的返回类型
	class GetInvoicHeadAsyncTask extends AsyncTask<Void, Void, Boolean> {
		@Override
		protected void onPreExecute() {
			super.onPreExecute();
			showProgressDialog("", "加载中...",
					new DialogInterface.OnCancelListener() {
						public void onCancel(DialogInterface dialog) {
						}
					});
		}

		@Override
		protected void onPostExecute(Boolean result) {
			super.onPostExecute(result);
			mAlertDialog.dismiss();
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			try {
				String strResponse = GetInvoiceHeadHelper.Request(
						exchange_in.this, "EXCHANGE_INVOICE", AppData.orgcode);
				GetInvoiceHeadHelper.Resolve(strResponse);
				edtexchange_invoicenosearch.setText(AppData.ginvoice);
				return true;
			} catch (Exception e) {
				return false;
			}
		}
	}

	// 用線程執行調撥交易
	public void exchangetrans() {
		new exchangetransAsyncTask().execute();
	}

	class exchangetransAsyncTask extends AsyncTask<Void, Void, Boolean> {
		@Override
		protected void onPreExecute() {
			super.onPreExecute();
			showProgressDialog("", "執行中...",
					new DialogInterface.OnCancelListener() {
						public void onCancel(DialogInterface dialog) {
						}
					});
		}

		@Override
		protected void onPostExecute(Boolean result) {
			super.onPostExecute(result);
			mAlertDialog.dismiss();
			//
			edtlog.setText("");
			edtlog.append("需求量[" + founditem.Getrequired_qty() + "]" + "\n");
			edtlog.append("剩餘量[" + founditem.Getbalance_qty() + "]" + "\n");
			edtlog.append("料號[" + pn_5in1 + "]" + "\n");
			//
			if (K_table.excuteres.equals("0")) {
				edtlog.append("數據處理成功");
				ExchangeQuery();
				//
				cleardone();
			} else {
				edtlog.append("數據處理失敗");
			}
			//
			CommonUtil.feedbackexcres(exchange_in.this, K_table.excuteres);
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			String strResponse = "";
			try {
				strResponse = Exchange_in_Adapter.Request_in(exchange_in.this,
						edtexchange_invoicenosearch.getText().toString());
				Exchange_in_Adapter.Resolve(strResponse);
				boolean isfound = false;
				for (int i = 1; i <= AppData.mexchangequery.size() - 1; i++) {
					Texchangequery item = new Texchangequery();
					//
					item = ((Texchangequery) AppData.mexchangequery.get(i));
					if (item.Getitem_name().equals(pn_5in1)) {
						isfound = true;
						founditem = item;
						//
						strResponse = Exchange_in_Adapter.CommitExchangeTrans_In(
								exchange_in.this,
								founditem.Getexchange_line_id(),
								edtexchange_invoicenosearch.getText().toString(),
								pn_5in1, 
								founditem.Getitem_id(), 
								founditem.Getout_sub_name(),
								founditem.Getout_locator_name(), 
								founditem.Getout_org_id(), 
								founditem.getOut_frame_name(),
								founditem.Getin_sub_name(),
								founditem.Getin_locator_name(), 
								founditem.Getin_org_id(), 
								edtframein.getText().toString(), 
								founditem.Getmodel_name(), 
								founditem.Getduty_dept_code(),
								founditem.Getscrap_type(),
								founditem.Getmodel_type(),
								founditem.Getreason_code(),
								founditem.Getwo_no(),
								founditem.Getscrap_line_id(), 
								founditem.Getremark(),
								founditem.Getcustomer_code(),
								founditem.Getdemand_id(),
								founditem.Getsuffocate_code(), 
								founditem.Gettransaction_type_id(), 
								founditem.Getcharge_no(), 
								AppData.user_id, 
								founditem.getOut_qty(),
								founditem.getOut_dc(),
								founditem.getOperation_line_id(),
								founditem.getOut_mtl_io_id());
						JSONObject jsobj = new JSONObject(strResponse);
						String res = jsobj.getString("result");
						if (!res.equals("0")) {
							K_table.excuteres = res;
							CommonUtil.errorsound();
							return false;
						}
					}
				}
				//
				if (!isfound) {
					founditem = null;
					K_table.excuteres = "未找到料號[" + pn_5in1 + "]";
					CommonUtil.errorsound();
					return false;
				}
				//
				JSONObject jsobj = new JSONObject(strResponse);
				String res = jsobj.getString("result");
				K_table.excuteres = res;
				if (res.equals("0")) {
					return true;
				} else {
					CommonUtil.errorsound();
					return false;
				}
			} catch (Exception e) {
				CommonUtil.errorsound();
				K_table.excuteres = e.getMessage().toString();
				return false;
			}
		}
	}

}
