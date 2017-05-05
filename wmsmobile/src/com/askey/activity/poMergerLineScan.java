package com.askey.activity;

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
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.TextView;

import com.askey.Adapter.poMergerLineScanAdapter;
import com.askey.model.AppData;
import com.askey.model.BaseActivity;
import com.askey.model.CommonUtil;
import com.askey.model.CustomHttpClient;
import com.askey.model.K_table;
import com.askey.model.T6in1;
import com.askey.wms.R;

public class poMergerLineScan extends BaseActivity implements OnClickListener {
	private ImageView ivCommit, ivdelete;
	private ListView lvDisplay;
	private ImageView ivGohome;
	private EditText etdeliverno_suffix, edtbarcode;
	// private EditText edtlog;
	private TextView tv_cnt, tv_itemqty;
	private RadioGroup rg_delivertype;
	private RadioButton rb;
	private String delivertype;

	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		if (AppData.orgid.equals("")) {
			CommonUtil.WMSToast(poMergerLineScan.this, "請先選擇ORG");
			finish();
		}
		setContentView(R.layout.pomergerlinescan);
		//
		ivGohome = (ImageView) this.findViewById(R.id.ivgohome);
		ivGohome.setOnClickListener(this);
		//
		ivCommit = (ImageView) this.findViewById(R.id.ivcommit);
		ivCommit.setOnClickListener(this);
		//
		ivdelete = (ImageView) this.findViewById(R.id.ivdelete);
		ivdelete.setOnClickListener(this);
		// lvDisplay = (ListView) findViewById(R.id.issue_display);
		//
		etdeliverno_suffix = (EditText) findViewById(R.id.etdeliverno_suffix);
		edtbarcode = (EditText) findViewById(R.id.etbarcode);
		tv_cnt = (TextView) findViewById(R.id.tv_cnt);
		tv_itemqty = (TextView) findViewById(R.id.tv_itemqty);
		// edtlog = (EditText) findViewById(R.id.issue_log);
		rg_delivertype = (RadioGroup) findViewById(R.id.rg_delivertype);
		//
		etdeliverno_suffix
				.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
					@Override
					public void onFocusChange(View v, boolean hasFocus) {
						if (hasFocus) {
							BaseActivity.mCommReception = etdeliverno_suffix;
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
							btnOpenFlag = true;
							Open("/dev/ttyHS1", 9600);
						} else {
							btnOpenFlag = false;
							Close();
						}
					}

				});
		registerReadgoodReceiver();
		//
		clearAll();
	}

	// 得到送貨單類型
	public boolean getdelivertype() {
		delivertype = "";
		for (int i = 0; i < rg_delivertype.getChildCount(); i++) {
			rb = (RadioButton) rg_delivertype.getChildAt(i);
			if (rb.isChecked()) {
				if (i == 0) {
					delivertype = "A";
					break;
				} else if (i == 1) {
					delivertype = "B";
					break;
				} else {
					delivertype = "C";
					break;
				}
			}
		}
		if (!delivertype.equals(""))
			return true;
		else {
			CommonUtil.WMSShowmsg(poMergerLineScan.this, "請先選擇送貨單類型");
			return false;
		}
	}

	// 檢查reel id是否重複
	public boolean checkReelidIsDup(String reelid) {
		if (reelid.equals(""))// 如果reelid爲空則檢查通過
			return false;
		//
		for (int i = 0; i <= AppData.m6in1.size() - 1; i++) {
			if (reelid.equals(AppData.m6in1.get(i).getBoxno())) {
				return true;
			}
		}
		//
		return false;
	}
	// 檢查vendor是否一致
	public boolean checkVendorcodeIsSame(String vc) {
		if (vc.equals(""))// 如果vc爲空則檢查不通過
			return false;
		//
		for (int i = 0; i <= AppData.m6in1.size() - 1; i++) {
			if (!vc.equals(AppData.m6in1.get(i).getVendorcode())) {
				return false;
			}
		}
		return true;
	}

	//
	public void trimandupperedt() {
		CommonUtil.trimandupper(etdeliverno_suffix);
		CommonUtil.trimandupper(edtbarcode);
	}

	public boolean checkinput() {
		trimandupperedt();
		//
		if (etdeliverno_suffix.getText().toString().length() != 5) {
			CommonUtil.WMSShowmsg(poMergerLineScan.this, "送貨單長度必須是5位");
			return false;
		}
		//
		if (!getdelivertype())
			return false;
		//
		String barcode = edtbarcode.getText().toString();
		//
		if (!CommonUtil.Split5in1(poMergerLineScan.this, barcode)) {
			return false;
		}
		//check reel id
		if (checkReelidIsDup(AppData.reel_5in1)) {
			CommonUtil.WMSShowmsg(poMergerLineScan.this,
					String.format("reel id[%s]重複", AppData.reel_5in1));
			return false;
		}
		//check vendor code
		if(!checkVendorcodeIsSame(AppData.vc_5in1))
		{
			CommonUtil.WMSShowmsg(poMergerLineScan.this,
					String.format("vendor code[%s]不一致", AppData.vc_5in1));
			return false;
		}
		//
		return true;
	}

	public void addqty() {
		int cnt;
		// 增加刷入筆數
		cnt = Integer.parseInt(tv_cnt.getText().toString());
		cnt++;
		tv_cnt.setText(String.valueOf(cnt).toString());
		// 增加輸入的pcs數
		int itemqty;
		itemqty = Integer.parseInt(tv_itemqty.getText().toString());
		itemqty += Integer.parseInt(AppData.qty_5in1);
		tv_itemqty.setText(String.valueOf(itemqty).toString());
		//
		edtbarcode.setText("");
		edtbarcode.requestFocus();
	}

	@Override
	public boolean dispatchKeyEvent(KeyEvent event) {
		if (event.getAction() == KeyEvent.ACTION_DOWN) {
			Log.d("BarcodeActivity",
					"BarcodeActivity keycode = " + event.getKeyCode());
			//
			// edtlog.setText("");
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
				if (etdeliverno_suffix.hasFocus()) {
					if (!etdeliverno_suffix.getText().toString().equals("")) {
						edtbarcode.requestFocus();
						break;
					}
				}
				//
				if (edtbarcode.hasFocus()) {
					if (edtbarcode.getText().toString().equals("")) {
						break;
					}
					if (!checkinput()) {
						CommonUtil.errorsound();
						break;
					}
					checkScaneddata();
				}
			}
			//
			return true;
		}
		//
		return super.dispatchKeyEvent(event);
	}

	public void clearAll() {
		etdeliverno_suffix.setText("");
		edtbarcode.setText("");
		etdeliverno_suffix.requestFocus();
		tv_cnt.setText("000");
		tv_itemqty.setText("00000");
		//
		AppData.m6in1.clear();
	}

	public void onClick(View v) {
		switch (v.getId()) {
		case R.id.ivgohome: {
			finish();
			break;
		}
		case R.id.ivcommit: {
			commitdata();
			break;
		}
		case R.id.ivdelete: {
			clearAll();
		}
		}
	}
	//檢查料號 & VC & Reel id
	public void checkScaneddata() {
		new checkScaneddata().execute();
	}
    //Params, Progress, Result
	class checkScaneddata extends AsyncTask<Void, Void, Boolean> {
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
			if (K_table.excuteres.equals("0")) {
				T6in1 A6in1 = new T6in1();
				A6in1.setBoxno(AppData.reel_5in1);
				A6in1.setDatecode(AppData.dc_5in1);
				A6in1.setLotno(AppData.lotno_5in1);
				A6in1.setPn(AppData.pn_5in1);
				A6in1.setQty(AppData.qty_5in1);
				A6in1.setVendorcode(AppData.vc_5in1);
				//
				AppData.m6in1.add(A6in1);
				//
				addqty();
			}else
			{
				CommonUtil.WMSShowmsg(poMergerLineScan.this, K_table.excuteres);
			}
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			try {
				String strURL = String.format(K_table.checkScanedData,
						delivertype, etdeliverno_suffix.getText().toString(),
						AppData.pn_5in1, AppData.vc_5in1, AppData.reel_5in1,
						AppData.orgcode).replaceAll(" ", "%20");
				String strData = CustomHttpClient.getFromWebByHttpClient(
						poMergerLineScan.this, strURL);
				K_table.excuteres = CommonUtil.GetStringFromResult(strData);
				if (!K_table.excuteres.equals("0")) {
					return false;
				}
				return true;
			} catch (Exception e) {
				return false;
			}
		}
	}

	// 根據單號查詢的線程
	public void commitdata() {
		new myAsyncTask().execute();
	}

	class myAsyncTask extends AsyncTask<Void, Void, Boolean> {
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
			clearAll();
			//
			if (!K_table.excuteres.equals("0")) {
				CommonUtil.WMSShowmsg(poMergerLineScan.this, K_table.excuteres);
			}
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			try {
				String pn = "", lotno = "", datecode = "", boxno = "", vendorcode = "", qty = "";
				for (int i = 0; i <= AppData.m6in1.size() - 1; i++) 
				{
					//
//					pn=AppData.pn_5in1;
//					lotno=AppData.lotno_5in1;
//					datecode=AppData.dc_5in1;
//					boxno=AppData.reel_5in1;
//					vendorcode=AppData.vc_5in1;
//					qty=AppData.qty_5in1;					
							
					pn = AppData.m6in1.get(i).getPn();
					lotno = AppData.m6in1.get(i).getLotno();
					datecode = AppData.m6in1.get(i).getDatecode();
					boxno = AppData.m6in1.get(i).getBoxno();
					vendorcode = AppData.m6in1.get(i).getVendorcode();
					qty = AppData.m6in1.get(i).getQty();
					//
					String strResponse = poMergerLineScanAdapter.commitdata(
							poMergerLineScan.this, delivertype,
							etdeliverno_suffix.getText().toString(), pn, lotno,
							datecode, boxno, vendorcode, qty, AppData.user_id,
							AppData.orgcode);
					K_table.excuteres = CommonUtil.GetStringFromResult(strResponse);
					if (!K_table.excuteres.equals("0")) {
						return false;
					}
				}
				return true;
			} catch (Exception e) {
				return false;
			}
		}
	}
}
