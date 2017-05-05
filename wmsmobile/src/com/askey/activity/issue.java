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

import com.askey.Adapter.GetInvoiceHeadHelper;
import com.askey.Adapter.IssueAdapter;
import com.askey.model.AppData;
import com.askey.model.BaseActivity;
import com.askey.model.CommonUtil;
import com.askey.model.K_table;
import com.askey.model.T6in1;
import com.askey.model.Tissuequery;
import com.askey.wms.R;

public class issue extends BaseActivity implements OnClickListener {
	Tissuequery founditem = new Tissuequery();
	//
	private String pn_5in1, dc_5in1, qty_5in1,boxno_5in1;
	private ImageView ivRefresh;
	private ListView lvDisplay;
	private ImageView ivGohome;
	private EditText edtissue_invoicenosearch, edtframe, edtbarcode, edtlog;
	private IssueAdapter mAdapter;

	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		if (AppData.orgid.equals("")) {
			CommonUtil.WMSToast(issue.this, "請先選擇ORG");
			finish();
		}
		setContentView(R.layout.issue);
		//
		ivGohome = (ImageView) this.findViewById(R.id.issue_gohome);
		ivGohome.setOnClickListener(this);
		ivRefresh = (ImageView) this.findViewById(R.id.issue_refresh);
		ivRefresh.setOnClickListener(this);
		lvDisplay = (ListView) findViewById(R.id.issue_display);
		//
		edtissue_invoicenosearch = (EditText) findViewById(R.id.issue_invoicenosearch);
		edtframe = (EditText) findViewById(R.id.issue_frame);
		edtbarcode = (EditText) findViewById(R.id.issue_barcode);
		edtlog = (EditText) findViewById(R.id.issue_log);
		//
		mAdapter = new IssueAdapter(this,edtbarcode);
		//
		AppData.m6in1.clear();
		//
//		if (!AppData.orgid.equals("")) {
//			GetInvoicHead();
//			// edtissue_invoicenosearch.setText("5BQ140510001");
//		}
		//
		edtissue_invoicenosearch.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
			@Override
			public void onFocusChange(View v, boolean hasFocus) {
				if (hasFocus) {
					BaseActivity.mCommReception = edtissue_invoicenosearch;
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
		CommonUtil.trimandupper(edtissue_invoicenosearch);
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
		if (!CommonUtil.Split5in1(issue.this, barcode)) {
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
				//按掃描鍵做查詢
				if(edtissue_invoicenosearch.hasFocus()){
					if (!edtissue_invoicenosearch.getText().toString().equals("")) {
						issue_query();
						edtbarcode.requestFocus();
						break;
					}
				}
				//
				if (edtframe.hasFocus()) {
					if (!edtframe.getText().toString().equals("")) {
						//edtbarcode.requestFocus();						
						issue_trans();//執行交易
						break;
					}
				}
				//
				if (edtbarcode.hasFocus()) {
					// issue_query();//查詢單據號對應的數據
					if (edtbarcode.getText().toString().equals("")) {
						break;
					}
					if (!checkinput()) {
						CommonUtil.errorsound();
						break;
					}
					//
					//issue_trans();
					boolean isfound = false;
					for (int i = 1; i <= AppData.missuequery.size() - 1; i++) {
						Tissuequery item = new Tissuequery();
						//
						item = ((Tissuequery) AppData.missuequery.get(i));
						if (item.Getitem_name().equals(pn_5in1)) {
							isfound = true;
							founditem = item;
							//
							break;
						}
					}// for end
					if (!isfound) {
						K_table.excuteres="未找到料號[" + pn_5in1+ "]";
						CommonUtil.WMSShowmsg(this,K_table.excuteres);
						CommonUtil.errorsound();
						return false;
					}
					//檢查reel id不能重複
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
							CommonUtil.WMSShowmsg(this,K_table.excuteres);
							CommonUtil.errorsound();
							return false;
						}
					}
					//
					edtframe.setText(founditem.getAframe());
					//
					edtframe.requestFocus();					
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
		edtbarcode.requestFocus();
	}

	public void onClick(View v) {
		CommonUtil.trimandupper(edtissue_invoicenosearch);
		//
		switch (v.getId()) {
		case R.id.issue_gohome: {
			finish();
			break;
		}
		case R.id.issue_refresh: {
			AppData.m6in1.clear();
			//
			issue_query();
			edtbarcode.requestFocus();
			break;
		}
		}
	}

	// 根據單號查詢的線程
	public void issue_query() {
		new IssueQueryAsyncTask().execute();
	}

	class IssueQueryAsyncTask extends AsyncTask<Void, Void, Boolean> {
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
			CommonUtil.feedbackqueryres(issue.this, result);
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			try {
				String strResponse = IssueAdapter.Request(issue.this,
						edtissue_invoicenosearch.getText().toString());
				IssueAdapter.Resolve(strResponse);
				return true;
			} catch (Exception e) {
				return false;
			}
		}
	}

	// get invoice head取單號前置碼的線程
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
				String strResponse = GetInvoiceHeadHelper.Request(issue.this,
						"ISSUE_INVOICE", AppData.orgcode);
				GetInvoiceHeadHelper.Resolve(strResponse);
				edtissue_invoicenosearch.setText(AppData.ginvoice);
				// issue_invoicenosearch.setText("3BQ110410021");
				return true;
			} catch (Exception e) {
				return false;
			}
		}
	}

	// 領料交易的線程
	public void issue_trans() {
		new issue_transAsyncTask().execute();
	}

	class issue_transAsyncTask extends AsyncTask<Void, Void, Boolean> {
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
				issue_query();
				//
				cleardone();
			} else {
				edtlog.append("數據處理失敗");
				edtbarcode.setSelection(edtbarcode.getText().toString().length());
			}
			//
			CommonUtil.feedbackexcres(issue.this, K_table.excuteres);
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			try {
				String strResponse = "";
				strResponse = IssueAdapter.Request(issue.this,
						edtissue_invoicenosearch.getText().toString());
				IssueAdapter.Resolve(strResponse);
				//
				boolean isfound = false;
				for (int i = 1; i <= AppData.missuequery.size() - 1; i++) {
					Tissuequery item = new Tissuequery();
					//
					item = ((Tissuequery) AppData.missuequery.get(i));
					if (item.Getitem_name().equals(pn_5in1)) {
						isfound = true;
						founditem = item;
						//
						break;
					}
				}// for end
				if (!isfound) {
					K_table.excuteres="未找到料號[" + pn_5in1+ "]";
					CommonUtil.errorsound();
					return false;
				}
				//檢查reel id不能重複
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
				strResponse = IssueAdapter.CommitIssueTrans(issue.this,
						founditem.Getorg_id(), edtissue_invoicenosearch
								.getText().toString(), founditem.Getwo_no(),
						founditem.Getwo_key(),
						founditem.Getoperation_seq_num(), founditem
								.Getreason_name(), pn_5in1, founditem
								.Getitem_id(), founditem.Getrequired_qty(),
						founditem.Getbalance_qty(), founditem.Getsub_name(),
						founditem.Getlocator(), edtframe.getText().toString(),
						dc_5in1, qty_5in1, founditem.Getpu(), founditem
								.Getreason_id(), founditem.Getdept_code(),
						founditem.Gettransaction_type_id(), founditem
								.Getsuffocate_code(), founditem.Getremark(),
						founditem.Getmodel_name(),
						founditem.Getcustomer_code(), founditem.Getdemand_id(),
						founditem.Getbatch_line_id(), founditem
								.Getissue_line_id(), AppData.user_id);
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
				K_table.excuteres = e.getMessage().toString();
				CommonUtil.errorsound();
				return false;
			}
		}
	}

}
