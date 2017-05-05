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
import android.widget.TextView;

import com.askey.Adapter.GetInvoiceHeadHelper;
import com.askey.Adapter.ReturnAdapter;
import com.askey.model.AppData;
import com.askey.model.BaseActivity;
import com.askey.model.CommonUtil;
import com.askey.model.K_table;
import com.askey.model.T6in1;
import com.askey.model.Treturnquery;
import com.askey.wms.R;

public class Return extends BaseActivity implements OnClickListener {
	Treturnquery founditem = new Treturnquery();
	//
	private String pn_5in1, dc_5in1, qty_5in1,boxno_5in1;
	private ImageView ivRefresh;
	private ListView lvDisplay;
	private ImageView ivGohome;
	private TextView tvlastframe;
	private EditText edtreturn_invoicenosearch, edtbarcode, edtframe, edtlog;

	private ReturnAdapter mAdapter;

	protected void onCreate(Bundle savedInstanceState) {

		super.onCreate(savedInstanceState);
		if (AppData.orgid.equals("")) {
			CommonUtil.WMSToast(Return.this, "請先選擇ORG");
			finish();
		}
		setContentView(R.layout.returnmtl);
		//
		ivGohome = (ImageView) this.findViewById(R.id.return_gohome);
		ivGohome.setOnClickListener(this);
		ivRefresh = (ImageView) this.findViewById(R.id.return_refresh);
		ivRefresh.setOnClickListener(this);
		lvDisplay = (ListView) findViewById(R.id.return_display);
		edtreturn_invoicenosearch = (EditText) findViewById(R.id.return_invoicenosearch);
		edtbarcode = (EditText) findViewById(R.id.ret_barcode);
		tvlastframe = (TextView) findViewById(R.id.ret_lastframe);
		edtframe = (EditText) findViewById(R.id.ret_frame);
		edtlog = (EditText) findViewById(R.id.ret_log);
		//
		mAdapter = new ReturnAdapter(this);
		//
		AppData.m6in1.clear();
		//
//		if (!AppData.orgcode.equals("")) {
//			GetInvoicHead();
//		}
		//
		edtreturn_invoicenosearch.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
			@Override
			public void onFocusChange(View v, boolean hasFocus) {
				if (hasFocus) {
					BaseActivity.mCommReception = edtreturn_invoicenosearch;
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
		edtframe.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
			@Override
			public void onFocusChange(View v, boolean hasFocus) {
				if (hasFocus) {
					BaseActivity.mCommReception = edtframe;
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
		registerReadgoodReceiver();
	}

	public void trimandupperedt() {
		CommonUtil.trimandupper(edtframe);
		CommonUtil.trimandupper(edtreturn_invoicenosearch);
		CommonUtil.trimandupper(edtbarcode);
	}

	public boolean checkinput() {
		trimandupperedt();
		//
		pn_5in1 = "";
		dc_5in1 = "";
		qty_5in1 = "";
		//
		String barcode = edtbarcode.getText().toString();
		if (!CommonUtil.Split5in1(Return.this, barcode)) {
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
					if (!checkinput()) {
						CommonUtil.errorsound();
						break;
					}
					//
					boolean isfound = false;
					for (int i = 1; i <= AppData.mreturnquery.size() - 1; i++) {
						Treturnquery item = new Treturnquery();
						//
						item = ((Treturnquery) AppData.mreturnquery.get(i));
						if (item.Getitem_name().equals(pn_5in1)) {
							isfound = true;
							founditem = item;
							//
							break;
						}
					}
					if (!isfound) {
						CommonUtil.WMSToast(Return.this, "未找到料號[" + pn_5in1
								+ "]");
						CommonUtil.errorsound();
						break;
					}
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
					tvlastframe.setText("提示料架["+founditem.getLastframe()+"]");
					//
					edtframe.requestFocus();
					break;

				}
				if (edtframe.hasFocus()) {
					if (edtframe.getText().toString().equals("")) {
						break;
					}
					if (!checkinput()) {
						CommonUtil.errorsound();
						break;
					}
					returntrans();
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
		edtframe.setText("");
		tvlastframe.setText("");
		//
		edtbarcode.requestFocus();
	}

	public void onClick(View v) {
		CommonUtil.trimandupper(edtreturn_invoicenosearch);
		//
		switch (v.getId()) {
		case R.id.return_gohome: {
			finish();
			break;
		}
		case R.id.return_refresh: {
			ReturnQuery();
			edtbarcode.requestFocus();
			break;
		}
		}
	}

	// 根據退料單查詢明細
	public void ReturnQuery() {
		new ReturnQueryAsyncTask().execute();
	}

	class ReturnQueryAsyncTask extends AsyncTask<Void, Void, Boolean> {
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
			CommonUtil.feedbackqueryres(Return.this, result);

		}

		@Override
		protected Boolean doInBackground(Void... params) {
			try {
				String strResponse = ReturnAdapter.Request(Return.this,
						edtreturn_invoicenosearch.getText().toString());
				ReturnAdapter.Resolve(strResponse);
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
				String strResponse = GetInvoiceHeadHelper.Request(Return.this,
						"RETURN_INVOICE", AppData.orgcode);
				GetInvoiceHeadHelper.Resolve(strResponse);
				edtreturn_invoicenosearch.setText(AppData.ginvoice);
				return true;
			} catch (Exception e) {
				return false;
			}
		}
	}

	// 執行退料交易
	public void returntrans() {
		new returntransAsyncTask().execute();
	}

	class returntransAsyncTask extends AsyncTask<Void, Void, Boolean> {
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
				//
				ReturnQuery();
				//
				cleardone();
			} else {
				edtlog.append("數據處理失敗");
				edtframe.setSelection(edtframe.getText().toString().length());
			}
			//
			CommonUtil.feedbackexcres(Return.this, K_table.excuteres);
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			String strResponse = "";
			try {
				strResponse = ReturnAdapter.Request(Return.this,
						edtreturn_invoicenosearch.getText().toString());
				ReturnAdapter.Resolve(strResponse);
				//
				boolean isfound = false;
				for (int i = 1; i <= AppData.mreturnquery.size() - 1; i++) {
					Treturnquery item = new Treturnquery();
					//
					item = ((Treturnquery) AppData.mreturnquery.get(i));
					if (item.Getitem_name().equals(pn_5in1)) {
						isfound = true;
						founditem = item;
						//
						break;
					}
				}
				//
				if (!isfound) {
					K_table.excuteres="未找到料號[" + pn_5in1+ "]";
					CommonUtil.errorsound();
					return false;
				}
				//
				strResponse = ReturnAdapter.CommitReturnTrans(Return.this,
						founditem.Getorg_id(), edtreturn_invoicenosearch
								.getText().toString(), founditem.Getwo_no(),
						founditem.Getwo_key(), founditem.Getroute(), founditem
								.Getreason_code(), pn_5in1, founditem
								.Getitem_id(), founditem.Getrequired_qty(),
						founditem.Getbalance_qty(), founditem.Getsub_name(),
						founditem.Getlocator(), edtframe.getText().toString(),
						dc_5in1, qty_5in1, founditem.Getpu(), founditem
								.Getreason_id(), founditem.Getdept_code(),
						founditem.Getremark(), founditem.Getbatch_line_id(),
						AppData.user_id, founditem.getTransaction_type_id(),
						founditem.Getreturn_line_id(), founditem
								.Getsuffocate_code());
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
