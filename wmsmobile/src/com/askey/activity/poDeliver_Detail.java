package com.askey.activity;

import org.json.JSONObject;

import android.content.DialogInterface;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;

import com.askey.Adapter.PoDeliverAdapter;
import com.askey.model.AppData;
import com.askey.model.BaseActivity;
import com.askey.model.CommonUtil;
import com.askey.model.K_table;
import com.askey.wms.R;

public class poDeliver_Detail extends BaseActivity implements OnClickListener {
	//
	private PoDeliverAdapter mAdapter;
	//
	private ImageView ivgohome, ivcommit;
	private TextView tvorgcode, tvpo, tvpoline, tvpn, tviqcflag, tvdc,
			tvsubinv, tvrcvqty;
	private EditText edtframe, edtdeliverqty;
	private String rcvno, orgid, locator, temp_region, delivered_qty,
			po_header_id, po_line_id, line_location_id, distribution_id,
			invoice, special_no, special_wo_no, suffocate_code,lastframe;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.podeliverdetail);// 设置布局文件
		//
		mAdapter = new PoDeliverAdapter(this);
		//
		ivgohome = (ImageView) findViewById(R.id.podeliverdetail_gohome);
		ivgohome.setOnClickListener(this);
		//
		ivcommit = (ImageView) findViewById(R.id.podeliverdetail_commit);
		ivcommit.setOnClickListener(this);
		//
		tvorgcode = (TextView) findViewById(R.id.podeliverdetail_edtorgcode);
		tvpo = (TextView) findViewById(R.id.podeliverdetail_edtpono);
		tvpoline = (TextView) findViewById(R.id.podeliverdetail_edtpoline);
		tvpn = (TextView) findViewById(R.id.podeliverdetail_edtpn);
		tviqcflag = (TextView) findViewById(R.id.podeliverdetail_edtiqcflag);
		tvdc = (TextView) findViewById(R.id.podeliverdetail_edtdc);
		tvsubinv = (TextView) findViewById(R.id.podeliverdetail_edtsubinv);
		edtframe = (EditText) findViewById(R.id.podeliverdetail_edtframe);
		tvrcvqty = (TextView) findViewById(R.id.podeliverdetail_edtrcvqty);
		edtdeliverqty = (EditText) findViewById(R.id.podeliverdetail_edtdeliverqty);
		//
		Intent intent = this.getIntent();
		//
		tvorgcode.setText(intent.getExtras().getString("orgcode"));
		tvpo.setText(intent.getExtras().getString("pono"));
		tvpoline.setText(intent.getExtras().getString("poline"));
		tvpn.setText(intent.getExtras().getString("pn"));
		tviqcflag.setText(intent.getExtras().getString("iqcflag"));
		tvdc.setText(intent.getExtras().getString("dc"));
		tvsubinv.setText(intent.getExtras().getString("subinv"));
		edtframe.setText(intent.getExtras().getString("frame"));
		tvrcvqty.setText(intent.getExtras().getString("rcvqty"));
		edtdeliverqty.setText(intent.getExtras().getString("deliverqty"));
		//
		rcvno = intent.getExtras().getString("rcvno");
		orgid = intent.getExtras().getString("orgid");
		locator = intent.getExtras().getString("locator");
		temp_region = intent.getExtras().getString("temp_region");
		delivered_qty = intent.getExtras().getString("delivered_qty");
		po_header_id = intent.getExtras().getString("po_header_id");
		po_line_id = intent.getExtras().getString("po_line_id");
		line_location_id = intent.getExtras().getString("line_location_id");
		distribution_id = intent.getExtras().getString("distribution_id");
		invoice = intent.getExtras().getString("invoice");
		special_no = intent.getExtras().getString("special_no");
		special_wo_no = intent.getExtras().getString("special_wo_no");
		suffocate_code = intent.getExtras().getString("suffocate_code");
		lastframe = intent.getExtras().getString("lastframe");
		//
		if(!lastframe.equals("")){
			edtframe.setText(lastframe);
		}
		//
		tvorgcode.setKeyListener(null); // 設置為只讀
		tvpo.setKeyListener(null);
		tvpoline.setKeyListener(null);
		tvpn.setKeyListener(null);
		tviqcflag.setKeyListener(null);
		tvdc.setKeyListener(null);
		tvsubinv.setKeyListener(null);
		tvrcvqty.setKeyListener(null);
		//
		edtframe.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
			@Override
			public void onFocusChange(View v, boolean hasFocus) {
				if (hasFocus) {
					BaseActivity.mCommReception = edtframe;
					// Log.d("BarcodeActivity_2", "3");
					// Log.d("BarcodeActivity_2", "BarcodeActivity OPEN");
					btnOpenFlag = true;
					Open("/dev/ttyHS1", 9600);
				} else {
					btnOpenFlag = false;
					Close();
				}
			}
		});
		//
		edtdeliverqty.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
			@Override
			public void onFocusChange(View v, boolean hasFocus) {
				if (hasFocus) {
					BaseActivity.mCommReception = edtdeliverqty;
					// Log.d("BarcodeActivity_2", "3");
					// Log.d("BarcodeActivity_2", "BarcodeActivity OPEN");
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

	@Override
	public boolean dispatchKeyEvent(KeyEvent event) {
		if (event.getAction() == KeyEvent.ACTION_DOWN) {
			Log.d("BarcodeActivity",
					"BarcodeActivity keycode = " + event.getKeyCode());
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
				if (edtframe.hasFocus()) {
					if (!edtframe.getText().toString().trim().equals("")) {
						edtdeliverqty.requestFocus();
					}
				}
				return true;
			}
		}
		//
		return super.dispatchKeyEvent(event);
	}

	public Boolean checkinput() {
		// if (tvonhand_qty.getText().toString().equals("")) {
		// CommonUtil.WMSToast(changeDCFrame_Detail.this, "在手量不能為空");
		// return false;
		// }
		// ;
		// if (tvsim_qty.getText().toString().equals("")) {
		// CommonUtil.WMSToast(changeDCFrame_Detail.this, "模擬量不能為空");
		// return false;
		// }
		// if (tvframe.getText().toString().equals("")) {
		// CommonUtil.WMSToast(changeDCFrame_Detail.this, "料架不能為空");
		// return false;
		// }
		//
		return true;
	}

	public void cleardone() {
		// edtframe.setText("");
		edtdeliverqty.setText("");
		//
		// edtframe.requestFocus();
	}

	public void trimandupperedt() {
		CommonUtil.trimandupper(edtdeliverqty);
		CommonUtil.trimandupper(edtframe);
	}

	public void onClick(View v) {
		trimandupperedt();
		//
		switch (v.getId()) {
		case R.id.podeliverdetail_gohome: {
			finish();
			break;
		}
		case R.id.podeliverdetail_commit: {
			if (checkinput() == false) {
				CommonUtil.errorsound();
				break;
			}
			PODeliver();
			//
			break;
		}
		}
	}

	public void PODeliver() {
		new PODeliverAsyncTask().execute();
	}

	// 参数1：传入doInBackground的参数类型
	// 参数2：传入onProgressUpdate的参数类型
	// 参数3：传入onPostExecute的参数类型，也是doInBackground的返回类型
	class PODeliverAsyncTask extends AsyncTask<Void, Void, Boolean> {
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
			CommonUtil.feedbackexcres(poDeliver_Detail.this, K_table.excuteres);
			if (K_table.excuteres.equals("0")) {
				cleardone();
			}
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			String strResponse = "";
			try {
				strResponse = PoDeliverAdapter.CommitPODeliver(
						poDeliver_Detail.this, rcvno,
						tvpo.getText().toString(), orgid, tvorgcode.getText()
								.toString(), tvpn.getText().toString(), tvdc
								.getText().toString(), locator, temp_region,
						edtframe.getText().toString(), tvrcvqty.getText()
								.toString(),
						edtdeliverqty.getText().toString(), delivered_qty,
						tviqcflag.getText().toString(), po_header_id,
						po_line_id, line_location_id, distribution_id, invoice,
						special_no, special_wo_no, AppData.user_id,
						suffocate_code);
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
				K_table.excuteres = strResponse;
				return false;
			}
		}
	}
}
