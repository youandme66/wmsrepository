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

import com.askey.Adapter.ExchangeAdapter;
import com.askey.Adapter.GetInvoiceHeadHelper;
import com.askey.model.AppData;
import com.askey.model.BaseActivity;
import com.askey.model.CommonUtil;
import com.askey.model.K_table;
import com.askey.model.T6in1;
import com.askey.model.Texchangequery;
import com.askey.wms.R;

public class exchange extends BaseActivity implements OnClickListener {
	Texchangequery founditem = new Texchangequery();
	private String outneedframe = "";
	private String inneedframe = "";
	private String pn_5in1, dc_5in1, qty_5in1,boxno_5in1;
	private ImageView ivRefresh;
	private ListView lvDisplay;
	private ImageView ivGohome;
	private EditText edtexchange_invoicenosearch, edtbarcode, edtframeout,
			edtframein, edtlog;

	private ExchangeAdapter mAdapter;

	protected void onCreate(Bundle savedInstanceState) {
		if (AppData.orgid.equals("")) {
			CommonUtil.WMSToast(exchange.this, "請先選擇ORG");
			finish();
		}
		super.onCreate(savedInstanceState);
		setContentView(R.layout.exchange);

		ivGohome = (ImageView) this.findViewById(R.id.exchange_gohome);
		ivGohome.setOnClickListener(this);
		ivRefresh = (ImageView) this.findViewById(R.id.exchange_refresh);
		lvDisplay = (ListView) findViewById(R.id.exchange_display);
		ivRefresh.setOnClickListener(this);
		edtexchange_invoicenosearch = (EditText) findViewById(R.id.exchange_invoicenosearch);
		edtbarcode = (EditText) findViewById(R.id.exchange_barcode);
		edtframeout = (EditText) findViewById(R.id.exchange_frame_out);
		edtframein = (EditText) findViewById(R.id.exchange_frame_in);
		edtlog = (EditText) findViewById(R.id.exchange_log);
		//
		mAdapter = new ExchangeAdapter(this);
		//
		AppData.m6in1.clear();
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
					// Log.d("BarcodeActivity_2", "3");
					// Log.d("BarcodeActivity_2",
					// "BarcodeActivity OPEN");
					btnOpenFlag = true;
					Open("/dev/ttyHS1", 9600);
				} else {
					btnOpenFlag = false;
					Close();
				}
			}

		});
		//
		edtbarcode
				.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
					@Override
					public void onFocusChange(View v, boolean hasFocus) {
						if (hasFocus) {
							BaseActivity.mCommReception = edtbarcode;
							// Log.d("BarcodeActivity_2", "3");
							// Log.d("BarcodeActivity_2",
							// "BarcodeActivity OPEN");
							btnOpenFlag = true;
							Open("/dev/ttyHS1", 9600);
						} else {
							btnOpenFlag = false;
							Close();
						}
					}

				});
		edtframeout
				.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
					@Override
					public void onFocusChange(View v, boolean hasFocus) {
						if (hasFocus) {
							BaseActivity.mCommReception = edtframeout;
							// Log.d("BarcodeActivity_2", "3");
							// Log.d("BarcodeActivity_2",
							// "BarcodeActivity OPEN");
							btnOpenFlag = true;
							Open("/dev/ttyHS1", 9600);
						} else {
							btnOpenFlag = false;
							Close();
						}
					}

				});
		edtframein
				.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
					@Override
					public void onFocusChange(View v, boolean hasFocus) {
						if (hasFocus) {
							BaseActivity.mCommReception = edtframein;
							// Log.d("BarcodeActivity_2", "3");
							// Log.d("BarcodeActivity_2",
							// "BarcodeActivity OPEN");
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
		CommonUtil.trimandupper(edtframeout);
		CommonUtil.trimandupper(edtframein);
	}

	public boolean checkinput() {
		trimandupperedt();
		//
		pn_5in1 = "";
		dc_5in1 = "";
		qty_5in1 = "";
		//
		String barcode = edtbarcode.getText().toString();
		if (!CommonUtil.Split5in1(exchange.this, barcode)) {
			return false;
		}
		pn_5in1 = AppData.pn_5in1;
		dc_5in1 = AppData.dc_5in1;
		qty_5in1 = AppData.qty_5in1;
		boxno_5in1=AppData.reel_5in1;
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
					edtframeout.setText("");
					edtframein.setText("");
					//
					outneedframe="";
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
						CommonUtil.WMSToast(exchange.this, "未找到料號[" + pn_5in1
								+ "]");
						CommonUtil.errorsound();
						break;
					}
					//
					if (!boxno_5in1.equals("")) {
						boolean reelidDup = false;
						for (int i = 1; i <= AppData.m6in1.size() - 1; i++) {
							T6in1 A6in1 = (T6in1) (AppData.m6in1.get(i));
							if (A6in1.getBoxno().equals(boxno_5in1)) {
								reelidDup = true;
								//
								break;
							}
						}// for end
						if (reelidDup) {
							K_table.excuteres = "Reel id[" + boxno_5in1 + "]重複";
							CommonUtil.errorsound();
							return false;
						}
					}
					//
					outneedframe = founditem.getOutneedframe();
					inneedframe = founditem.getInneedframe();
					//
					if (outneedframe.equals("Y")) {
						edtframeout.requestFocus();
						edtframeout.setText(founditem.getAframe());
						break;
					} else if (outneedframe.equals("N")) {
						edtframein.requestFocus();
						break;
					} else {
						CommonUtil.WMSToast(exchange.this, "無法判斷是否需要刷入調出料架");
						CommonUtil.errorsound();
						break;
					}
				}
				//
				if (edtframeout.hasFocus()) {
					if (inneedframe.equals("Y")) {
						edtframein.requestFocus();
						break;
					} else if (inneedframe.equals("N")) {
						if (!checkinput()) {
							CommonUtil.errorsound();
							break;
						}
						exchangetrans();
					} else {
						CommonUtil.WMSToast(exchange.this, "無法判斷是否需要刷入調入料架");
						CommonUtil.errorsound();
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
						CommonUtil.WMSToast(exchange.this, "不需要刷入調入料架");
						CommonUtil.errorsound();
						break;
					} else {
						CommonUtil.WMSToast(exchange.this, "無法判斷是否需要刷入調入料架");
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
		edtframeout.setText("");
		edtframein.setText("");
		//
		edtbarcode.requestFocus();
	}

	public void onClick(View v) {
		CommonUtil.trimandupper(edtexchange_invoicenosearch);
		//
		switch (v.getId()) {
		case R.id.exchange_gohome: {
			finish();
			break;
		}
		case R.id.exchange_refresh: {
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
			CommonUtil.feedbackqueryres(exchange.this, result);
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			try {
				String strResponse = ExchangeAdapter.Request(exchange.this,
						edtexchange_invoicenosearch.getText().toString());
				ExchangeAdapter.Resolve(strResponse);
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
						exchange.this, "EXCHANGE_INVOICE", AppData.orgcode);
				GetInvoiceHeadHelper.Resolve(strResponse);
				edtexchange_invoicenosearch.setText(AppData.ginvoice);
				// exchange_invoicenosearch.setText("3BQ110410021");
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
			edtlog.append("數量[" + qty_5in1 + "]" + "\n");
			edtlog.append("D/C[" + dc_5in1 + "]\n");
			//
			if (K_table.excuteres.equals("0")) {
				edtlog.append("數據處理成功");
				//
				if(!boxno_5in1.equals(""))
				{
					T6in1 A6in1 = new T6in1();
					A6in1.setBoxno(boxno_5in1);
					AppData.m6in1.add(A6in1);
				}
				ExchangeQuery();
				//
				cleardone();
			} else {
				edtlog.append("數據處理失敗");
			}
			//
			CommonUtil.feedbackexcres(exchange.this, K_table.excuteres);
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			String strResponse = "";
			try {
				strResponse = ExchangeAdapter.Request(exchange.this,
						edtexchange_invoicenosearch.getText().toString());
				ExchangeAdapter.Resolve(strResponse);
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
				//
				if (!isfound) {
					founditem = null;
					K_table.excuteres = "未找到料號[" + pn_5in1 + "]";
					CommonUtil.errorsound();
					return false;
				}
				//
				strResponse = ExchangeAdapter.CommitExchangeTrans(
						exchange.this, founditem.Getexchange_header_id(),
						founditem.Getexchange_line_id(),
						edtexchange_invoicenosearch.getText().toString(),
						pn_5in1, founditem.Getitem_id(), founditem
								.Getrequired_qty(), founditem.Getbalance_qty(),
						founditem.Getout_sub_name(), founditem
								.Getout_locator_name(), founditem
								.Getout_org_id(), founditem.Getin_sub_name(),
						founditem.Getin_locator_name(), founditem
								.Getin_org_id(), edtframein.getText()
								.toString(), edtframeout.getText().toString(),
						founditem.Getmodel_name(), founditem
								.Getduty_dept_code(),
						founditem.Getscrap_type(), founditem.Getmodel_type(),
						founditem.Getreason_code(), founditem.Getwo_no(),
						founditem.Getscrap_line_id(), founditem.Getremark(),
						founditem.Getcustomer_code(), founditem.Getdemand_id(),
						founditem.Getsuffocate_code(), founditem
								.Gettransaction_type_id(), founditem
								.Getcharge_no(), AppData.user_id, qty_5in1,
						dc_5in1);
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
