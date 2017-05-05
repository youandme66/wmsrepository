package com.askey.activity;

import org.json.JSONObject;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.WindowManager;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.TextView;

import com.askey.Adapter.Exchange_in_Adapter;
import com.askey.Adapter.TakeStockAdapter;
import com.askey.model.AppData;
import com.askey.model.BaseActivity;
import com.askey.model.CommonUtil;
import com.askey.model.K_table;
import com.askey.model.Texchangequery;
import com.askey.wms.R;

public class takeStock extends BaseActivity implements OnClickListener {
	//Texchangequery founditem = new Texchangequery();
	private String pn_5in1 = "", pn_pandian = "", PickingupQty = "";
	private String querytype = "";
	private ImageView ivRefresh;
	private Button btnInsert, btnClear;
	private ListView lvDisplay;
	private ImageView ivGohome;
	private EditText edtpn, edtsubname, edtakestock_barcodeplus, edscanedqty;
	private TextView tvgetonhandqty, tvgetallframe, tvpickingupqty;
	private RadioGroup rg_querytype;
	private RadioButton rb;
	private TakeStockAdapter mAdapter;

	protected void onCreate(Bundle savedInstanceState) {
		if (AppData.orgid.equals("")) {
			CommonUtil.WMSToast(takeStock.this, "請先選擇ORG");
			finish();
		}
		super.onCreate(savedInstanceState);
		setContentView(R.layout.take_stock);

		ivGohome = (ImageView) this.findViewById(R.id.takestock_gohome);
		ivGohome.setOnClickListener(this);
		ivRefresh = (ImageView) this.findViewById(R.id.takestock_refresh);
		ivRefresh.setOnClickListener(this);
		btnInsert = (Button) this.findViewById(R.id.take_stock_btn);
		btnInsert.setOnClickListener(this);
		btnClear = (Button) this.findViewById(R.id.take_scandataclear_btn);
		btnClear.setOnClickListener(this);
		//
		lvDisplay = (ListView) findViewById(R.id.takestock_display);
		edtpn = (EditText) findViewById(R.id.takestock_pnsearch);
		edtsubname = (EditText) findViewById(R.id.takestock_subinvsearch);
		edtakestock_barcodeplus = (EditText) findViewById(R.id.takestock_barcodeplus);
		edscanedqty = (EditText) findViewById(R.id.takestock_edscanedqty);
		//
		tvgetallframe = (TextView) findViewById(R.id.takestock_frame);
		tvgetonhandqty = (TextView) findViewById(R.id.takestock_onhandqty);
		tvpickingupqty = (TextView) findViewById(R.id.tvpickingupqty);
		//
		rg_querytype = (RadioGroup) findViewById(R.id.takestock_querytype);
		//
		String ip = getIP();
		if (ip.equals(getString(R.string.myip)) == false) {// 如果不是自己測試則清除賬號和密碼
			edtpn.setText("");
			edtsubname.setText("");
		}
		// 这个可以被用来控制键盘，直到使用者确实碰了编辑框
		getWindow().setSoftInputMode(
				WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
		// 隱藏某一編輯框的軟鍵盤
		// InputMethodManager imm = (InputMethodManager)
		// takeStock.this.getSystemService(Context.INPUT_METHOD_SERVICE);
		// imm.hideSoftInputFromWindow(edtsubname.getWindowToken(), 0);
		//
		/*
		 * 一、布局中软键盘自动弹出 bargain_dialog_offer_edit.requestFocus();
		 * bargain_dialog_offer_edit.setFocusable(true); InputMethodManager imm
		 * = (InputMethodManager)
		 * context.getSystemService(Context.INPUT_METHOD_SERVICE);
		 * imm.showSoftInputFromInputMethod
		 * (bargain_dialog_offer_edit.getWindowToken(),0); 二、布局中软键盘自动关闭
		 * InputMethodManager imm = (InputMethodManager)
		 * context.getSystemService(Context.INPUT_METHOD_SERVICE);
		 * imm.hideSoftInputFromWindow(talking_edit.getWindowToken() , 0);
		 * 三、对话框中软键盘自动弹出和关闭
		 * getWindow().setSoftInputMode(WindowManager.LayoutParams
		 * .SOFT_INPUT_STATE_VISIBLE
		 * |WindowManager.LayoutParams.SOFT_INPUT_ADJUST_RESIZE);
		 */
		//
		mAdapter = new TakeStockAdapter(this);
		//
		edtpn.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
			@Override
			public void onFocusChange(View v, boolean hasFocus) {
				if (hasFocus) {
					BaseActivity.mCommReception = edtpn;
					btnOpenFlag = true;
					Open("/dev/ttyHS1", 9600);
				} else {
					btnOpenFlag = false;
					Close();
				}
			}

		});
		edtsubname
				.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
					@Override
					public void onFocusChange(View v, boolean hasFocus) {
						if (hasFocus) {
							BaseActivity.mCommReception = edtsubname;
							btnOpenFlag = true;
							Open("/dev/ttyHS1", 9600);
						} else {
							btnOpenFlag = false;
							Close();
						}
					}

				});
		//
		edtakestock_barcodeplus
				.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
					@Override
					public void onFocusChange(View v, boolean hasFocus) {
						if (hasFocus) {
							BaseActivity.mCommReception = edtakestock_barcodeplus;
							btnOpenFlag = true;
							Open("/dev/ttyHS1", 9600);
						} else {
							btnOpenFlag = false;
							Close();
						}
					}

				});
		//
		registerReadgoodReceiver();
	}

	public void trimandupperedt() {
		CommonUtil.trimandupper(edtsubname);
		CommonUtil.trimandupper(edtpn);
	}

	public boolean checkinput() {
		trimandupperedt();
		//
		if (edtpn.getText().toString().indexOf("#") <= 0) {
			return true;
		}
		//
		pn_5in1 = "";
		//
		String barcode = edtpn.getText().toString();
		if (!CommonUtil.Split5in1(takeStock.this, barcode)) {
			return false;
		}
		//
		pn_5in1 = AppData.pn_5in1;
		edtpn.setText(pn_5in1);
		//
		pn_5in1 = AppData.pn_5in1;
		//
		return true;
	}

	// 盤點計數,累加數量
	public boolean plusPnQty(String barcode) {
		int scanedqty = 0;
		if (!CommonUtil.Split5in1(takeStock.this, barcode)) {
			return false;
		}
		edtakestock_barcodeplus.setText(AppData.pn_5in1);
		if (pn_pandian.equals("")) {
			pn_pandian = AppData.pn_5in1;
		} else if (!AppData.pn_5in1.equals(pn_pandian)) {
			CommonUtil.WMSShowmsg(this, "料號不一致,請檢查");
			return false;
		}
		//
		if (!AppData.reel_5in1.equals("")) {
			if (AppData.mreelid.contains(AppData.reel_5in1)) {
				CommonUtil.WMSShowmsg(this, "reel id重複");
				return false;
			} else
				AppData.mreelid.add(AppData.reel_5in1);
		}
		//
		scanedqty = Integer.parseInt(edscanedqty.getText().toString())
				+ Integer.parseInt(AppData.qty_5in1);
		edscanedqty.setText(String.valueOf(scanedqty));
		//
		return true;
	}

	public void getquerytype() {
		for (int i = 0; i < rg_querytype.getChildCount(); i++) {
			rb = (RadioButton) rg_querytype.getChildAt(i);
			if (rb.isChecked()) {
				if (i == 0) {
					querytype = "OUT";
					break;
				} else if (i == 1) {
					querytype = "IN";
					break;
				} else {
					querytype = "ALL";
					break;
				}
			}
		}
	}

	@Override
	public boolean dispatchKeyEvent(KeyEvent event) {
		if (event.getAction() == KeyEvent.ACTION_DOWN) {
			Log.d("BarcodeActivity",
					"BarcodeActivity keycode = " + event.getKeyCode());
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
				if (edtsubname.hasFocus()) {
					if (edtsubname.getText().toString().equals("")) {
						break;
					}
					//
					edtpn.requestFocus();
					break;
				}
				//
				if (edtakestock_barcodeplus.hasFocus()) {
					if (edtakestock_barcodeplus.getText().toString().equals("")) {
						break;
					}
					plusPnQty(edtakestock_barcodeplus.getText().toString());
					//
					break;
				}
				//
				if (edtpn.hasFocus()) {
					if (edtpn.getText().toString().equals("")) {
						break;
					}
					//
					if (!checkinput()) {
						CommonUtil.errorsound();
						break;
					}
					//
					getquerytype();
					TakeStockQuery();
				}
			}
			//
			return true;
		}
		//
		return super.dispatchKeyEvent(event);
	}

	public void cleardone() {
		edtpn.setText("");
		edtsubname.setText("");
		//
		edtpn.requestFocus();
	}

	public void onClick(View v) {
		switch (v.getId()) {
		case R.id.takestock_gohome: {
			finish();
			break;
		}
		case R.id.takestock_refresh: {
			trimandupperedt();
			getquerytype();
			TakeStockQuery();
			edtpn.requestFocus();
			break;
		}
		case R.id.take_scandataclear_btn: {
			ClearScanedQty();
			break;
		}
		case R.id.take_stock_btn: {
			trimandupperedt();
			if (!CommonUtil.isNumeric(edscanedqty.getText().toString())) {
				CommonUtil.WMSShowmsg(this, "料號數量必須爲數字型");
				break;
			}
			//
			// if(Integer.parseInt(edscanedqty.getText().toString())==0)
			// {
			// CommonUtil.WMSShowmsg(this, "刷入數量不能爲0");
			// break;
			// }
			//
			AlertDialog.Builder builder = new AlertDialog.Builder(
					takeStock.this);
			builder.setMessage("確定要生成盤點資料嗎?")
					.setCancelable(false)
					.setPositiveButton("Yes",
							new DialogInterface.OnClickListener() {
								public void onClick(DialogInterface dialog,
										int id) {
									//
									inserttakestockdata();
								}
							})
					.setNegativeButton("No",
							new DialogInterface.OnClickListener() {
								public void onClick(DialogInterface dialog,
										int id) {
									dialog.cancel();
								}
							});
			AlertDialog alert = builder.create();
			alert.show();
			//
			break;
		}
		}
	}

	private void ClearScanedQty() {
		pn_pandian = "";
		edtakestock_barcodeplus.setText("");
		edscanedqty.setText("000000");
		AppData.mreelid.clear();
	}

	// 用線程得到當前庫存&所有儲位&歷史記錄
	public void TakeStockQuery() {
		new TakeStockQueryAsyncTask().execute();
	}

	class TakeStockQueryAsyncTask extends AsyncTask<Void, Void, Boolean> {
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
		protected Boolean doInBackground(Void... params) {
			try {
				// get all frame
				String strResponse = TakeStockAdapter.Request_getallframe(
						takeStock.this, AppData.orgcode, edtsubname.getText()
								.toString(), edtpn.getText().toString());
				TakeStockAdapter.Resolve_getallframe(strResponse);
				// get onhand qty
				strResponse = TakeStockAdapter.Request_getonhandqty(
						takeStock.this, AppData.orgid, edtsubname.getText()
								.toString(), edtpn.getText().toString());
				TakeStockAdapter.Resolve_getonhandqty(strResponse);
				// get operation detail
				strResponse = TakeStockAdapter.Request_querytakestock(
						takeStock.this, AppData.orgid, edtsubname.getText()
								.toString(), edtpn.getText().toString(),
						querytype, AppData.getonhandqty);
				TakeStockAdapter.Resolve_querytakestock(strResponse);
				//
				strResponse = TakeStockAdapter.Request_querypickingupqty(
						takeStock.this, AppData.orgid, edtpn.getText()
								.toString(), edtsubname.getText().toString());
				PickingupQty = CommonUtil.GetStringFromResult(strResponse);
				//
				return true;
			} catch (Exception e) {
				return false;
			}
		}

		@Override
		protected void onPostExecute(Boolean result) {
			super.onPostExecute(result);
			mAlertDialog.dismiss();
			//
			if(!result)
			{
				CommonUtil.WMSShowmsg(takeStock.this, "查詢出現異常");
				return;
			}
			//
			tvgetallframe.setText("儲位:" + AppData.getallframe);
			tvgetonhandqty.setText("當前庫存:" + AppData.getonhandqty);
			int qtyinframe;
			qtyinframe = Integer.parseInt(AppData.getonhandqty)
					- Integer.parseInt(PickingupQty);
			//(1) String.valueOf(i)(2) Integer.toString(i)(3) i+""
			tvpickingupqty.setText("已備料量:" + PickingupQty + "	在料架數量:"
					+ qtyinframe);
			//
			lvDisplay.setAdapter(mAdapter);// 顯示資料
			//
			CommonUtil.feedbackqueryres(takeStock.this, result);
		}
	}

	// 用線程執行insert盤點資料的動作
	public void inserttakestockdata() {
		new inserttakestockdataAsyncTask().execute();
	}

	class inserttakestockdataAsyncTask extends AsyncTask<Void, Void, Boolean> {
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
			CommonUtil.feedbackexcres(takeStock.this, K_table.excuteres);
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			String strResponse = "";
			try {
				K_table.excuteres = "";
				strResponse = TakeStockAdapter.Request_inserttakestockdata(
						takeStock.this, AppData.orgid, edtpn.getText()
								.toString(), edtsubname.getText().toString(),
						AppData.getonhandqty, edscanedqty.getText().toString(),
						AppData.user_id);
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
