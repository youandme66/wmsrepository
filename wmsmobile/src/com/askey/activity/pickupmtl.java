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
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.TextView;

import com.askey.Adapter.DoPickupAdapter;
import com.askey.Adapter.PickupedDetailAdapter;
import com.askey.Adapter.SimbasedataAdapter;
import com.askey.Adapter.SimdataAdapter;
import com.askey.model.AppData;
import com.askey.model.BaseActivity;
import com.askey.model.CommonUtil;
import com.askey.model.K_table;
import com.askey.model.Tsimlutebasedata;
import com.askey.wms.R;

public class pickupmtl extends BaseActivity implements OnClickListener {
	Tsimlutebasedata founditem = new Tsimlutebasedata();
	//
	private String pn_5in1, dc_5in1, qty_5in1,reelid_5in1,vendorcode_5in1, querytype;
	private ImageView ivRefresh;
	private ListView lvsimulatebasedata, lvsimulatedata, lvpickedupdetail;
	private ImageView ivGohome;
	private EditText edtsimid, edtbarcode;
	private TextView tvwo, tvopseq, tvsubname;
	private RadioGroup rg_querytype;
	private RadioButton rb;
	//
	private SimbasedataAdapter msimbasedataAdapter;
	private SimdataAdapter msimdataAdapter;
	private PickupedDetailAdapter mPickupDetailAdapter;

	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		if (AppData.orgid.equals("")) {
			CommonUtil.WMSToast(pickupmtl.this, "請先選擇ORG");
			finish();
		}
		setContentView(R.layout.pickupmtl);
		//
		ivGohome = (ImageView) this.findViewById(R.id.pickup_gohome);
		ivGohome.setOnClickListener(this);
		ivRefresh = (ImageView) this.findViewById(R.id.pickup_refresh);
		ivRefresh.setOnClickListener(this);
		lvsimulatebasedata = (ListView) findViewById(R.id.pickup_simulatebasedata);
		lvsimulatedata = (ListView) findViewById(R.id.pickup_simulatedetail);
		lvpickedupdetail = (ListView) findViewById(R.id.pickup_pickedupdetail);
		//
		edtsimid = (EditText) findViewById(R.id.pickup_simid);
		edtbarcode = (EditText) findViewById(R.id.pickup_5in1);
		tvwo = (TextView) findViewById(R.id.pickup_tvwo);
		tvopseq = (TextView) findViewById(R.id.pickup_tvopseq);
		tvsubname = (TextView) findViewById(R.id.pickup_tvsubname);
		rg_querytype = (RadioGroup) findViewById(R.id.pickup_querytype);
		//
		msimbasedataAdapter = new SimbasedataAdapter(pickupmtl.this, tvwo,
				tvopseq, tvsubname,edtbarcode);
		msimdataAdapter = new SimdataAdapter(pickupmtl.this);
		mPickupDetailAdapter = new PickupedDetailAdapter(pickupmtl.this);
		//
		edtsimid.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
			@Override
			public void onFocusChange(View v, boolean hasFocus) {
				if (hasFocus) {
					BaseActivity.mCommReception = edtsimid;
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
		//
		registerReadgoodReceiver();
	}

	public void trimandupperedt() {
		CommonUtil.trimandupper(edtsimid);
		CommonUtil.trimandupper(edtbarcode);
	}

	public boolean checkinput() {
		trimandupperedt();
		//
		pn_5in1 = "";
		dc_5in1 = "";
		qty_5in1 = "";
		reelid_5in1="";
		vendorcode_5in1="";
		//
		String barcode = edtbarcode.getText().toString();
		if (!CommonUtil.Split5in1(pickupmtl.this, barcode)) {
			return false;
		}
		pn_5in1 = AppData.pn_5in1;
		dc_5in1 = AppData.dc_5in1;
		qty_5in1 = AppData.qty_5in1;
		reelid_5in1=AppData.reel_5in1;
		vendorcode_5in1=AppData.vc_5in1;
		//
//		if (tvwo.getText().toString().equals("")) {
//			CommonUtil.WMSToast(this, "工單不能為空");
//			return false;
//		}
		if (tvopseq.getText().toString().equals("")) {
			CommonUtil.WMSToast(this, "製程不能為空");
			return false;
		}
		if (tvsubname.getText().toString().equals("")) {
			CommonUtil.WMSToast(this, "庫別不能為空");
			return false;
		}
		//
		return true;
	}

	public void getquerytype() {
		for (int i = 0; i < rg_querytype.getChildCount(); i++) {
			rb = (RadioButton) rg_querytype.getChildAt(i);
			if (rb.isChecked()) {
				if (i == 0) {
					querytype = "PICKED";
					break;
				} else if (i == 1) {
					querytype = "UNPICKED";
					break;
				} else {
					querytype = "ALL";
					break;
				}
			}
		}
	}

	public void ClearSimBaseData() {
		tvwo.setText("");
		tvopseq.setText("");
		tvsubname.setText("");
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
				if (edtsimid.hasFocus()) {
					if (!edtsimid.getText().toString().equals("")) {
						getquerytype();
						simbasedata_query();
						ClearSimBaseData();
						break;
					}
				}
				if (edtbarcode.hasFocus()) {
					// issue_query();//查詢單據號對應的數據
					if (edtbarcode.getText().toString().equals("")) {
						break;
					}
					if (!checkinput()) {
						CommonUtil.errorsound();
						break;
					}
					DoPickup();
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
	}

	public void onClick(View v) {
		CommonUtil.trimandupper(edtsimid);
		//
		switch (v.getId()) {
		case R.id.pickup_gohome: {
			finish();
			break;
		}
		case R.id.pickup_refresh: {
			if (!edtsimid.getText().toString().equals("")) {
				getquerytype();
				simbasedata_query();
				ClearSimBaseData();
				break;
			}
		}
		}
	}

	// 根據單號查詢的線程
	public void simbasedata_query() {
		new simbasedataAsyncTask().execute();
	}

	class simbasedataAsyncTask extends AsyncTask<Void, Void, Boolean> {
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
			lvsimulatebasedata.setAdapter(msimbasedataAdapter);// 顯示資料
			lvsimulatedata.setAdapter(msimdataAdapter);// 顯示資料
			//
			CommonUtil.feedbackqueryres(pickupmtl.this, result);
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			try {
				String strResponse = SimbasedataAdapter.Request(pickupmtl.this,
						edtsimid.getText().toString());
				SimbasedataAdapter.Resolve(strResponse);
				//
				String strResp = SimdataAdapter.Request(pickupmtl.this,
						edtsimid.getText().toString(), querytype, "", "", "");// itemname=""表示查所有料的模擬資料
				SimdataAdapter.Resolve(strResp);
				//
				return true;
			} catch (Exception e) {
				return false;
			}
		}
	}

	// DoPickup線程
	public void DoPickup() {
		new DoPickupAsyncTask().execute();
	}

	class DoPickupAsyncTask extends AsyncTask<Void, Void, Boolean> {
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
			CommonUtil.feedbackexcres(pickupmtl.this, K_table.excuteres);
			if (K_table.excuteres.equals("0")) {
				lvsimulatedata.setAdapter(msimdataAdapter);// 顯示資料
				lvpickedupdetail.setAdapter(mPickupDetailAdapter);// 顯示資料
				cleardone();
			}			
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			try {
				String Response = "";
				Response = DoPickupAdapter.Request(pickupmtl.this, edtsimid
						.getText().toString(), tvopseq.getText().toString(),
						tvsubname.getText().toString(), pn_5in1, qty_5in1,
						dc_5in1,reelid_5in1,vendorcode_5in1);
				JSONObject jsobj = new JSONObject(Response);
				String res = jsobj.getString("result");
				K_table.excuteres = res;
				if (res.equals("0")) {
					// 查詢該料號的模擬和備料資料
					Response = SimdataAdapter.Request(pickupmtl.this, edtsimid
							.getText().toString(), "", tvopseq.getText()
							.toString(), tvsubname.getText().toString(),
							pn_5in1);
					SimdataAdapter.Resolve(Response);
					// 查詢已刷入的資料
					Response = PickupedDetailAdapter.Request(pickupmtl.this,
							edtsimid.getText().toString(), pn_5in1);
					PickupedDetailAdapter.Resolve(Response);
					return true;
				} else {
					CommonUtil.errorsound();
					return false;
				}
			} catch (Exception e) {
				K_table.excuteres = e.getMessage().toString();
				CommonUtil.errorsound();
				return false;
			}
		}
	}

}
